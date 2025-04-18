using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjectLayer;
using DataAccessLayerInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/15
    /// Approver: 
    /// This class for creation a fake spplier data which will used 
    /// for testing Logic layer methods.
    /// </summary>
    public class FakeSupplierAccessor : ISupplierAccessor
    {
        private List<Supplier> suppliers = null;

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// This is a Constructor method which has fake Fake supplier list. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        public FakeSupplierAccessor()
        {
            suppliers = new List<Supplier>()
            {
                new Supplier ()
                {
                    SupplierId = 90,
                    Name = "LG",
                    PhoneNumber = "3196378822",
                    Email = "Lg@lg.com",
                    AddressID = 100,
                    Active = true,
                },
                new Supplier ()
                {
                    SupplierId = 1000,
                    Name = "LG",
                    PhoneNumber = "3196378822",
                    Email = "Lg@lg.com",
                    AddressID = 100,
                    Active = true,
                },
                new Supplier ()
                {
                    SupplierId = 1002,
                    Name = "KO",
                    PhoneNumber = "319637832",
                    Email = "Lg@lk.com",
                    AddressID = 120,
                    Active = true,
                },
            };
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// Select supplier by ID. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="supplierId"></param>
        /// <returns>supplier</returns>
        public Supplier SelectSupplierById(int supplierId)
        {
            Supplier _supplier = new Supplier();
            foreach (var supplier in suppliers)
            {
                if (supplier.SupplierId == supplierId)
                {
                    _supplier = supplier;
                    break;
                }
            }
            return _supplier;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// Select product by Active status. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="active = true"></param>
        /// <returns>suppliers list</returns>
        public List<Supplier> SelectSupplierByActive(bool active = true)
        {
            List<Supplier> _suppliers;
            _suppliers = (from Supplier in suppliers
                          where Supplier.Active == active
                          select Supplier).ToList();
            return _suppliers;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// Deactivates Product.
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="supplierId"></param>
        /// <returns>One or zero depending if the record was updated</returns>
        public int DeactivateSupplier(int supplierId)
        {
            return (from e in suppliers
                    where e.SupplierId == supplierId
                    select e).Count();
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// Inserts Product into the Product table. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="supplier"></param>
        /// <returns>One or zero depending if the record was updated</returns>
        public int InsertSupplier(Supplier supplier)
        {
            suppliers.Add(supplier);
            return 1;
        }

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver: 
        /// Updates Product into the Product table. 
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// Update: ()
        /// </remarks>
        /// <param name="oldSupplier"></param>
        /// <param name="newSupplier"></param>
        /// <returns>One or zero depending if the record was updated</returns>
        public int UpdateSupplier(Supplier oldSupplier, Supplier newSupplier)
        {
            return 1;
        }
    }
}
