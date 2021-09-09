using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimesheetPlayground.DAL.Entities;
using TimesheetPlayground.UI.Common;
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.BLL
{
    public class TimesheetService : ServiceBase, ITimesheetService
    {
        private const int maximumWeekHours = 40;

        public async Task<TimesheetDO> GetTimesheetsById(int timesheetId)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var timesheet = await dbContext.Timesheets.FirstOrDefaultAsync(t => t.Id == timesheetId);

                return mapper.Map<TimesheetDO>(timesheet);
            }
        }

        public async Task<List<TimesheetDO>> GetTimesheetsByUser(int userId)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var timesheets = await dbContext.Timesheets.
                                       Include(x => x.WorkedHours).Include(x => x.User).
                                       Where(t => t.UserId == userId).ToListAsync();

                var timesheetDOs = timesheets.Select(t => mapper.Map<TimesheetDO>(t)).ToList();

                return timesheetDOs;
            }
        }

        public async Task<TimesheetDO> InsertTimesheetAsync(TimesheetDO timesheetDO)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                timesheetDO.StartDate = new DateTime(timesheetDO.StartDate.Year, timesheetDO.StartDate.Month, 1);
                var newtimesheet = mapper.Map<Timesheet>(timesheetDO);

                dbContext.Add(newtimesheet);

                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ue) when (ue.InnerException is SqlException)
                {
                    var sqlEx = ue.InnerException as SqlException;

                    if (sqlEx != null)
                    {
                        if(sqlEx.Number == 2627)
                        {
                            throw new InvalidOperationException("Existing timesheet");
                        }
                        else if(sqlEx.Number == 547)
                        {
                            throw new InvalidOperationException("Invalid user Id");
                        }
                    }

                    throw;
                }
                

                return mapper.Map<TimesheetDO>(newtimesheet);
            }
        }

        private void ValidateTimesheet(Timesheet timesheet, TimesheetDO timesheetDO)
        {
            if (timesheet == null)
            {
                throw new InvalidOperationException("Timesheet does not exist");
            }

            if (timesheetDO.WorkedHours != null && timesheetDO.WorkedHours.Any(w => w.Hours < 0))
            {
                throw new ArgumentException("Hours can't be less than zero");
            }

            if (timesheetDO.WorkedHours != null && timesheetDO.WorkedHours.Select(w => w.Hours).Sum() > maximumWeekHours)
            {
                throw new ArgumentException($"Week hours for a week can't be more than {maximumWeekHours} ");
            }

            if (timesheetDO.Status == StatusEnum.Submitted || timesheetDO.Status == StatusEnum.Approved)
            {
                throw new ArgumentException("Submitted or approved timesheet can't be updated");
            }

            var workedHoursForDay = timesheetDO.WorkedHours.GroupBy(w => w.WorkDay).Select(d => d.Sum(h => h.Hours)).ToList();

            if (workedHoursForDay.Any(h => h > 24))
            {
                throw new ArgumentException("Maximum 24 hours can be logged for a day");
            }
        }

        public async Task<TimesheetDO> UpdateTimesheetAsync(TimesheetDO timesheetDO)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var timesheet = await dbContext.Timesheets.Include(x => x.WorkedHours).FirstOrDefaultAsync(t => t.Id == timesheetDO.Id);

                ValidateTimesheet(timesheet, timesheetDO);

                var projectService = new ProjectService();
                var allProjectIds = (await projectService.GetAllProjectsAsync()).Select(p => p.Id).ToList();
                var workedHourDOsOfMonth = WorkDayHandler.GetWorkedHourDOsOfMonth(timesheet.Id, allProjectIds, timesheet.StartDate).Where(w => w.IsWeekend == false);
                WorkedHourDO inputWorkedHourDO;

                foreach(var workedHourDO in workedHourDOsOfMonth)
                {
                    inputWorkedHourDO = timesheetDO.WorkedHours.FirstOrDefault(w => w.WorkDay == workedHourDO.WorkDay && w.ProjectId == workedHourDO.ProjectId);

                    if (inputWorkedHourDO != null)
                    {
                        var timesheetWorkedHour = timesheet.WorkedHours.Where(w => w.WorkDay == inputWorkedHourDO.WorkDay && w.ProjectId == inputWorkedHourDO.ProjectId).FirstOrDefault();
                        
                        if (timesheetWorkedHour != null)
                        {
                            if(timesheetWorkedHour.Hours != inputWorkedHourDO.Hours)
                            {
                                timesheetWorkedHour.Hours = inputWorkedHourDO.Hours;
                            }    
                        }
                        else
                        {
                            timesheet.WorkedHours.Add(new WorkedHour()
                            { 
                                TimesheetId = timesheet.Id,
                                ProjectId = inputWorkedHourDO.ProjectId,
                                Hours = inputWorkedHourDO.Hours,
                                WorkDay = inputWorkedHourDO.WorkDay,
                            });
                        }
                    }
                }

                await dbContext.SaveChangesAsync();

                return mapper.Map<TimesheetDO>(timesheet);
            }
        }

        public async Task ChangeTimesheetStatusAsync(int timesheetId, StatusEnum status, bool IsManagerUser)
        {
            using (TimesheetPlaygroundContext dbContext = new TimesheetPlaygroundContext())
            {
                var timesheet = await dbContext.Timesheets.FirstOrDefaultAsync(t => t.Id == timesheetId);

                if (timesheet == null)
                {
                    throw new InvalidOperationException("Timesheet does not exist");
                }

                if (timesheet.Status == StatusEnum.Draft.ToString() && status == StatusEnum.Approved)
                {
                    throw new InvalidOperationException("Draft timesheet can't be approved. It has to be submitted first");
                }

                if (timesheet.Status == StatusEnum.Approved.ToString() && !IsManagerUser)
                {
                    throw new InvalidOperationException("Only a manager is allowed to change approved timesheet status");
                }

                timesheet.Status = status.ToString();

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
