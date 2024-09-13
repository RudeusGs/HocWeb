using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HocWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : BaseController
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userService)
        {
            _userManagementService = userService;
        }

        [Authorize]
        [HttpPost("update-info")]
        public async Task<IActionResult> Add([FromBody] UpdateInfoModel model)
        {
            try
            {
                var result = await _userManagementService.UpdateInfo(model);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                var result = await _userManagementService.ChangePassword(model);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
