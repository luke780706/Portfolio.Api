using Portfolio.Api.Entities;
using Portfolio.Api.Models;

namespace Portfolio.Api.Services
{
    public interface IPortfolioService
    {
        Task<ProfileModel?> GetProfileAsync();
        Task<List<ProjectModel>> GetProjectsAsync();        
        Task<List<ExperienceModel>> GetExperiencesAsync();
        Task<List<SkillModel>> GetSkillsAsync();
        Task<ContactModel?> GetContactAsync();
        Task<Project> AddProjectAsync(ProjectModel projectModel);
        Task<Project?> GetProjectByIdAsync(int id);
        Task<ProjectModel?> UpdateProjectAsync(int id, ProjectModel projectModel);
        Task<bool> DeleteProjectAsync(int id);
    }
}
