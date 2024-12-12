#nullable disable

namespace TimesheetPlayground.DAL.Entities
{
    public partial class WorkedHour
    {
        public int Id { get; set; }

        public int TimesheetId { get; set; }

        public int ProjectId { get; set; }

        public int Hours { get; set; }

        public int WorkDay { get; set; }

        public virtual Project Project { get; set; }

        public virtual Timesheet Timesheet { get; set; }
    }
}
