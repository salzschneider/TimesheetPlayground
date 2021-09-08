using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.Models.DTO
{
    public class WorkedHourDO
    {
        public int Id { get; set; }

        public int TimesheetId { get; set; }

        public int ProjectId { get; set; }

        public int Hours { get; set; }

        public int WorkDay { get; set; }

        public virtual ProjectDO Project { get; set; }

        public virtual TimesheetDO Timesheet { get; set; }

        public bool IsWeekend { get; set; }
    }
}
