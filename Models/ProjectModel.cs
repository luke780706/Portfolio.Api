namespace Portfolio.Api.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = String.Empty;
        public string ProjectDesc { get; set; } = String.Empty;
        public string GitHubUrl { get; set; } = String.Empty;
    }
}
