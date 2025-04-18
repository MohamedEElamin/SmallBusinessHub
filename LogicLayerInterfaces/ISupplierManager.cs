
using DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/15
    /// Approver: 
    /// This is the Supplier Manager interface.
    /// </summary>
    public interface ISupplierManager
    {
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
        List<Supplier> GetSupplierListByActive(bool active = true);

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
        bool EditSupplier(Supplier supplier);

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
        bool DeactivateSupplier(Supplier supplier);


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
        bool InsertSupplier(Supplier supplier);

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
        Supplier RetrieveSupplierById(int supplierId);
    }
    
}
