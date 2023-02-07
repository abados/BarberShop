using System;
using System.ComponentModel.DataAnnotations;


namespace BarberShop.Models
{
    public class Manager:User
    {
        [Display(Name = "תאריך תחילת המינוי"),DataType(DataType.Date)]
        public DateTime StartWorkingDate { get; set; }
    }
}
