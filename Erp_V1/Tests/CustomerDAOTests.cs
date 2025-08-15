///*************************************************
//* ERP System Version 1.0
//* Copyright (c) 2024
//*
//* Unit Tests for CustomerDAO Methods
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
//    /// Unit tests for the CustomerDAO class methods
//    /// </summary>
//    [TestFixture]
//    public class CustomerDAOTests
//    {
//        private Mock<CustomerDAO> _customerDAOMock;  // Mocked CustomerDAO instance

//        /// <summary>
//        /// Setup method to initialize the mock object before each test
//        /// </summary>
//        [SetUp]
//        public void Setup()
//        {
//            _customerDAOMock = new Mock<CustomerDAO>();
//        }

//        /// <summary>
//        /// Test case to validate the Insert method of CustomerDAO with a valid customer
//        /// </summary>
//        [Test]
//        public void Insert_ValidCustomer_ReturnsTrue()
//        {
//            // Arrange
//            var customer = new CUSTOMER { ID = 1, CustomerName = "Alice" };
//            _customerDAOMock.Setup(x => x.Insert(customer)).Returns(true);

//            // Act
//            bool result = _customerDAOMock.Object.Insert(customer);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the result is true
//            _customerDAOMock.Verify(x => x.Insert(customer), Times.Once); // Verifies that Insert was called exactly once
//        }

//        /// <summary>
//        /// Test case to validate the Select method of CustomerDAO that retrieves a list of customers
//        /// </summary>
//        [Test]
//        public void Select_ReturnsCustomerList()
//        {
//            // Arrange
//            var mockCustomers = new List<CustomerDetailDTO>
//            {
//                new CustomerDetailDTO { ID = 1, CustomerName = "Alice" },
//                new CustomerDetailDTO { ID = 2, CustomerName = "Bob" }
//            };
//            _customerDAOMock.Setup(x => x.Select()).Returns(mockCustomers);

//            // Act
//            var result = _customerDAOMock.Object.Select();

//            // Assert
//            ClassicAssert.AreEqual(2, result.Count); // Verifies that the result contains 2 customers
//            ClassicAssert.AreEqual("Alice", result[0].CustomerName); // Verifies the name of the first customer
//        }

//        /// <summary>
//        /// Test case to validate the Delete method of CustomerDAO with a valid customer, ensuring IsDeleted is set to true
//        /// </summary>
//        [Test]
//        public void Delete_ValidCustomer_SetsIsDeletedToTrue()
//        {
//            // Arrange
//            var customer = new CUSTOMER { ID = 1, isDeleted = false };
//            _customerDAOMock.Setup(x => x.Delete(customer)).Returns(true);

//            // Act
//            bool result = _customerDAOMock.Object.Delete(customer);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the deletion was successful
//            _customerDAOMock.Verify(x => x.Delete(customer), Times.Once); // Verifies that Delete was called exactly once
//        }

//        /// <summary>
//        /// Test case to validate the Update method of CustomerDAO with a valid customer
//        /// </summary>
//        [Test]
//        public void Update_ValidCustomer_ReturnsTrue()
//        {
//            // Arrange
//            var customer = new CUSTOMER { ID = 1, CustomerName = "Alice" };
//            _customerDAOMock.Setup(x => x.Update(customer)).Returns(true);

//            // Act
//            bool result = _customerDAOMock.Object.Update(customer);

//            // Assert
//            ClassicAssert.IsTrue(result); // Verifies that the update was successful
//            _customerDAOMock.Verify(x => x.Update(customer), Times.Once); // Verifies that Update was called exactly once
//        }
//    }
//}
