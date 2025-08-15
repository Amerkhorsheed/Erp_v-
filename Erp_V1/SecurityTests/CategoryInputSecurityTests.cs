using Microsoft.VisualStudio.TestTools.UnitTesting;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;

namespace Erp_V1.SecurityTests
{
    [TestClass]
    public class CategoryInputSecurityTests
    {
        private CategoryBLL _categoryBLL;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize your business logic layer instance.
            _categoryBLL = new CategoryBLL();
        }

        /// <summary>
        /// Test that inserting a category with an excessively long name is rejected
        /// or fails gracefully without leaking internal details.
        /// </summary>
        [TestMethod]
        public void InsertCategory_WithExcessivelyLongInput_ShouldRejectInput()
        {
            // Arrange: Create a category with a very long name (e.g., 10,000 characters).
            string longName = new string('A', 10000);
            var category = new CategoryDetailDTO { CategoryName = longName };

            // Act & Assert: Expect the insertion to fail or throw an exception.
            try
            {
                bool result = _categoryBLL.Insert(category);
                // If the method returns a result (false), you can assert that.
                // Otherwise, if an exception is expected, we call Fail.
                Assert.Fail("Expected an exception or a failure result due to excessively long input.");
            }
            catch (Exception ex)
            {
                // Assert: The thrown exception should contain a generic error message
                // (e.g., "Category insertion failed") without leaking internal details.
                Assert.IsTrue(ex.Message.Contains("Category insertion failed"),
                              "The error message should be generic when long input is provided.");
                // Optionally, ensure that no internal exception details (e.g., related to overflow or SQL errors)
                // are leaked:
                Assert.IsFalse(ex.Message.Contains("Overflow") || ex.Message.Contains("SqlException"),
                               "The error message should not reveal internal details.");
            }
        }
    }
}
