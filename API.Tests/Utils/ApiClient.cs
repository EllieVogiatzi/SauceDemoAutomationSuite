using MyE2ETests.API.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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
    }
}
