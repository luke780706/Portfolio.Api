using Microsoft.EntityFrameworkCore;

namespace Portfolio.Api.Data
{
    public class DbInitializer
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(AppDbContext appDbContext, ILogger<DbInitializer> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task SendDataToDb()
        {
            if (await _appDbContext.Profiles.AnyAsync())
            {
                return;
            }

            _appDbContext.Contacts.Add(new Entities.Contact
            {
                Name = "Jan",
                Surname = "Kowalski",
                Email = "jan.kowalski@example.com",
                PhoneNumber = "+48 600 000 000"
            });

            _appDbContext.Experiences.AddRange(
                new Entities.Experience
                {
                    ExperienceName = "Senior Software Developer – Example Software\r\n2022–Present",
                    ExperienceDesc = "Development and maintenance of enterprise applications using C#/.NET, SQL Server and Entity Framework.\r\nWorking with business requirements and translating them into practical technical solutions.\r\nAnalysing applications, dependencies and the potential impact of requested changes."
                },
                new Entities.Experience
                {
                    ExperienceName = "Software Developer – Example Solutions\r\n2018–2022",
                    ExperienceDesc = "Development of business applications using C#/.NET and SQL Server.\r\nImplementation of REST APIs and database solutions.\r\nCooperation with business stakeholders and technical teams."
                },
                new Entities.Experience
                {
                    ExperienceName = "Junior Software Developer – Example Company\r\n2015–2018",
                    ExperienceDesc = "Development and maintenance of internal business applications.\r\nWorking with SQL databases and application support.\r\nImplementation of new functionality and automated business processes."
                });

            _appDbContext.Profiles.Add(new Entities.Profile
            {
                ProfileDescription = "Software developer focused on C#/.NET, ASP.NET Core, REST APIs and SQL Server. Experienced in developing and maintaining business applications, working with databases and translating business requirements into practical technical solutions."
            });

            _appDbContext.Projects.AddRange(
                new Entities.Project
                {
                    ProjectName = "Portfolio API",
                    ProjectDesc = "ASP.NET Core Web API with Entity Framework Core and SQL Server.",
                    GitHubUrl = "https://github.com/luke780706/Portfolio.Api"
                },
                new Entities.Project
                {
                    ProjectName = "SkeletonApi",
                    ProjectDesc = "A reusable ASP.NET Core Web API foundation designed to practice and demonstrate clean API architecture, dependency injection, validation, error handling and automated testing.",
                    GitHubUrl = "https://github.com/luke780706/SkeletonApi"
                });

            _appDbContext.Skills.AddRange(
                new Entities.Skill
                {
                    SkillName = "C# / .NET",
                    SkillDesc = "Backend application development using C# and .NET."
                },
                new Entities.Skill
                {
                    SkillName = "SQL / SQL Server",
                    SkillDesc = "Database design, querying and data management using SQL Server."
                },
                new Entities.Skill
                {
                    SkillName = "ASP.NET Core / REST API",
                    SkillDesc = "Development of RESTful Web APIs using ASP.NET Core."
                },
                new Entities.Skill
                {
                    SkillName = "Entity Framework Core",
                    SkillDesc = "Data access and database management using Entity Framework Core."
                },
                new Entities.Skill
                {
                    SkillName = "Azure",
                    SkillDesc = "Cloud application development and deployment using Microsoft Azure."
                },
                new Entities.Skill
                {
                    SkillName = "Git / GitHub",
                    SkillDesc = "Source control and collaborative software development using Git and GitHub."
                }
            );

            await _appDbContext.SaveChangesAsync();
        }
    }
}