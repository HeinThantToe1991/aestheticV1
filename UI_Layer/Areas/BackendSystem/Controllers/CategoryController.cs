using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data;
using UI_Layer.Data.AppData;
using UI_Layer.Data.IdentityModel;
using UI_Layer.Globalizer;
using UI_Layer.Mapper;
using UI_Layer.Models;
using UI_Layer.Resources;


namespace UI_Layer.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class CategoryController : Controller
    {
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoryController(IStringLocalizer<Resource> localizer, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _localizer = localizer;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public IActionResult Index()
        {
            return View();
        }

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        [HttpGet]
        public async Task<ActionResult> GetData()
        {
            ReturnMessageViewModel<CategoryViewModel> data = new ReturnMessageViewModel<CategoryViewModel>();
            try
            {
                var result = _dbContext.Category.ToList();//
                if (result == null)
                {
                    data.Success = false;
                }
                else
                {
                    data.Success = true;
                    data.DataList = new List<CategoryViewModel>();
                    data.DataList = await CategoryMapper.DataModelToViewModel_List(result, _userManager);
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
        public async Task<ActionResult> Add(CategoryViewModel viewModel)
        {
            ReturnMessageViewModel<CategoryViewModel> data = new ReturnMessageViewModel<CategoryViewModel>();
            data.MessageStatus = MessageStatus.Success;
            data.Success = true;
            data.Message = _localizer["MI00003"].ToString();
            try
            {
                var isRecordExists = _dbContext.Category.Select(s => s.Code.Equals(viewModel.Code)).FirstOrDefault();
                if (isRecordExists)
                {
                    data.MessageStatus = MessageStatus.Info;
                    data.Success = false;
                    data.Message = _localizer["MV00006"].ToString();
                    return new JsonResult(data);
                }

                CategoryDM entity = new CategoryDM();
                entity.Id = Guid.NewGuid();
                entity.Code = viewModel.Code.ToUpper();
                entity.Description = viewModel.Description;
                entity.Active = true;
                entity.CreatedUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                entity.CreatedDate = DateTime.Now;
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
                _dbContext.AddAsync(entity);
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
            catch (Exception ex)
            {
                data.MessageStatus = MessageStatus.Info;
                data.Success = false;
                data.Message = Globalizer.CommonMethods.GetInnerException(ex, _localizer["MI00023"].ToString());
            }
            return Json(data);
        }

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public async Task<JsonResult> Edit(string id)
        {
            var data = _dbContext.Category.FirstOrDefault(s => s.Id.ToString().Equals(id));
            var viewModel = await CategoryMapper.DataModelToViewModel(data, _userManager);
            
            return new JsonResult(viewModel);
        }

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        [HttpPost]
        public async Task<ActionResult> Update(CategoryViewModel viewModel)
        {
            ReturnMessageViewModel<CategoryViewModel> data = new ReturnMessageViewModel<CategoryViewModel>();

            try
            {
                var entity = _dbContext.Category.FirstOrDefault(s => s.Id.ToString().Equals(viewModel.Id));
                entity.Code = viewModel.Code;
                entity.Description = viewModel.Description;
                entity.UpdatedUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                entity.UpdatedDate = DateTime.Now;
                _dbContext.Update(entity);
                var transaction = await _dbContext.SaveChangesAsync();
                if (transaction == 1)
                {
                    data.MessageStatus = MessageStatus.Success;
                    data.Success = true;
                    data.Message = _localizer["MI00004"].ToString();
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
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public async Task<ActionResult> Delete(string id)
        {
            ReturnMessageViewModel<CategoryViewModel> data = new ReturnMessageViewModel<CategoryViewModel>();
            try
            {
                var entity = _dbContext.Category.FirstOrDefault(s => s.Id.ToString().Equals(id));
                _dbContext.Remove(entity);
                var transaction = await _dbContext.SaveChangesAsync();
                if (transaction == 1)
                {
                    data.MessageStatus = MessageStatus.Success;
                    data.Success = true;
                    data.Message = _localizer["MI00005"].ToString();
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
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public JsonResult GetDataForCombo(string search)
        {
            var _list = _dbContext.Category.Select(s => s).ToList();
            var result = (from data in _list
                          select new
                          {
                              id = data.Id,
                              text = data.Code + "     :     " + data.Description
                          }).OrderBy(o => o.text).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(x => x.text.ToLower().Contains(search.ToLower())).ToList();
            }
            return Json(result);
        }
    }
}
