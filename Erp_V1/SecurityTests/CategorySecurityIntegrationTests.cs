using Microsoft.VisualStudio.TestTools.UnitTesting;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System.Linq;

namespace Erp_V1.SecurityTests
{
    [TestClass]
    public class CategorySecurityIntegrationTests
    {
        private CategoryBLL _categoryBLL;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize the Business Logic Layer instance.
            _categoryBLL = new CategoryBLL();
        }

        /// <summary>
        /// Test that inserting a category with malicious SQL input does not execute injection.
        /// </summary>
        [TestMethod]
        public void InsertCategory_WithSqlInjectionAttempt_ShouldNotExecuteInjection()
        {
            // Arrange: Malicious input that attempts SQL injection.
            string maliciousInput = "CategoryName'); DROP TABLE CATEGORies;--";
            var category = new CategoryDetailDTO
            {
                CategoryName = maliciousInput
            };

            // Act: Insert the category.
            bool insertResult = _categoryBLL.Insert(category);

            // Assert: 
            // 1. The insertion should return true (i.e. complete without error).
            Assert.IsTrue(insertResult, "Insertion should succeed without executing SQL injection.");

            // 2. Retrieve categories and verify that the malicious input is stored literally.
            var categories = _categoryBLL.Select().Categories;
            var insertedCategory = categories.FirstOrDefault(c => c.CategoryName == maliciousInput);
            Assert.IsNotNull(insertedCategory, "The malicious input should be stored as literal text and not execute as SQL.");
        }

        /// <summary>
        /// Test that updating a category with malicious SQL input does not execute injection.
        /// </summary>
        [TestMethod]
        public void UpdateCategory_WithSqlInjectionAttempt_ShouldNotExecuteInjection()
        {
            // Arrange: Insert a safe category first.
            var safeCategory = new CategoryDetailDTO { CategoryName = "SafeCategory" };
            bool insertResult = _categoryBLL.Insert(safeCategory);
            Assert.IsTrue(insertResult, "Safe category insertion failed.");

            // Retrieve the inserted safe category.
            var categoryList = _categoryBLL.Select().Categories;
            var categoryToUpdate = categoryList.FirstOrDefault(c => c.CategoryName == "SafeCategory");
            Assert.IsNotNull(categoryToUpdate, "Safe category not found for update.");

            // Malicious update: attempt SQL injection via update.
            string maliciousUpdate = "UpdatedCategory'); DROP TABLE CATEGORies;--";
            categoryToUpdate.CategoryName = maliciousUpdate;

            // Act: Update the category.
            bool updateResult = _categoryBLL.Update(categoryToUpdate);

            // Assert:
            // 1. The update should return true (i.e. complete without error).
            Assert.IsTrue(updateResult, "Update should succeed without executing SQL injection.");

            // 2. Verify that the updated value is stored as literal text.
            var updatedCategoryList = _categoryBLL.Select().Categories;
            var updatedCategory = updatedCategoryList.FirstOrDefault(c => c.CategoryName == maliciousUpdate);
            Assert.IsNotNull(updatedCategory, "The malicious update input should be stored as literal text and not execute as SQL.");
        }
    }
}
