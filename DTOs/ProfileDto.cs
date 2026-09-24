using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.DTOs
{
    public class ProfileDto
    {
        [Required]
        public string ProfileDescription { get; set; } = String.Empty;
    }
}
