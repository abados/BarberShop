using BarberShop.Models;
using BarberShop.Services;
using BarberShop.ViewModelsBarber;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
    public class BarberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelectAction()
        {
            List<HaircutActions> actions = DataLayer.Data.HaircutActions.ToList();
            return View( new VMSelectAction { HaircutOptionalActions= actions, Barber=(Barber)DataLayer.Data.GetUser, BarberID= DataLayer.Data.GetUser.ID});
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SelectAction(VMSelectAction VM)
        {
            HaircutActionsPerBarber haircutActionsPerBarber = new HaircutActionsPerBarber();
            haircutActionsPerBarber.HaircutActions = DataLayer.Data.HaircutActions.FirstOrDefault(g => g.ID == VM.HaircutActionID);

            if (haircutActionsPerBarber != null && VM != null) 
            {
                haircutActionsPerBarber.Barber = DataLayer.Data.Users.OfType<Barber>().ToList().FirstOrDefault(g => g.ID == VM.BarberID);
                haircutActionsPerBarber.ActionDuration = VM.ActionDuration;
                haircutActionsPerBarber.Price= VM.Price;
                haircutActionsPerBarber.PercentFromTotalActions= VM.PercentFromTotalActions;
                haircutActionsPerBarber.Barber.HaircutList.Add(haircutActionsPerBarber);
                DataLayer.Data.HaircutActionsPerBarber.Add(haircutActionsPerBarber);
                DataLayer.Data.SaveChanges();

            }

            return RedirectToAction("Index","home");

        }

        public IActionResult MyActions(int? id)
        {
           
            Barber barber = DataLayer.Data.getBarbersAllIncludes.Find(b => b.ID == id);

            List<HaircutActionsPerBarber> myActions = barber.HaircutList;


            return View(myActions);
        }

        //פונקצית הוספת תוכנית תאריכית
        public IActionResult addProgram(int? id)
        {
            if (id == null) return RedirectToAction("index", "home");
            User user = DataLayer.Data.Users.ToList().Find(u => u.RND == id);
            if(user==null) return RedirectToAction("index", "home");
            user.RND = 0;
            DataLayer.Data.SaveChanges();
            DataLayer.Data.GetUser = user;
            VMSchedualProgram vMSchedual = new VMSchedualProgram
            {
                Barber = (Barber)user,
                BarberID = user.RND
            };
            return View(vMSchedual);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult addProgram(VMSchedualProgram VM)
        {
            if (VM == null) return RedirectToAction("index", "home");
            new BarberService { VM = VM }.PlanProgram();
            DataLayer.Data.SaveChanges();
            return RedirectToAction("Calender", new { id =VM.BarberID });


        }

        public IActionResult Calender(int? id)
        {
            Barber barber = DataLayer.Data.getBarbersAllIncludes.Find(u => u.RND == id);
            
            return View(new VMCalender { Apoints = barber.Apointments});
        }
    }
}
