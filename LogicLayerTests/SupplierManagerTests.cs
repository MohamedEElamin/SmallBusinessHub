using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjectLayer;
using DataAccessFakes;
using LogicLayer;
using DataAccessLayerInterfaces;

namespace LogicLayerTests
{
    [TestClass]
    public class SupplierManagerTests
    {
        private ISupplierAccessor _fakeSupplierAccessor;
        private SupplierManager _supplierManager;

        [TestInitialize]
        public void TestSetup()
        {
            _fakeSupplierAccessor = new FakeSupplierAccessor();
            _supplierManager = new SupplierManager(_fakeSupplierAccessor);
        }

        [TestMethod]
        public void TestSelectSupplierById()
        {
            // Arrange
            Supplier supplier = null;
            const int SupplierID = 1000;
            int result = 0;
            // Act
            supplier = _supplierManager.RetrieveSupplierById(SupplierID);
            if (supplier != null)
            {
                result = 1;
            }
            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestSelectSupplierByActive()
        {
            // Arrang
            List<Supplier> SelectSupplierByActive;
            // Act
            SelectSupplierByActive = _supplierManager.GetSupplierListByActive(true);
            // Assert
            Assert.AreEqual(3, SelectSupplierByActive.Count);
        }

        [TestMethod]
        public void TestDeactivateSupplier()
        {
            // Arrange
            bool expectedResult = true;
            Supplier _supplier = new Supplier()
            {
                SupplierId = 1002,
                Name = "Zac",           
                PhoneNumber = "3198376522",
                Email = "mo@gmail.com",
                Active = true
            };
            ISupplierManager supplierManager = new SupplierManager(_fakeSupplierAccessor);
            // Act

            bool actualResult = supplierManager.DeactivateSupplier(_supplier);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestInsertSupplier()
        {
            // Arrange
           // bool result = true;
            Supplier _supplier = new Supplier()
            {
                SupplierId = 1002,
                Name = "Zac",
                PhoneNumber = "3198376522",
                Email = "mo@gmail.com",
                Active = true
            };
           
            // Act
           bool result = _supplierManager.InsertSupplier(_supplier);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEditSupplier()
        {

            bool expected = true;
            bool result;
            Supplier supplier = new Supplier()
            {
                SupplierId = 1002,
                Name = "Zac",
                PhoneNumber = "3198376522",
                Email = "mo@gmail.com",
                Active = true
            };
    
            result = _supplierManager.EditSupplier(supplier);
            Assert.AreEqual(expected, result);
        }
    }
}
