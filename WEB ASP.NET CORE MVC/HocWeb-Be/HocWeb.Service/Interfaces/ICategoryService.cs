using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Category;

namespace HocWeb.Service.Interfaces
{
    public interface ICategoryService : IServiceBase<Category>
    {
        Task<ApiResult> Add(AddCategoryModel model);
        Task<ApiResult> Update(UpdateCategoryModel model);
        Task<ApiResult> GetAll2();
    }
}
