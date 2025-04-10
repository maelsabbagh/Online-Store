using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Repository.IRepository;
using Store.Models;

namespace OnlineStore.Areas.Customer.Controllers;

[Area("Customer")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var products = _unitOfWork.Product.GetAll(includeProperties:"Category");
        return View(products);
    }

    public IActionResult Details(int? id)
    {
        if (id == 0 || id == null) return NotFound();
        ShoppingCart cart = new()
        {
            product = _unitOfWork.Product.Get(p => p.Id == id, includeProperties: "Category"),
            Count = 1,
            ProductId = (int)id,
            
        };
        return View(cart);
    }
    [HttpPost]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        shoppingCart.ApplicationUserId = userId;

        ShoppingCart cartFromDB = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == userId && c.ProductId == shoppingCart.ProductId);

        if(cartFromDB!=null) // it present
        {
            cartFromDB.Count += shoppingCart.Count;
            _unitOfWork.ShoppingCart.Update(cartFromDB);
        }
        else // add a new one
        {
            // Ensure the primary key value is default (0) for new records 
            // Model binding tries to bind id with the same value of product id
            shoppingCart.Id = 0;
            _unitOfWork.ShoppingCart.Add(shoppingCart);
        }
        TempData["success"] = "Cart updated successfully";

          
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
