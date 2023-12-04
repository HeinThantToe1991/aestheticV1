using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data;
using UI_Layer.Globalizer;
using UI_Layer.Mapper;
using UI_Layer.Models;
using UI_Layer.Resources;
using System.Net;

namespace UI_Layer.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class CompanyInformationController : Controller
    {
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ApplicationDbContext _dbContext;

        public CompanyInformationController(IStringLocalizer<Resource> localizer, ApplicationDbContext dbContext)
        {
            _localizer = localizer;
            _dbContext = dbContext; 
        }
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public IActionResult Index()
        {
            return View();
        }
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        [HttpGet]
        public ActionResult GetData()
        {
            ReturnMessageViewModel<CompanyInformationViewModel> data = new ReturnMessageViewModel<CompanyInformationViewModel>();
            try
            {
                var result = _dbContext.CompanyInformation.Select(s => s).FirstOrDefault();//.Select(s=> new CompanyInformationViewModel (){CompanyName_LNG1 = s.CompanyName_LNG1,CompanyName_LNG2 = s.CompanyName_LNG2,Email=s.Email,ID= s.ID }).ToList();
                if (result == null)
                {
                    data.Success = false;
                }
                else
                {
                    data.Success = true;
                    data.DataEntity = new CompanyInformationViewModel();
                    data.DataEntity = CompanyInformationMapper.DataModelToViewModel(result);
                    data.MessageStatus = MessageStatus.Info;
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

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        [HttpPost]
        public async Task<IActionResult> SaveData([FromForm] CompanyInformationViewModel viewModel)
        {
            ReturnMessageViewModel<CompanyInformationViewModel> data = new ReturnMessageViewModel<CompanyInformationViewModel>();
            data.MessageStatus = MessageStatus.Success;
            data.Success = true;
            data.Message = _localizer["MI00003"].ToString();
            try
            {
                var createdUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(viewModel.Id) )
                {
                    var updateData = _dbContext.CompanyInformation.Select(s => s).FirstOrDefault(s => s.Id == Guid.Parse(viewModel.Id));
                    updateData.CompanyName = viewModel.CompanyName;
                    updateData.CompanyShortName = viewModel.CompanyShortName;
                    updateData.CompanyAddress = viewModel.CompanyAddress;
                    updateData.ContactPerson = viewModel.ContactPerson;
                    updateData.PhoneNumber = viewModel.PhoneNumber;
                    updateData.WebsiteLink = viewModel.WebsiteLink;
                    updateData.FacebookLink = viewModel.FacebookLink;
                    updateData.RefCompanyId = viewModel.RefCompanyId;
                    updateData.BusinessType = viewModel.BusinessType;
                    updateData.StreetNumber_Name = viewModel.StreetNumber_Name;
                    if(viewModel.CompanyLogo != null)
                    {
                        viewModel.CompanyLogo_str = $"{Guid.NewGuid().ToString()}{Path.GetExtension(viewModel.CompanyLogo.FileName)}";
                        string ftpUrl = $"ftp://localhost:91/images/{viewModel.CompanyLogo_str}"; //viewModel.CompanyLogo.FileName

                        using (Stream fileStream = viewModel.CompanyLogo.OpenReadStream())
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
                    }
                    updateData.Email = viewModel.Email;
                    updateData.District = viewModel.District;
                    updateData.StateDivision = viewModel.StateDivision;
                    updateData.Township = viewModel.Township;
                    updateData.UpdatedUserId = Guid.Parse((ReadOnlySpan<char>)createdUserId);
                    updateData.UpdatedDate = DateTime.Now;
                    _dbContext.Update(updateData);
                }
                else
                {
                    if (viewModel.CompanyLogo.Length > 0)
                    {
                        viewModel.Id = Guid.NewGuid().ToString();
                        viewModel.CreatedDate = DateTime.Now;
                        viewModel.CreatedUserId = createdUserId;
                        viewModel.CompanyLogo_str = $"{Guid.NewGuid().ToString()}{Path.GetExtension(viewModel.CompanyLogo.FileName)}";
                        string ftpUrl = $"ftp://localhost:91/images/{viewModel.CompanyLogo_str}" ; //viewModel.CompanyLogo.FileName

                        using (Stream fileStream = viewModel.CompanyLogo.OpenReadStream())
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
                    }
                    var saveData = CompanyInformationMapper.ViewModelToDataModel(viewModel);
                    await _dbContext.AddAsync(saveData);
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
                    data.Message = _localizer["MI00003"].ToString();
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
    }


}
