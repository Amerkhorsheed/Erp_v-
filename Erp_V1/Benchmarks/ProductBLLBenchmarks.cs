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
    /// Comprehensive benchmarks for Product BLL operations.
    /// </summary>
    [MemoryDiagnoser]
    [RankColumn]
    [Config(typeof(BenchmarkConfig))]
    [BenchmarkCategory("Data Layer", "CRUD Operations")]
    public class ProductBLLBenchmarks
    {
        #region Configuration

        private class BenchmarkConfig : ManualConfig
        {
            public BenchmarkConfig()
            {
                AddJob(Job.Default
                    .WithRuntime(ClrRuntime.Net48)
                    .WithPlatform(Platform.X64)
                    .WithJit(Jit.RyuJit)
                    .WithGcServer(true)
                    .WithGcForce(true)
                    .WithIterationCount(15)
                    .WithWarmupCount(5));

                AddDiagnoser(MemoryDiagnoser.Default);
            }
        }

        #endregion

        #region Constants & Fields

        private const string TestProductPrefix = "BM_Product_";
        private const string TestCategoryPrefix = "BM_ProductTestCategory_";
        private const int OperationsPerInvoke = 100;
        private const int MaxRetryAttempts = 3;
        private static readonly TimeSpan TransactionTimeout = TimeSpan.FromSeconds(15);

        private ProductBLL _productBLL;
        private CategoryBLL _categoryBLL;
        private int _testCategoryId;
        private static readonly Random _random = new Random();

        private volatile int dummyResult;

        #endregion

        #region Lifecycle Management

        [GlobalSetup]
        public void GlobalInitialize()
        {
            _productBLL = new ProductBLL();
            _categoryBLL = new CategoryBLL();
            PurgeTestData(); // Clean state before benchmarks

            // Create test category for products
            string testCategoryName = GenerateUniqueCategoryName();
            var category = new CategoryDetailDTO { CategoryName = testCategoryName };
            ExecuteWithRetry(() => _categoryBLL.Insert(category));

            // Retrieve the test category to get its ID
            var categories = _categoryBLL.Select().Categories;
            var testCategory = categories.First(c => c.CategoryName == testCategoryName);
            _testCategoryId = testCategory.ID;
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
                    var product = GenerateUniqueProduct();
                    results[i] = ExecuteWithRetry(() => _productBLL.Insert(product));
                    scope.Complete();
                }
            }

            dummyResult = results.Count(r => r);
        }

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "High-Performance Selects")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public ProductDTO SelectBenchmark()
        {
            ProductDTO result = null;
            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                result = ExecuteWithRetry(() => _productBLL.Select());
            }
            return result;
        }

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "Transactional Update Workflow")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void UpdateBenchmark()
        {
            var testData = GenerateTestProducts(OperationsPerInvoke);
            bool[] results = new bool[OperationsPerInvoke];

            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                using (var scope = CreateTransactionScope())
                {
                    testData[i].ProductName = GenerateUniqueProductName();
                    results[i] = ExecuteWithRetry(() => _productBLL.Update(testData[i]));
                    scope.Complete();
                }
            }
            dummyResult = results.Count(r => r);
        }

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "Bulk Delete Operations")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void DeleteBenchmark()
        {
            bool[] results = new bool[OperationsPerInvoke];

            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                var product = GenerateUniqueProduct();

                using (var insertScope = CreateTransactionScope())
                {
                    if (!ExecuteWithRetry(() => _productBLL.Insert(product)))
                        throw new BenchmarkException("Insert failed in delete benchmark")
                            .WithContext("ProductName", product.ProductName);
                    insertScope.Complete();
                }

                var insertedProduct = ExecuteWithRetry(() =>
                    _productBLL.Select().Products.FirstOrDefault(p => p.ProductName == product.ProductName));
                if (insertedProduct == null)
                    throw new BenchmarkException("Product not found after insertion")
                        .WithContext("ProductName", product.ProductName);

                using (var deleteScope = CreateTransactionScope())
                {
                    results[i] = ExecuteWithRetry(() => _productBLL.Delete(insertedProduct));
                    deleteScope.Complete();
                }
            }

            dummyResult = results.Sum(r => r ? 1 : 0);
        }

        [Benchmark(OperationsPerInvoke = OperationsPerInvoke, Description = "Restore Operation Throughput")]
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void RestoreBenchmark()
        {
            bool[] results = new bool[OperationsPerInvoke];

            for (int i = 0; i < OperationsPerInvoke; i++)
            {
                var product = GenerateUniqueProduct();

                using (var insertScope = CreateTransactionScope())
                {
                    if (!ExecuteWithRetry(() => _productBLL.Insert(product)))
                        throw new BenchmarkException("Insert failed in restore benchmark")
                            .WithContext("ProductName", product.ProductName);
                    insertScope.Complete();
                }

                using (var deleteScope = CreateTransactionScope())
                {
                    ExecuteWithRetry(() => _productBLL.Delete(product));
                    deleteScope.Complete();
                }

                using (var restoreScope = CreateTransactionScope())
                {
                    results[i] = ExecuteWithRetry(() => _productBLL.GetBack(product));
                    restoreScope.Complete();
                }
            }

            dummyResult = results.Sum(r => r ? 1 : 0);
        }

        #endregion

        #region Core Utilities

        private ProductDetailDTO GenerateUniqueProduct()
        {
            return new ProductDetailDTO
            {
                ProductName = GenerateUniqueProductName(),
                CategoryID = _testCategoryId,
                price = 100,         // Updated from UnitPrice to match DTO property 'price'
                stockAmount = 100    // Updated from StockQuantity to match DTO property 'stockAmount'
                // Set other properties as required
            };
        }

        private ProductDetailDTO[] GenerateTestProducts(int count)
        {
            var products = new ProductDetailDTO[count];
            for (int i = 0; i < count; i++)
            {
                products[i] = GenerateUniqueProduct();
                ExecuteWithRetry(() => _productBLL.Insert(products[i]));
            }
            return products;
        }

        private string GenerateUniqueProductName()
        {
            return $"{TestProductPrefix}{Guid.NewGuid():N}";
        }

        private string GenerateUniqueCategoryName()
        {
            return $"{TestCategoryPrefix}{Guid.NewGuid():N}";
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
                    return operation();
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
                        // Delete test products
                        var testProducts = _productBLL.Select().Products
                            .Where(p => p.ProductName.StartsWith(TestProductPrefix))
                            .ToList();

                        foreach (var product in testProducts)
                        {
                            ExecuteWithRetry(() => _productBLL.Delete(product));
                        }

                        // Delete test categories
                        var testCategories = _categoryBLL.Select().Categories
                            .Where(c => c.CategoryName.StartsWith(TestCategoryPrefix))
                            .ToList();

                        foreach (var category in testCategories)
                        {
                            ExecuteWithRetry(() => _categoryBLL.Delete(category));
                        }

                        purgeScope.Complete();
                        Console.WriteLine($"Purged {testProducts.Count} products and {testCategories.Count} categories in {sw.Elapsed}");
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

    // Include BenchmarkException and DiagnosticMonitor classes as in the original code.
}
