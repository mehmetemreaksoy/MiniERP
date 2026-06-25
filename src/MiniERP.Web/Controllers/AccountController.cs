using Microsoft.AspNetCore.Mvc;

namespace MiniERP.Web.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string username, string password)
    {
        if (username == "admin" && password == "Admin123")
        {
            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction("Index", "Home");
        }

        ViewBag.ErrorMessage = "Username or password is incorrect.";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
