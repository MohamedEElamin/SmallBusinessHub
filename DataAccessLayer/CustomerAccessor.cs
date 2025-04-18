using System;

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjectLayer;

namespace DataAccessLayerInterfaces
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/04
    /// Approver: 
    /// Class for the Customer Accessor. 
    /// </summary>
    public class CustomerAccessor : ICustomerAccessor
    {


        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name=" ID"></param>
        /// <returns>one or zero depending if the record was updated </returns>
        public int DeactivateCustomerByID(int ID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_customer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customerID", ID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
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
        /// Created: 2025/04/04
        /// Approver:
        /// 
        /// Actual summary of the method
        /// </summary>
        ///
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name="customer"></param>
        /// <returns>zero or one depending if the record was edited </returns>
        public int InsertCustomer(Customer customer)
        {
            int customerId = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_customer", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", customer.Email);

            try
            {
                conn.Open();
                customerId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return customerId;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver:
        /// 
        /// Actual summary of the method
        /// </summary>
        ///
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<Customer> SelectCustomerByActive(bool active)
        {

            List<Customer> customers = new List<Customer>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_customer_by_active");
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
                        var customer = new Customer();
                        customer.CustomerId = reader.GetInt32(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                        customer.PhoneNumber = reader.GetString(3);
                        customer.Email = reader.GetString(4);
                        customer.Active = active;
                        customers.Add(customer);
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
            return customers;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver:
        /// 
        /// Actual summary of the method
        /// </summary>
        ///
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name=" ID"></param>
        /// <returns>customer</returns>
        public Customer SelectCustomerById(int ID)
        {
            Customer customer = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_customer_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", ID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    customer = new Customer()
                    {


                        CustomerId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),

                        Active = reader.GetBoolean(5)


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

            return customer;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: 
        /// Updated:  
        /// Update: 
        /// </remarks>
        /// <param name="oldCustomer"></param>
        /// <param name="newCustomer"></param>
        /// <returns>True or false depending if the record was edited </returns>
        public int UpdateCustomer(Customer oldCustomer, Customer newCustomer)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_customer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            Customer customer = new Customer();
            cmd.Parameters.AddWithValue("@CustomerID", oldCustomer.CustomerId);

            cmd.Parameters.AddWithValue("@NewFirstName", newCustomer.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", newCustomer.LastName);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", newCustomer.PhoneNumber);
            cmd.Parameters.AddWithValue("@NewEmail", newCustomer.Email);

            cmd.Parameters.AddWithValue("@OldFirstName", oldCustomer.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldCustomer.LastName);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldCustomer.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldCustomer.Email);

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
    }
}
