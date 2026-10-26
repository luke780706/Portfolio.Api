using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.DTOs
{
    public class UpdateProjectDto
    {
        [Required]
        public string ProjectName { get; set; } = String.Empty;
        [Required]
        public string ProjectDesc { get; set; } = String.Empty;
        [Required]
        public string GitHubUrl { get; set; } = String.Empty;
    }
}
