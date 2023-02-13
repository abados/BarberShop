using BarberShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BarberShop.ViewModelsBarber
{
    public class VMCalender
    {
        public VMCalender() 
        {

        }

        //פגישות של 10 ימים קדימה
        //כל טור מכיל תאריך עם הפגישות של אותו היום
        //כל פגישה עם ארבעה מאפיינים
        //1.מספר רץ
        //2. שעה-זמן
        //3. סוג פעולה
        //4.אם יש לקוח-שם לקוח ואם אין לקוח - פנוי
        //5.אם יש לקוח צבע הפגישה בירוק אם אין לקוח צבע הפגישה באדום

        private List<Apointment> _appointments;

        public List<Apointment> Apoints { get { return _appointments.ToList(); } 
            set { _appointments = value.FindAll(a => a.DateTime >= DateTime.Now && a.DateTime.Date <= DateTime.Now.AddDays(10).Date); }
        }

        //רשימה של כל התורים מחולקת למערכים של כל יום
        public List<Apointment[]> DailyAppointment
        {
            get
            {
                //בניית רשימה של מערכים לשליחה בפונקציה
                List<Apointment[]> MyDaily = new List<Apointment[]>();
                //ריצה על כל הימים שבטווח של הפגישות
                for(DateTime i= Apoints.First().DateTime.Date;i<=Apoints.Last().DateTime.Date;i.AddDays(1))
                {
                    //יצירת מערך יומי ובדיקה האם יש פגישות באותו היום  
                    Apointment[] temp = Apoints.FindAll(a => a.DateTime.Date == i.Date).ToArray();
                    if(temp.Length > 0) 
                    {
                        MyDaily.Add(temp);

                    }
                }
                return MyDaily;
                
            }
        }



    }

    
}
