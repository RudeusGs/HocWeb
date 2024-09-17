using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Authenticate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HocWeb.Service.Interfaces
{
    public interface IAccountService : IServiceBase<User>
    {
        Task<ApiResult> AddUser(RegisterModel model);
        Task<ApiResult> DeleteUser(int userId);
        Task<ApiResult> UpdateUser(User user);
        Task<ApiResult> AssignRoles(int userId, List<string> roleNames);
        Task<ApiResult> GetRoles(int userId);
    }
}
