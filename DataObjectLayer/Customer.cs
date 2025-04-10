//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace DataObjects
{
  
    public class Customer
    {
      
        public int CustomerId { get; set; }
     
        [Required(ErrorMessage = "Please enter first name")]

        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string  Email { get; set; }

        public bool Active { get; set; }

        public override string ToString()
        {
            return CustomerId + ": " + FirstName + " " + LastName;
        }
    }
}
