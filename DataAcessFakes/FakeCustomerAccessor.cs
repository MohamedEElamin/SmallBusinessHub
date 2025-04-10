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
	/// Creator: Your Name
	/// Created: yyyy/mm/dd
	/// Approver: 
	///
	/// Actual summary of the class
	/// (example: Class for the creation of User Objects with set data fields)
	/// </summary>
    public class FakeCustomerAccessor : ICustomerAccessor
    {
        private List<Customer> customers = null;

        public FakeCustomerAccessor()
        {
            customers = new List<Customer>()
            {
                new Customer()
                {
                    CustomerId = 1000,
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber ="3198376522",
                    Email = "mo@gmail.com",
                    Active = true
                },
                new Customer()
                {
                    CustomerId = 1001,
                    FirstName = "Mohmaed",
                    LastName = "Elamin",
                    PhoneNumber ="3198376522",
                    Email = "mo@gmail.com",
                    Active = false
                },
                 new Customer()
                 {
                    CustomerId = 1002,
                    FirstName = "Zac",
                    LastName = "Tim",
                    PhoneNumber ="3198376522",
                    Email = "mo@gmail.com",
                    Active = true
                 },
            };
        }

        public List<Customer> SelectCustomerByActive(bool active = true)
        {
            List<Customer> _customers;
            _customers = (from Customer in customers
                          where Customer.Active == active
                          select Customer).ToList();
            return _customers;
        }


        public int UpdateCustomer(Customer oldCustomer, Customer newCustomer)
        {
            int result = 0;


            if (customers.Contains(oldCustomer))
            {
                customers.Remove(oldCustomer);
                customers.Add(newCustomer);

            }
            if (customers.Contains(newCustomer)&& !customers.Contains(oldCustomer))
            {
                result = 1;
            }

            return result;
        }


        public int InsertCustomer(Customer customer)
        {
            int oldCount = customers.Count;
            customers.Add(customer);
            return customers.Count - oldCount;
        }



        public int DeactivateCustomerByID(int ID)
        {
            int rows = 0;
            foreach (var a in customers)
            {
                if (a.CustomerId == Convert.ToInt32(ID))
                {

                    rows = 1;
                }
            }
            return rows;
        }

  

        public Customer SelectCustomerById(int ID)
        {
            Customer _customer = new Customer();
            foreach (var customer in customers)
            {

                if (customer.CustomerId == Convert.ToInt32(ID))
                {
                    _customer = customer;
                    break;
                }
            }
            return _customer;
        }
    }
}

