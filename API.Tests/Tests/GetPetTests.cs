using MyE2ETests.API.Tests.Models;
using MyE2ETests.API.Tests.Utils;
using Newtonsoft.Json;
using System.Net;

namespace API.Tests.Tests
{
    public class GetPetTests
    {
        private readonly ApiClient _api = new ApiClient();

        [Fact]
        public async Task GetPet_ReturnsFluffy()
        {
            var response = await _api.GetPet(123);
            var body = await response.Content.ReadAsStringAsync();
            var pet = JsonConvert.DeserializeObject<Pet>(body);
            Assert.Equal("doggie", pet.Name);
        }

        [Fact]
        public async Task GetPet_NonExistent_ReturnsNotFound()
        {
            var response = await _api.GetPet(99999);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
