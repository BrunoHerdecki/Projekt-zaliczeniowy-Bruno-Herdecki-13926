using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Entity;
using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Models;
using System.Data;
using System.Security.Claims;

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Controllers
{
    [Authorize(Roles = Roles.CUSTOMER)]
    public class UserPanelController : Controller
    {
        public IActionResult UserPanel()
        {
            var userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            List<ReservedBookDto> listReserved = new();

            if (userIdString != null)
            {
                int.TryParse(userIdString.Value, out int userId);

                using var db = new LibraryContext();


                var reserved = db.HistoryOfReservation.Where(x => x.UserId == userId)
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
                                HistoryId= item.HistoryId,
                                Title = item.Books.Title,
                                Author = item.Books.Author,
                                IsBorrowing = item.Books.IsBorrowed && !item.HasReturned,
                                HasReturned = item.HasReturned,
                                ReservedDate = item.ReservedDate,
                                ExpectedReturnDate = item.ExpectedReturnDate,
                            });
                    }
                }
            }
            return View(listReserved);
        }

        public IActionResult Return(int Id)
        {
            using var db = new LibraryContext();
            var history = db.HistoryOfReservation.FirstOrDefault(x => x.HistoryId == Id);
            var books = db.Books.FirstOrDefault(x => x.BookId == history.BookId);

            history.HasReturned = true;

            books.IsBorrowed = false;

            db.SaveChanges();

            return View(nameof(UserPanel));
        }
    }
}
