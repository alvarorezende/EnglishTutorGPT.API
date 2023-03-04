using EnglishTutorGPT.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorGPT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnglishTutorController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public EnglishTutorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string text, [FromServices] IConfiguration configuration)
        {
            using(EnglishTutorService service = new EnglishTutorService(_httpClient, configuration))
            {
                string answer = await service.GetAnswer(text);               
                return Ok(answer);
            }
        }
    }
}
