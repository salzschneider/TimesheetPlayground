using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.DTO;

namespace TimesheetPlayground.UI.BLL
{
    public interface IProjectService
    {
        Task<List<ProjectDO>> GetAllProjectsAsync();

        Task<ProjectDO> InsertProjectAsync(ProjectDO projectDO);

        Task UpdateProjectsAsync(List<ProjectDO> projectDOs);

        Task DeleteProjectAsync(int projectId);
    }
}
