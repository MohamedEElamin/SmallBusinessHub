
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectLayer
{
    public class Supplier
    {
      
        public int SupplierId { get; set; }


        [Required(ErrorMessage = "Please enter first name")]

        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }
        public Address Address { get; set; }

        public int AddressID { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
