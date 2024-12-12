using DEMOProject.Models;
using DEMOProject.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DEMOProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly APPDBContext _context;

        public ProductController()
        {
            _context = new APPDBContext();
        }

        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var products = _context.Products.Include(p => p.Category)
                                            .OrderBy(p => p.ProductId)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            var totalRecords = _context.Products.Count();
            ViewBag.TotalPages = Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            return View(products);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, string categoryName)
        {
            if (ModelState.IsValid)
            {
                // Check if the category exists based on the entered category name
                var category = _context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);

                if (category == null)
                {
                    ModelState.AddModelError("CategoryName", "Invalid Category Name. Please enter a valid category.");
                    return View(product);
                }

                // Assign the CategoryId from the found category
                product.CategoryId = category.CategoryId;

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return HttpNotFound();


            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {

            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }




    }

}