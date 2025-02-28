using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using Store.Models;

namespace OnlineStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            var categoriesList = _categoryRepo.GetAll();
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
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["success"] = "category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category categoryFromDb = _categoryRepo.Get(c => c.Id == id);
            if (categoryFromDb == null) return NotFound();

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData["success"] = "category Edited successfully";

                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category? category = _categoryRepo.Get(c => c.Id == id);
            if (category == null) return NotFound();


            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? category=_categoryRepo.Get(c => c.Id == id);
            if (category == null) return NotFound();
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData["success"] = "category Deleted successfully";

            return RedirectToAction("Index");

            
           
        }
    }
}
