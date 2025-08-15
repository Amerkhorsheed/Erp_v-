using System;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

// Alias to disambiguate IsolationLevel.
using TransIsolationLevel = System.Transactions.IsolationLevel;

namespace Erp_V1.Benchmarks
{
    /// <summary>
    /// Comprehensive benchmarks for Category BLL operations.
    /// </summary>
    [MemoryDiagnoser]
    [RankColumn]
    [Config(typeof(BenchmarkConfig))]
    [BenchmarkCategory("Data Layer", "CRUD Operations")]
    public class CategoryBLLBenchmarks
    {
        #region Configuration

        private class BenchmarkConfig : ManualConfig
        {
            public BenchmarkConfig()
            {
                // Target .NET Framework 4.8 (EventPipeProfiler removed since it supports only .NET Core 3.0+)
                AddJob(Job.Default
                    .WithRuntime(ClrRuntime.Net48)
                    .WithPlatform(Platform.X64)
                    .WithJit(Jit.RyuJit)
                    .WithGcServer(true)
                    .WithGcForce(true)
                    .WithIterationCount(15)
                    .WithWarmupCount(5));

                // Only add MemoryDiagnoser since EventPipeProfiler is not applicable
                AddDiagnoser(MemoryDiagnoser.Default);
            }
        }

        #endregion

        #region Constants & Fields

        private const string TestCategoryPrefix = "BM_";
        private const int OperationsPerInvoke = 100;
        private const int MaxRetryAttempts = 3;
        private static readonly TimeSpan TransactionTimeout = TimeSpan.FromSeconds(15);

        private CategoryBLL _categoryBLL;
        private static readonly Random _random = new Random();

        // A volatile field to "consume" benchmark results (preventing dead-code elimination)
        private volatile int dummyResult;

        #endregion

        #region Lifecycle Management

        [GlobalSetup]
        public void GlobalInitialize()
        {
            _categoryBLL = new CategoryBLL();
            PurgeTestData(); // Clean state before benchmarks
        }

        [GlobalCleanup]
        public void GlobalTeardown()
        {
            PurgeTestData();
        }

        [IterationCleanup]
        public void IterationTeardown()
        {
            GC.Collect(2, GCCollectionMode.Forced, true, true);
            GC.WaitForPendingFinalizers();
        }

        #endregion

        #region Benchmark Operations

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "Atomic Insert Operations")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void InsertBenchmark()
        {
            bool[] results = new bool[OperationsPerInvoke];

            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                using (var scope = CreateTransactionScope())
                {
                    var category = GenerateUniqueCategory("INS");
                    results[i] = ExecuteWithRetry(() => _categoryBLL.Insert(category));
                    scope.Complete();
                }
            }

            // Consume results to avoid dead-code elimination.
            dummyResult = results.Count(r => r);
        }

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "High-Performance Selects")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public CategoryDTO SelectBenchmark()
        {
            CategoryDTO result = null;
            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                result = ExecuteWithRetry(() => _categoryBLL.Select());
            }
            return result;
        }

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "Transactional Update Workflow")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void UpdateBenchmark()
        {
            // Pre-generate and insert test data for update benchmark.
            var testData = GenerateTestCategories("UPD", OperationsPerInvoke);
            bool[] results = new bool[OperationsPerInvoke];

            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                using (var scope = CreateTransactionScope())
                {
                    // Change the category name before updating.
                    testData[i].CategoryName = GenerateUniqueName("UPD_NEW");
                    results[i] = ExecuteWithRetry(() => _categoryBLL.Update(testData[i]));
                    scope.Complete();
                }
            }
            dummyResult = results.Count(r => r);
        }

        // --- Revised Bulk Delete Benchmark ---
        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "Bulk Delete Operations")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void DeleteBenchmark()
        {
            bool[] results = new bool[OperationsPerInvoke];

            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                // Create unique category data.
                var category = GenerateUniqueCategory("DEL");

                // Insert within a transaction to ensure commit.
                using (var insertScope = CreateTransactionScope())
                {
                    if (!ExecuteWithRetry(() => _categoryBLL.Insert(category)))
                        throw new BenchmarkException("Insert failed in delete benchmark")
                            .WithContext("CategoryName", category.CategoryName);
                    insertScope.Complete();
                }

                // Optionally verify that the category exists in the database.
                var insertedCategory = ExecuteWithRetry(() =>
                    _categoryBLL.Select().Categories.FirstOrDefault(c => c.CategoryName == category.CategoryName));
                if (insertedCategory == null)
                    throw new BenchmarkException("Category not found after insertion")
                        .WithContext("CategoryName", category.CategoryName);

                // Delete within its own transaction.
                using (var deleteScope = CreateTransactionScope())
                {
                    results[i] = ExecuteWithRetry(() => _categoryBLL.Delete(insertedCategory));
                    deleteScope.Complete();
                }
            }

            // Consume results to prevent dead-code elimination.
            dummyResult = results.Sum(r => r ? 1 : 0);
        }

        // --- Revised Restore Benchmark ---
        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "Restore Operation Throughput")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void RestoreBenchmark()
        {
            bool[] results = new bool[OperationsPerInvoke];

            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                var category = GenerateUniqueCategory("RST");

                // Insert and commit.
                using (var insertScope = CreateTransactionScope())
                {
                    if (!ExecuteWithRetry(() => _categoryBLL.Insert(category)))
                        throw new BenchmarkException("Insert failed in restore benchmark")
                            .WithContext("CategoryName", category.CategoryName);
                    insertScope.Complete();
                }

                // Delete and commit.
                using (var deleteScope = CreateTransactionScope())
                {
                    ExecuteWithRetry(() => _categoryBLL.Delete(category));
                    deleteScope.Complete();
                }

                // Restore within its own transaction.
                using (var restoreScope = CreateTransactionScope())
                {
                    results[i] = ExecuteWithRetry(() => _categoryBLL.GetBack(category));
                    restoreScope.Complete();
                }
            }

            // Consume results to avoid dead-code elimination.
            dummyResult = results.Sum(r => r ? 1 : 0);
        }

        #endregion

        #region Core Utilities

        private CategoryDetailDTO GenerateUniqueCategory(string operation)
        {
            return new CategoryDetailDTO
            {
                CategoryName = GenerateUniqueName(operation)
            };
        }

        /// <summary>
        /// Generates and inserts a batch of test categories.
        /// </summary>
        private CategoryDetailDTO[] GenerateTestCategories(string operation, int count)
        {
            var categories = new CategoryDetailDTO[count];
            for (int i = 0; i < count; i++)
            {
                categories[i] = GenerateUniqueCategory(operation);
                ExecuteWithRetry(() => _categoryBLL.Insert(categories[i]));
            }
            return categories;
        }

        private static string GenerateUniqueName(string prefix)
        {
            return $"{TestCategoryPrefix}{prefix}_{Guid.NewGuid():N}";
        }

        #endregion

        #region Transaction & Retry Management

        private TransactionScope CreateTransactionScope()
        {
            return new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions
                {
                    IsolationLevel = TransIsolationLevel.ReadCommitted,
                    Timeout = TransactionTimeout
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }

        private T ExecuteWithRetry<T>(Func<T> operation)
        {
            for (int attempt = 1; attempt <= MaxRetryAttempts; attempt++)
            {
                try
                {
                    var result = operation();
                    // Optionally log successful attempts here
                    return result;
                }
                catch (Exception ex) when (attempt < MaxRetryAttempts)
                {
                    DiagnosticMonitor.LogRetryAttempt(ex, attempt);
                    HandleRetryException(ex, attempt);
                }
            }
            throw new OperationCanceledException($"Operation failed after {MaxRetryAttempts} attempts");
        }

        private void HandleRetryException(Exception ex, int attempt)
        {
            // Exponential backoff: (2^attempt * 50ms) plus a random 0-50ms.
            int backoffMs = (int)(Math.Pow(2, attempt) * 50 + _random.Next(0, 50));
            Thread.Sleep(backoffMs);
            Console.Error.WriteLine($"Retry {attempt}: {ex.Message}");
        }

        #endregion

        #region Data Sanitization

        private void PurgeTestData()
        {
            using (var purgeScope = CreateTransactionScope())
            {
                var sw = Stopwatch.StartNew();
                int retries = 0;

                while (true)
                {
                    try
                    {
                        var testCategories = _categoryBLL.Select().Categories
                            .Where(c => c.CategoryName.StartsWith(TestCategoryPrefix))
                            .ToList();

                        foreach (var category in testCategories)
                        {
                            ExecuteWithRetry(() => _categoryBLL.Delete(category));
                        }
                        purgeScope.Complete();
                        Console.WriteLine($"Purged {testCategories.Count} items in {sw.Elapsed}");
                        return;
                    }
                    catch (Exception ex) when (retries++ < 3)
                    {
                        Console.WriteLine($"Purge failed (attempt {retries}): {ex.Message}");
                        Thread.Sleep(100 * retries);
                    }
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Custom exception to provide contextual details during benchmarks.
    /// </summary>
    public class BenchmarkException : Exception
    {
        public BenchmarkException(string message) : base(message) { }
        public System.Collections.Generic.Dictionary<string, object> Context { get; } = new System.Collections.Generic.Dictionary<string, object>();

        public BenchmarkException WithContext(string key, object value)
        {
            Context[key] = value;
            return this;
        }
    }



    /// <summary>
    /// Simple diagnostic monitor for logging retry attempts.
    /// </summary>
    public static class DiagnosticMonitor
    {
        public static void LogRetryAttempt(Exception ex, int attempt)
        {
            // Log or report the exception details and attempt number.
            Console.Error.WriteLine($"Diagnostic Log - Attempt {attempt}: {ex.Message}");
        }
    }
}
