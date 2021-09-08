using System;
using System.Collections.Generic;

#nullable disable

namespace TimesheetPlayground.DAL.Entities
{
    public partial class Timesheet
    {
        public Timesheet()
        {
            WorkedHours = new HashSet<WorkedHour>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<WorkedHour> WorkedHours { get; set; }
    }
}
