using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace MVCPresentationLayer.Controllers
{
  
    public class EmployeeController : Controller
    {
        private IUserManager _userManager;

        public EmployeeController()
        {
                _userManager = new UserManager();
        }
        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.Title = "Employees";
            var employees = _userManager.GetUserListByActive();
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Employee Details";
            var employee = _userManager.SelectEmployeeById(id);

            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add Employee ";
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    _userManager.AddEmployee(user);
                    return RedirectToAction("Index");

                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            User user = _userManager.SelectEmployeeById(id);


            return View(user);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                _userManager.EditEmployee(user);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            User user = _userManager.SelectEmployeeById(id);
          

            return View(user);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                User user = _userManager.SelectEmployeeById(id);
                var u = _userManager.DeactivateEmployee(user);
                ViewBag.Title = "Delete Employee";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
