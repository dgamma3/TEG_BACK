using Newtonsoft.Json;
using TEG.Domain;

namespace TEG.Service;

public class VenueService : IVenueService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public VenueService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<VenuDTO>> GetVenues(string apiBaseUrl)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(apiBaseUrl);
        
        var requestUri = $"events/event-data.json";

        var response = await client.GetAsync(requestUri);

        response.EnsureSuccessStatusCode(); 

        var responseContent = await response.Content.ReadAsStringAsync();
      
        var eventsAndVenus = JsonConvert.DeserializeObject<EventVenus>(responseContent);

        return eventsAndVenus.venues.Select(x => new VenuDTO(x.id, x.name)).ToList();

    }
}

public record VenuDTO(int Id, string Name);

public interface IVenueService
{
    Task<List<VenuDTO>> GetVenues(string httpsPastebinCom);
}
