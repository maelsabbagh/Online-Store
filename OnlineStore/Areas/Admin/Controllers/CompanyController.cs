using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Models.ViewModels;
using Store.Utility;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            var productList = _unitOfWork.Company.GetAll();

            return View(productList);
        }
        //update/insert
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0) // create
            {

                return View(new Company());
            }
            else // update
            {
                Company company = _unitOfWork.Company.Get(p => p.Id == id);
                return View(company);

            }
        }


        // id present => update
        // no id present // create
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                bool isCreate = true;
                
                if (company.Id == 0)// add
                {
                    _unitOfWork.Company.Add(company);
                }
                else // update
                {
                    isCreate = false;
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                if (isCreate)
                    TempData["success"] = "Company created successfully";
                else TempData["success"] = "Company updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(company);
            }

        }

        
        #region API calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Company.GetAll();
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(p => p.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
           


              
                _unitOfWork.Company.Remove(companyToBeDeleted);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Deleted Successfully" });
            
        }
        #endregion

    }
}
