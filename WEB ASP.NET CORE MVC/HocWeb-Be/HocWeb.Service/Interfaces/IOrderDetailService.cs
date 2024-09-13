using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Models;

namespace HocWeb.Service.Interfaces
{
    public interface IOrderDetailService : IServiceBase<OrderDetail>
    {
        Task<ApiResult> GetOrderDetailByOrderId(int orderId);
    }
}
