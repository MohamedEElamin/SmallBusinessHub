using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjectLayer;

namespace DataAccessLayerInterfaces
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/03/31
    /// Approver: 
    /// This is the Product Accessor class that implements the product interface.
    /// </summary>
    ///

    public class ProductAccessor : IProductAccessor
    {
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
        /// <returns> The producd id if the record was updated</returns>
        public int InsertProduct(Product product)
        {
            int productId = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_product", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cost", product.Cost);
            cmd.Parameters.AddWithValue("description", product.Description);
            cmd.Parameters.AddWithValue("@dateReceived", product.DateReceived);
            cmd.Parameters.AddWithValue("@productTypeID", product.ProductType);
            cmd.Parameters.AddWithValue("@manufacturerName", product.ManufacturerName);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@purchaseUnit", product.PurchaseUnit);
            cmd.Parameters.AddWithValue("@saleUnit", product.SaleUnit);
            cmd.Parameters.AddWithValue("@supplierID", product.SupplierId);
            cmd.Parameters.AddWithValue("@qoh", product.Qoh);
            cmd.Parameters.AddWithValue("@reorderLevel", product.ReorderLevel);
            cmd.Parameters.AddWithValue("@active", product.Active);

            try
            {
                conn.Open();
                productId = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return productId;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Retrieves Product Active
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active = true"></param>
        /// <returns>product list</returns>
        public List<Product> SelectProductByActive(bool active = true)
        {
            List<Product> products = new List<Product>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_product_by_active");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var product = new Product()
                        {
                            ProductId = reader.GetInt32(0),
                            Cost = reader.GetDecimal(1),
                            Description = reader.GetString(2),
                            DateReceived = reader.GetDateTime(3),
                            ProductType = reader.GetString(4),
                            ManufacturerName = reader.GetString(5),
                            SupplierId = reader.GetInt32(6),
                            Price = reader.GetDecimal(7),
                            PurchaseUnit = reader.GetString(8),
                            SaleUnit = reader.GetString(9),
                            Qoh = reader.GetInt32(10),
                            ReorderLevel = reader.GetInt32(11),
                            Active = active
                        };

                        products.Add(product);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return products;
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
        /// <returns> Rows effected if record was updated</returns>        
        public int UpdateProduct(Product oldProduct, Product newProduct)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_product", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProductID", newProduct.ProductId);
            cmd.Parameters.AddWithValue("@NewCost", newProduct.Cost);
            cmd.Parameters.AddWithValue("@NewDescription", newProduct.Description);
            cmd.Parameters.AddWithValue("@NewProductTypeID", newProduct.ProductType);
            cmd.Parameters.AddWithValue("@NewManufacturerName", newProduct.ManufacturerName);
            cmd.Parameters.AddWithValue("@NewPrice", newProduct.Price);
            cmd.Parameters.AddWithValue("@NewPurchaseUnit", newProduct.PurchaseUnit);
            cmd.Parameters.AddWithValue("@NewSaleUnit", newProduct.SaleUnit);
            cmd.Parameters.AddWithValue("@NewSupplierID", newProduct.SupplierId);
            cmd.Parameters.AddWithValue("@NewDateReceived", newProduct.DateReceived);
            cmd.Parameters.AddWithValue("@NewQOH", newProduct.Qoh);
            cmd.Parameters.AddWithValue("@NewReorderLevel", newProduct.ReorderLevel);
            cmd.Parameters.AddWithValue("@NewActive", newProduct.Active);
            cmd.Parameters.AddWithValue("@OldCost", oldProduct.Cost);
            cmd.Parameters.AddWithValue("@OldDescription", oldProduct.Description);
            cmd.Parameters.AddWithValue("@OldProductTypeID", oldProduct.ProductType);
            cmd.Parameters.AddWithValue("@OldManufacturerName", oldProduct.ManufacturerName);
            cmd.Parameters.AddWithValue("@OldPrice", oldProduct.Price);
            cmd.Parameters.AddWithValue("@OldPurchaseUnit", oldProduct.PurchaseUnit);
            cmd.Parameters.AddWithValue("@OldSaleUnit", oldProduct.SaleUnit);
            cmd.Parameters.AddWithValue("@OldSupplierID", oldProduct.SupplierId);
            cmd.Parameters.AddWithValue("@OldDateReceived", oldProduct.DateReceived);
            cmd.Parameters.AddWithValue("@OldQOH", oldProduct.Qoh);
            cmd.Parameters.AddWithValue("@OldReorderLevel", oldProduct.ReorderLevel);
            cmd.Parameters.AddWithValue("@OldActive", oldProduct.Active);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ApplicationException("Record not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
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
        /// <param name="product id"></param>
        /// <returns> Rows effected if the record was deleted</returns>
        public int DeactivateProduct(int productId)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_product", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", productId);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ApplicationException("Record not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
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
        /// <param name="product Id"></param>
        /// <returns>product</returns>
        public Product SelectProductById(int productId)
        {
            Product product = null;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_product_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", productId);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        Cost = reader.GetDecimal(1),
                        Description = reader.GetString(2),
                        DateReceived = reader.GetDateTime(3),
                        ProductType = reader.GetString(4),
                        ManufacturerName = reader.GetString(5),
                        SupplierId = reader.GetInt32(6),
                        Price = reader.GetDecimal(7),
                        PurchaseUnit = reader.GetString(8),
                        SaleUnit = reader.GetString(9),
                        Qoh = reader.GetInt32(10),
                        ReorderLevel = reader.GetInt32(11),
                        Active = reader.GetBoolean(12)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return product;
        }
    }
}