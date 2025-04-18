
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectLayer
{
    public class Address
    {
       
        public int AddressId { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string Type { get; set; }
        public string ZipCode { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return LineOne;
        }
    }
}
