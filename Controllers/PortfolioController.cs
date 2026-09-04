using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetProfile()
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