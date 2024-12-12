using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.BLL
{
    public interface ITimesheetService
    {
        Task<TimesheetDO> GetTimesheetsById(int timesheetId);

        Task<List<TimesheetDO>> GetTimesheetsByUser(int userId);

        Task<TimesheetDO> InsertTimesheetAsync(TimesheetDO timesheetDO);

        Task<TimesheetDO> UpdateTimesheetAsync(TimesheetDO timesheetDO);

        Task ChangeTimesheetStatusAsync(int timesheetId, StatusEnum status, bool IsManagerUser);
    }
}
