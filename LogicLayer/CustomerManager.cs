using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/04
    /// Approver: 
    /// Class for the Customer manage all 
    /// </summary>
    public class CustomerManager : ICustomerManager
    {
        ICustomerAccessor _customerAccessor;

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
        public CustomerManager()
        {
            _customerAccessor = new CustomerAccessor();
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
        public CustomerManager(ICustomerAccessor customerAccessor)
        {
            _customerAccessor = customerAccessor;
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
        public bool AddCustomer(Customer customer)
        {
            bool result = false;

            try
            {
                result = (_customerAccessor.InsertCustomer(customer) > 0);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add failed.", ex);
            }
            return result;
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
        /// <returns>True or false depending if the record was updated </returns>
        public bool DeactivateCustomerByID(int  ID)
        {

            try
            {
                return (1 == _customerAccessor.DeactivateCustomerByID(ID));
            }
            catch (Exception)
            {
                throw;
            }

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
        /// Updater: 
        /// Updated:  
        /// Update: 
        /// </remarks>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns>True or false depending if the record was edited </returns>
        public bool EditCustomer(Customer oldCustomer, Customer newCustomer)
        {
            bool result = false;

            try
            {
                result = (1 == _customerAccessor.UpdateCustomer(oldCustomer, newCustomer));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
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
        public List<Customer> GetCustomerListByActive(bool active)
        {
            try
            {
                return _customerAccessor.SelectCustomerByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("List Not Available", ex);
            }
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
        public Customer SelectCustomerByID(int ID)
        {

            try
            {
                return _customerAccessor.SelectCustomerById(ID);
            }
            catch (Exception)
            {

                throw;
            }

        }
    
    }
}