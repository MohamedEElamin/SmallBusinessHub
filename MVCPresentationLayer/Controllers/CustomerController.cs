using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayerInterfaces;
using LogicLayer;
using DataObjectLayer;

namespace MVCPresentationLayer.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerManager _customerManager;


        public CustomerController()
        {
            _customerManager = new CustomerManager();
        }
        // GET: Customer
        public ActionResult Index()
        {
            ViewBag.Title = "Customers";
            IEnumerable<Customer> customers = _customerManager.GetCustomerListByActive(true);
            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Customer Details";
            var customer = _customerManager.SelectCustomerByID(id);

            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.Titel = "Add Customer";
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    _customerManager.AddCustomer(customer);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = _customerManager.SelectCustomerByID(id);


            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                Customer oldCustomer = _customerManager.SelectCustomerByID(id);
                _customerManager.EditCustomer(oldCustomer, customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = _customerManager.SelectCustomerByID(id);


            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Customer customer = _customerManager.SelectCustomerByID(id);
                var p = _customerManager.DeactivateCustomerByID(id);
                ViewBag.Title = "Delete Customer";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
