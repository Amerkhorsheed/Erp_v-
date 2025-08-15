///*************************************************
//* ERP System Version 1.0
//* Copyright (c) 2024
//*
//* Unit Tests for CategoryDAO Methods
//*************************************************/
//using NUnit.Framework;
//using NUnit.Framework.Legacy; // Added for ClassicAssert
//using Moq;
//using Erp_V1.DAL.DAO;
//using Erp_V1.DAL.DTO;
//using Erp_V1.DAL;
//using System.Collections.Generic;
//using System;

//namespace Erp_V1.Tests
//{
//    /// <summary>
//    /// Unit tests for the CategoryDAO class methods
//    /// </summary>
//    [TestFixture]
//    public class CategoryDAOTests
//    {
//        private Mock<CategoryDAO> _categoryDAOMock;  // Mocked CategoryDAO instance

//        /// <summary>
//        /// Setup method to initialize the mock object before each test
//        /// </summary>
//        [SetUp]
//        public void Setup()
//        {
//            _categoryDAOMock = new Mock<CategoryDAO>();
//        }

//        /// <summary>
//        /// Test case to validate the Insert method of CategoryDAO with a valid category
//        /// </summary>
//        [Test]
//        public void Insert_ValidCategory_ReturnsTrue()
//        {
//            // Arrange
//            var category = new CATEGORY { ID = 1, CategoryName = "Books" };
//            _categoryDAOMock.Setup(x => x.Insert(category)).Returns(true);

//            // Act
//            bool result = _categoryDAOMock.Object.Insert(category);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the result is true
//            _categoryDAOMock.Verify(x => x.Insert(category), Times.Once); // Verifies that Insert was called exactly once
//        }

//        /// <summary>
//        /// Test case to validate the Select method of CategoryDAO that retrieves a list of categories
//        /// </summary>
//        [Test]
//        public void Select_ReturnsCategoryList()
//        {
//            // Arrange
//            var mockCategories = new List<CategoryDetailDTO>
//            {
//                new CategoryDetailDTO { ID = 1, CategoryName = "Books" },
//                new CategoryDetailDTO { ID = 2, CategoryName = "Electronics" }
//            };
//            _categoryDAOMock.Setup(x => x.Select()).Returns(mockCategories);

//            // Act
//            var result = _categoryDAOMock.Object.Select();

//            // Assert
//            ClassicAssert.AreEqual(2, result.Count); // Verifies that the result contains 2 categories
//            ClassicAssert.AreEqual("Books", result[0].CategoryName); // Verifies the name of the first category
//        }

//        /// <summary>
//        /// Test case to validate the Delete method of CategoryDAO with a valid category
//        /// </summary>
//        [Test]
//        public void Delete_ValidCategory_ReturnsTrue()
//        {
//            // Arrange
//            var category = new CATEGORY { ID = 1, CategoryName = "Books" };
//            _categoryDAOMock.Setup(x => x.Delete(category)).Returns(true); // Mocking the Delete method

//            // Act
//            bool result = _categoryDAOMock.Object.Delete(category);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the deletion was successful
//            _categoryDAOMock.Verify(x => x.Delete(category), Times.Once); // Verifies that Delete was called exactly once
//        }

//        /// <summary>
//        /// Test case to validate the Update method of CategoryDAO with a valid category
//        /// </summary>
//        [Test]
//        public void Update_ValidCategory_ReturnsTrue()
//        {
//            // Arrange
//            var category = new CATEGORY { ID = 1, CategoryName = "Updated Books" };
//            _categoryDAOMock.Setup(x => x.Update(category)).Returns(true); // Mocking the Update method

//            // Act
//            bool result = _categoryDAOMock.Object.Update(category);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the update was successful
//            _categoryDAOMock.Verify(x => x.Update(category), Times.Once); // Verifies that Update was called exactly once
//        }
//    }
//}
