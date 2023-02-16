using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Entity;
using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Models;

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel modelLogin)
        {

            using var db = new LibraryContext();

            var user = db.Users.FirstOrDefault(x => !x.IsRemoved && x.Email == modelLogin.Email && x.Password == modelLogin.Password);

            if (user != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? Roles.ADMIN : Roles.CUSTOMER),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }



            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        public IActionResult Register()
        {
            Users user = new Users();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Users user)
        {
            using var db = new LibraryContext();

            if (!db.Users.Any(x => x.Email == user.Email))
            {
                await db.AddAsync(user);
                await db.SaveChangesAsync();
            }

            return View(nameof(Login));
        }

    }
}

