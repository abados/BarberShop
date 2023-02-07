using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
    public class HaircutActionsPerBarber
    {
         

        [Key]
        public int ID { get; set; }
        public Barber Barber { get; set; }
        public HaircutActions HaircutActions { get; set; }
        [Display(Name = "זמן פעולה בדקות")]
        public int ActionDuration { get; set; }
        [Display(Name = "מחיר פעולה")]
        public int Price { get; set; }
        [Display(Name = "אחוז יחסי לפעולות")]
        public int PercentFromTotalActions { get; set; }

    }
}
