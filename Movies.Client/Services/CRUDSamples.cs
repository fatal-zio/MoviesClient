using Movies.Client.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Xml.Serialization;

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
        var httpClient = GetHttpClient();

        var response = await httpClient.GetAsync("api/movies");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var movies = new List<Movie>();

        if (response.Content.Headers.ContentType?.MediaType == "application/json")
        {
            movies = JsonSerializer.Deserialize<List<Movie>>(content,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        } else if (response.Content.Headers.ContentType?.MediaType == "application/xml")
        {
            var serializer = new XmlSerializer(typeof(List<Movie>));
            movies = serializer.Deserialize(new StringReader(content)) as List<Movie>;
        }
    }

    private HttpClient GetHttpClient()
    {
        var httpClient = _httpClientFactory.CreateClient();

        httpClient.BaseAddress = new Uri("http://localhost:5000");
        httpClient.Timeout = new TimeSpan(0, 0, 30);
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

        return httpClient;
    }
}
