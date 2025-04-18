using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjectLayer;

namespace MVCPresentationLayer.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        private ISupplierManager _supplierManager;

        public SupplierController()
        {
            _supplierManager = new SupplierManager();

        }
        public ActionResult Index()
        {
            ViewBag.Title = "Suppliers";
            var suppliers = _supplierManager.GetSupplierListByActive(true);
            return View(suppliers);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Supplier Details";
            var supplier = _supplierManager.RetrieveSupplierById(id);
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add Supplier ";
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    _supplierManager.InsertSupplier(supplier);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            Supplier supplier = _supplierManager.RetrieveSupplierById(id);


            return View(supplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Supplier supplier)
        {
            try
            {
                // TODO: Add update logic here
                _supplierManager.EditSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            Supplier supplier = _supplierManager.RetrieveSupplierById(id);


            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Supplier supplier = _supplierManager.RetrieveSupplierById(id);
                var s = _supplierManager.DeactivateSupplier(supplier);
                ViewBag.Title = "Delete Supplier";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
