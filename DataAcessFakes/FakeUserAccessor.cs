using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using DataAccessLayer;

namespace DataAccessFakes
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/03/31
    /// Approver: 
    /// This class for creation a fake users data which will used 
    /// for testing Logic layer methods.
    /// </summary>
    public class FakeUserAccessor : IUserAccessor
    {
        private User _user;
        private List<User> users = null;

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// This is a Constructor method which has fake Fake Employee list. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        public FakeUserAccessor()
        {
              users = new List<User>()
                {
                    new User()
                    {
                        EmployeeId = 10,
                        FirstName = "Mohamed",
                        LastName = "Elamin",
                        PhoneNumber = "3197637822",
                        Email = "mojj@gmail.com",
                        Active = true,
                    },
                    new User()
                    {
                        EmployeeId = 100,
                        FirstName = "Mohamed",
                        LastName = "Elamjin",
                        PhoneNumber = "3197637822",
                        Email = "mo@gail.com",
                        Active = true,
                    },
                    new User()
                    {
                        EmployeeId = 100,
                        FirstName = "Mohamed",
                        LastName = "Elamin",
                        PhoneNumber = "3197637822",
                        Email = "mo@gmail.com",
                        Active = true,
                    },
              };
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Select EmployeeId by ID. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="employeeId"></param>
        /// <returns>user</returns>
        public User SelectEmployeeById(int employeeId)
        {
            User _user = new User();
            foreach (var user in users)
            {
                if (user.EmployeeId == employeeId)
                {
                    _user = user;
                    break;
                }
            }
            return _user;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Select EmployeeId by Active status. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active = true"></param>
        /// <returns>Employees list</returns>
        public List<User> SelectUserByActive(bool active = true)
        {
            List<User> _user;
            _user = (from User in users
                     where User.Active == active
                     select User).ToList();
            return _user;
        }



        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Inserts Product into the Product table. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="oldUser"></param>
        /// <param name="newUser"></param>
        /// <returns>One or zero depending if the record was updated</returns>
        public int UpdateEmployee(User oldUser, User newUser)
        {
            return 1;
        }


        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// get an user by Email. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="email"></param>        
        /// <returns>user</returns>
        public User getUserByEmail(string email)
        {
            if (email.Equals("j.doe@RandoGuy.com"))
            {
                _user = new User()
                {
                    EmployeeId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "j.doe@RandoGuy.com",
                    PhoneNumber = "5632102101",
                    Active = true,
                };
                return _user;
            }
            else
            {
                throw new ApplicationException("User not found");
            }
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Deactivates Employee. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="employeeId"></param>        
        /// <returns>user</returns>
        public int DeactivateEmployee(int employeeId)
        {
            return (from e in users
                    where e.EmployeeId == employeeId
                    select e).Count();
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/03/31
        /// Approver: 
        /// Inserts Employee. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="user"></param>        
        /// <returns>user</returns>
        public int InsertEmployee(User user)
        {
            users.Add(user);
            return 1;
        }

    }
}
