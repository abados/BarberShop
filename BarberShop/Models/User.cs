using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Models
{
    public abstract class User
    {
        public User() {
            RND = 0;
        }
        [Key]
        public int ID { get; set; }
        //מספר נוסף המשתנה רנדומלית עם כל התחברות למערכת
        public int IDRandom { get; set; }

        [NotMapped]
        public int RND { 
            get 
           {
                return (ID * 100000) + IDRandom;
           } 
            set
           {
                Random random = new Random();
                IDRandom= random.Next(10000, 100000);
           } 
        }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Display(Name = "שם משפחה")]
        public string LastName { get; set; }

        [NotMapped,Display(Name = "שם מלא")]
        public string FullName { get { return FirstName+" "+LastName; } }

        [Display(Name = "מספר פלאפון")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage ="כתובת מייל תקינה"),Display(Name = "כתובת מייל")]
        public string Email { get; set; }

        [DataType(DataType.Password),Display(Name ="סיסמה")]
        public string Password { get; set; }

        [Display(Name = "פעיל")]
        public bool Active { get; set; } = true;

        [Display(Name = "פעיל"),NotMapped]
        public string isActive { get { if (Active) return "פעיל"; return "לא פעיל"; } } 

        //[NotMapped]
        //System.Timers.Timer timer = new System.Timers.Timer();  
    }
}
