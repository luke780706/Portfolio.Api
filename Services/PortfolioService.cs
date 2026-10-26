using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.DTOs;
using Portfolio.Api.Entities;
using Portfolio.Api.Models;

namespace Portfolio.Api.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;
        public PortfolioService(AppDbContext appDbContext, ILogger<PortfolioService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<ProfileModel?> GetProfileAsync()
        {
            Profile? profile = await _appDbContext.Profiles.FirstOrDefaultAsync();

            if (profile == null)
            {
                _logger.LogWarning("Profile was not found");
                return null;
            }

            ProfileModel profileModel = new()
            {
                ProfileDescription = profile.ProfileDescription
            };

            return profileModel;
        }

        public async Task<List<ProjectModel>> GetProjectsAsync()
        {
            return await _appDbContext.Projects
                .Select(x =>
                 new ProjectModel()
                 {
                     Id = x.Id,
                     ProjectDesc = x.ProjectDesc,
                     ProjectName = x.ProjectName,
                     GitHubUrl = x.GitHubUrl
                 }).ToListAsync();
        }

        public async Task<List<ExperienceModel>> GetExperiencesAsync()
        {
            return await _appDbContext.Experiences
                .Select(x =>
                new ExperienceModel()
                {
                    ExperienceDesc = x.ExperienceDesc,
                    ExperienceName = x.ExperienceName
                })
                .ToListAsync();
        }

        public async Task<List<SkillModel>> GetSkillsAsync()
        {
            return await _appDbContext.Skills
                                            .Select(x =>
                                               new SkillModel()
                                               {
                                                   SkillDesc = x.SkillDesc,
                                                   SkillName = x.SkillName
                                               }).ToListAsync();
        }

        public async Task<ContactModel?> GetContactAsync()
        {
            var contact = await _appDbContext.Contacts.FirstOrDefaultAsync();

            if (contact == null)
            {
                _logger.LogWarning("Contact was not found");
                return null;
            }

            ContactModel contactModells = new()
            {
                Email = contact.Email,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Surname = contact.Surname
            };

            return contactModells;
        }

        public async Task<Project> AddProjectAsync(ProjectModel projectModel)
        {
            Project newProject = new()
            {
                ProjectName = projectModel.ProjectName,
                ProjectDesc = projectModel.ProjectDesc,
                GitHubUrl = projectModel.GitHubUrl
            };

            _appDbContext.Projects.Add(newProject);

            await _appDbContext.SaveChangesAsync();

            return newProject;
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _appDbContext.Projects
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ProjectModel?> UpdateProjectAsync(int id, ProjectModel projectModel)
        {
            Project? result = await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                _logger.LogWarning("Project could not be found");
                return null;
            }

            result.ProjectName = projectModel.ProjectName;
            result.ProjectDesc = projectModel.ProjectDesc;
            result.GitHubUrl = projectModel.GitHubUrl;

            await _appDbContext.SaveChangesAsync();

            return new ProjectModel()
            {
                Id = result.Id,
                ProjectName = result.ProjectName,
                ProjectDesc = result.ProjectDesc,
                GitHubUrl = result.GitHubUrl
            };
        }
        public async Task<bool> DeleteProjectAsync(int id)
        {
            var deleted = await _appDbContext.Projects
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return deleted > 0;
        }
    }
}
