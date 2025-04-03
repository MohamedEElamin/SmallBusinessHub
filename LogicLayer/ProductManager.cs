using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjectLayer;
using LogicLayerInterfaces;

namespace LogicLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2020/03/31
    /// Approver: 
    /// This is the Product Manager class that implements the product Product Manager interface.
    /// </summary>
    public class ProductManager : IProductManager
    {
        IProductAccessor _productAccessor;

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2020/03/31
        /// Approver: 
        ///  Product Manager Constructor method
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        public ProductManager()
        {
            _productAccessor = new ProductAccessor();
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        ///  Product Manager Constructor method
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="productAccessor"></param>
        public ProductManager(IProductAccessor productAccessor)
        {
            _productAccessor = productAccessor;
          
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Updates product.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="product"></param>
        /// <returns>True or False depending if the record was updated</returns>
        public bool EditProduct(Product product)

        {
            bool result = false;

            try
            {
                Product oldProduct = _productAccessor.SelectProductById(product.ProductId);
                result = (1 == _productAccessor.UpdateProduct(oldProduct, product));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }
            return result;

        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Retrieves Products List By Active.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active"></param>
        /// <returns>Products list</returns>
        public List<Product> GetProductListByActive(bool active = true)
        {
            try
            {
                return _productAccessor.SelectProductByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("List Not Available", ex);
            }
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Inserts Product.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="product"></param>
        /// <returns>True or False depending if the record was updated</returns>
        public bool InsertProduct(Product product)
        {
            bool result = false;

            try
            {
                result = (_productAccessor.InsertProduct(product) > 0);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Retrieves Product By Id.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="productId"></param>
        /// <returns>product</returns>
        public Product RetrieveProductById(int productId)
        {
            try
            {
                return _productAccessor.SelectProductById(productId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
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
        /// <param name="product"></param>
        /// <returns>True or False depending if the record was updated</returns>
        bool IProductManager.DeactivateProduct(Product product)
        {
            bool result = false;
            try
            {
                result = (_productAccessor.DeactivateProduct(product.ProductId) > 0);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Deactivation failed.", ex);
            }
            return result;
        }
    }
}



