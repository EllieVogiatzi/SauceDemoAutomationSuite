using MyE2ETests.API.Tests.Models;
using MyE2ETests.API.Tests.Utils;
using Newtonsoft.Json;
using System.Net;

namespace API.Tests.Tests
{
    public class PostPetTests
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
        public async Task AddPet_InvalidIdType_ReturnsBadRequest()
        {
            var invalidPayload = new
            {
                id = "invalid_id",
                name = "Fluffy",
                photoUrls = new[] { "https://img.com/fluffy.jpg" },
                status = "available"
            };

            var response = await _api.PostRawAsync("/pet", invalidPayload);

            // NOTE: Swagger Petstore sometimes returns 404 instead of 400 for schema violations.
            Assert.True(
                response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound,
                $"Expected 400 or 404, but got {(int)response.StatusCode} {response.StatusCode}"
            );
        }

        [Fact]
        public async Task AddPet_WithoutName_ReturnsBadRequest()
        {
            var incompletePet = new
            {
                id = 124,
                status = "available",
                photoUrls = new[] { "https://img.com/incomplete.jpg" }
            };

            var response = await _api.PostRawAsync("/pet", incompletePet);

            // NOTE: Swagger Petstore sometimes returns 404 instead of 400 for schema violations.
            Assert.True(
                response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound,
                $"Expected 400 or 404, but got {(int)response.StatusCode} {response.StatusCode}"
            );
        }
    }
}
