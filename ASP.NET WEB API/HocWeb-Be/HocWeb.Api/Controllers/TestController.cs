using HocWeb.Service.Common.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace HocWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IFtpDirectoryService _ftpDirectoryService;

        public TestController(IFtpDirectoryService ftpDirectoryService)
        {
            _ftpDirectoryService = ftpDirectoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] Model model)

        {
            var strem = model.file.OpenReadStream();
            var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(strem, "public", model.file.FileName);
            return Ok();
        }


        public class Model
        {
            public IFormFile file { get; set; }
            public string fileName { get; set; }
        }
    }
}
