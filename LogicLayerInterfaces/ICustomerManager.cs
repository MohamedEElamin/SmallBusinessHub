using DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/04
    /// Approver: 
    /// This is the Customer Manager interface.
    /// </summary>
    public interface ICustomerManager
   {
        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver: 
        /// Retrieves Customer List By Active.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active"></param>
        /// <returns>Customers list</returns>
        List<Customer> GetCustomerListByActive(bool active);

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver: 
        /// Edits Customer.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="newCustomer"></param>
        /// <param name="oldCustomer"></param>
        /// <returns>True or False depending if the record was updated</returns>
        bool EditCustomer(Customer oldCustomer, Customer newCustomer);

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver: 
        /// Add a Customer
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="customer"></param>
        /// <returns>True or False depending if the record was updated</returns>
        bool AddCustomer(Customer customer);

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver: 
        /// Deactivates Product.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="ID"></param>
        /// <returns>True or False depending if the record was updated</returns>
        bool DeactivateCustomerByID(int ID);

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/04
        /// Approver: 
        /// Deactivates Product.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="ID"></param>
        /// <returns>Customer</returns>
        Customer SelectCustomerByID(int ID);


    }
}
