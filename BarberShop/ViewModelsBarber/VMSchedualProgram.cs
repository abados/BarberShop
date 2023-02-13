using BarberShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace BarberShop.ViewModelsBarber
{
    public class VMSchedualProgram
    {
        public VMSchedualProgram() { }
        public Barber Barber { get; set; }
        public int BarberID { get; set; }

        [Display(Name ="תאריך התחלה"), DataType(DataType.Date)]
        public DateOnly startDate { get; set; }

        [Display(Name = "תאריך סיום"), DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }

        [Display(Name = "עבודה ביום ראשון")]
        public bool workOnSunday { get; set; }

        [Display(Name = "שעת התחלה"), DataType(DataType.Time)]
        public TimeOnly startSunday { get; set; } = new TimeOnly(9, 0);

        [Display(Name = "שעת סיום"), DataType(DataType.Time)]
        public TimeOnly EndSunday { get; set; }


        [Display(Name = "עבודה ביום שני")]
        public bool workOnMonday { get; set; }

        [Display(Name = "שעת התחלה"), DataType(DataType.Time)]
        public TimeOnly startMonday { get; set; } = new TimeOnly(9, 0);

        [Display(Name = "שעת סיום"), DataType(DataType.Time)]
        public TimeOnly EndMonday { get; set; }

        [Display(Name = "עבודה ביום שלישי")]
        public bool workOnTuesday { get; set; }

        [Display(Name = "שעת התחלה"), DataType(DataType.Time)]
        public TimeOnly startTuesday { get; set; } = new TimeOnly(9, 0);

        [Display(Name = "שעת סיום"), DataType(DataType.Time)]
        public TimeOnly EndTuesday { get; set; }

        [Display(Name = "עבודה ביום רביעי")]
        public bool workOnWednesday { get; set; }

        [Display(Name = "שעת התחלה"), DataType(DataType.Time)]
        public TimeOnly startWednesday { get; set; } = new TimeOnly(9, 0);

        [Display(Name = "שעת סיום"), DataType(DataType.Time)]
        public TimeOnly EndWednesday { get; set; }

        [Display(Name = "עבודה ביום חמישי")]
        public bool workOnThursday { get; set; }

        [Display(Name = "שעת התחלה"), DataType(DataType.Time)]
        public TimeOnly startThursday { get; set; } = new TimeOnly(9, 0);

        [Display(Name = "שעת סיום"), DataType(DataType.Time)]
        public TimeOnly EndThursday { get; set; }
        [Display(Name = "עבודה ביום שישי")]
        public bool workOnFriday { get; set; }

        [Display(Name = "שעת התחלה"), DataType(DataType.Time)]
        public TimeOnly startFriday { get; set; } = new TimeOnly(9, 0);

        [Display(Name = "שעת סיום"), DataType(DataType.Time)]
        public TimeOnly EndFriday { get; set; }
    
    }
}
