using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private const string AdminUser = "admin";
    private const string AdminPass = "password123"; // you can move this to appsettings.json

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string username, string password)
    {
        if (username == AdminUser && password == AdminPass)
        {
            HttpContext.Session.SetString("AdminLoggedIn", "true");
            return RedirectToAction("Create", "Cars");
        }

        ViewBag.Error = "Invalid login";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("AdminLoggedIn");
        return RedirectToAction("Login");
    }
}
