using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjectLayer;
using LogicLayerInterfaces;
using LogicLayer;
using DataAccessLayerInterfaces;
using DataAccessFakes;

namespace LogicLayerTests
{
    [TestClass]
    public class ProductManagerTests
    {
        private IProductAccessor _fackProductAccessor;
        private ProductManager _productManager;

        [TestInitialize]
        public void TestSetup()
        {
            _fackProductAccessor = new FakeProductAccessor();
            _productManager = new ProductManager(_fackProductAccessor);
        }

        [TestMethod]
        public void TestSelectProductByActive()
        {
            // Arrang
            List<Product> SelectProductByActive;
            // Act
            SelectProductByActive = _productManager.GetProductListByActive(true);
            // Assert
            Assert.AreEqual(2, SelectProductByActive.Count);
        }

        [TestMethod]
        public void TestSelectProductById()
        {
            // Arrange
            Product product = null;
            const int ProductID = 1000;
            int result = 0;
            // Act
            product = _productManager.RetrieveProductById(ProductID);
            if (product != null)
            {
                result = 1;

            }
            // Assert
            Assert.AreEqual(1, result);


        }

        [TestMethod]
        public void TestInsertProduct()
        {
            // Arrange
            Product product = new Product()
            {
                ProductId = 1,
                Cost = 10,
                Description = "new Des",
                DateReceived = DateTime.Now,
                ProductType = "Hot",
                ManufacturerName = "LG",
                SupplierId = 1002,
                Price = 100,
                PurchaseUnit = "Box",
                SaleUnit = "Box",
                Qoh = 100,
                ReorderLevel = 20,
                Active = true
            };

            // Act
            bool result = _productManager.InsertProduct(product);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestDeactivateProduct()
        {
            // Arrange
            bool expectedResult = false;
            Product product = new Product()
            {
                ProductId = 1,
                Cost = 10,
                Description = "new Des",
                DateReceived = DateTime.Now,
                ProductType = "Hot",
                ManufacturerName = "LG",
                SupplierId = 1002,
                Price = 100,
                PurchaseUnit = "Box",
                SaleUnit = "Box",
                Qoh = 100,
                ReorderLevel = 20,
                Active = true
            };

            IProductManager productManager = new ProductManager(_fackProductAccessor);
            // Act

            bool actualResult = productManager.DeactivateProduct(product);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void TestEditProduct()
        {
            // Arrange
            bool expected = true;     
            Product product = new Product()
            {
                ProductId = 1,
                Cost = 10,
                Description = "new Des",
                DateReceived = DateTime.Now,
                ProductType = "Hot",
                ManufacturerName = "LG",
                SupplierId = 1002,
                Price = 100,
                PurchaseUnit = "Box",
                SaleUnit = "Box",
                Qoh = 100,
                ReorderLevel = 20,
                Active = true
            };
            // Act
            IProductManager productManager = new ProductManager(_fackProductAccessor);
            bool result = productManager.EditProduct(product);
            // Assert
            Assert.AreEqual(expected, result);
        }

    }
}
