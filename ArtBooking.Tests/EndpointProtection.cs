namespace ArtBooking.Tests;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

public class EndpointProtectionTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EndpointProtectionTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_OrganizationsReturnUnauthorizedCode()
    {
        // Arrange
        var url = "/api/organization/all";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Post_OrganizationSaveReturnUnauthorizedCode()
    {
        // Arrange
        var url = "/api/organization/save";
        var payload = new { Name = "Test", Value = 123 };

        // Act
        var response = await _client.PostAsJsonAsync(url, payload);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }
}