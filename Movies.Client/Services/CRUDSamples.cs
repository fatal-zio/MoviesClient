namespace Movies.Client.Services;

public class CRUDSamples(IHttpClientFactory httpClientFactory) : IIntegrationService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task RunAsync()
    {
        await GetResourceAsync();
    }

    public async Task GetResourceAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();

        httpClient.BaseAddress = new Uri("http://localhost:5000");
        httpClient.Timeout = new TimeSpan(0, 0, 30);

        var response = await httpClient.GetAsync("api/movies");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        Console.Write(content.ToString());
    }
}
