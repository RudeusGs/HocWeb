using HocWeb.Service.Models;
using HocWeb.Service.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Interfaces
{
    public interface IUserManagementService
    {
        Task<ApiResult> UpdateInfo(UpdateInfoModel model);
        Task<ApiResult> ChangePassword(ChangePasswordModel model);
    }
}
