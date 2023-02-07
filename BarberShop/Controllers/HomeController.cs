using BarberShop.Models;
using BarberShop.ViewModelsHome;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;




namespace BarberShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DataLayer dataLayer = DataLayer.Data;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LogIn(int? id)
        {
            if(id == null)DataLayer.Data.GetUser = new Client { FirstName = "התחבר" };
            return View(new VMLogin());
           
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult LogIn(VMLogin logIn)
        {
            User user = DataLayer.Data.Users.FirstOrDefault(u => u.Email == logIn.Email && u.Password==logIn.Password);
            if (user == null)
            {
                logIn.Message = "Invalid Email or Password";
                logIn.Color = "red";
                return View(logIn);
            }
            DataLayer.Data.GetUser = user;

            if(user is Manager)
            {
                logIn.Message = "Welcome Manager " + user.FirstName;
                logIn.Color = "green";
                return View(logIn);
            }
            logIn.Message = "Welcome " + user.FirstName;
            logIn.Color = "green";
            return View(logIn);
        }

       
       
    }
}