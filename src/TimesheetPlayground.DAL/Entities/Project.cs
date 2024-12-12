using System.Collections.Generic;

#nullable disable

namespace TimesheetPlayground.DAL.Entities
{
    public partial class Project
    {
        public Project()
        {
            WorkedHours = new HashSet<WorkedHour>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<WorkedHour> WorkedHours { get; set; }
    }
}
