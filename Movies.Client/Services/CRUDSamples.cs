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

        httpClient.BaseAddress = new Uri("http://localhost:5255");
        httpClient.Timeout = new TimeSpan(0, 0, 30);

        Thread.Sleep(5000);

        var response = await httpClient.GetAsync("api/movies");
        response.EnsureSuccessStatusCode();

        var content = response.Content.ReadAsStringAsync();
    }
}
