using HocWeb.Service.Models;
using HocWeb.Service.Models.Authenticate;

namespace HocWeb.Service.Interfaces
{
    public interface IAuthenticateService
    {
        Task<ApiResult> Register(RegisterModel model);
        Task<ApiResult> Login(LoginModel model);
    }
}
