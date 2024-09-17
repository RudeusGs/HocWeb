using HocWeb.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Models.Order
{
    public class OrderModel
    {
        public int CustomerId { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }

        public List<OrderDetailModel>? OrderDetails { get; set; }
    }

    public class OrderDetailModel
    {
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
