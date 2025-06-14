using MyE2ETests.API.Tests.Models;
using MyE2ETests.API.Tests.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyE2ETests.API.Tests
{
    public class PetTests
    {
        private readonly ApiClient _api = new ApiClient();

        [Fact]
        public async Task AddValidPet_ReturnsSuccess()
        {
            var pet = new Pet { Id = 123, Name = "Fluffy", Status = "available", PhotoUrls = new[] { "https://img.com/fluffy.jpg" } };
            var response = await _api.AddPet(pet);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetPet_ReturnsFluffy()
        {
            var response = await _api.GetPet(123);
            var body = await response.Content.ReadAsStringAsync();
            var pet = JsonConvert.DeserializeObject<Pet>(body);
            Assert.Equal("Fluffy", pet.Name);
        }
    }
}
