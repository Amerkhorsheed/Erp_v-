///*************************************************
//* ERP System Version 1.0
//* Copyright (c) 2024
//*
//* Unit Tests for SalesDAO Methods
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
//    /// Unit tests for the SalesDAO class methods
//    /// </summary>
//    [TestFixture]
//    public class SalesDAOTests
//    {
//        private Mock<SalesDAO> _salesDAOMock;  // Mocked SalesDAO instance

//        /// <summary>
//        /// Setup method to initialize the mock object before each test
//        /// </summary>
//        [SetUp]
//        public void Setup()
//        {
//            _salesDAOMock = new Mock<SalesDAO>();
//        }

//        /// <summary>
//        /// Test case to validate the Insert method of SalesDAO with a valid sale
//        /// </summary>
//        [Test]
//        public void Insert_ValidSale_ReturnsTrue()
//        {
//            // Arrange
//            var sale = new SALES
//            {
//                ID = 1,
//                ProductID = 1,
//                CustomerID = 1,
//                ProductSalesAmout = 2,
//                ProductSalesPrice = 1000,
//                SalesDate = DateTime.Now
//            };
//            _salesDAOMock.Setup(x => x.Insert(sale)).Returns(true);

//            // Act
//            bool result = _salesDAOMock.Object.Insert(sale);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the result is true
//            _salesDAOMock.Verify(x => x.Insert(sale), Times.Once); // Verifies that Insert was called exactly once
//        }

//        /// <summary>
//        /// Test case to validate the Select method of SalesDAO that retrieves a list of sales
//        /// </summary>
//        [Test]
//        public void Select_ReturnsSalesList()
//        {
//            // Arrange
//            var mockSales = new List<SalesDetailDTO>
//            {
//                new SalesDetailDTO { SalesID = 1, ProductName = "Laptop", CustomerName = "Alice", SalesAmount = 2, Price = 2000, SalesDate = DateTime.Now },
//                new SalesDetailDTO { SalesID = 2, ProductName = "Smartphone", CustomerName = "Bob", SalesAmount = 1, Price = 1000, SalesDate = DateTime.Now }
//            };
//            _salesDAOMock.Setup(x => x.Select()).Returns(mockSales);

//            // Act
//            var result = _salesDAOMock.Object.Select();

//            // Assert
//            ClassicAssert.AreEqual(2, result.Count); // Verifies that the result contains 2 sales
//            ClassicAssert.AreEqual("Laptop", result[0].ProductName); // Verifies the name of the first product in the sales list
//        }

//        /// <summary>
//        /// Test case to validate the Delete method of SalesDAO with a valid sale
//        /// </summary>
//        [Test]
//        public void Delete_ValidSale_ReturnsTrue()
//        {
//            // Arrange
//            var sale = new SALES { ID = 1 };
//            _salesDAOMock.Setup(x => x.Delete(sale)).Returns(true);

//            // Act
//            bool result = _salesDAOMock.Object.Delete(sale);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the deletion was successful
//            _salesDAOMock.Verify(x => x.Delete(sale), Times.Once); // Verifies that Delete was called exactly once
//        }

//        /// <summary>
//        /// Test case to validate the Update method of SalesDAO with a valid sale
//        /// </summary>
//        [Test]
//        public void Update_ValidSale_ReturnsTrue()
//        {
//            // Arrange
//            var sale = new SALES { ID = 1, ProductSalesAmout = 5 };
//            _salesDAOMock.Setup(x => x.Update(sale)).Returns(true);

//            // Act
//            bool result = _salesDAOMock.Object.Update(sale);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the update was successful
//            _salesDAOMock.Verify(x => x.Update(sale), Times.Once); // Verifies that Update was called exactly once
//        }
//    }
//}
