using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.DTOs;
using Portfolio.Api.Models;
using Portfolio.Api.Services;

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
                {
                    ProjectDesc = x.ProjectDesc,
                    ProjectName = x.ProjectName
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
    }
}