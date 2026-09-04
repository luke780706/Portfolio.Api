using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.Services
{
    public class PortfolioService : IPortfolioService
    {
        public async Task<ActionResult> GetProfileAsync()
        {

        }

        [HttpGet("projects")]
        public async Task<ActionResult> GetProjects()
        {

        }

        [HttpGet("experience")]
        public async Task<ActionResult> GetExperience()
        {

        }

        [HttpGet("skills")]
        public async Task<ActionResult> GetSkills()
        {

        }

        [HttpGet("contact")]
        public async Task<ActionResult> GetContact()
        {

        }
    }
}
