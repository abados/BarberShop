using BarberShop.Models;
using BarberShop.ViewModelsHome;
using BarberShop.ViewModelsManager;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.Collections.Generic;
using System.Data.Entity;



namespace BarberShop.Controllers
{
    public class ManagerController : Controller
    {

       
        //פונקציה המוסיפה ספר
        public IActionResult ViewBarbers()
        {

            VMIsActiveBarber vm = new VMIsActiveBarber(DataLayer.Data.Users.ToList());

            return View(vm);
        }

        
        public IActionResult ChangeActive(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            List<User>users = DataLayer.Data.Users.ToList();

            Barber barber = users.OfType<Barber>().ToList().Find(User=> User.ID==id);
            
            if (barber == null) return RedirectToAction("Index");

            if(barber.Active) barber.Active=false;
            else barber.Active=true;

            DataLayer.Data.SaveChanges();

            VMIsActiveBarber vm = new VMIsActiveBarber(users);


            return View("ViewBarbers",vm);

        }

        public IActionResult CreateBarber()
        {
           
            return View(new VMCreateBarber());
        }

        [HttpPost, ValidateAntiForgeryToken]

        public IActionResult CreateBarber(VMCreateBarber vMCreateBarber)
        {

            Barber barber = new Barber { FirstName = vMCreateBarber.FirstName, LastName = vMCreateBarber.LastName, Email = vMCreateBarber.Email, Password = vMCreateBarber.Password, PhoneNumber = vMCreateBarber.PhoneNumber };
            DataLayer.Data.Users.Add(barber);

            DataLayer.Data.SaveChanges();

            return RedirectToAction("ViewBarbers");
        }
        //פונקציה המוסיפה פועלה 
        public IActionResult ViewAndCreateActions()
        {
            List<HaircutActions> Actions = DataLayer.Data.HaircutActions.ToList();
            
            return View(new VMActions { HaircutActions=Actions});
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddAction(VMActions VM)
        {
            if(VM.HaircutActions!=null)
            DataLayer.Data.HaircutActions.Add(VM.action);

            DataLayer.Data.SaveChanges();
            return RedirectToAction("ViewAndCreateActions");
        }

        public IActionResult ShowAllUsers()
        {
            List<User> users = DataLayer.Data.Users.ToList();
            return View(users);

        }

        public IActionResult MakeABarber(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            List<User> users = DataLayer.Data.Users.ToList();

            Client client = users.OfType<Client>().ToList().Find(User => User.ID == id);

            DataLayer.Data.Database.ExecuteSqlCommand("UPDATE BarberShop.dbo.Users SET Discriminator = 'Barber' WHERE ID = {0}", id);

            DataLayer.Data.SaveChanges();

            //VMIsActiveBarber vm = new VMIsActiveBarber(DataLayer.Data.Users.ToList());


            return View("index");

        }


        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                DataLayer.Data.GetUser = new Client { FirstName = "התחבר" };
                return RedirectToAction("Index", "Home");
            }

            User user = DataLayer.Data.Users.FirstOrDefault(u => u.IDRandom == id);
            if(user!=null)
                if(user is Manager) 
                {
                    DataLayer.Data.GetUser = user;
                    user.RND = 0; //generate new random id
                    DataLayer.Data.SaveChanges();
                    return View();
                }
            DataLayer.Data.GetUser = new Client { FirstName = "התחבר" };
            return RedirectToAction("Index", "Home");
        }
    }
}

