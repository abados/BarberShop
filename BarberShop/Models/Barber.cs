using BarberShop.Services;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace BarberShop.Models
{
    public class Barber:User
    {
        public Barber() {
            HaircutList = new List<HaircutActionsPerBarber>();
        }
        [Display(Name ="תמונה")]
        public byte[] Image { get; set; }


        [Display(Name ="תיאור הספר")]   
        public string Discription { get; set; }
        public List<HaircutActionsPerBarber> HaircutList { get; set; }

        //פונקציה המוסיפה פעולה
        public void AddHaircutAction(string action,int price, int duration, int percent)
        {
            HaircutActions haircutActions = DataLayer.Data.HaircutActions.ToList().FirstOrDefault(a => a.Name== action);
            if(haircutActions!= null)
            {
                HaircutActionsPerBarber haircutActionsPerBarber = new HaircutActionsPerBarber
                {
                    HaircutActions = haircutActions,
                    Barber = this,
                    ActionDuration = duration,
                    Price = price,
                    PercentFromTotalActions = percent
                };
                //הוספה לרשימה של הספר
                HaircutList.Add(haircutActionsPerBarber);
                //הוספה לרשימה של הפעולות כמישהו שמבצע את הפעולה
                haircutActions.PerBarbers.Add(haircutActionsPerBarber);
            }
            return;
        }
        public IFormFile SetImage { set { Image = new SetImage(value).Image; } }


        [Display(Name = "רשימת פגישות")]
        public List<Apointment> Apointments { get; set; }

        //פונקציה המוסיפה פגישה
        public Apointment addApoitment(HaircutActionsPerBarber haircut, DateTime dateTime)
        {
            Apointment apointment = new Apointment {DateTime=dateTime, Haircut=haircut};
            Apointments.Add(apointment);
            return apointment;
        }

        //פונקציה המוסיפה פגישה לפי שם פעולה
        public Apointment addApoitment(string hairCut, DateTime dateTime)
        {
            HaircutActionsPerBarber haircutPerBarber = HaircutList.Find(h=>h.HaircutActions.Name== hairCut);
            return addApoitment(haircutPerBarber, dateTime);
        }

        //פונקציה המוסיפה פגישות לפי כמה ימים בשבוע
        public void AddWeeklyAppoitments(DateTime start, DateTime end, List<MyDay> days)
        {
            int counter = 0;
            for (DateTime i = start; i <= end; i=i.AddDays(1))
            {
                MyDay myDay = days.Find(d => d.Day == ((int)i.DayOfWeek));
                if (myDay != null)
                {
                    // Add the appointment for this day

                    //יצירת משתנה של תאריך ושעה של אותו יום התחלה וסוף 
                    DateTime dayStart = new DateTime(i.Year, i.Month, i.Day, myDay.start.Hour, myDay.start.Minute, 0);
                    DateTime dayEnd = new DateTime(i.Year, i.Month, i.Day, myDay.end.Hour, myDay.end.Minute, 0);

                    //שליחה לפונקציה היומית להוספת הפגישה
                    addDailyApointment(dayStart, dayEnd, counter, out counter);    
                    
                }

            }
        }

        //פונקציה המחשבת את השארית ומודיעה היכן צריך להתחיל
        

        //החזרת מחזוריות

        private int SumPlan
        {
            get
            {
                return Plan.Count;
            }
        }

        //החזרת רשימה של כמות כל אחת מהפעולות

        private List<HaircutActionsPerBarber> Plan 
        {
            get
            {
                List<HaircutActionsPerBarber> temp = new List<HaircutActionsPerBarber>();

                foreach (HaircutActionsPerBarber haircutActionsPer in HaircutList)

                {//לפי הכמות שהספר ציין לכל פעולה, מתווסף מקום ברשימה של התכנון

                    for (int i = 0;i< haircutActionsPer.PercentFromTotalActions;i++)
                    {
                        temp.Add(haircutActionsPer);
                    }
                    
                    
                    
                }
                return temp;
            }
        
        }



        //פונקציה המוסיפה פגישות יומיות
        public void addDailyApointment(DateTime start, DateTime end, int counter, out int modulu)
        {
            //אם המונה מגיע 0 אז מעדכנים לפי מספר הפעולות של הספר
            if (counter == 0) counter = SumPlan;

            //שעון יומי
            DateTime currenTime = start;
            int duration = 0;

            do
            {
                //הוספת פגישה לפי סוג הפגישה בזמן
                //חישוב כל הפגישות פחות השארית
                addApoitment(Plan[SumPlan - counter], currenTime);
                //הוספת הזמן לשעון היומי לפי משך הפגישה שנקבעה
                currenTime=currenTime.AddMinutes(Plan[SumPlan - counter].ActionDuration);
                
                //הפחתת השארית
                counter--;
                //אם השארית הגיעה ל-0 : טעינה מחדש לפי סך הפגישות
                if (counter == 0) counter = SumPlan;
                //הוצאת זמן הפגישה הבאה למשתנה חיצוני
                duration = Plan[SumPlan - counter ].ActionDuration;


                //רץ על עוד יש זמן לפגישה נוספת
            } while (currenTime < end.AddMinutes(-duration));

            modulu = counter;

        }

        //פונקציה המקבלת טווח תאריכים עם שעות של יום בשבוע
        //public void addRangePerDaily(DateTime start, DateTime end, int DayOfTheWeek, TimeOnly timeStart, TimeOnly timeEnd)
        //{
        //    int result = 0;
        //    for(DateTime day=start;day<=end;day=day.AddDays(1)) 
        //    {
        //        if((int)day.DayOfWeek== DayOfTheWeek) 
        //        {
        //            addDailyApointment(new DateTime(day.Year, day.Month, day.Day, timeStart.Hour, timeStart.Minute, 0), new DateTime(day.Year, day.Month, day.Day, timeEnd.Hour, timeEnd.Minute, 0),out result);
        //        }
        //    }
        //}
    }
}