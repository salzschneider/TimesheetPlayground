using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.Models.DTO
{
    public class UserDO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }

        public virtual List<TimesheetDO> Timesheets { get; set; }
    }
}
