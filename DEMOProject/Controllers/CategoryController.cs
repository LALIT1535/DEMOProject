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
    public class CategoryController : Controller
    {
            private readonly APPDBContext _context;

            public CategoryController()
            {
                _context = new APPDBContext();
            }

            public ActionResult Index()
            {
                var categories = _context.Categories.ToList();
                return View(categories);
            }
            [HttpGet]
            public ActionResult Create() => View();

            [HttpPost]
            public ActionResult Create(Category category)
            {
                if (ModelState.IsValid)
                {
                    _context.Categories.Add(category);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            [HttpGet]
            public ActionResult Edit(int id)
            {
                var category = _context.Categories.Find(id);
                if (category == null) return HttpNotFound();
                return View(category);
            }

            [HttpPost]
            public ActionResult Edit(Category category)
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(category).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(category);
            }

         
            [HttpGet]
            public ActionResult Delete(int id)
            {
                var category = _context.Categories.Find(id);
                if (category == null) return HttpNotFound();
                return View(category);
            }

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(int id)
            {
                var category = _context.Categories.Find(id);
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
}