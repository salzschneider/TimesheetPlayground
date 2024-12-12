using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TimesheetPlayground.UI.BLL;
using TimesheetPlayground.UI.Models.DTO;
using System.Data;

namespace TimesheetPlayground.Test.Integration.Service
{
    public class ProjectServiceTests
    {
        private readonly ProjectService projectService;

        private static void AssertAllFields(ProjectDO expected, ProjectDO actual)
        {
            Assert.Equal(expected.Name, actual.Name);
        }

        public ProjectServiceTests()
        {
            projectService = new ProjectService();
        }

        public static IEnumerable<object[]> ValidProjectDOs =>
            [
                new object[]{ new ProjectDO() 
                { 
                    Name = "ProjectName" + Guid.NewGuid().ToString(),
                }},
            ];

        [Theory]
        [MemberData(nameof(ValidProjectDOs))]
        public async Task InsertProjectAsync_ValidProjectDO_ReturnNewProjectDO(ProjectDO expected)
        {
            // arrange, act
            var actual = await projectService.InsertProjectAsync(expected);

            // assert
            AssertAllFields(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ValidProjectDOs))]
        public async Task InsertProjectAsync_ProjectWithExistingName_ThrowException(ProjectDO projectDO)
        {
            // arrange
            var actual = await projectService.InsertProjectAsync(projectDO);

            // act, assert
            await Assert.ThrowsAsync<DuplicateNameException>(async () => await projectService.InsertProjectAsync(actual));
        }

        [Theory]
        [MemberData(nameof(ValidProjectDOs))]
        public async Task GetAllProjectsAsync_ValidProjectDO_ReturnProjectListWithNewProjectDO(ProjectDO projectDO)
        {
            // arrange
            var expected = await projectService.InsertProjectAsync(projectDO);

            // act
            var allProjects = await projectService.GetAllProjectsAsync();
            var actual = allProjects.Where(p => p.Id == expected.Id).FirstOrDefault();

            // assert
            Assert.NotNull(actual);
            AssertAllFields(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ValidProjectDOs))]
        public async Task DeleteProjectAsync_ValidProject_DeletedProject(ProjectDO projectDO)
        {
            // arrange
            var actual = await projectService.InsertProjectAsync(projectDO);

            // act
            await projectService.DeleteProjectAsync(actual.Id);
            var projects = await projectService.GetAllProjectsAsync();

            // assert
            Assert.DoesNotContain(projects, p => p.Id == actual.Id);
        }

        [Fact]
        public async Task DeleteProjectAsync_DeletedProject_NoException()
        {
            // arrange 
            await projectService.DeleteProjectAsync(-1);

            // act
            var exception = await Record.ExceptionAsync(async () => await projectService.DeleteProjectAsync(-1));
            
            // assert
            Assert.Null(exception);
        }
    }
}
