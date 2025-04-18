using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjectLayer;
using DataAccessLayerInterfaces;
using DataAccessLayerInterfaces;
using DataObjectLayer;


namespace DataAccessFakes
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/03/31
    /// Approver: 
    /// This class for creation a fake Product data which will used 
    /// for testing Logic layer methods.
    /// </summary>
    public class FakeProductAccessor : IProductAccessor
    {
        private List<Product> Products = null;

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// This is a Constructor method which has fake Fake Product list. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        public FakeProductAccessor()
        {
            Products = new List<Product>()
            {
                new Product()
                {
                    ProductId =1000,
                    Cost = 2000,
                    Description = "new product",
                    DateReceived = DateTime.Now,
                    ProductType = "Hot",
                    ManufacturerName = "LG",
                    SupplierId = 1000,
                    Price = 20000,
                    PurchaseUnit = "box",
                    SaleUnit = "box",
                    Qoh = 20,
                    ReorderLevel = 20,
                    Active = true
                },


                new Product()
                {
                    ProductId =1000,
                    Cost = 2000,
                    Description = "new product",
                    DateReceived = DateTime.Now,
                    ProductType = "Hot",
                    ManufacturerName = "LG",
                    SupplierId = 1000,
                    Price = 20000,
                    PurchaseUnit = "box",
                    SaleUnit = "box",
                    Qoh = 20,
                    ReorderLevel = 20,
                    Active = true
                },
            };

        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Select product by ID. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="productId"></param>
        /// <returns>product</returns>
        public Product SelectProductById(int productId)
        {
            Product _product = new Product();
            foreach (var product in Products)
            {
                if (product.ProductId == productId)
                {
                    _product = product;
                    break;
                }
            }
            return _product;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Select product by Active status. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active = true"></param>
        /// <returns>product active list</returns>
        public List<Product> SelectProductByActive(bool active = true)
        {
            List<Product> _product;
            _product = (from Product in Products
                         where Product.Active == active
                          select Product).ToList();
            return _product;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Deactivates Product.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="productId"></param>
        /// <returns>One or zero depending if the record was updated</returns>
        public int DeactivateProduct(int productId)
        {
            return (from e in Products
                    where e.ProductId == productId
                    select e).Count();
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Inserts Product into the Product table. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="product"></param>
        /// <returns>One or zero depending if the record was updated</returns>
        public int InsertProduct(Product product)
        {
            Products.Add(product);
            return 1;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Updates Product into the Product table. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="oldProduct"></param>
        /// <param name="newProduct"></param>
        /// <returns>One or zero depending if the record was updated</returns>
        public int UpdateProduct(Product oldProduct, Product newProduct)
        {
            return 1;
        }

    }
}
