using DataObjectLayer;
using System.Collections;
using System.Collections.Generic;

namespace LogicLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2020/03/31
    /// Approver: 
    /// This is the User Manager interface.
    /// </summary>
    public interface IUserManager
    {

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Retrieves users list by active.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active"></param>
        /// <returns>User list</returns>
        List<User> GetUserListByActive(bool active = true);

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
        /// <returns>True or False depending if the record was updated</returns>
        bool EditEmployee(User user);

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Inserts User.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="user"></param>
        /// <returns>True or False depending if the record was updated</returns>
        bool AddEmployee(User user);

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Deactivates a user.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="user"></param>
        /// <returns>True or False depending if the record was updated</returns>
        bool DeactivateEmployee(User user);

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
        /// <param name="userId"></param>
        /// <returns>user</returns>
        User SelectEmployeeById(int employeeId);
    }
}