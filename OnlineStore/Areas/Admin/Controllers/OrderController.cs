using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Store.Models.ViewModels;
using Store.Utility;
using Stripe.Climate;
using System.Diagnostics;
using System.Security.Claims;

namespace OnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        [BindProperty]
        public OrderVM orderVM { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string status)
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            orderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(o => o.Id == orderId, includeProperties: nameof(ApplicationUser)),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(o => o.OrderHeader.Id == orderId, includeProperties: nameof(Store.Models.Product))
            };
            return View(orderVM);
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult UpdateOrderDetails()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = orderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = orderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.StreetAddress = orderVM.OrderHeader.StreetAddress;
            orderHeaderFromDb.City = orderVM.OrderHeader.City;
            if (!string.IsNullOrEmpty(orderVM.OrderHeader.Carrier))
            {
                orderHeaderFromDb.Carrier = orderVM.OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(orderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFromDb.TrackingNumber = orderVM.OrderHeader.TrackingNumber;
            }
            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Order Details Updated Successfully.";


            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
        }
        #region API calls
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaders;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: nameof(ApplicationUser)).ToList();

            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(o => o.ApplicationUserId == userId, includeProperties: nameof(ApplicationUser));
            }
                switch (status)
                {
                    case "pending":
                        objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusPending);
                        break;
                    case "inprocess":
                        objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.StatusInProcess);
                        break;
                    case "completed":
                        objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.StatusShipped);
                        break;
                    case "approved":
                        objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.StatusApproved);
                        break;
                    default:
                        break;

                }
            return Json(new { data = objOrderHeaders });
        }
        #endregion
    }
}
