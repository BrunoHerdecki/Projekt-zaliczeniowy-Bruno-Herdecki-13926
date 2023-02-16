using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Models;
using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Entity;
using System.Security.Claims;

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var db = new LibraryContext();

            List<BookDto> books = new();

            var listOfBooks = db.Books.Where(x => !x.IsBorrowed);

            if (listOfBooks.Any())
            {
                foreach (var item in listOfBooks)
                {
                    books.Add(
                        new BookDto()
                        {
                            BookId = item.BookId,
                            Title = item.Title,
                            Author = item.Author,
                            IsBorrowed = item.IsBorrowed,
                        });
                }
            }

            return View(books);
        }



        [Authorize(Roles = Roles.CUSTOMER)]
        public IActionResult Borrow(int Id)
        {
            using var db = new LibraryContext();
            var boo = db.Books.FirstOrDefault(x => x.BookId == Id);

            var res = new HistoryOfReservation()
            {
                BookId = Id,
                Title = boo.Title,
                Author = boo.Author,
                ReservedDate = DateTime.Now,
                ExpectedReturnDate = DateTime.Now,
            };

            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(HistoryOfReservation book)
        {
            var userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIdString == null)
            {
                return Index();
            }

            int userId = Int32.Parse(userIdString.Value);
            if (userId <= 0)
            {
                return View(nameof(Index));
            }
            book.UserId = userId;

            using var db = new LibraryContext();

            var bookToReserve = db.Books.FirstOrDefault(x => x.BookId == book.BookId);

            if (bookToReserve != null && !bookToReserve.IsBorrowed)
            {
                bookToReserve.IsBorrowed = true;
                await db.AddAsync(book);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}