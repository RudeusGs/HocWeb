using HocWeb.Infrastructure.Base;

namespace HocWeb.Infrastructure.Entities
{
    public class Category : EntityBase
    {
        public string? NameVN { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Icon { get; set; }
    }
}
