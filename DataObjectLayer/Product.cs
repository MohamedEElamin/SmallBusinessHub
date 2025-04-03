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
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/03/31
    /// Approver: 
    /// This class for the Product object data.  
    /// </summary>
    public class Product
    { 
        public int ProductId { get; set; }
        public decimal Cost { get; set; }
        public String Description { get; set; }
        public DateTime DateReceived { get; set; }
        public String ProductType { get; set; }        
        public String ManufacturerName { get; set; }
        public int SupplierId { get; set; }
        public decimal Price { get; set; }
        public String PurchaseUnit { get; set; }
        public String SaleUnit { get; set; }
        public int Qoh { get; set; }
        public int ReorderLevel { get; set; }
        public bool Active { get; set; }

       
    }
}
