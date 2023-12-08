using UI_Layer.Areas.BackendSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using UI_Layer.Data;
using UI_Layer.Resources;
using UI_Layer.Data.IdentityModel;
using UI_Layer.Models;
using Newtonsoft.Json;

namespace UI_Layer.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ApplicationDbContext _dbContext;
        public AccountController(ApplicationDbContext dbContext,SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IStringLocalizer<Resource> localizer)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager; 
            _localizer = localizer;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var viewModel = new LoginViewModel
            {
                UserName = null,
                Password = null
            };
            return View(viewModel);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                ReturnMessageViewModel<string> data = new ReturnMessageViewModel<string>();
                if (!ModelState.IsValid)
                {
                    
                    data.Success = false;
                    data.Message = Globalizer.HttGlobalizer.GetErrorMessage(ModelState,_localizer); 
                    data.MessageStatus = MessageStatus.Info;
                    ModelState.AddModelError(string.Empty, _localizer["MI00027"].ToString());
                    return new JsonResult(data);
          
                }

                if (model.UserName != null)
                {
                    var userinfo = await _userManager.FindByNameAsync(model.UserName);
                    if (userinfo == null)
                    {
                        data.Success = false;
                        data.Message = _localizer["MV00071"].ToString();
                        data.MessageStatus = MessageStatus.Info;
                        ModelState.AddModelError(string.Empty, _localizer["MV00071"].ToString());
                        return new JsonResult(data);
                    }
                    if (userinfo.SystemUser == false)
                    {
                        data.Success = false;
                        data.Message = _localizer["MI00026"].ToString();
                        data.MessageStatus = MessageStatus.Info;
                        ModelState.AddModelError(string.Empty, _localizer["MI00026"].ToString());
                        return new JsonResult(data);
                    }
                    if (!userinfo.Active)
                    {
                        data.Success = false;
                        data.Message = _localizer["MI00026"].ToString(); 
                        data.MessageStatus = MessageStatus.Info;
                        ModelState.AddModelError(string.Empty, _localizer["MI00026"].ToString());
                        return new JsonResult(data);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    var adminHomeUrl = Url.Action("AdminHome", "AdminHome");
                    data.Success = true;
                    data.Message = adminHomeUrl;
                    data.MessageStatus = MessageStatus.Success;
                    //var aa = new JsonResult(new { data = result }, new JsonSerializerSettings());
                    //return new JsonResult(new { data = result }, new JsonSerializerSettings());
                    ////return Json(data);
                    return new JsonResult(data);
                }
                else
                {
                    data.Success = false;
                    data.Message = _localizer["MV00093"].ToString();
                    data.MessageStatus = MessageStatus.Info;
                    ModelState.AddModelError(string.Empty, data.Message);
                    return Json(data);
                    //return new JsonResult(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        
           
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            ReturnMessageViewModel<string> data = new ReturnMessageViewModel<string>();
            if (ModelState.IsValid)
            {
                var isExistsEmail = await _userManager.FindByEmailAsync(model.Email);
                if (isExistsEmail != null)
                {
                    //ModelState.AddModelError("", _localizer["MI00011"].ToString());
                    data.Success = false;
                    data.Message = _localizer["MI00011"].ToString();
                    data.MessageStatus = MessageStatus.Info;
                    return new JsonResult(data);
                    //return View(model);
                }
                var userid = Guid.NewGuid();
                var user = new ApplicationUser
                {
                    FullName = model.FullName,
                    UserName = model.UserName,
                    Id = userid,
                    Email = model.Email,
                    SystemUser = true,
                    NormalizedUserName = model.UserName.ToUpper(),
                    NormalizedEmail = model.Email.ToUpper(),
                    Active = true,
                    EmailConfirmed = true,
                    CreatedUserId = userid,
                    CreatedDate = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    data.Success = true;
                    data.Message = "Registration Successful!.";
                    data.MessageStatus = MessageStatus.Success;
                    return new JsonResult(data);
                }
                else
                {
                    var errorMessage = "";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += error.Description + "</br>";
                    }
                    data.Success = false;
                    data.Message = errorMessage;
                    data.MessageStatus = MessageStatus.Info;
                    return new JsonResult(data);
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key]?.Errors;
                    if (errors != null)
                        foreach (var error in errors)
                        {
                            data.Success = false;
                            data.Message = error.ErrorMessage;
                            data.MessageStatus = MessageStatus.Info;
                            return new JsonResult(data);
                        }
                }
               
            }
            return View(model);

        }
        public async Task<IActionResult> LogOut(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
