using Portfolio.Api.DTOs;
using Portfolio.Api.Models;

namespace Portfolio.Api.Mappings
{
    public static class ProjectMapper
    {
        public static ProjectModel ToModel(UpdateProjectDto dto)
        {
            return new ProjectModel()
            {
                ProjectName = dto.ProjectName,
                ProjectDesc = dto.ProjectDesc,
                GitHubUrl = dto.GitHubUrl
            };
        }

        public static ProjectDto ToDto(ProjectModel model)
        {
            return new ProjectDto()
            {
                Id = model.Id,
                ProjectName = model.ProjectName,
                ProjectDesc = model.ProjectDesc,
                GitHubUrl = model.GitHubUrl
            };
        }
    }
}