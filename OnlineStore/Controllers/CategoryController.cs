using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;

namespace OnlineStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db=db;   
        }
        public IActionResult Index()
        {
            var categoriesList = _db.Categories.ToList();
            return View(categoriesList);
        }
    }
}
