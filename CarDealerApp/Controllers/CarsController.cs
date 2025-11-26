using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cars = _context.Cars.OrderByDescending(c => c.CreatedAt).ToList();
            return View(cars);
        }

        public IActionResult Details(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null) return NotFound();
            return View(car);
        }

        // Admin: Add Car
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
                return RedirectToAction("Login", "Admin");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car, IFormFile? imageFile)
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            return RedirectToAction("Login", "Admin");


            if (imageFile != null)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string filePath = Path.Combine("wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                car.ImagePath = "/images/" + fileName;
            }

            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
