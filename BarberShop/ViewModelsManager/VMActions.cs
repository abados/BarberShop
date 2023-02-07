using BarberShop.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BarberShop.ViewModelsManager
{
    public class VMActions
    {
        public VMActions() {
            HaircutActions = new List<HaircutActions>();
            action = new HaircutActions();
        }
        public List<HaircutActions> HaircutActions { get; set; }

        public HaircutActions action { get; set; }

        [Display(Name = "הוספת תמונה")]
        public IFormFile FileItem { get; set; }
    }
}
