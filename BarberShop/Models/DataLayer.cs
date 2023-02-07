using System.Data.Entity;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BarberShop.Models
{
    public class DataLayer: DbContext
    {
        public User GetUser = new Client { FirstName = "התחבר" };
        private static DataLayer data;
        private DataLayer() : base("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BarberShop;Data Source=localhost\\SQLEXPRESS")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataLayer>());

            //כאשר מסד הנתונים ריק, נפעיל את הפונקציה הזורעת
            if (Users.Count() == 0) seed();
        }

        public static DataLayer Data
        {
            get

            {
                if (data == null) data = new DataLayer();
                return data;
            }
        }

        private void seed()
        {
            //מנהל המערכת
            Manager manager = new Manager { FirstName = "יוסי", LastName="החותך",Email="hay@walla.com",Password="1234",PhoneNumber="05444",StartWorkingDate= DateTime.Now };
            Users.Add(manager);

            Barber barber = new Barber { FirstName = "אבי", LastName = "הספר", Email = "hayBar@walla.com", Password = "5678", PhoneNumber = "058333"};
            Users.Add(barber);

            SaveChanges();
        }


        public List<User> AllUsers
        {
            get
            {
                List<User> Users = DataLayer.data.Users.ToList();
                return Users;
            }
        }
        //פונקציה המחזירה רק ספרים עם כל הפעולות שלהם
        public List<Barber> getBarbersAllIncludes
        {

            get
            {
                HaircutActions.Include(h=>h.PerBarbers).ToList();
                return Users.OfType<Barber>().Include(b => b.HaircutList).Include(b => b.Apointments).ToList();
            }
        }

        public DbSet<User> Users { get; set; }
        //public DbSet<Barber> Barbers { get; set; }
        //public DbSet<Manager> Managers { get; set; }
        public DbSet<HaircutActions> HaircutActions { get; set; }
        public DbSet<HaircutActionsPerBarber> HaircutActionsPerBarber { get; set; }

        public DbSet<Apointment> Apointments { get; set; }

    }
}
