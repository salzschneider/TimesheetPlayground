using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimesheetPlayground.UI.Models;
using TimesheetPlayground.UI.Common;
using TimesheetPlayground.UI.BLL;
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.Models.Enum;

namespace TimesheetPlayground.UI.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;

        private readonly ITimesheetService timesheetService;
        private readonly IUserService userService;
        private readonly IProjectService projectService;

        public TimesheetController(ILogger<TimesheetController> logger, 
                                   ITimesheetService timesheetService, 
                                   IUserService userService, 
                                   IProjectService projectService)
        {
            _logger = logger;
            this.timesheetService = timesheetService;
            this.userService = userService;
            this.projectService = projectService;
        }

        private object TimesheetIndexRouteValues(int timesheetId, int userId, int weekNumber)
        {
            return new
            {
                timesheetId = timesheetId,
                userId = userId,
                weekNumber = weekNumber,
            };
        }

        [Authorize]
        public async Task<IActionResult> Index(int userId = 0, int timesheetId = 0, int weekNumber = 1)
        {
            var timesheetViewModel = new TimesheetViewModel();
            int userClaimId;
            string userName = "";

            if (UserClaim.IsManager(User))
            {
                var user = await userService.GetUserByIdAsync(userId);
                userClaimId = Convert.ToInt32(user?.Id);
                userName = user?.Name;

                if (userClaimId == 0)
                {
                    var firstUser = await userService.GetFirstUserAsync();
                    userClaimId = firstUser.Id;
                    userName = firstUser.Name;
                }
            }
            else
            {
                userClaimId = Convert.ToInt32(UserClaim.GetUserId(User));
                userName = UserClaim.GetUserName(User);
            }

            if(weekNumber < 1 || weekNumber > 6)
            {
                weekNumber = 1;
            }

            if (userClaimId != 0)
            {
                timesheetViewModel.UserId = userClaimId;

                timesheetViewModel.AllUserTimesheetDOs = 
                    (await timesheetService.GetTimesheetsByUser(userClaimId)).OrderBy(o => o.StartDate).ToList();

                if (timesheetId != 0)
                {
                    timesheetViewModel.TimesheetDO = timesheetViewModel.AllUserTimesheetDOs.Where(t => t.Id == timesheetId).FirstOrDefault();
                }

                if (timesheetId == 0 || timesheetViewModel.TimesheetDO is null)
                {
                    timesheetViewModel.TimesheetDO = timesheetViewModel.AllUserTimesheetDOs.LastOrDefault();
                }

                if(timesheetViewModel.TimesheetDO != null)
                {
                    var allProjects = await projectService.GetAllProjectsAsync();
                    var allProjectIds = allProjects.Select(p => p.Id).ToList();

                    var workedHoursOfWeek = WorkDayHandler.GetWorkedHourDOsOfWeek(timesheetViewModel.TimesheetDO.Id,
                                                                                  allProjectIds,
                                                                                  timesheetViewModel.TimesheetDO.StartDate,
                                                                                  weekNumber);

                    if (timesheetViewModel.TimesheetDO.WorkedHours != null)
                    {
                        foreach (var timesheetworkedHourDO in timesheetViewModel.TimesheetDO.WorkedHours)
                        {
                            var workedHourDO = workedHoursOfWeek.FirstOrDefault(w => w.WorkDay == timesheetworkedHourDO.WorkDay && w.ProjectId == timesheetworkedHourDO.ProjectId);

                            if (workedHourDO != null)
                            {
                                workedHourDO.Hours = timesheetworkedHourDO.Hours;
                            }
                        }
                    }

                    timesheetViewModel.AllProjectDOs = allProjects;
                    timesheetViewModel.TimesheetDO.WorkedHours = workedHoursOfWeek;
                    timesheetViewModel.WeekNumber = weekNumber;
                }

                if (UserClaim.IsManager(User))
                {
                    timesheetViewModel.Users = (await userService.GetAllUsersAsync()).Where(u => u.Role == RoleEnum.Employee).ToList();
                }

                timesheetViewModel.Username = userName;
            }

            return View(timesheetViewModel);
        }

        [Authorize]
        public async Task<IActionResult> ChangeStatus(int timesheetId, int userId, int status, int weekNumber)
        {
            if (!Enum.IsDefined(typeof(StatusEnum), status))
            {
                TempData["ErrorMessage"] = "Invalid status chosen";
                return RedirectToAction("Index", TimesheetIndexRouteValues(timesheetId, userId, weekNumber));
            }

            var statusEnum = (StatusEnum)status;

            if (!UserClaim.IsManager(User) && statusEnum == StatusEnum.Approved)
            {
                TempData["ErrorMessage"] = "Not allowed to approve the timesheet";
                return RedirectToAction("Index", TimesheetIndexRouteValues(timesheetId, userId, weekNumber));
            }

            if (!UserClaim.IsManager(User) && userId != UserClaim.GetUserId(User))
            {
                TempData["ErrorMessage"] = "Not allowed to make changes";
                return RedirectToAction("Index", TimesheetIndexRouteValues(timesheetId, userId, weekNumber));
            }    

            try
            {
                await timesheetService.ChangeTimesheetStatusAsync(timesheetId, statusEnum, UserClaim.IsManager(User));
            }
            catch(InvalidOperationException exception)
            {
                TempData["ErrorMessage"] = exception.Message;
                return RedirectToAction("Index", TimesheetIndexRouteValues(timesheetId, userId, weekNumber));
            }

            TempData["SuccessMessage"] = "Status is successfully changed";

            return RedirectToAction("Index", TimesheetIndexRouteValues(timesheetId, userId, weekNumber));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(TimesheetViewModel timesheet, int userId, int weekNumber)
        {
            if (userId != UserClaim.GetUserId(User))
            {
                TempData["ErrorMessage"] = "Not allowed to change timesheet";
                return RedirectToAction("Index", TimesheetIndexRouteValues(timesheet.TimesheetDO.Id, userId, weekNumber));
            }

            try
            {
                await timesheetService.UpdateTimesheetAsync(timesheet.TimesheetDO);
                TempData["SuccessMessage"] = "Timesheet is successfully updated";

                return RedirectToAction("Index", TimesheetIndexRouteValues(timesheet.TimesheetDO.Id, userId, weekNumber));

            }
            catch(ArgumentException exception)
            {
                TempData["ErrorMessage"] = exception.Message;

                return RedirectToAction("Index", TimesheetIndexRouteValues(timesheet.TimesheetDO.Id, userId, weekNumber));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
