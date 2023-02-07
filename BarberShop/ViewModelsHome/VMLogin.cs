using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BarberShop.ViewModelsHome
{
    public class VMLogin
    {

        public VMLogin()
        {
        }

        [Display(Name = "Please Insert EMail")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [Display(Name = "Please Insert Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }
        public string Color { get; internal set; }
    }
}


