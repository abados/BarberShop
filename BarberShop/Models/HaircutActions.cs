using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
    public class HaircutActions
    {
        public HaircutActions() {
            PerBarbers = new List<HaircutActionsPerBarber>();
        }
        [Key]
        public int ID { get; set; }

        [Display(Name ="סוג פעולה")]
        public string Name { get; set; }

        [Display(Name = "תיאור פעולה")]
        public string Description { get; set; }
        [Display(Name = "תמונה")]
        public byte[] Image { get; set; }


        //רשימה של כל הספרים שעושים את הפעולה
        public List<HaircutActionsPerBarber> PerBarbers { get; set; }

        public IFormFile SetImage { set { Image = new SetImage(value).Image; } }
    }
}
