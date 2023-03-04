using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using EnglishTutorGPT.API.Models;

namespace EnglishTutorGPT.API.Services
{
    public class EnglishTutorService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EnglishTutorService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetAnswer(string text)
        {
            if (String.IsNullOrEmpty(text))
                throw new Exception("Invalid text");

            var token = _configuration.GetValue<string>("ChatGptSecretKey");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            ChatGptInputModel model = new ChatGptInputModel(text);

            string requsetBody = JsonSerializer.Serialize(model);

            StringContent content = new StringContent(requsetBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            var result = await response.Content.ReadFromJsonAsync<ChatGptViewModel>();

            string promptResponse = result.choices.FirstOrDefault().text.Replace("\n", "").Replace("\t", "");

            return promptResponse;
        }

        public void Dispose()
        {

        }
    }
}