using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjectLayer;

namespace DataAccessLayerInterfaces
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/15
    /// Approver: 
    /// This is the Supplier Accessor class.
    /// </summary>
    public class SupplierAccessor : ISupplierAccessor
	{
        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// Deactivates supplier.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="supplier"></param>
        /// <returns>True or False depending if the record was updated</returns>
        public int DeactivateSupplier(int supplierId)
		{
			 int rows = 0;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_deactivate_supplier", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@SupplierID", supplierId);

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
        /// Created: 2025/04/15
        /// Approver: 
        /// Retrieves supplier By Id.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="supplierId"></param>
        /// <returns>supplier</returns>
        public Supplier SelectSupplierById(int supplierId)
		{
			Supplier _supplier = null;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_select_supplier_by_id", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@SupplierID", supplierId);

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					reader.Read();
					var supplier = new Supplier()
					{
						/*
						 *	[SupplierID], [name], [phoneNumber],[Email],[addressID],[active]
						 *
						 */
						SupplierId = reader.GetInt32(0),
						Name = reader.GetString(1),
						PhoneNumber = reader.GetString(2),
						Email = reader.GetString(3),
					  
						Active = reader.GetBoolean(5)
					};

					/**
					 *[addressLineOne], [addressLineTwo], [addressTypeID], [zipCodeID], [active]
					 */

					int addressId = reader.GetInt32(4);
					supplier.Address = new AddressAccessor().SelectAddressByAddressId(addressId);
					_supplier = supplier;
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

			return _supplier;
		}

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// Retrieves supplier List By Active.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active"></param>
        /// <returns>Supplier list</returns>
        public List<Supplier> SelectSupplierByActive(bool active = true)
		{

			List<Supplier> supppliers = new List<Supplier>();
				var conn = DBConnection.GetConnection();
				var cmd = new SqlCommand("sp_select_supplier_by_active");
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

						var supplier = new Supplier()
						{
							SupplierId = reader.GetInt32(0),
							Name = reader.GetString(1),
							PhoneNumber = reader.GetString(2),
							Email = reader.GetString(3),
							
							Active = reader.GetBoolean(5)
						};
						int addressId = reader.GetInt32(4);
						supplier.Address = new AddressAccessor().SelectAddressByAddressId(addressId);
						supppliers.Add(supplier);
					}
					reader.Close();
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

			return supppliers;
		}

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// Updates supplier.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="supplier"></param>
        /// <returns>True or False depending if the record was updated</returns>
        public int UpdateSupplier(Supplier oldSupplier, Supplier newSupplier)
		{
			int rows = 0;

			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_update_supplier", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			Supplier supplier = new Supplier();
			
			cmd.Parameters.AddWithValue("@SupplierID", oldSupplier.SupplierId);
			cmd.Parameters.AddWithValue("@NewName", newSupplier.Name);          
			cmd.Parameters.AddWithValue("@NewPhoneNumber", newSupplier.PhoneNumber);
			cmd.Parameters.AddWithValue("@NewEmail", newSupplier.Email);

			cmd.Parameters.AddWithValue("@OldName", oldSupplier.Name);
			cmd.Parameters.AddWithValue("@OldPhoneNumber", oldSupplier.PhoneNumber);
			cmd.Parameters.AddWithValue("@OldEmail", oldSupplier.Email);

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
        /// Created: 2025/04/15
        /// Approver: 
        /// Inserts supplier.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="supplier"></param>
        /// <returns>True or False depending if the record was updated</returns>
        public int InsertSupplier(Supplier supplier)
		{
			int supplierId = 0;
			var conn = DBConnection.GetConnection();
			var cmd = new SqlCommand("sp_insert_supplier", conn);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("@name", supplier.Name);
			cmd.Parameters.AddWithValue("@PhoneNumber", supplier.PhoneNumber);
			cmd.Parameters.AddWithValue("@Email", supplier.Email);

			cmd.Parameters.AddWithValue("@AddressId", supplier.AddressID);

			try
			{
				conn.Open();
				supplierId = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
			}
			return supplierId;
		}
	}
	
}
