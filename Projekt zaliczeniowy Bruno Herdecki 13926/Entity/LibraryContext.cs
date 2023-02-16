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
                        Email = "admin@admin.com",
                        Name = "Nikolaas",
                        IsAdmin = true,
                        Surrname = "Delphinus",
                        Password = "Password1",
                        Pesel = "12345678901",
                        UserId = 1
                    });

                db.Add(
                    new Users()
                    {
                        Email = "test@test.com",
                        Name = "LimAngela",
                        IsAdmin = false,
                        Surrname = "Arianna",
                        Password = "Password1",
                        Pesel = "32132132132",
                        UserId = 2
                    });

                db.Add(
                    new Users()
                    {
                        Email = "PleioneFiachra@gmail.com",
                        Name = "Pleione",
                        IsAdmin = false,
                        Surrname = "Fiachra",
                        Password = "Password1",
                        Pesel = "22233311224",
                        UserId = 3
                    });

                db.Add(
                    new Users()
                    {
                        Email = "Setare@gmail.com",
                        Name = "Gerry",
                        IsAdmin = false,
                        Surrname = "Setare",
                        Password = "Password1",
                        Pesel = "33441178900",
                        UserId = 4
                    });

                db.Add(
                    new Users()
                    {
                        Email = "LimHraban@gmail.com",
                        Name = "Lim",
                        IsAdmin = false,
                        Surrname = "Hraban",
                        Password = "Password1",
                        Pesel = "32132132132",
                        UserId = 5
                    });

                db.Add(
                    new Users()
                    {
                        Email = "LimBrycen@gmail.com",
                        Name = "Lim",
                        IsAdmin = false,
                        Surrname = "Brycen",
                        Password = "Password1",
                        Pesel = "32132199132",
                        UserId = 6
                    });

                db.Add(
                   new Books()
                   {
                       BookId = 1,
                       Title = "Blacksmith Of Destruction",
                       Author = "Filomena Prakash",
                       IsBorrowed = true,
                   });

                db.Add(
                   new Books()
                   {
                       BookId = 2,
                       Title = "Blacksmith Of Destruction",
                       Author = "Filomena Prakash",
                       IsBorrowed = false,
                   });

                db.Add(
                   new Books()
                   {
                       BookId = 3,
                       Title = "Owls Of The Frontline",
                       Author = "Helena Simba",
                       IsBorrowed = true,
                   });

                db.Add(
                   new Books()
                   {
                       BookId = 4,
                       Title = "Soldiers Of The Void",
                       Author = "Helena Simba",
                       IsBorrowed = false,
                   });

                db.Add(
                   new Books()
                   {
                       BookId = 5,
                       Title = "Assassins And Spiders",
                       Author = "Ceadda Godofredo",
                       IsBorrowed = false,
                   });

                db.Add(
                   new Books()
                   {
                       BookId = 6,
                       Title = "Tree Without A Conscience",
                       Author = "Finn Houston",
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
                
                db.Add(
                  new HistoryOfReservation()
                  {
                      HistoryId = 3,
                      BookId = 3,
                      UserId = 2,
                      ReservedDate = DateTime.Parse("2022/10/02"),
                      ExpectedReturnDate = DateTime.Parse("2023/01/01"),
                      HasReturned = false,
                  });

                db.Add(
                  new HistoryOfReservation()
                  {
                      HistoryId = 4,
                      BookId = 4,
                      UserId = 3,
                      ReservedDate = DateTime.Parse("2023/02/02"),
                      ExpectedReturnDate = DateTime.Parse("2023/12/02"),
                      HasReturned = true,
                  });
                
                db.Add(
                  new HistoryOfReservation()
                  {
                      HistoryId = 5,
                      BookId = 5,
                      UserId = 2,
                      ReservedDate = DateTime.Now,
                      ExpectedReturnDate = DateTime.Parse("2022/12/02"),
                      HasReturned = true,
                  });

                db.Add(
                  new HistoryOfReservation()
                  {
                      HistoryId = 6,
                      BookId = 2,
                      UserId = 2,
                      ReservedDate = DateTime.Parse("2021/02/02"),
                      ExpectedReturnDate = DateTime.Parse("2021/10/10"),
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
