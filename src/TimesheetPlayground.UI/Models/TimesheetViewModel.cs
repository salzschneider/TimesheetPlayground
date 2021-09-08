using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.Models
{
    public class TimesheetViewModel
    {
        public int UserId { get; set; }

        public TimesheetDO TimesheetDO { get; set; }

        public List<TimesheetDO> AllUserTimesheetDOs { get; set; } = new List<TimesheetDO>();

        public List<ProjectDO> AllProjectDOs { get; set; } = new List<ProjectDO>();

        public List<UserDO> Users { get; set; } = new List<UserDO>();

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public int WeekNumber { get; set; }
    }
}
