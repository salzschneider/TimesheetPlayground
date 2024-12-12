using System.Collections.Generic;

#nullable disable

namespace TimesheetPlayground.DAL.Entities
{
    public partial class User
    {
        public User()
        {
            Timesheets = new HashSet<Timesheet>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
