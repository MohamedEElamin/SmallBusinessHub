
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using DataObjectLayer;


namespace DataAccessLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/15
    /// Approver: 
    /// Class for the Address Accessor class. 
    /// </summary>
    public class AddressAccessor : IAddressAccessor
    {
        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name="address"></param>

        public int InsertAddress(Address address)
        {
            int addressId = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_address", conn);
            cmd.CommandType = CommandType.StoredProcedure;
             
            cmd.Parameters.AddWithValue("@addressLineOne", address.LineOne);
            cmd.Parameters.AddWithValue("@addressLineTwo", address.LineTwo);
            cmd.Parameters.AddWithValue("@addressTypeID", address.Type);
            cmd.Parameters.AddWithValue("@zipCodeID", address.ZipCode);

            try
            {
                conn.Open();
                addressId = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return addressId;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name="addressId"></param>
        /// <returns>adderss</returns>
        public Address SelectAddressByAddressId(int addressId)
        {
            Address address = null;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_address_by_address_id");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AddressId", SqlDbType.Int);
            cmd.Parameters["@AddressId"].Value = addressId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        address = new Address()
                        {
                            LineOne = reader.GetString(0),
                            LineTwo = reader.IsDBNull(1) ? null: reader.GetString(1),
                            Type = reader.GetString(2),
                            ZipCode = reader.GetString(3),
                            Active = reader.GetBoolean(4),
                            AddressId = addressId
                        };
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
            return address;
        }


        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <returns>all adderss types </returns>
        public List<string> SelectAllAddressTypes()
        {
            List<string> addressTypes = new List<string>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_address_types");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        addressTypes.Add(reader.GetString(0));
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
            return addressTypes;
        }


        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name="oldAddress"></param>
        /// <param name="newAddress"></param>
        /// <returns>True or false depending if the record was edited </returns>
        public int UpdateAddress(Address oldAddress, Address newAddress)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_address", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AddressId", oldAddress.AddressId);

            cmd.Parameters.AddWithValue("@NewAddressLineOne", newAddress.LineOne);
            cmd.Parameters.AddWithValue("@NewAddressLineTwo", newAddress.LineTwo);
            cmd.Parameters.AddWithValue("@NewAddressTypeID", newAddress.Type);
            cmd.Parameters.AddWithValue("@NewZipCodeID", newAddress.ZipCode);
            cmd.Parameters.AddWithValue("@NewActive", newAddress.Active);

            cmd.Parameters.AddWithValue("@OldAddressLineOne", oldAddress.LineOne);
            if(oldAddress.LineTwo == null)
            {
                cmd.Parameters.AddWithValue("@OldAddressLineTwo", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldAddressLineTwo", oldAddress.LineTwo);
            }
            
            cmd.Parameters.AddWithValue("@OldAddressTypeID", oldAddress.Type);
            cmd.Parameters.AddWithValue("@OldZipCodeID", oldAddress.ZipCode);
            cmd.Parameters.AddWithValue("@OldActive", oldAddress.Active);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ApplicationException("Address record not found.");
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
