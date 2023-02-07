using BarberShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BarberShop.ViewModelsBarber
{
    public class VMSelectAction
    {
        [Display(Name = "בחר פעולה")]
        public List<HaircutActions> HaircutOptionalActions { get; set; }

        public Barber Barber { get; set; }
        public int BarberID { get; set; }
        public HaircutActions HaircutAction { get; set; }

        public int HaircutActionID { get; set; }
        [Display(Name = "זמן פעולה בדקות")]
        public int ActionDuration { get; set; }
        [Display(Name = "מחיר פעולה")]
        public int Price { get; set; }
        [Display(Name = "אחוז יחסי לפעולות")]
        public int PercentFromTotalActions { get; set; }
        public List<HaircutActionsPerBarber> MyHaircutActions { get; set; }
    }
}
