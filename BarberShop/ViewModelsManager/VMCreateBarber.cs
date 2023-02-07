using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BarberShop.ViewModelsManager
{
    public class VMCreateBarber
    {
        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Display(Name = "שם משפחה")]
        public string LastName { get; set; }


        [Display(Name = "מספר פלאפון")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "כתובת מייל תקינה"), Display(Name = "כתובת מייל")]
        public string Email { get; set; }

        [DataType(DataType.Password), Display(Name = "סיסמה")]
        public string Password { get; set; }

        [Display(Name = "הוספת תמונה")]
        public IFormFile FileItem { get; set; }
    }
}
