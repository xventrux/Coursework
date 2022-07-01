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
    public class CourseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<StructuralDivision> _structRepository;
        private readonly IRepository<DirectionOfTraining> _dirRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Faculty> _facRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<EducationalMaterial> _materialRepository;
        private readonly IStructuralDivisionRepository _superStructRepository;
        private readonly IDirectionRepository _superDirRepository;
        private readonly IUserRepository _superUserRepository;
        private readonly ICourseRepository _superCourseRepository;

        public CourseController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IRepository<StructuralDivision> structRepository, IRepository<DirectionOfTraining> dirRepository, IRepository<Group> groupRepository, IRepository<Faculty> facRepository, IRepository<Course> courseRepository, IStructuralDivisionRepository superStructRepository, IDirectionRepository superDirRepository, IUserRepository superUserRepository, ICourseRepository superCourseRepository, IRepository<EducationalMaterial> materialRepository)
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
            _materialRepository = materialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(Guid id)
        {
            Course c = await _superCourseRepository.GetByIdWithAll(id);
            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse(Guid id)
        {
            Course c = await _superCourseRepository.GetByIdWithAll(id);
            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(string description, Guid id)
        {
            Course c = await _courseRepository.GetByIdAsync(id);
            c.Description = description;
            await _courseRepository.UpdateAsync(c);
            c = await _superCourseRepository.GetByIdWithAll(id);
            return View(c);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddMaterial(Guid id, string name)
        {
            EducationalMaterial m = new EducationalMaterial()
            {
                CreatingDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Name = name,
                CourseId = id,
            };

            await _materialRepository.AddAsync(m);
            Course c = await _superCourseRepository.GetByIdWithAll(id);
            Dictionary<string, string> RouteValues = new Dictionary<string, string>();
            RouteValues.Add("id", c.Id.ToString());
            return RedirectToAction("EditCourse", RouteValues);
        }
    }
}
