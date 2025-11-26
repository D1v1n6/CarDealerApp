using CarDealerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiriesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InquiriesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SubmitInquiry(Inquiry inquiry)
        {
            _context.Inquiries.Add(inquiry);
            _context.SaveChanges();
            return Ok(inquiry);
        }
    }
}
