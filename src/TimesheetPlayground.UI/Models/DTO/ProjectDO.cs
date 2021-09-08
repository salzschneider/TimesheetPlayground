using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.Models.DTO
{
    public class ProjectDO
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
