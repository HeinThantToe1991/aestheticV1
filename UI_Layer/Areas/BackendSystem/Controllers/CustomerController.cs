using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Security.Claims;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data;
using UI_Layer.Globalizer;
using UI_Layer.Mapper;
using UI_Layer.Models;
using UI_Layer.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UI_Layer.Data.AppData;
using UI_Layer.Data.IdentityModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;

namespace UI_Layer.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class CustomerController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomerController(IWebHostEnvironment webHostEnvironment, IStringLocalizer<Resource> localizer, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public IActionResult Index()
        {
            return View();
        }


        public JsonResult GetGender(string search)
        {
            
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "data/gender.txt");
            var genderList = new List<object>();
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var trimmedLine = line?.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmedLine))
                        {
                            genderList.Add(new {id = trimmedLine, text = trimmedLine });
                        }
                    }
                }
                if (!string.IsNullOrEmpty(search))
                {
                    var result = genderList.Where(x => x.GetType().GetProperty("text") != null && x.GetType().GetProperty("text").GetValue(x, null)?.ToString().ToLower().Contains(search.ToLower()) == true).ToList();
                    return Json(result);
                }
                return Json(genderList);
            }
            catch (Exception)
            {
                // Handle exceptions, e.g., file not found, invalid format, etc.
                return Json(genderList);
            }
        }

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] CustomerViewModel model)
        {
            ReturnMessageViewModel<ApplicationUser> data = new ReturnMessageViewModel<ApplicationUser>();
            data.MessageStatus = MessageStatus.Success;
            data.Success = true;
            data.Message = _localizer["MI00003"].ToString();
            try
            {
                RegisterViewModel registerViewModel = new RegisterViewModel();
                registerViewModel.UserName = model.CustomerName.Trim();
                registerViewModel.FullName = model.FullName;
                registerViewModel.Email = model.Email;
                registerViewModel.Password = HttGlobalizer.GenerateRandomString(6);
                registerViewModel.ConfirmPassword = registerViewModel.Password;
                model.DateOfBirth = new DateTime(model.Year, model.Month, model.Day);
                model.UserImageStr = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.UserImage.FileName)}";
                model.CreatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.CreatedDate = DateTime.Now;

                var registerStatus = await Register(registerViewModel);
                if (registerStatus.Success)
                {
                    model.UserId = registerStatus.DataEntity.Id.ToString();
                    string ftpUrl = $"ftp://localhost:91/images/{model.UserImageStr}"; //viewModel.CompanyLogo.FileName
                    using (Stream fileStream = model.UserImage.OpenReadStream())
                    {
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        using (Stream ftpStream = request.GetRequestStream())
                        {
                            fileStream.CopyTo(ftpStream);
                        }

                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            if (response.StatusCode != FtpStatusCode.ClosingData &&
                                response.StatusCode != FtpStatusCode.FileActionOK)
                            {
                                // The FTP upload was not successful
                                data.Success = false;
                                data.MessageStatus = MessageStatus.Error;
                                data.Message = response.StatusDescription;
                                return new JsonResult(data);
                            }
                        }
                    }
                    model.Id = Guid.NewGuid().ToString();
                    model.Active = true;
                    model.DeleteRemark = string.Empty;
                    var saveData = CustomerMapper.ViewModelToDataModel(model);
                    await _dbContext.AddAsync(saveData);
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
                else
                {
                    data = registerStatus;
                }
            }
            catch (Exception e)
            {
                data.Success = false;
                data.MessageStatus = MessageStatus.Error;
                data.Message = e.Message;
            }
            return new JsonResult(data);
        }

        private async Task<ReturnMessageViewModel<ApplicationUser>> Register(RegisterViewModel model)
        {
            ReturnMessageViewModel<ApplicationUser> data = new ReturnMessageViewModel<ApplicationUser>();
           
            var isExists = await _userManager.FindByEmailAsync(model.Email);
            if (isExists != null)
            {
                data.Success = false;
                data.Message = _localizer["MI00011"].ToString();
                data.MessageStatus = MessageStatus.Info;
                return data;
            }
            isExists = await _userManager.FindByNameAsync(model.UserName);
            if (isExists != null)
            {
                data.Success = false;
                data.Message = _localizer["MI00011"].ToString();
                data.MessageStatus = MessageStatus.Info;
                return data;
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
                data.DataEntity = user;
                data.Success = true;
                data.Message = "Registration Successful!.";
                data.MessageStatus = MessageStatus.Success;
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
            }
            return data;
        }

        [HttpPost]
        public ActionResult CustomerInfoReport(CustomerFilterViewModel viewModel)
        {
            ReturnMessageViewModel<CustomerViewModel> data = new()
            {
                DataList = new List<CustomerViewModel>(),
                MessageStatus = MessageStatus.Info,
                Success = true,
                Message = _localizer["MI00003"].ToString()
            };
       
            try
            {
                if (!_dbContext.Customer.Any() )
                {
                    data.MessageStatus = MessageStatus.Info;
                    data.Success = false;
                    data.Message = _localizer["MI00001"].ToString();
                }
                var _list = new List<CustomerDM>();
                var fromDate = DateTime.ParseExact(viewModel.FilterFromValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var toDate = DateTime.ParseExact(viewModel.FilterToValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(viewModel.FilterKey) )
                {
                    switch (viewModel.FilterKey)
                    {
                        case "CreatedDate":
                            _list = _dbContext.Customer
                                .Where(w => w.CreatedDate.Date >= fromDate && w.CreatedDate.Date <= toDate)
                                .Include(customerDm => customerDm.ApplicationUser)
                                .ToList();
                            break;
                        case "Book":

                            break;
                        case "Appointment":

                            break;
                        case "Birth":
                            _list = _dbContext.Customer
                                .Where(w => w.DateOfBirth.ToString("yyyy/MM/dd").CompareTo(fromDate) >= 0 && w.DateOfBirth.ToString("yyyy/MM/dd").CompareTo(toDate) <= 0)
                                .Include(customerDm => customerDm.ApplicationUser)
                                .ToList();
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(viewModel.Filter1Key))
                {
                    if (viewModel.FilterEquation.Equals("Contain"))
                    {
                        switch (viewModel.Filter1Key)
                        {
                            case "Name":
                                if (_list.Count > 0)
                                {
                                    _list = _list.Where(w => w.ApplicationUser.FullName.ToUpper().Contains(viewModel.Filter1Value1.ToUpper())).ToList();
                                }
                                else
                                {
                                    _list = _dbContext.Customer.Where(w => w.ApplicationUser.FullName.ToUpper().Contains(viewModel.Filter1Value1.ToUpper()))
                                        .Include(customerDm => customerDm.ApplicationUser)
                                        .ToList();
                                }

                                break;
                            case "Email":
                                if (_list.Count > 0)
                                {
                                    _list = _list.Where(w => w.ApplicationUser.Email.ToUpper().Contains(viewModel.Filter1Value1.ToUpper())).ToList();
                                }
                                else
                                {
                                    _list = _dbContext.Customer.Where(w => w.ApplicationUser.Email.ToUpper().Contains(viewModel.Filter1Value1.ToUpper()))
                                        .Include(customerDm => customerDm.ApplicationUser).ToList();
                                }
                                break;
                            case "Phone":
                                if (_list.Count > 0)
                                {
                                    _list = _list.Where(w => w.ApplicationUser.PhoneNumber.Contains(viewModel.Filter1Value1)).ToList();
                                }
                                else
                                {
                                    _list = _dbContext.Customer.Where(w => w.ApplicationUser.PhoneNumber.Contains(viewModel.Filter1Value1))
                                        .Include(customerDm => customerDm.ApplicationUser).ToList();
                                }
                                break;
                            case "Address":
                                if (_list.Count > 0)
                                {
                                    _list = _list.Where(w => w.Address.ToUpper().Contains(viewModel.Filter1Value1.ToUpper())).ToList();
                                }
                                else
                                {
                                    _list = _dbContext.Customer.Where(w => w.Address.ToUpper().Contains(viewModel.Filter1Value1.ToUpper()))
                                        .Include(customerDm => customerDm.ApplicationUser).ToList();
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (viewModel.Filter1Key)
                        {
                            case "Name":
                                if (_list.Count > 0)
                                {
                                    _list = _list.Where(w => w.ApplicationUser.FullName.ToUpper().Equals(viewModel.Filter1Value1.ToUpper())).ToList();
                                }
                                else
                                {
                                    _list = _dbContext.Customer.Where(w => w.ApplicationUser.FullName.ToUpper().Equals(viewModel.Filter1Value1.ToUpper()))
                                        .Include(customerDm => customerDm.ApplicationUser)
                                        .ToList();
                                }

                                break;
                            case "Email":
                                if (_list.Count > 0)
                                {
                                    _list = _list.Where(w => w.ApplicationUser.Email.ToUpper().Equals(viewModel.Filter1Value1.ToUpper())).ToList();
                                }
                                else
                                {
                                    _list = _dbContext.Customer.Where(w => w.ApplicationUser.Email.ToUpper().Equals(viewModel.Filter1Value1.ToUpper()))
                                        .Include(customerDm => customerDm.ApplicationUser).ToList();
                                }
                                break;
                            case "Phone":
                                if (_list.Count > 0)
                                {
                                    _list = _list.Where(w => w.ApplicationUser.PhoneNumber.Equals(viewModel.Filter1Value1)).ToList();
                                }
                                else
                                {
                                    _list = _dbContext.Customer.Where(w => w.ApplicationUser.PhoneNumber.Equals(viewModel.Filter1Value1))
                                        .Include(customerDm => customerDm.ApplicationUser).ToList();
                                }
                                break;
                            case "Address":
                                if (_list.Count > 0)
                                {
                                    _list = _list.Where(w => w.Address.ToUpper().Equals(viewModel.Filter1Value1.ToUpper())).ToList();
                                }
                                else
                                {
                                    _list = _dbContext.Customer.Where(w => w.Address.ToUpper().Equals(viewModel.Filter1Value1.ToUpper()))
                                        .Include(customerDm => customerDm.ApplicationUser).ToList();
                                }
                                break;
                        }
                    }
                    
                }

                foreach (var item in _list)
                {
                    CustomerViewModel outputVM = new CustomerViewModel();
                    outputVM.CustomerName = item.ApplicationUser.UserName;
                    outputVM.FullName = item.ApplicationUser.FullName;
                    outputVM.Email = item.ApplicationUser.Email;
                    outputVM.PhoneNo = item.ApplicationUser.PhoneNumber;
                    outputVM.Address = item.Address;
                    outputVM.DateOfBirth = item.DateOfBirth;
                    outputVM.Gender = item.Gender;
                    data.DataList.Add(outputVM);
                }
            }
            catch (Exception ex)
            {
                data.MessageStatus = MessageStatus.Info;
                data.Success = false;
                data.Message = Globalizer.CommonMethods.GetInnerException(ex, _localizer["MI00023"].ToString());
            }

            return Json(data);
        }

        public ActionResult CustomerInfoFilterReport(string viewModel)
        {
            List<CustomerViewModel> viewModelList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(viewModel);

            ViewBag.PrintDate = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.List = viewModelList.ToList();
            #region CompanyInformation            
            var companyInformation = _dbContext.CompanyInformation.Select(s=>s).FirstOrDefault();
            ViewBag.Company = companyInformation;
            #endregion
            return View();
        }
    }

 
}
