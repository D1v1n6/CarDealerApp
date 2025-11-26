using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers
{
    public class InquiryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InquiryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Submit(Inquiry inquiry)
        {
            _context.Inquiries.Add(inquiry);
            _context.SaveChanges();
            TempData["Message"] = "Inquiry submitted successfully!";
            return RedirectToAction("Index", "Cars", new { id = inquiry.CarId });
        }

        public IActionResult AdminList()
        {
            // Require admin login
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
                return RedirectToAction("Login", "Admin");

            var inquiries = _context.Inquiries
                .OrderByDescending(i => i.CreatedAt)   // newest first
                .ToList();

            return View(inquiries);
        }

    }
}
