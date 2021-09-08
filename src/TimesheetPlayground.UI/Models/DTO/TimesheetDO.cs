using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.Models.DTO
{
    public class TimesheetDO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public StatusEnum Status { get; set; }

        public DateTime StartDate { get; set; }

        public virtual UserDO User { get; set; }

        public virtual List<WorkedHourDO> WorkedHours { get; set; }
    }
}
