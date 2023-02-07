using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BarberShop.Models
{
    public class Apointment
    {
        public Apointment() { }

        [Key]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public Client Client { get; set; }

        public HaircutActionsPerBarber Haircut { get; set; }

        //פונקציה המציגה לספר שמות הלקוחות
        [NotMapped,Display(Name ="פגישות")]

        public string NameClient { get { if (Client == null) return "תור פנוי"; return Client.FullName; } }

        //פונקציה המציגה ללקוחות פגישות פנויות
        [NotMapped,Display(Name = "פגישות")]

        public string StatusApoitment { get { if (Client == null) return "פנויה"; return "תפוסה"; } }
    }
}
