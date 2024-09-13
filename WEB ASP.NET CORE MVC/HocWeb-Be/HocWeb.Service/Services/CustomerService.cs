using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace HocWeb.Service.Services
{
    internal class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(DataContext dataContext, Common.IServices.IUserService userService) : base(dataContext, userService) { }

        public async Task<ApiResult> Add(Customer model)
        {
            var cusTomer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (cusTomer == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();

                try
                {
                    model.CreatedDate = _now; // hoi thay cach link voi thang entitybase
                    _dataContext.Customers.Add(model); // chu y thay cusTomer = model
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(model);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }

            }
            return new() { Message = "Khách hàng này đã tồn tại." };
        }

        public async Task<ApiResult> Delete(int id)
        {
            var cusTomer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (cusTomer != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();

                try
                {
                    //_dataContext.Customers.Remove(cusTomer);
                    cusTomer.DeleteDate = _now;
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }

            }
            return new ApiResult() { Message = "Khách hàng này không tồn tại." };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Customers.Exist().ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Customers.Exist()
                .FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Update(Customer model)
        {
            var cusTomer = await _dataContext.Customers.Exist().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (cusTomer != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();

                try
                {
                    cusTomer.Activated = model.Activated;
                    cusTomer.Photo = model.Photo;
                    cusTomer.Email = model.Email;
                    cusTomer.Fullname = model.Fullname;
                    cusTomer.Password = model.Password;
                    cusTomer.Id = model.Id;
                    cusTomer.UpdatedDate = _now;

                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }

            }

            return new() { Message = "Khách hàng này không tồn tại." };
        }
    }
}
