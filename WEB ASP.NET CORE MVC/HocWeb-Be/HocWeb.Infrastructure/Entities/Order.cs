using HocWeb.Infrastructure.Base;

namespace HocWeb.Infrastructure.Entities
{
    public class Order : EntityBase
    {
        public  int CustomerId { get; set; }
        public DateTime? RequireDate { get; set; }
        public string? Receiver { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
    }
}
