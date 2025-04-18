using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjectLayer;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjectLayer;



namespace MVCPresentationLayer.Controllers
{

    public class InventoryController : Controller
    {
        private IProductManager _productManager;
         
        public InventoryController()
        {
                _productManager = new ProductManager();
        }

        // GET: Inventory
        public ActionResult Index()
        {
            ViewBag.Title = "Products";
            var products = _productManager.GetProductListByActive();
            return View(products);
            
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Product Details";
            var product = _productManager.RetrieveProductById(id);

            return View(product);
        }



        // GET: Inventory/Create
    
        public ActionResult Create()
        {
            ViewBag.Title = "Add Product ";          
            return View();
           
        }

        // POST: Inventory/Create
        [HttpPost]
       
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    _productManager.InsertProduct(product);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        // GET: Inventory/Edit/5
 
        public ActionResult Edit(int id)
        {
            Product product = _productManager.RetrieveProductById(id);
            
           
            return View(product);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        public ActionResult Edit(int  id, Product product)
        {
            try
            {
                _productManager.EditProduct(product);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = _productManager.RetrieveProductById(id);

            return View(product);

        }

        // POST: Inventory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Product product = _productManager.RetrieveProductById(id);
                var p = _productManager.DeactivateProduct(product);
                ViewBag.Title = "Delete Product";

                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
