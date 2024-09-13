using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Common.Services;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HocWeb.Service.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IFtpDirectoryService _ftpDirectoryService;

        public CategoryService(DataContext dataContext, IFtpDirectoryService ftpDirectoryService,
            Common.IServices.IUserService userService) : base(dataContext, userService)
        {
            _ftpDirectoryService = ftpDirectoryService;
        }

        public async Task<ApiResult> Add(Category model)
        {
            var cateGory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (cateGory == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    model.CreatedDate = _now;
                    _dataContext.Categories.Add(model);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(model);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new()
            {
                Message = "Category này đã tồn tại",
            };
        }

        public async Task<ApiResult> Add(AddCategoryModel model)
        {
            var cateGory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (cateGory == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {

                    var newCate = new Category()
                    {
                        Name = model.Name,
                        NameVN = model.NameVN,
                        Icon = model.Icon,
                        CreatedDate = _now,
                    };
                    _dataContext.Categories.Add(newCate);
                    await _dataContext.SaveChangesAsync();

                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(newCate.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            newCate.Image = fileUpload;
                        }
                    }

                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(newCate);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new()
            {
                Message = "Category này đã tồn tại",
            };
        }

        private async Task<string?> UploadFile(int? cateId, IFormFile? file)
        {
            if (file == null || !cateId.HasValue)
            {
                return string.Empty;
            }
            var fileExt = Path.GetExtension(file.FileName);
            var stream = file.OpenReadStream();
            var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, $"public/category", $"{cateId}{fileExt}");
            if (result.Succeed)
            {
                return $"category/{cateId}{fileExt}";
            }
            return string.Empty;
        }

        public async Task<ApiResult> Delete(int id)
        {
            var cateGory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (cateGory != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    cateGory.DeleteDate = _now;
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new()
            {
                Message = "Không tìm thấy Category này"
            };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Categories.Exist().ToListAsync();
            return new(result);
        }

        [Authorize]
        public async Task<ApiResult> GetAll2()
        {
            var result = await _dataContext.Categories
                              .OrderBy(category => category.DeleteDate.HasValue ? 1 : 0)
                              .ThenBy(category => category.DeleteDate)
                              .ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Update(Category model)
        {
            var cateGory = await _dataContext.Categories.Exist()
                             .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (cateGory != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    cateGory.Name = model.Name;
                    cateGory.Image = model.Image;
                    cateGory.Icon = model.Icon;
                    cateGory.NameVN = model.NameVN;
                    cateGory.UpdatedDate = _now;

                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new() { Message = "Không tìm thấy Category này" };
        }

        public async Task<ApiResult> Update(UpdateCategoryModel model)
        {
            var cateGory = await _dataContext.Categories
                             .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (cateGory != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    if (model.IsDelete == true)
                    {
                        cateGory.DeleteDate = _now;
                        await _dataContext.SaveChangesAsync();
                        await tran.CommitAsync();
                        return new();
                    }

                    if (string.IsNullOrEmpty(model.Name)
                        && string.IsNullOrEmpty(model.Icon)
                        && string.IsNullOrEmpty(model.NameVN)
                        && model.File == null)
                    {
                        return new() { Message = "Không có thông tin nào được cập nhật" };
                    }

                    var findCate = await _dataContext.Categories.Exist().FirstOrDefaultAsync(x =>
                    !string.IsNullOrEmpty(model.Name) && x.Name == model.Name && x.Id != model.Id);
                    if (findCate != null)
                    {
                        return new() { Message = "Tên Danh mục này đã tồn tại" };
                    }

                    cateGory.Name = model.Name ?? cateGory.Name;
                    cateGory.Icon = model.Icon ?? cateGory.Icon;
                    cateGory.NameVN = model.NameVN ?? cateGory.NameVN;
                    cateGory.UpdatedDate = _now;

                    if (model.IsDelete == false)
                    {
                        cateGory.DeleteDate = null;
                    }

                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(cateGory.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            cateGory.Image = fileUpload;
                        }
                    }

                    await _dataContext.SaveChangesAsync();

                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new() { Message = "Không tìm thấy Category này" };
        }
    }
}