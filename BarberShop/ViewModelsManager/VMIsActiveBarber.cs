using BarberShop.Models;

namespace BarberShop.ViewModelsManager
{
    public class VMIsActiveBarber
    {
        public VMIsActiveBarber(List<User> Barbers) 
        {

            ActiveBarbers = Barbers.OfType<Barber>().Where(b => b.Active).ToList();
            NotActiveBarbers = Barbers.OfType<Barber>().Where(b => !b.Active).ToList();

        }
        //רשימה של הספרים הפעילים
        public List<Barber> ActiveBarbers { get; set; }

        //רשימה של הספרים הלא הפעילים
        public List<Barber> NotActiveBarbers { get; set; }
    }
}
