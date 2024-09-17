using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Entities;
using HocWeb.Infrastructure.Extensions;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace HocWeb.Service.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(DataContext context, Common.IServices.IUserService userService) : base(context, userService) { }

        public async Task<ApiResult> Add(Order model)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    model.CreatedDate = _now;
                    _dataContext.Orders.Add(model);
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
            return new ApiResult() { Message = "Order này đã tồn tại" };
        }

        public async Task<ApiResult> CreateNew(OrderModel model)
        {
            if (model.OrderDetails == null || model.OrderDetails.Count() == 0)
            {
                return new() { Message = "Không có sản phẩm nào được chọn" };
            }

            using var tran = _dataContext.Database.BeginTransaction();
            try
            {
                var order = new Order()
                {
                    CreatedDate = _now,
                    Address = model.Address,
                    CustomerId = model.CustomerId,
                    Description = model.Description,
                    Amount = model.Amount
                };
                _dataContext.Orders.Add(order);
                await _dataContext.SaveChangesAsync();

                List<OrderDetail> orderDetails = new();
                if (order.Id != null)
                {
                    foreach (var item in model.OrderDetails)
                    {
                        var newItem = new OrderDetail()
                        {
                            ProductId = item.ProductId,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity,
                            CreatedDate = _now,
                            OrderId = order.Id ?? 0
                        };
                        orderDetails.Add(newItem);
                    }

                    _dataContext.OrderDetails.AddRange(orderDetails);
                    await _dataContext.SaveChangesAsync();
                }

                await tran.CommitAsync();
                return new();
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception(e.Message);

            }
        }


        public async Task<ApiResult> Delete(int id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
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

            return new ApiResult() { Message = "Order này không tồn tại" };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Orders.Exist().ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Orders.Exist()
                .FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> GetOrderByUserId()
        {
            var result = await _dataContext.Orders.Exist()
               .Where(x => x.CustomerId == _userService.UserId).ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> Update(Order model)
        {
            var order = await _dataContext.Orders.Exist().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {

                    order.CustomerId = model.CustomerId;
                    order.RequireDate = model.RequireDate;
                    order.Receiver = model.Receiver;
                    order.Address = model.Address;
                    order.Description = model.Description;
                    order.Amount = model.Amount;
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
            return new ApiResult() { Message = "Order này không tồn tại" };
        }
    }

}
