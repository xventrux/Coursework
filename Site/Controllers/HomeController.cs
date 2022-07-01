using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Infrastructure.Repository;
using Site.Models;
using Site.Models.Entities;
using Site.Services.Repositories.DirectionRepositories;
using Site.Services.Repositories.StructReposotories;
using Site.Services.Repositories.UserRepositories;
using Site.Services.Repositories.СourseRepositories;
using Site.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<StructuralDivision> _structRepository;
        private readonly IRepository<DirectionOfTraining> _dirRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Faculty> _facRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IStructuralDivisionRepository _superStructRepository;
        private readonly IDirectionRepository _superDirRepository;
        private readonly IUserRepository _superUserRepository;
        private readonly ICourseRepository _superCourseRepository;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IRepository<StructuralDivision> structRepository, IRepository<DirectionOfTraining> dirRepository, IRepository<Group> groupRepository, IRepository<Faculty> facRepository, IRepository<Course> courseRepository, IStructuralDivisionRepository superStructRepository, IDirectionRepository superDirRepository, IUserRepository superUserRepository, ICourseRepository superCourseRepository)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _structRepository = structRepository;
            _dirRepository = dirRepository;
            _groupRepository = groupRepository;
            _facRepository = facRepository;
            _courseRepository = courseRepository;
            _superStructRepository = superStructRepository;
            _superDirRepository = superDirRepository;
            _superUserRepository = superUserRepository;
            _superCourseRepository = superCourseRepository;
        }

        public IActionResult Index()
        {
            if(User.IsInRole("teacher"))
            {
                return RedirectToAction("Teacher");
            }
            else if(User.IsInRole("student"))
            {
                return RedirectToAction("Student");
            }
            return View();
        }

        public async Task<IActionResult> Teacher()
        {
            User u = await _userManager.GetUserAsync(User);
            List<Course> l = await _superCourseRepository.GetAllForTeacherWithAll(u.Id);
            return View(l);
        }

        public async Task<IActionResult> Student()
        {
            User u = await _userManager.GetUserAsync(User);
            List<Course> l = await _superCourseRepository.GetAllForGroupWithAll((Guid)u.GroupId);
            return View(l);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
