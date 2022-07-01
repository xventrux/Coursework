using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Infrastructure.Repository;
using Site.Models.Entities;
using Site.Services.Repositories.DirectionRepositories;
using Site.Services.Repositories.StructReposotories;
using Site.Services.Repositories.UserRepositories;
using Site.Services.Repositories.СourseRepositories;
using Site.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    public class AdminController : Controller
    {
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

        public AdminController(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IRepository<StructuralDivision> structRepository,
            IStructuralDivisionRepository superStructRepository,
            IRepository<DirectionOfTraining> dirRepository,
            IRepository<Group> groupRepository,
            IDirectionRepository superDirRepository,
            IRepository<Faculty> facRepository,
            IUserRepository superUserRepository,
            IRepository<Course> courseRepository, 
            ICourseRepository superCourseRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _structRepository = structRepository;
            _superStructRepository = superStructRepository;
            _dirRepository = dirRepository;
            _groupRepository = groupRepository;
            _superDirRepository = superDirRepository;
            _facRepository = facRepository;
            _superUserRepository = superUserRepository;
            _courseRepository = courseRepository;
            _superCourseRepository = superCourseRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Структурные подразделения
        [HttpGet]
        public async Task<IActionResult> GetStructures()
        {
            List<StructuralDivision> l = _structRepository.GetAll().ToList();

            return new JsonResult(l);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCoursesForGroup(Guid gId)
        {
            List<Course> l = await _superCourseRepository.GetAllForGroupWithAll(gId);

            return new JsonResult(l);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCourse(Guid gId, string tId, string name, int hours)
        {
            Course c = new Course()
            {
                CreatingDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Name = name,
                Hours = hours,
                GroupId = gId,
                TeacherId = tId
            };

            await _courseRepository.AddAsync(c);

            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveCourse(Guid id)
        {
            Course c = await _courseRepository.GetByIdAsync(id);

            await _courseRepository.DeleteAsync(c);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetStructInfo(Guid id)
        {
            StructuralDivision sd = await _superStructRepository.GetByIdWithAllAsync(id);

            return new JsonResult(sd);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDirOfFac(Guid id)
        {
            List<DirectionOfTraining> sd = await _superDirRepository.GetByIdFacultyWithAllAsync(id);

            return new JsonResult(sd);
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectionInfo(Guid id)
        {
            DirectionOfTraining dt = await _superDirRepository.GetByIdWithAllAsync(id);

            return new JsonResult(dt);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(Guid id, string name)
        {
            Group g = new Group()
            {
                CreatingDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Name = name,
                DirectionOfTrainingId = id
            };
            await _groupRepository.AddAsync(g);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirection(Guid id, string name, string code)
        {
            DirectionOfTraining dt = new DirectionOfTraining()
            {
                CreatingDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Name = name,
                Code = code,
                StructuralDivisionId = id
            };
            await _dirRepository.AddAsync(dt);
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateDirectionForFac(Guid id, string name, string code)
        {
            DirectionOfTraining dt = new DirectionOfTraining()
            {
                CreatingDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Name = name,
                Code = code,
                FacultyId = id
            };
            await _dirRepository.AddAsync(dt);
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateFac(Guid id, string name)
        {
            Faculty dt = new Faculty()
            {
                CreatingDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Name = name,
                StructuralDivisionId = id
            };
            await _facRepository.AddAsync(dt);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStructure(string name, bool isFac)
        {
            StructuralDivision s = new StructuralDivision()
            {
                CreatingDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Name = name,
                IsThereAreFaculties = isFac
            };

            await _structRepository.AddAsync(s);

            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveStructureItem(Guid id)
        {
            StructuralDivision s = await _structRepository.GetByIdAsync(id);

            await _structRepository.DeleteAsync(s);

            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveFac(Guid id)
        {
            Faculty s = await _facRepository.GetByIdAsync(id);

            await _facRepository.DeleteAsync(s);

            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveDirection(Guid id)
        {
            DirectionOfTraining s = await _dirRepository.GetByIdAsync(id);

            await _dirRepository.DeleteAsync(s);

            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveGroup(Guid id)
        {
            Group s = await _groupRepository.GetByIdAsync(id);

            await _groupRepository.DeleteAsync(s);

            return Ok();
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            List<User> users = await _superUserRepository.GetAllWithGroup();

            return new JsonResult(users);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            List<User> users = (List<User>)await _userManager.GetUsersInRoleAsync("teacher");

            return new JsonResult(users);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(string id)
        {
            User u = _userManager.Users.Where(u => u.Id == id).FirstOrDefault();
            if (u != null)
                await _userManager.DeleteAsync(u);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AcceptUser(string id)
        {
            User u = _userManager.Users.Where(u => u.Id == id).FirstOrDefault();
            if (u != null)
            {
                u.IsConfirmed = true;
                await _userManager.UpdateAsync(u);
            }
                

            return Ok();
        }

        public async Task<List<UserViewModel>> SearchUsers(IQueryable<User> users, string str, string[] roles)
        {
            List<UserViewModel> list = new List<UserViewModel>();

            if (String.IsNullOrEmpty(str))
            {
                list = await UserViewModel.CreateList(users.ToList(), _userManager);
            }

            else
            {
                list = await UserViewModel.CreateList(users.Where(u => u.Email.ToLower().Contains(str.ToLower()) || str.ToLower().Contains(u.Email.ToLower())).ToList(), _userManager);
            }

            for(int i = 0; i < list.Count; i++)
            {
                bool cond = false;

                foreach (string item in roles)
                {
                    if(await _userManager.IsInRoleAsync(list[i].User, item))
                    {
                        cond = true;
                        break;
                    }
                }

                if(!cond)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            
            

            return list;
        }
    }
}
