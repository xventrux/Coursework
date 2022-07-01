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
    public class MaterialController : Controller
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

        public MaterialController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IRepository<StructuralDivision> structRepository, IRepository<DirectionOfTraining> dirRepository, IRepository<Group> groupRepository, IRepository<Faculty> facRepository, IRepository<Course> courseRepository, IStructuralDivisionRepository superStructRepository, IDirectionRepository superDirRepository, IUserRepository superUserRepository, ICourseRepository superCourseRepository, IRepository<EducationalMaterial> materialRepository)
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
        public async Task<IActionResult> EditMaterial(Guid id)
        {
            EducationalMaterial c = await _materialRepository.GetByIdAsync(id);
            return View(c);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetMaterial(Guid id)
        {
            EducationalMaterial c = await _materialRepository.GetByIdAsync(id);
            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> GetHtml(Guid id)
        {
            EducationalMaterial c = await _materialRepository.GetByIdAsync(id);
            return new JsonResult(c.HTML);
        }

        [HttpPost]
        public async Task<IActionResult> EditMaterial(Guid id, string HTML)
        {
            EducationalMaterial c = await _materialRepository.GetByIdAsync(id);
            c.HTML = HTML;
            await _materialRepository.UpdateAsync(c);
            return View(c);
        }
    }
}
