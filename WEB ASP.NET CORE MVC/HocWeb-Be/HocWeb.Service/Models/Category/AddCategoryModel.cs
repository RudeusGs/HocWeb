using Microsoft.AspNetCore.Http;

namespace HocWeb.Service.Models.Category
{
    public class AddCategoryModel
    {
        public IFormFile? File { get; set; }
        public string? NameVN { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Icon { get; set; }
    }
}
