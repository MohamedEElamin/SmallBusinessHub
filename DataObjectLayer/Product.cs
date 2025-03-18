//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectLayer
{
    public class Product
    {
        //[HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        //[Range(0.01, double.MaxValue, ErrorMessage =
            //"Please enter a positive price")]
        public decimal Cost { get; set; }

        public String Description { get; set; }

        //[Required]
        public DateTime DateReceived { get; set; }

        //[Required]
        public String ProductType { get; set; }

        //[Required]
        public String ManufacturerName { get; set; }

        //[HiddenInput(DisplayValue = false)]
        public int SupplierId { get; set; }

        //[Range(0.01, double.MaxValue, ErrorMessage =
            //"Please enter a positive price")]
        public decimal Price { get; set; }

        //[Required]
        public String PurchaseUnit { get; set; }

        //[Required]
        public String SaleUnit { get; set; }

        //[Required]
        public int Qoh { get; set; }

        //[Required]
        public int ReorderLevel { get; set; }

        public bool Active { get; set; }

       
    }
}
