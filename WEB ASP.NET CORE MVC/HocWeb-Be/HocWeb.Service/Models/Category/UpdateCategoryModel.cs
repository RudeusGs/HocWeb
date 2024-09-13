using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Models.Category
{
    public class UpdateCategoryModel
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public string? NameVN { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Icon { get; set; }
        public bool? IsDelete { get; set; }
    }
}
