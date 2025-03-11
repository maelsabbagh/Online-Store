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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category");
            
            return View(productList);
        }
        //update/insert
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()

            };
           
            
            if(id==null || id==0) // create
            {

                return View(productVM);
            }
            else // update
            {
                productVM.Product = _unitOfWork.Product.Get(p=>p.Id==id);
            }
            return View(productVM);
        }
        

        // id present => update
        // no id present // create
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!=null) // end point received image
                {
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // check if this object has a file already then it is update
                    // we need to delete the already existed image
                    // then add the new one

                    string oldImagePath = productVM.Product.ImageUrl;
                    if(!string.IsNullOrEmpty(oldImagePath)) //  image saved before
                    {
                        // we have a new image and an old image
                        // delete old image

                        var oldImageFullPath = Path.Combine(wwwRootPath, oldImagePath.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImageFullPath))
                        {
                            System.IO.File.Delete(oldImageFullPath);
                        }

                        
                    }
                    // Add a new one
                    using (var fileStream=new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                bool isCreate = false;

                if (productVM.Product.Id == 0)// add
                {
                    _unitOfWork.Product.Add(productVM.Product);
                    isCreate = true;
                } 
                else // update
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                    _unitOfWork.Save();
                if(isCreate)
                TempData["success"] = "product created successfully";
                else TempData["success"] = "product updated successfully";
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

        //public IActionResult Edit(int? id)
        //{
        //    if (id == 0 || id == null) return NotFound();
        //    Product product = _unitOfWork.Product.Get(p => p.Id == id);

        //    if (product == null) return NotFound();

        //    return View(product);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "product updated Successfully";
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

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
        #region API calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return Json(new { data = productList });
        }
        #endregion
    }
}
