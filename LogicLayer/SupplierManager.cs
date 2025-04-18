using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjectLayer;

namespace LogicLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/15
    /// Approver: 
    /// This is the Supplier Manager Manager class.
    /// </summary>
    public class SupplierManager : ISupplierManager
    {
       ISupplierAccessor _supplierAccessor;

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        ///  Supplier Manager Constructor method
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        public SupplierManager()
        {
            _supplierAccessor = new SupplierAccessor();
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        ///  Supplier Manager Constructor method
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="supplierAccessor"></param>
        public SupplierManager(ISupplierAccessor supplierAccessor)
        {
            _supplierAccessor = supplierAccessor;
        }

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
        public bool DeactivateSupplier(Supplier supplier)
        {
            bool result = false;
            try
            {
                result = (_supplierAccessor.DeactivateSupplier(supplier.SupplierId) > 0);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Deactivation failed.", ex);
            }
            return result;
           
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
        public bool EditSupplier(Supplier supplier)
        {
            bool result = false;
            try
            {         
               Supplier oldSupplier = _supplierAccessor.SelectSupplierById(supplier.SupplierId);
                result = (1 == _supplierAccessor.UpdateSupplier(oldSupplier, supplier));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }
            return result;
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
        public List<Supplier> GetSupplierListByActive(bool active = true)
        {
            try
            {
                return _supplierAccessor.SelectSupplierByActive(active);
            }
            catch (Exception)
            {

                throw;
            }
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
        public bool InsertSupplier(Supplier supplier)
        {
            bool result = false;
            try
            {
               
                result = (_supplierAccessor.InsertSupplier(supplier) > 0);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
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
        public Supplier RetrieveSupplierById(int supplierId)
        {
    
            try
            {
                return  _supplierAccessor.SelectSupplierById(supplierId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

             
        }
    }
}
