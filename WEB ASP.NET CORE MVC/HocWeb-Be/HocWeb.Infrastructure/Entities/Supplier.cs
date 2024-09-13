using HocWeb.Infrastructure.Base;

namespace HocWeb.Infrastructure.Entities
{
    public class Supplier : EntityBase
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
