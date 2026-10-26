using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Moq;
using Portfolio.Api.Controllers;
using Portfolio.Api.DTOs;
using Portfolio.Api.Entities;
using Portfolio.Api.Models;
using Portfolio.Api.Services;

namespace Portfolio.Api.Tests
{
    public class PortfolioControllerTests
    {
        [Fact]
        public async Task GetProfile_ProfileExists_ReturnsOk()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.GetProfileAsync())
                .ReturnsAsync(new ProfileModel()
                {
                    ProfileDescription = "Profile Description"
                });

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.GetProfile();

            //Then
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var profileDto = Assert.IsType<ProfileDto>(okObjectResult.Value);

            Assert.Equal("Profile Description", profileDto.ProfileDescription);
        }

        [Fact]
        public async Task GetProfile_ProfileDoesNotExist_ReturnsNotFound()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.GetProfileAsync())
                       .ReturnsAsync(value: null);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.GetProfile();

            //Then
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetProjects_ProjectExists_ReturnsProjects()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.GetProjectsAsync())
                       .ReturnsAsync([ new ProjectModel
                       {
                           Id = 1,
                           ProjectName = "New Project",
                           ProjectDesc = "This is a new project",
                           GitHubUrl = "urlGitHub"
                       }, new ProjectModel
                       {
                           Id = 2,
                           ProjectName = "New Project2",
                           ProjectDesc = "This is a new project2",
                           GitHubUrl = "urlGitHub2"
                       }
                       ]);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.GetProjects();

            //Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsType<List<ProjectDto>>(okResult.Value);

            Assert.Equal(2, products.Count());
            Assert.Equal(1, products[0].Id);
            Assert.Equal("New Project", products[0].ProjectName);
            Assert.Equal("This is a new project", products[0].ProjectDesc);
            Assert.Equal("urlGitHub", products[0].GitHubUrl);

            Assert.Equal(2, products[1].Id);
            Assert.Equal("New Project2", products[1].ProjectName);
            Assert.Equal("This is a new project2", products[1].ProjectDesc);
            Assert.Equal("urlGitHub2", products[1].GitHubUrl);
        }

        [Fact]
        public async Task GetExperience_ExperienceExists_ReturnsOk()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.GetExperiencesAsync())
                       .ReturnsAsync([new ExperienceModel
                       {
                         ExperienceName = "Experience name",
                         ExperienceDesc = "Experience description" }
                       ,new ExperienceModel{
                         ExperienceName = "Experience name",
                         ExperienceDesc = "Experience description" }
                       ]);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.GetExperience();

            //Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var experienceList = Assert.IsType<List<ExperienceDto>>(okResult.Value);

            Assert.Equal(2, experienceList.Count);
        }

        [Fact]
        public async Task GetSkills_SkillExist_ReturnsSkills()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.GetSkillsAsync())
                       .ReturnsAsync(
                        [
                           new SkillModel{ SkillName = "Skill_1", SkillDesc = "SkillDesc_1"},
                           new SkillModel{ SkillName = "Skill_2", SkillDesc = "SkillDesc_2"}
                        ]);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.GetSkills();

            //Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var skillList = Assert.IsType<List<SkillDto>>(okResult.Value);

            Assert.Equal(2, skillList.Count());
        }

        [Fact]
        public async Task GetContact_ContactDataExists_ReturnsOk()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.GetContactAsync())
                       .ReturnsAsync(new ContactModel
                       {
                           Name = "Jan",
                           Surname = "Kowalski",
                           Email = "jan.kowalski@gmail.com",
                           PhoneNumber = "123456789"
                       });

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.GetContact();

            //Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var contactData = Assert.IsType<ContactDto>(okResult.Value);

            Assert.Equal("Jan", contactData.Name);
        }

        [Fact]
        public async Task AddProject_ValidProject_ReturnsCreated()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            var project = new Project()
            {
                Id = 1,
                ProjectName = "Project_Name",
                ProjectDesc = "Project_Desc",
                GitHubUrl = "Github_Url"
            };

            var createProjectDto = new CreateProjectDto()
            {
                ProjectName = "Project_Name",
                ProjectDesc = "Project_Desc",
                GitHubUrl = "Github_Url"
            };

            mockService.Setup(x => x
                       .AddProjectAsync(It.IsAny<ProjectModel>()))
                       .ReturnsAsync(project);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.AddProject(createProjectDto);

            //Then
            var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var addedProject = Assert.IsType<ProjectDto>(okResult.Value);

            Assert.Equal(1, addedProject.Id);
            Assert.Equal("Project_Name", addedProject.ProjectName);
            Assert.Equal("Project_Desc", addedProject.ProjectDesc);
            Assert.Equal("Github_Url", addedProject.GitHubUrl);
            Assert.Equal(nameof(PortfolioController.GetProjectById), okResult.ActionName);
            Assert.NotNull(okResult.RouteValues);
            Assert.Equal(1, okResult.RouteValues["Id"]);
        }

        [Fact]
        public async Task GetProjectById_ProjectExists_ReturnsOk()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            Project project = new Project()
            {
                Id = 1,
                ProjectName = "Project_Name",
                ProjectDesc = "Project_Desc",
                GitHubUrl = "Guthub_Url"
            };

            mockService.Setup(x => x.GetProjectByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync(project);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.GetProjectById(project.Id);

            //Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var projectResult = Assert.IsType<ProjectDto>(okResult.Value);

            Assert.Equal("Project_Name", projectResult.ProjectName);
            Assert.Equal(1, projectResult.Id);
        }

        [Fact]
        public async Task GetProjectById_ProjectDoesNotExist_ReturnsNotFound()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.GetProjectByIdAsync(It.IsAny<int>()))
                                  .ReturnsAsync(value: null);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.GetProjectById(0);

            //Then
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateProject_ProjectExists_ReturnsOk()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            var projectModel = new ProjectModel()
            {
                ProjectName = "Project_Name2",
                ProjectDesc = "Project_Desc2",
                GitHubUrl = "Github_Url2"
            };

            var updateProjectDto = new UpdateProjectDto()
            {
                ProjectName = "Project_Name2",
                ProjectDesc = "Project_Desc2",
                GitHubUrl = "Github_Url2"
            };

            mockService.Setup(x => x.UpdateProjectAsync(
                        1,
                        It.IsAny<ProjectModel>()))
                        .ReturnsAsync(projectModel);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.UpdateProject(1, updateProjectDto);

            //Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var projChanged = Assert.IsType<ProjectDto>(okResult.Value);

            Assert.Equal("Project_Name2", projChanged.ProjectName);

            mockService.Verify(x => x.UpdateProjectAsync(
                                    1,
                                    It.Is<ProjectModel>(p =>
                                        p.ProjectName == "Project_Name2" &&
                                        p.ProjectDesc == "Project_Desc2" &&
                                        p.GitHubUrl == "Github_Url2")),
                                    Times.Once);
        }

        [Fact]
        public async Task UpdateProject_ProjectDoesNotExist_ReturnsNotFound()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            var updateProjectDto = new UpdateProjectDto()
            {
                ProjectName = "Project_Name2",
                ProjectDesc = "Project_Desc2",
                GitHubUrl = "Github_Url2"
            };

            mockService.Setup(x => x.UpdateProjectAsync(99, It.IsAny<ProjectModel>()))
                       .ReturnsAsync(value: null);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.UpdateProject(99, updateProjectDto);

            //Then
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteProject_ProjectExists_ReturnsNoContent()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.DeleteProjectAsync(It.IsAny<int>()))
                       .ReturnsAsync(true);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.DeleteProject(1);

            //Then
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProject_ProjectDoesNotExist_ReturnsNotFound()
        {
            //Given
            var mockService = new Mock<IPortfolioService>();

            mockService.Setup(x => x.DeleteProjectAsync(1))
                       .ReturnsAsync(false);

            var controller = new PortfolioController(mockService.Object);

            //When
            var result = await controller.DeleteProject(1);

            //Then
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
