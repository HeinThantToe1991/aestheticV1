using System.Security.Claims;
using UI_Layer.Globalizer;
using UI_Layer.Areas.BackendSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using UI_Layer.Data;
using UI_Layer.Resources;
using UI_Layer.Data.IdentityModel;
using UI_Layer.Models;
using Newtonsoft.Json;
using UI_Layer.Globalizer;
using UI_Layer.Helpers;
using Microsoft.AspNetCore.SignalR;
using UI_Layer.Hubs;

namespace UI_Layer.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDistributedCache _cache;
        private readonly IHubContext<NotificationHub> _notiHubContext;

        public AccountController(ApplicationDbContext dbContext,SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IStringLocalizer<Resource> localizer, IDistributedCache cache, IHubContext<NotificationHub> notiHubContext)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager; 
            _localizer = localizer;
            _cache = cache;
            _notiHubContext = notiHubContext;
            
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
                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                    };
                    var logInView = new CustomerViewModel();
                    switch (user?.UserType)
                    {
                        case Constant.UserType.Staffs:
                            var staffInfo = _dbContext.Staff.FirstOrDefault(w => w.ApplicationUserId.Equals(user.Id));
                            logInView.Id = staffInfo?.Id.ToString();
                            logInView.UserId = staffInfo?.ApplicationUserId.ToString();
                            logInView.Address = staffInfo?.Address;
                            logInView.UserImageStr = staffInfo.UserImage;
                            logInView.CustomerName = user.UserName;
                            logInView.FullName = user.FullName;
                            logInView.Gender = staffInfo.Gender;
                            logInView.Day = staffInfo.DateOfBirth.Day;
                            logInView.Month = staffInfo.DateOfBirth.Month;
                            logInView.Year = staffInfo.DateOfBirth.Year;
                            logInView.TownshipId = staffInfo.TownshipId;
                            logInView.LogInUserStatus = Constant.UserType.Staffs;
                            break;
                        case Constant.UserType.Customers:
                            var customerInfo = _dbContext.Customer.FirstOrDefault(w => w.ApplicationUserId.Equals(user.Id));
                            logInView.Id = customerInfo?.Id.ToString();
                            logInView.UserId = customerInfo?.ApplicationUserId.ToString();
                            logInView.Address = customerInfo?.Address;
                            logInView.UserImageStr = customerInfo?.UserImage;
                            logInView.CustomerName = user.UserName;
                            logInView.FullName = user.FullName;
                            logInView.Gender = customerInfo.Gender;
                            logInView.Day = customerInfo.DateOfBirth.Day;
                            logInView.Month = customerInfo.DateOfBirth.Month;
                            logInView.Year = customerInfo.DateOfBirth.Year;
                            logInView.TownshipId = customerInfo.TownshipId;
                            logInView.LogInUserStatus = Constant.UserType.Customers;
                            break;
                        case Constant.UserType.Doctors:
                            var doctorInfo = _dbContext.Doctor.FirstOrDefault(w => w.ApplicationUserId.Equals(user.Id));
                            logInView.Id = doctorInfo?.Id.ToString();
                            logInView.UserId = doctorInfo?.ApplicationUserId.ToString();
                            logInView.Address = doctorInfo?.Address;
                            logInView.UserImageStr = doctorInfo?.UserImage;
                            logInView.CustomerName = user.UserName;
                            logInView.FullName = user.FullName;
                            logInView.Gender = doctorInfo.Gender;
                            logInView.Day = doctorInfo.DateOfBirth.Day;
                            logInView.Month = doctorInfo.DateOfBirth.Month;
                            logInView.Year = doctorInfo.DateOfBirth.Year;
                            logInView.TownshipId = doctorInfo.TownshipId;
                            logInView.LogInUserStatus = Constant.UserType.Doctors;
                            break;
                    }
                    var jsonInfo = JsonConvert.SerializeObject(logInView);
                    var existingUserJson = await _cache.GetStringAsync("LoggedInUser");
                    if (!existingUserJson.IsNullOrEmpty()) await _cache.RemoveAsync("LoggedInUser");
                    //context
                    await _cache.SetStringAsync("LoggedInUser", jsonInfo, cacheOptions);
                    var companyInfo = _dbContext.CompanyInformation.Select(s => s).FirstOrDefault();
                    await _cache.SetStringAsync("CompanyInfo", JsonConvert.SerializeObject(companyInfo), cacheOptions);
                    var adminHomeUrl = Url.Action("Index", "DashBoard");
                    data.Success = true;
                    data.Message = adminHomeUrl;
                    data.MessageStatus = MessageStatus.Success;
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
            var existingUserJson = await _cache.GetStringAsync("LoggedInUser");
            if (!existingUserJson.IsNullOrEmpty()) await _cache.RemoveAsync("LoggedInUser");
            if (returnUrl != null)  return LocalRedirect(returnUrl); 
            else return RedirectToAction("Login", "Account");
        }

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        [HttpPost]
        public async Task<ActionResult> Update(CustomerViewModel viewModel)
        {
            ReturnMessageViewModel<string> data = new ReturnMessageViewModel<string>();
            try
            {
                ApplicationUser entity = _dbContext.ApplicationUser.Where(w=>w.Id.ToString().Equals(viewModel.Id)).FirstOrDefault();
                entity.UserName = viewModel.CustomerName;
                entity.Email = viewModel.Email;
                entity.PhoneNumber = viewModel.PhoneNo;
                entity.FullName = viewModel.FullName;
                entity.UpdatedUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                entity.UpdatedDate = DateTime.Now;
                _dbContext.Update(entity);
                switch (viewModel.LogInUserStatus)
                {
                    case Constant.UserType.Customers:
                        var updateCustomerInfo = _dbContext.Customer.FirstOrDefault(w => w.ApplicationUserId.ToString().Equals(viewModel.Id));
                        updateCustomerInfo.DateOfBirth = new DateTime(viewModel.Year, viewModel.Month, viewModel.Day);
                        updateCustomerInfo.UserImage = $"{Guid.NewGuid().ToString()}{Path.GetExtension(viewModel.UserImage.FileName)}";
                        updateCustomerInfo.UpdatedUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        updateCustomerInfo.UpdatedDate = DateTime.Now;
                        _dbContext.Update(updateCustomerInfo);
                        break;
                    case Constant.UserType.Staffs:
                        var updateStaffsInfo = _dbContext.Staff.FirstOrDefault(w => w.ApplicationUserId.ToString().Equals(viewModel.Id));
                        updateStaffsInfo.DateOfBirth = new DateTime(viewModel.Year, viewModel.Month, viewModel.Day);
                        updateStaffsInfo.UserImage = $"{Guid.NewGuid().ToString()}{Path.GetExtension(viewModel.UserImage.FileName)}";
                        updateStaffsInfo.UpdatedUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        updateStaffsInfo.UpdatedDate = DateTime.Now;
                        _dbContext.Update(updateStaffsInfo);
                        break;
                    case Constant.UserType.Doctors:
                        var updateDoctorInfo = _dbContext.Doctor.FirstOrDefault(w => w.ApplicationUserId.ToString().Equals(viewModel.Id));
                        updateDoctorInfo.DateOfBirth = new DateTime(viewModel.Year, viewModel.Month, viewModel.Day);
                        updateDoctorInfo.UserImage = $"{Guid.NewGuid().ToString()}{Path.GetExtension(viewModel.UserImage.FileName)}";
                        updateDoctorInfo.UpdatedUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        updateDoctorInfo.UpdatedDate = DateTime.Now;
                        _dbContext.Update(updateDoctorInfo);
                        break;
                }
                var transaction = await _dbContext.SaveChangesAsync();
                if (transaction == 0)
                {
                    data.Success = false;
                    data.MessageStatus = MessageStatus.Error; 
                    data.Message = _localizer["ME00001"].ToString();
                }
                else
                {
                    data.Success = true;
                    data.MessageStatus = MessageStatus.Success;
                    data.Message = _localizer["MI00004"].ToString();
                }
            }
            catch (Exception ex)
            {
                data.Success = false;
                data.Message = ex.Message;
                data.MessageStatus = MessageStatus.Info;
            }
            return new JsonResult(data);
        }

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public async Task<JsonResult> EditCurrentUser()
        {
            var existingUserJson = await _cache.GetStringAsync("LoggedInUser");
            var loggedUserInfo = JsonConvert.DeserializeObject<CustomerViewModel>(existingUserJson);
            var appUserInfo = _dbContext.ApplicationUser.FirstOrDefault(w => w.Id.ToString().Equals(loggedUserInfo.UserId));
            var model = new CustomerViewModel();
            model.Id = loggedUserInfo.Id;
            model.CustomerName = loggedUserInfo.CustomerName;
            model.Email = appUserInfo.Email;
            model.FullName = loggedUserInfo.FullName;
            model.PhoneNo = appUserInfo.PhoneNumber;
            model.Gender = loggedUserInfo.Gender;
            model.Day = loggedUserInfo.Day;
            model.Month = loggedUserInfo.Month;
            model.Year = loggedUserInfo.Year;
            model.UserImageStr = $"/images/{loggedUserInfo.UserImageStr}";
            model.DistrictId = loggedUserInfo.DistrictId;
            model.DivisionId = loggedUserInfo.DivisionId;
            model.TownshipId = loggedUserInfo.TownshipId;
            model.Address = loggedUserInfo.Address;
            return new JsonResult(model);
        }
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            ReturnMessageViewModel<string> data = new ReturnMessageViewModel<string>();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var user = _dbContext.ApplicationUser.FirstOrDefault(w=>w.UserName.Equals(model.UserName));
                if (user != null)
                {
                    PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
                    user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
                    user.UpdatedDate = DateTime.Now;
                    user.UpdatedUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    _dbContext.Update(user);
                    var transaction = await _dbContext.SaveChangesAsync();
                    if (transaction == 0)
                    {
                        data.Success = false;
                        data.MessageStatus = MessageStatus.Error;
                        data.Message = _localizer["ME00001"].ToString();
                    }
                    else
                    {
                        data.Success = true;
                        data.MessageStatus = MessageStatus.Success;
                        data.Message = _localizer["MI00003"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                data.Success = false;
                data.Message = ex.Message;
                data.MessageStatus = MessageStatus.Info;
            }
            return new JsonResult(data);
        }
    }
}
