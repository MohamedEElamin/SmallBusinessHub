using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataObjectLayer
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/03/31
    /// Approver: 
    /// This class for the user object data.  
    /// </summary>
    public class User
    {          
            public int EmployeeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }     
            public string PhoneNumber { get; set; }
            public bool Active { get; set; }
            public List<string> Roles { get; set; }
    }
}

