using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Infrastructure.Repository;
using Site.Models.Entities;
using Site.Services.EmailServices;
using Site.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<StructuralDivision> _structRepository;
        private readonly IRepository<Faculty> _facRepository;
        private readonly IRepository<DirectionOfTraining> _dirRepository;
        private readonly IRepository<Group> _groupRepository;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService,
            RoleManager<IdentityRole> roleManager,
            IRepository<Faculty> facRepository,
            IRepository<DirectionOfTraining> dirRepository,
            IRepository<Group> groupRepository, 
            IRepository<StructuralDivision> structRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
            _facRepository = facRepository;
            _dirRepository = dirRepository;
            _groupRepository = groupRepository;
            _structRepository = structRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStructures()
        {
            List<StructuralDivision> l = _structRepository.GetAll().ToList();

            return new JsonResult(l);
        }

        [HttpGet]
        public async Task<IActionResult> GetFacs(Guid id)
        {
            List<Faculty> l = _facRepository.GetAllFiltered(f => f.StructuralDivisionId == id).ToList();
            return new JsonResult(l);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetGroups(Guid id)
        {
            List<Group> l = _groupRepository.GetAllFiltered(g => g.DirectionOfTrainingId == id).ToList();

            return new JsonResult(l);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDirections(Guid id, bool cond)
        {

            List<DirectionOfTraining> l = null;
            if (cond)
            {
                l = _dirRepository.GetAllFiltered(d => d.FacultyId == id).ToList();
            }
            else
            {
                l = _dirRepository.GetAllFiltered(d => d.StructuralDivisionId == id).ToList();
            }

            return new JsonResult(l);
        }

        [HttpGet]
        public async Task<IActionResult> GetIsFac(Guid id)
        {
            StructuralDivision sd = await _structRepository.GetByIdAsync(id);

            return new JsonResult(sd.IsThereAreFaculties);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            return View(new CabinetViewModel() { Email = user.Email });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(CabinetViewModel model)
        {
            User user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                

                // добавляем пользователя
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            model.Email = user.Email;
            return View(model);
        }

        #region Логин
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await _userManager.FindByNameAsync(model.Email);
                //if (user != null)
                //{
                //    // проверяем, подтвержден ли email
                //    if (!await _userManager.IsEmailConfirmedAsync(user))
                //    {
                //        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                //        return View(model);
                //    }
                //}

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        #endregion

        #region Регистрация
        [HttpGet]
        public IActionResult Registration(string returnUrl = null)
        {
            return View(new RegistrationViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User 
                { 
                    Email = model.Email, 
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    SurName = model.SurName,
                    MiddleName = model.MiddleName,
                };

                if(model.Role == "student")
                {
                    user.GroupId = model.GroupId;
                }


                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    IdentityRole role = await _roleManager.FindByNameAsync("admin");
                    if (role == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                    }
                    role = await _roleManager.FindByNameAsync("student");
                    if (role == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("student"));
                    }
                    role = await _roleManager.FindByNameAsync("teacher");
                    if (role == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("teacher"));
                    }

                    List<User> ul = _userManager.Users.ToList();

                    if(ul.Count == 1)
                    {
                        await _userManager.AddToRoleAsync(user, "admin");
                    }

                    else if(model.Role == "student")
                    {
                        await _userManager.AddToRoleAsync(user, "student");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "teacher");
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Подтверждение регистрации",
                        $"Подтвердите регистрацию, перейдя по <a href='{callbackUrl}'>ссылке</a>");

                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");

                    //return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }
        #endregion

        #region Сброс пароля
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return Content("Для сброса пароля перейдите по ссылке в письме, отправленном на ваш email.");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Сброс пароля",
                    $"Для сброса пароля пройдите по <a href='{callbackUrl}'>ссылке</a>");
                return Content("Для сброса пароля перейдите по ссылке в письме, отправленном на ваш email.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        #endregion

        #region Выход
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
