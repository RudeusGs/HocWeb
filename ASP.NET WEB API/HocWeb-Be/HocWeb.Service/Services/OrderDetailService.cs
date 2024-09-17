using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace HocWeb.Service.Services
{
    public class OrderDetailService : BaseService, IOrderDetailService
    {
        public OrderDetailService(DataContext context, Common.IServices.IUserService userService) : base(context,userService) { }

        public async Task<ApiResult> Add(OrderDetail model)
        {
            var order = await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    model.CreatedDate = _now;
                    _dataContext.OrderDetails.Add(model);
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
            return new ApiResult() { Message = "OrderDetail này đã tồn tại" };
        }

        public async Task<ApiResult> Delete(int id)
        {
            var order = await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    order.DeleteDate = _now;
                    // _dataContext.Orders.Remove(order);
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

            return new ApiResult() { Message = "OrderDetail này không tồn tại" };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.OrderDetails.Exist().ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.OrderDetails.Exist()
              .FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> GetOrderDetailByOrderId(int orderId)
        {
            var result = await _dataContext.OrderDetails.Exist()
            .Where(x => x.OrderId == orderId).
            Join(_dataContext.Products, o => o.ProductId, p => p.Id, (o, p) => new
            {
                o.OrderId,
                o.ProductId,
                o.UnitPrice,
                o.Quantity,
                o.Discount,
                p.Name
            }).ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> Update(OrderDetail model)
        {
            var order = await _dataContext.OrderDetails.Exist().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    order.OrderId = model.OrderId;
                    order.ProductId = model.ProductId;
                    order.UnitPrice = model.UnitPrice;
                    order.Quantity = model.Quantity;
                    order.Discount = model.Discount;
                    order.UpdatedDate = _now;
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
            return new ApiResult() { Message = "Order detail này không tồn tại" };
        }
    }
}
