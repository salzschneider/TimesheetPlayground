using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.DAL.Entities;
using TimesheetPlayground.UI.Common;
using TimesheetPlayground.UI.Models.DTO;

namespace TimesheetPlayground.UI.BLL
{
    public class ProjectService : ServiceBase, IProjectService
    {
        public async Task<List<ProjectDO>> GetAllProjectsAsync()
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var allProjectEntities = await dbContext.Projects.ToListAsync();
                var allProjects = allProjectEntities.Select(p => mapper.Map<ProjectDO>(p)).ToList();

                return allProjects;
            }
        }

        public async Task<ProjectDO> InsertProjectAsync(ProjectDO projectDO)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Name == projectDO.Name);

                if (project != null)
                {
                    throw new DuplicateNameException("Existing project");
                }

                var newProject = mapper.Map<Project>(projectDO);

                dbContext.Add(newProject);
                await dbContext.SaveChangesAsync();

                return mapper.Map<ProjectDO>(newProject);
            }
        }

        public async Task UpdateProjectsAsync(List<ProjectDO> projectDOs)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var allProject = await dbContext.Projects.ToListAsync();

                foreach (var projectDO in projectDOs)
                {
                    if (string.IsNullOrEmpty(projectDO.Name) || projectDO.Name.Length < 6)
                    {
                        throw new ArgumentException("The field Name must be a string or array type with a minimum length of 6.");
                    }

                    if (!allProject.Any(p => p.Id == projectDO.Id))
                    {
                        throw new InvalidOperationException("Project does not exist");
                    }

                    if (allProject.Any(p => p.Id != projectDO.Id && p.Name == projectDO.Name))
                    {
                        throw new DuplicateNameException("Existing project with same name");
                    }

                    var projectEntity = allProject.Where(p => p.Id == projectDO.Id).First();
                    projectEntity.Name = projectDOs.Where(p => p.Id == projectEntity.Id).First().Name;
                }

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

                if (project != null)
                {
                    try
                    {
                        dbContext.Remove(project);
                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException ue) when (ue.InnerException is SqlException)
                    {
                        var sqlEx = ue.InnerException as SqlException;

                        if (sqlEx != null && sqlEx.Number == 547)
                        {
                            throw new InvalidOperationException("Project can't be deleted.");
                        }

                        throw;
                    }
                }
            }
        }
    }
}
