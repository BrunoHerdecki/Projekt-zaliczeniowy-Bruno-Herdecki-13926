using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Entity
{
    public class LibraryContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<HistoryOfReservation> HistoryOfReservation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=booking.db");

        public void DefaultData()
        {
            try
            {
                RemoveDate();
                using var db = new LibraryContext();

                db.Add(
                    new Users()
                    {
                        Email = "test@gmail.com",
                        Name = "Bruno",
                        IsAdmin = true,
                        Surrname = "Random",
                        Password = "123",
                        Pesel = "12332112332",
                        UserId = 1
                    });

                db.Add(
                    new Users()
                    {
                        Email = "testmail@gmail.com",
                        Name = "Julia",
                        IsAdmin = false,
                        Surrname = "Notrandom",
                        Password = "123",
                        Pesel = "32132132132",
                        UserId = 2
                    });

                db.Add(
                   new Books()
                   {
                       BookId = 1,
                       Title = "Fajny Tytul ksiazki",
                       Author = "Ktos Tam Nie Wiem Kto",
                       IsBorrowed = true,
                   });

                db.Add(
                   new Books()
                   {
                       BookId = 2,
                       Title = "Niefajny Tytul ksiazki",
                       Author = "Nie wiem kto",
                       IsBorrowed = false,
                   });

                db.Add(
                  new HistoryOfReservation()
                  {
                      HistoryId = 1,
                      BookId = 1,
                      UserId = 2,
                      ReservedDate = DateTime.Now,
                      ExpectedReturnDate = DateTime.Parse("2022/12/02"),
                      //ExpectedReturnDate = DateTime.Now.AddDays(14),
                  });
                
                db.Add(
                  new HistoryOfReservation()
                  {
                      HistoryId = 2,
                      BookId = 2,
                      UserId = 2,
                      ReservedDate = DateTime.Parse("2023/02/02"),
                      ExpectedReturnDate = DateTime.Parse("2023/12/02"),
                      HasReturned = true,
                  });

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void RemoveDate()
        {
            using var db = new LibraryContext();
            foreach (var item in db.Users)
            {
                db.Remove(item);
            }
            foreach (var item in db.Books)
            {
                db.Remove(item);
            }
            foreach (var item in db.HistoryOfReservation)
            {
                db.Remove(item);
            }
            db.SaveChanges();

        }
    }
}
