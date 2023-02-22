using EnglishTutorGPT.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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
            var token = configuration.GetValue<string>("ChatGptSecretKey");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            ChatGptInputModel model = new ChatGptInputModel(text);

            var requsetBody = JsonSerializer.Serialize(model);

            StringContent content = new StringContent(requsetBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            var result = await response.Content.ReadFromJsonAsync<ChatGptViewModel>();

            var promptResponse = result.choices.FirstOrDefault();

            return Ok(promptResponse.text.Replace("\n", "").Replace("\t", ""));
        }
    }
}
