using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace HocWeb.Service.Services
{
    public class SupplierService : BaseService, ISupplierService
    {

        public SupplierService(DataContext dataContext, Common.IServices.IUserService userService) : base(dataContext, userService) { }
 

        public async Task<ApiResult> Add(Supplier model)
        {
            var existingSupplier = await _dataContext.Suppliers.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (existingSupplier != null)
            {
                return new ApiResult() { Message = "Nhà cung cấp này đã tồn tại." };
            }

            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                model.CreatedDate = _now;
                await _dataContext.Suppliers.AddAsync(model);
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

        public async Task<ApiResult> Delete(int id)
        {
            var supplier = await _dataContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null)
            {
                return new ApiResult() { Message = "Không tìm thấy nhà cung cấp này." };
            }

            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                supplier.DeleteDate = _now;
                //_dataContext.Suppliers.Remove(supplier);
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

        public async Task<ApiResult> GetAll()
        {
            var result =  await _dataContext.Suppliers.Exist().ToListAsync();
            return new(result);

        }

        public async Task<ApiResult> GetById(int id)
        {
            var result =  await _dataContext.Suppliers.Exist()
                .FirstOrDefaultAsync(x => x.Id == id);
            return new(result);

        }

        public async Task<ApiResult> Update(Supplier model)
        {
            var supplier = await _dataContext.Suppliers.Exist()
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (supplier == null)
            {
                return new ApiResult() { Message = "Không tìm thấy nhà cung cấp này." };
            }

            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                supplier.Name = model.Name;
                supplier.Logo = model.Logo;
                supplier.Email = model.Email;
                supplier.Phone = model.Phone;
                supplier.UpdatedDate = _now;

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
    }
}
