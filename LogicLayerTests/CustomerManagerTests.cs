using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;
using DataAccessFakes;
using LogicLayer;
using DataAccessLayer;

namespace LogicLayerTests
{
    [TestClass]
    public class CustomerManagerTests
    {
        private ICustomerAccessor _fackCustomerAccessor;
        private CustomerManager _customerManager;

        [TestInitialize]
        public void TestSetup()
        {
            _fackCustomerAccessor = new FakeCustomerAccessor();
            _customerManager = new CustomerManager(_fackCustomerAccessor);
        }

        [TestMethod]
        public void TestSelectCustomerByActive()
        {
            // Arrang
            List<Customer> SelectCustomerByActive;
            // Act
            SelectCustomerByActive = _customerManager.GetCustomerListByActive( true);
            // Assert
            Assert.AreEqual(2, SelectCustomerByActive.Count);

        }

        [TestMethod]
        public void TestEditCustomer()
        {
           
           bool expectedResults = false;
            Customer oldCustomer 
             = new Customer()
             {
                  CustomerId = 1002,
                  FirstName = "Zac",
                  LastName = "Tim",
                  PhoneNumber = "3198376522",
                  Email = "mo@gmail.com",
                  Active = true

             };
           Customer newCustomer
              = new Customer()
              {
                  CustomerId = 1002,
                  FirstName = "Zac",
                  LastName = "Tom",
                  PhoneNumber = "3198376522",
                  Email = "mo@gmail.com",
                  Active = true

              };
             ICustomerManager _customerManager = new CustomerManager(_fackCustomerAccessor);
            //Act
            bool actualResult = _customerManager.EditCustomer(oldCustomer, newCustomer);

            //Assert
            Assert.AreEqual(actualResult, expectedResults);

        }

        [TestMethod]
        public void TestSelectCustomerByID()
        {
            // Arrange
            Customer customer = null;
            const int CustomerID = 1000;
            int result = 0;

            // Act
            customer = _customerManager.SelectCustomerByID(CustomerID);
            if (customer!= null)
            {
                result = 1;

            }
            // Assert
            Assert.AreEqual(1, result);
        }

        
        [TestMethod]
        public void TestDeactivateCustomerByID()
        {
            // Arrange
            bool expectedResult = true;

            ICustomerManager customerManager = new CustomerManager(_fackCustomerAccessor);
            // Act

            var actualResult = customerManager.DeactivateCustomerByID(1000);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void TestAddCustomer()
        {
            // Arrange
            bool expectedResult = true;
            Customer _customer = new Customer()
            {             
                 CustomerId = 1002,
                 FirstName = "Zac",
                 LastName = "Tim",
                 PhoneNumber ="3198376522",
                 Email = "mo@gmail.com",
                 Active = true
            };

            ICustomerManager customerManager = new CustomerManager(_fackCustomerAccessor);
            // Act

           bool actualResult = customerManager.AddCustomer(_customer);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
