using MyE2ETests.API.Tests.Models;
using MyE2ETests.API.Tests.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            Assert.Equal("Fluffy", pet.Name);
        }

        [Fact]
        public async Task GetPet_NonExistent_ReturnsNotFound()
        {
            var response = await _api.GetPet(99999);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
