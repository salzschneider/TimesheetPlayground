using System.Collections.Generic;
using TimesheetPlayground.UI.Models.DTO;

namespace TimesheetPlayground.UI.Models
{
    public class TimesheetViewModel
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public TimesheetDO TimesheetDO { get; set; }

        public List<TimesheetDO> AllUserTimesheetDOs { get; set; } = [];

        public List<ProjectDO> AllProjectDOs { get; set; } = [];

        public List<UserDO> Users { get; set; } = [];

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public int WeekNumber { get; set; }
    }
}
