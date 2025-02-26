using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Data;
using Store.Models;

namespace OnlineStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var categoriesList = _db.Categories.ToList();
            return View(categoriesList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null) return NotFound();

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "category Edited successfully";

                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category? category = _db.Categories.Find(id);
            if (category == null) return NotFound();


            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? category=_db.Categories.Find(id);
            if (category == null) return NotFound();
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "category Deleted successfully";

            return RedirectToAction("Index");

            
           
        }
    }
}
