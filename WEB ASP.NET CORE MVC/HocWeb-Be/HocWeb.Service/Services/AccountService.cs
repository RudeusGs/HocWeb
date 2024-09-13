using HocWeb.Infrastructure.Constants;
using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Authenticate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AccountService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResult> AssignRoles(int userId, List<string> roleNames)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiResult { Message = $"Không tìm thấy người dùng với ID {userId}." };
                }

                var rolesToAdd = new List<string>();

                foreach (var roleName in roleNames)
                {
                    var normalizedRoleName = roleName.ToUpper();
                    if (!await _roleManager.RoleExistsAsync(normalizedRoleName))
                    {
                        return new ApiResult { Message = $"Vai trò '{roleName}' không tồn tại." };
                    }

                    if (!await _userManager.IsInRoleAsync(user, normalizedRoleName))
                    {
                        rolesToAdd.Add(normalizedRoleName);
                    }
                }

                if (rolesToAdd.Any())
                {
                    var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                    if (addResult.Succeeded)
                    {
                        return new ApiResult();
                    }
                    else
                    {
                        return new ApiResult { Message = string.Join(", ", addResult.Errors.Select(e => e.Description)) };
                    }
                }
                else
                {
                    return new ApiResult { Message = "Người dùng đã có tất cả các vai trò được chỉ định." };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }

        public async Task<ApiResult> GetAll()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userRoles = new List<object>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    userRoles.Add(new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.FullName,
                        Roles = roles
                    });
                }

                return new ApiResult(userRoles);
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }


        public async Task<ApiResult> GetById(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return new ApiResult { Message = $"Không tìm thấy người dùng với ID {id}." };
                }

                return new ApiResult(user);
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }

        public async Task<ApiResult> AddUser(RegisterModel model)
        {
            try
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return new ApiResult(user);
                }
                else
                {
                    return new ApiResult { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }

        public async Task<ApiResult> UpdateUser(User model)
        {
            try
            {
                var result = await _userManager.UpdateAsync(model);
                if (result.Succeeded)
                {
                    return new ApiResult();
                }
                else
                {
                    return new ApiResult { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }

        public async Task<ApiResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return new ApiResult { Message = $"Không tìm thấy người dùng với ID: {id}." };
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return new ApiResult();
                }
                else
                {
                    return new ApiResult { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }
        public async Task<ApiResult> Add(User model)
        {
            try
            {
                var result = await _userManager.CreateAsync(model);
                if (result.Succeeded)
                {
                    return new ApiResult(model);
                }
                else
                {
                    return new ApiResult { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }

        public async Task<ApiResult> Update(User model)
        {
            try
            {
                var result = await _userManager.UpdateAsync(model);
                if (result.Succeeded)
                {
                    return new ApiResult();
                }
                else
                {
                    return new ApiResult { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }

        public async Task<ApiResult> Delete(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return new ApiResult { Message = $"Không tìm thấy người dùng với ID: {id}." };
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return new ApiResult();
                }
                else
                {
                    return new ApiResult { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }
        public async Task<ApiResult> GetRoles(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new ApiResult { Message = $"Không tìm thấy người dùng với ID {userId}." };
                }

                var roles = await _userManager.GetRolesAsync(user);
                return new ApiResult(roles);
            }
            catch (Exception ex)
            {
                return new ApiResult { Message = ex.Message };
            }
        }
    }
}
