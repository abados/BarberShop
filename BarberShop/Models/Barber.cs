using System.ComponentModel.DataAnnotations;

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


        //פונקציה המוסיפה פגישות יומיות
        public void addDailyApointment(DateTime start, DateTime end, out int modulu)
        {
            List<int> plan = new List<int>();
            DateTime time = start;
         
            
            int counter = 0;
            foreach (HaircutActionsPerBarber haircutActionsPer in HaircutList)
                
            {//לפי הכמות שהספר ציין לכל פעולה, מתווסף מקום ברשימה של התכנון
                plan.Add(haircutActionsPer.PercentFromTotalActions);
                counter += haircutActionsPer.PercentFromTotalActions;

            }
            //ריצה על כל המקומות ברשימה
            for(int i=0; i<plan.Count; i++)
            {
                //ריצה לפי סוגי הפעולות בכמות של כל אחד מהם
                for(int j = 0; j < plan[i];j++ )
                {
                    HaircutActionsPerBarber haircut = HaircutList[j];
                    for (DateTime current = time; current < end.AddMinutes(haircut.ActionDuration); current = current.AddMinutes(haircut.ActionDuration)) 
                    {
                        time = current;
                        //הוספת הפגישה בפועל
                        addApoitment(haircut, current);
                        counter--;
                    }
                }
            }
            modulu = counter;
        }

        //פונקציה המקבלת טווח תאריכים עם שעות של יום בשבוע
        public void addRangePerDaily(DateTime start, DateTime end, int DayOfTheWeek, TimeOnly timeStart, TimeOnly timeEnd)
        {
            int result = 0;
            for(DateTime day=start;day<=end;day=day.AddDays(1)) 
            {
                if((int)day.DayOfWeek== DayOfTheWeek) 
                {
                    addDailyApointment(new DateTime(day.Year, day.Month, day.Day, timeStart.Hour, timeStart.Minute, 0), new DateTime(day.Year, day.Month, day.Day, timeEnd.Hour, timeEnd.Minute, 0),out result);
                }
            }
        }
    }
}