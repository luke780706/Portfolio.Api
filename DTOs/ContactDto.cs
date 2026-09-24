using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.DTOs
{
    public class ContactDto
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Surname { get; set; } = String.Empty;
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Phone number must contain exactly 6 digits.")]
        public string PhoneNumber { get; set; } = String.Empty;
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
    }
}
