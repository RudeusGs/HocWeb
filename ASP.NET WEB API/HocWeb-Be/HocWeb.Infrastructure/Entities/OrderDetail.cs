using HocWeb.Infrastructure.Base;

namespace HocWeb.Infrastructure.Entities
{
    public class OrderDetail : EntityBase
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
}
