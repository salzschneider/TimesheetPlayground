using AutoMapper;
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
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.BLL;
using System.Data;

namespace TimesheetPlayground.UI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> logger;
        private readonly IProjectService projectService;

        public ProjectController(ILogger<ProjectController> logger, IProjectService projectService)
        {
            this.logger = logger;
            this.projectService = projectService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if(UserClaim.IsManager(User))
            {
                ProjectViewModel projectViewModel = new ProjectViewModel()
                { 
                    AllProjects = await projectService.GetAllProjectsAsync(),
                    SuccessMessage = TempData["SuccessMessage"]?.ToString(),
                    ErrorMessage = TempData["ErrorMessage"]?.ToString(),
                };

                return View(projectViewModel);
            }
            else
            {
                return View("Unauthorized");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ProjectViewModel project)
        {
            if (UserClaim.IsManager(User))
            {
                ProjectViewModel responseProjectViewModel = new ProjectViewModel();
                bool HasError = true;

                if (ModelState.IsValid)
                {
                    try
                    {
                        var newProject = await projectService.InsertProjectAsync(new ProjectDO()
                        {
                            Name = project.Name,
                        });

                        TempData["SuccessMessage"] = "Project is successfully added";
                        ModelState.Clear();
                        HasError = false;
                    }
                    catch (DuplicateNameException)
                    {
                        ModelState.AddModelError("Name", "Existing project name");
                    }
                }

                if(HasError)
                {
                    responseProjectViewModel.AllProjects = await projectService.GetAllProjectsAsync();
                    return View("Index", responseProjectViewModel);
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                return View("Unauthorized");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(ProjectViewModel project)
        {
            if (UserClaim.IsManager(User))
            {
                try
                {
                    await projectService.UpdateProjectsAsync(project.AllProjects);
                    TempData["SuccessMessage"] = "Project list is successfully updated";

                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if(ex is ArgumentException || ex is DuplicateNameException || ex is InvalidOperationException)
                    {
                        TempData["ErrorMessage"] = ex.Message;
                    }

                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("/");
            }
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (UserClaim.IsManager(User))
            {
                try
                {
                    await projectService.DeleteProjectAsync(id);
                    TempData["SuccessMessage"] = "Project is successfully deleted";
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "Project can't be deleted";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Unauthorized");
            }
        }
    }
}
