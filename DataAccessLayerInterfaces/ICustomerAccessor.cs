using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/04
    /// Approver: 
    /// Class for the Customer Accessor. 
    /// </summary>
    public interface ICustomerAccessor
    {
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
        /// <param name="bool active"></param>
        /// <returns>customer list</returns>
        List<Customer> SelectCustomerByActive(bool active);

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
        int UpdateCustomer(Customer oldCustomer, Customer newCustomer);

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
        int InsertCustomer(Customer customer);

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
        /// <returns>True or false depending if the record was updated </returns>
        int DeactivateCustomerByID(int  ID);


        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2020/04/24
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
        Customer SelectCustomerById(int ID);

    }
}
