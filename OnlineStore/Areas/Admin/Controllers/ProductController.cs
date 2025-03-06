using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Models.ViewModels;

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
            IEnumerable<SelectListItem> categories = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            //ViewBag.categoryList = categories;
            ProductVM productVm = new()
            {
                CategoryList = categories,
                Product = new Product()
            };
            return View(productVm);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList= _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
               
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
