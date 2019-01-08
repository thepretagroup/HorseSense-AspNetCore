using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HorseSense_AspNetCore.Helpers;
using HorseSense_AspNetCore.Models;

namespace HorseSense_AspNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly HorseSenseContext _context;

        public UploadController(HorseSenseContext context)
        {
            _context = context;
        }

        // POST: api/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var raceDay = await DrfParser.ParseFile(_context, file);

            // @@@@@@ TODO: Calculate Factors

            return Ok(raceDay);
        }
    }

}