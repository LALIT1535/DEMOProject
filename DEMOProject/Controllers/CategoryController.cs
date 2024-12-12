using DEMOProject.DBContext;
using DEMOProject.Models;
using System.Web.Mvc;

public class CategoryController : Controller
{
    private readonly APPDBContext _context;

    public CategoryController()
    {
        _context = new APPDBContext();
    }

    // GET: Category/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Category/Create
    [HttpPost]
    public ActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");  // Redirect to the Product list after category creation
        }
        return View(category);
    }
}
