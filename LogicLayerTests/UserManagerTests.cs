using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataObjects;
using LogicLayer;
using DataAccessLayer;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        private IUserAccessor _fakeUserAccessor;
        private UserManager _userManager;

        [TestInitialize]
        public void TestSetup()
        {
            _fakeUserAccessor = new FakeUserAccessor();
            _userManager = new UserManager(_fakeUserAccessor);
        }
        [TestMethod]
        public void TestSelectUserByActive()
        {
            // Arrang
            List<User> SelectUserByActive;
            // Act
            SelectUserByActive = _userManager.GetUserListByActive(true);
            // Assert
            Assert.AreEqual(3, SelectUserByActive.Count);
        }

        [TestMethod]
        public void TestSelectEmployeeById()
        {
            // Arrange
            User user = null;
            const int UserID = 10;
            int result = 0;
            // Act
            user = _userManager.SelectEmployeeById(UserID);
            if (user != null)
            {
                result = 1;
            }
            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestDeactivateEmployee()
        {
            // Arrange
            bool expectedResult = false;
            User _user = new User()
            {
                EmployeeId = 1002,
                FirstName = "Zac",
                LastName = "Tim",
                PhoneNumber = "3198376522",
                Email = "mo@gmail.com",
                Active = true
            };
            IUserManager userManager = new UserManager(_fakeUserAccessor);
            // Act
            var actualResult = userManager.DeactivateEmployee(_user);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
