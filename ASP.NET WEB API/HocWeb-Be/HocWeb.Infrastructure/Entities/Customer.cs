using HocWeb.Infrastructure.Base;

namespace HocWeb.Infrastructure.Entities
{
    public class Customer : EntityBase
    {
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public bool Activated { get; set; }
    }
}
