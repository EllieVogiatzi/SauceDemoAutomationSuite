using MyE2ETests.API.Tests.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace MyE2ETests.API.Tests.Utils
{
    public class ApiClient
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://petstore.swagger.io/v2/";

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<HttpResponseMessage> AddPet(Pet pet)
        {
            return await _client.PostAsJsonAsync("pet", pet);
        }

        public async Task<HttpResponseMessage> GetPet(long id)
        {
            return await _client.GetAsync($"pet/{id}");
        }

        public async Task<HttpResponseMessage> PostRawAsync(string url, object payload)
        {
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PostAsync(url, content);
        }

    }
}
