using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Entity;
using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Models;

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Controllers
{
    public class AdminPanelController : Controller
    {
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult AdminPanel(string searchString)
        {
            using var db = new LibraryContext();

            List<UserDto> users = new();

            List<Books> books = new();

            foreach (var item in db.Users)
            {
                users.Add(
                    new UserDto()
                    {
                        Email = item.Email,
                        Name = item.Name,
                        Surrname = item.Surrname,
                        UserId = item.UserId,
                        Pesel = item.Pesel,
                        Role = item.IsAdmin ? Roles.ADMIN : Roles.CUSTOMER
                    });
            }

            foreach (var item in db.Books)
            {
                books.Add(
                    new Books()
                    {
                        BookId = item.BookId,
                        Title = item.Title,
                        Author = item.Author,
                        IsBorrowed = item.IsBorrowed
                    });
            }

            AdminPanelModel adminPanelModel = new AdminPanelModel() { Books = books, Users = users };

            return View(adminPanelModel);
        }

        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult Borrowed(int id)
        {
            using var db = new LibraryContext();

            List<ReservedBookDto> listReserved = new();

            var reserved = db.HistoryOfReservation.Where(x => x.UserId == id)
                .Include(inc => inc.Books)
                .Include(inc => inc.Users);


            if (reserved.Any())
            {
                foreach (var item in reserved)
                {
                    listReserved.Add(
                        new ReservedBookDto()
                        {
                            UserId = item.UserId,
                            BookId = item.BookId,
                            Title = item.Books.Title,
                            Author = item.Books.Author,
                            IsBorrowing = item.Books.IsBorrowed && !item.HasReturned,
                            HasReturned = item.HasReturned,
                            ReservedDate = item.ReservedDate,
                            ExpectedReturnDate = item.ExpectedReturnDate,
                        });
                }
            }

            return View(listReserved);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewBook([Bind("Id,Title,Author")] Books book)
        {
            using var db = new LibraryContext();

            if (ModelState.IsValid)
            {
                db.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(AdminPanel));
            }
            return View(book);
        }

        public IActionResult AddNewBook()
        {
            return View();
        }
    }
}
