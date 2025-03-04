using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Repository.IRepository;
using Store.Models;

namespace OnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["success"] = "product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            Product product = _unitOfWork.Product.Get(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["success"] = "product updated Successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if(id==0 || id==null)
            {
                return NotFound();
            }

            Product product = _unitOfWork.Product.Get(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);

        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int ?id)
        {
            if (id == 0 || id == null) return NotFound();

            Product product = _unitOfWork.Product.Get(p => p.Id == id);
            if (product == null) return NotFound();

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "product removed successfully";
            return RedirectToAction("Index");
        }
    }
}
