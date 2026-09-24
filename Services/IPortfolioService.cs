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
    }
}
