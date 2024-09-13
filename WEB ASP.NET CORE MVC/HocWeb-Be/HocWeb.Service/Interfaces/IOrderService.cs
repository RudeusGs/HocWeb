using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Order;

namespace HocWeb.Service.Interfaces
{
    public interface IOrderService : IServiceBase<Order>
    {
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        Task<ApiResult> CreateNew(OrderModel model);
        Task<ApiResult> GetOrderByUserId();
    }
}
