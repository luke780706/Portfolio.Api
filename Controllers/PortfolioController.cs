using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.DTOs;
using Portfolio.Api.Entities;
using Portfolio.Api.Models;
using Portfolio.Api.Services;
using Portfolio.Api.Mappings;

namespace Portfolio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<ProfileDto>> GetProfile()
        {
            ProfileModel? profile = await _portfolioService.GetProfileAsync();

            if (profile == null)
            {
                return NotFound();
            }

            ProfileDto result = new()
            {
                ProfileDescription = profile.ProfileDescription
            };

            return Ok(result);
        }

        [HttpGet("projects")]
        public async Task<ActionResult<List<ProjectDto>>> GetProjects()
        {
            var projects = await _portfolioService.GetProjectsAsync();

            List<ProjectDto> result = [.. projects
                .Select(x => new ProjectDto()
                {   Id = x.Id,
                    ProjectDesc = x.ProjectDesc,
                    ProjectName = x.ProjectName,
                    GitHubUrl = x.GitHubUrl
                })];

            return Ok(result);
        }

        [HttpGet("experience")]
        public async Task<ActionResult<List<ExperienceDto>>> GetExperience()
        {
            var experiences = await _portfolioService.GetExperiencesAsync();

            List<ExperienceDto> result = [.. experiences.Select(x=> new ExperienceDto()
            {
                ExperienceDesc = x.ExperienceDesc,
                ExperienceName = x.ExperienceName
            })];

            return Ok(result);
        }

        [HttpGet("skills")]
        public async Task<ActionResult<List<SkillDto>>> GetSkills()
        {
            var skills = await _portfolioService.GetSkillsAsync();

            List<SkillDto> result = [.. skills
                .Select(x => new SkillDto()
                {
                    SkillDesc = x.SkillDesc,
                    SkillName = x.SkillName
                })];

            return Ok(result);
        }

        [HttpGet("contact")]
        public async Task<ActionResult<ContactDto>> GetContact()
        {
            ContactModel? contactData = await _portfolioService.GetContactAsync();

            if (contactData == null)
            {
                return NotFound();
            }

            ContactDto result = new()
            {
                Email = contactData.Email,
                PhoneNumber = contactData.PhoneNumber,
                Name = contactData.Name,
                Surname = contactData.Surname
            };

            return Ok(result);
        }
        [HttpPost("projects")]
        public async Task<ActionResult<ProjectDto>> AddProject(CreateProjectDto createProjectDto)
        {
            ProjectModel newProjectModel = new()
            {
                ProjectName = createProjectDto.ProjectName,
                ProjectDesc = createProjectDto.ProjectDesc,
                GitHubUrl = createProjectDto.GitHubUrl
            };

            Project result = await _portfolioService.AddProjectAsync(newProjectModel);

            ProjectDto projectDto = new()
            {
                Id = result.Id,
                ProjectName = result.ProjectName,
                ProjectDesc = result.ProjectDesc,
                GitHubUrl = result.GitHubUrl
            };

            return CreatedAtAction(nameof(GetProjectById), new { projectDto.Id }, projectDto);
        }

        [HttpGet("project/{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            var result = await _portfolioService.GetProjectByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            ProjectDto project = new()
            {
                Id = result.Id,
                ProjectName = result.ProjectName,
                ProjectDesc = result.ProjectDesc,
                GitHubUrl = result.GitHubUrl
            };

            return Ok(project);

        }
        [HttpPut("updateproject/{id}")]
        public async Task<ActionResult<ProjectDto>> UpdateProject(int id, UpdateProjectDto updateProjectDto)
        {
            var projectModel = ProjectMapper.ToModel(updateProjectDto);

            var result = await _portfolioService.UpdateProjectAsync(id, projectModel);

            if (result == null)
            {
                return NotFound();
            }

            ProjectDto projectDto = ProjectMapper.ToDto(result);

            return Ok(projectDto);
        }
        [HttpDelete("deleteproject/{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var result = await _portfolioService.DeleteProjectAsync(id);

            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}