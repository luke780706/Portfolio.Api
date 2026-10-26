namespace Portfolio.Api.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = String.Empty;
        public string ProjectDesc { get; set; } = String.Empty;
        public string GitHubUrl { get; set; } = String.Empty;
    }
}
