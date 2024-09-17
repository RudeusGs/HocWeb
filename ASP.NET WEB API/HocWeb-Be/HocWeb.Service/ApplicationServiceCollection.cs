using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Common.Services;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HocWeb.Service
{
    public static class ApplicationServiceCollection
    {
        /// <summary>
        /// Adds the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Common services
            // JWT
            services.AddScoped<IJwtService, JwtService>();

            // User Management Service.
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>, SignInManager<User>>();
            services.AddScoped<Common.IServices.IUserService, UserService>();

            // Ftp
            services.AddScoped<IFtpDirectoryService, FtpDirectoryService>();
            #endregion

            #region Business services
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IAccountService, AccountService>();
            #endregion
            return services;
        }
    }
}
