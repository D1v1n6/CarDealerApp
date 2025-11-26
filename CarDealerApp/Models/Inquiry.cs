using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerApp.Models
{
    public class Inquiry
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }

        public string? Message { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car? Car { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
