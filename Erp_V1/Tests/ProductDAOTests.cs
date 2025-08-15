///*************************************************
//* ERP System Version 1.0
//* Copyright (c) 2024
//*
//* Unit Tests for ProductDAO Methods
//*************************************************/
//using NUnit.Framework;
//using NUnit.Framework.Legacy; // Added for ClassicAssert
//using Moq;
//using Erp_V1.DAL.DAO;
//using Erp_V1.DAL.DTO;
//using Erp_V1.DAL;
//using System.Collections.Generic;

//namespace Erp_V1.Tests
//{
//    /// <summary>
//    /// Unit tests for the ProductDAO class methods
//    /// </summary>
//    [TestFixture]
//    public class ProductDAOTests
//    {
//        private Mock<ProductDAO> _productDAOMock;  // Mocked ProductDAO instance

//        /// <summary>
//        /// Setup method to initialize the mock object before each test
//        /// </summary>
//        [SetUp]
//        public void Setup()
//        {
//            _productDAOMock = new Mock<ProductDAO>();
//        }

//        /// <summary>
//        /// Test case to validate the Insert method of ProductDAO with a valid product
//        /// </summary>
//        [Test]
//        public void Insert_ValidProduct_ReturnsTrue()
//        {
//            // Arrange
//            var product = new PRODUCT
//            {
//                ID = 1,
//                ProductName = "Laptop",
//                CategoryID = 1,
//                StockAmount = 10,
//                Price = 1000
//            };
//            _productDAOMock.Setup(x => x.Insert(product)).Returns(true);

//            // Act
//            bool result = _productDAOMock.Object.Insert(product);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the result is true
//            _productDAOMock.Verify(x => x.Insert(product), Times.Once); // Verifies that Insert was called exactly once
//        }

//        /// <summary>
//        /// Test case to validate the Select method of ProductDAO that retrieves a list of products
//        /// </summary>
//        [Test]
//        public void Select_ReturnsProductList()
//        {
//            // Arrange
//            var mockProducts = new List<ProductDetailDTO>
//            {
//                new ProductDetailDTO { ProductID = 1, ProductName = "Laptop", CategoryName = "Electronics", stockAmount = 10, price = 1000 },
//                new ProductDetailDTO { ProductID = 2, ProductName = "Smartphone", CategoryName = "Electronics", stockAmount = 20, price = 500 }
//            };
//            _productDAOMock.Setup(x => x.Select()).Returns(mockProducts);

//            // Act
//            var result = _productDAOMock.Object.Select();

//            // Assert
//            ClassicAssert.AreEqual(2, result.Count); // Verifies that the result contains 2 products
//            ClassicAssert.AreEqual("Laptop", result[0].ProductName); // Verifies the name of the first product
//        }

//        /// <summary>
//        /// Test case to validate the Delete method of ProductDAO with a valid product
//        /// </summary>
//        [Test]
//        public void Delete_ValidProduct_ReturnsTrue()
//        {
//            // Arrange
//            var product = new PRODUCT { ID = 1, ProductName = "Laptop" };
//            _productDAOMock.Setup(x => x.Delete(product)).Returns(true);

//            // Act
//            bool result = _productDAOMock.Object.Delete(product);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the deletion was successful
//            _productDAOMock.Verify(x => x.Delete(product), Times.Once); // Verifies that Delete was called exactly once
//        }

//        /// <summary>
//        /// Test case to validate the Update method of ProductDAO with a valid product
//        /// </summary>
//        [Test]
//        public void Update_ValidProduct_ReturnsTrue()
//        {
//            // Arrange
//            var product = new PRODUCT { ID = 1, ProductName = "Laptop", StockAmount = 10 };
//            _productDAOMock.Setup(x => x.Update(product)).Returns(true);

//            // Act
//            bool result = _productDAOMock.Object.Update(product);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the update was successful
//            _productDAOMock.Verify(x => x.Update(product), Times.Once); // Verifies that Update was called exactly once
//        }
//    }
//}
