using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Security.Cryptography;
using DataObjects;
using System.Collections;
using DataAccessLayerInterfaces;


namespace LogicLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2020/03/31
    /// Approver: 
    /// This is the User Manager class that IProductManager the User Manager interface.
    /// </summary>
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor;

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// No argument constructor 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// User Manager Constructor.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;         
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Retrieves User List By Active.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active"></param>
        /// <returns>users list</returns>
        public List<User> GetUserListByActive(bool active = true)
        {
            try
            {
                return _userAccessor.SelectUserByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("List Not Available", ex);
            }
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Updates user.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="user"></param>
        /// <returns> True or False depending if the record was updated</returns>
        public bool EditEmployee(User user)
        {
            bool result = false;

            try
            {
                User oldUser = _userAccessor.SelectEmployeeById(user.EmployeeId);
               
                result = (1 == _userAccessor.UpdateEmployee(oldUser, user));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Inserts a user.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="user"></param>
        /// <returns>True or False depending if the record was updated</returns>
        public bool AddEmployee(User user)
        {
            bool result = false;

            try
            {
                result = (_userAccessor.InsertEmployee(user) > 0);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add failed.", ex);
            }
            return result;

        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Deactivates user.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="user"></param>
        /// <returns>True or False depending if the record was updated</returns>
        public bool DeactivateEmployee(User user)
        {
            bool result = false;
            try
            {
                result = (_userAccessor.DeactivateEmployee(user.EmployeeId) > 0);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Deactivation failed.", ex);
            }
            return result;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Retrieves user By Id.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="user iD"></param>
        /// <returns>user</returns>
        public User SelectEmployeeById(int employeeId)
        {
            try
            {
                return _userAccessor.SelectEmployeeById(employeeId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
        }
    }
}