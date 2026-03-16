using WDI2026.Presentation.API.Client.Models;
using WDI2026.Presentation.API.Common.Clients;
using WDI2026.Presentation.API.Common.Configuration;
using WDI2026.Presentation.API.Common.Logging.Interfaces;

namespace WDI2026.Presentation.API.Client;

public class ApiClient
{
    private readonly CustomRestClient _client;
    private string _username, _password;

    public ApiClient(IStepsLogger stepsLogger)
    {
        _username = "admin";
        _password = "admin";

        var options = new RestClientOptions()
        {
            Authenticator = new ApiAuthenticator(TestConfiguration.Settings.ShopApiClientSettings.Url, _username, _password)
        };

        _client = new CustomRestClient(TestConfiguration.Settings.ShopApiClientSettings.Url, options, stepsLogger);
    }

    public void SetCredentials(string username, string password)
    {
        _username = username;
        _password = password;
    }

    public async Task<RestResponse<Created>> CreateProductResponse(Product product)
    {
        var request = new RestRequest("/api/v1/products");
        request.AddBody(product);
        return await _client.ExecutePostAsync<Created>(request);
    }

    public async Task<Created> CreateProduct(Product product)
    {
        var response = await CreateProductResponse(product);

        if (!response.IsSuccessful) throw new Exception($"Request should be successful but was: {response.StatusCode}");

        if (response.Data is null)
            throw new Exception($"Response.Data was null for successful request when creating product. StatusCode: {response.StatusCode}, ErrorMessage: {response.ErrorMessage}");

        return response.Data;
    }

    public async Task<RestResponse<Product>> GetProductResponse(int id)
    {
        var request = new RestRequest($"/api/v1/products/{id}");
        return await _client.ExecuteGetAsync<Product>(request);
    }

    public async Task<Product> GetProduct(int id)
    {
        var response = await GetProductResponse(id);

        if (!response.IsSuccessful) throw new Exception($"Request should be successful but was: {response.StatusCode}");

        if (response.Data is null)
            throw new Exception($"Response.Data was null for successful request when getting product id={id}. StatusCode: {response.StatusCode}, ErrorMessage: {response.ErrorMessage}");

        return response.Data;
    }

    public async Task<RestResponse> UpdateProductResponse(int id, Product product)
    {
        var request = new RestRequest($"/api/v1/products/{id}");
        request.AddBody(product);
        return await _client.ExecutePutAsync(request);
    }

    public async Task<bool> UpdateProduct(int id, Product product)
    {
        var response = await UpdateProductResponse(id, product);

        if (!response.IsSuccessful) throw new Exception($"Request should be successful but was: {response.StatusCode}");

        return true;
    }

    public async Task<RestResponse> DeleteProductResponse(int id)
    {
        var request = new RestRequest($"/api/v1/products/{id}");
        return await _client.ExecuteDeleteAsync(request);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var response = await DeleteProductResponse(id);

        if (!response.IsSuccessful) throw new Exception($"Request should be successful but was: {response.StatusCode}");

        return true;
    }
}

