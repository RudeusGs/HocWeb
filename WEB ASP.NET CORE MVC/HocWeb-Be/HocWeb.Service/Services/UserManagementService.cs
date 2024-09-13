using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using HocWeb.Service.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class UserManagementService : BaseService, IUserManagementService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserManagementService(DataContext dataContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            Common.IServices.IUserService userService) : base(dataContext, userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApiResult> ChangePassword(ChangePasswordModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new() { Message = $"Không tìm thấy user: {model.UserName}" };
            }

            using var tran = _dataContext.Database.BeginTransaction();
            try
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.OldPassword, false, lockoutOnFailure: false);
                if (!signInResult.Succeeded)
                {
                    return new() { Message = $"Mật khẩu củ không chính xác" };
                }
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await tran.CommitAsync();
                    return new(result);

                }
                var err = result.Errors.Select(x => x.Description);
                await tran.RollbackAsync();
                return new() { Message = string.Join('\n', err) };
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                return new() { Message = ex.Message };
            }
        }

        public async Task<ApiResult> UpdateInfo(UpdateInfoModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new() { Message = $"Không tìm thấy user: {model.UserName}" };
            }

            using var tran = _dataContext.Database.BeginTransaction();
            try
            {
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.UpdatedDate = _now;
                await _dataContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new();
            }
            catch (Exception ex)
            {
                await tran.RollbackAsync();
                return new() { Message = ex.Message };
            }

        }
    }
}
