using HocWeb.Service.Interfaces;
using HocWeb.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HocWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : BaseController
    {

        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("get-by-oder-id")]
        public async Task<IActionResult> GetByOrderId([FromQuery] int oderId)
        {
            try
            {
                var result = await _orderDetailService.GetOrderDetailByOrderId(oderId);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
