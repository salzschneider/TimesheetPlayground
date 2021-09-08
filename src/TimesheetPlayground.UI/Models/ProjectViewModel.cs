using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models.DTO;

namespace TimesheetPlayground.UI.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(6)]
        public string Name { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public List<ProjectDO> AllProjects { get; set; }
    }
}
