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

namespace DataAccessLayer
{
    public class ProductAccessor : IProductAccessor
    {
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
    }
}
