using System.Globalization;
using Newtonsoft.Json;
using TEG.Domain;

namespace TEG.Service;

// EventService 
public class EventService : IEventService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EventService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<EventDto>> GetEventsByMonthYearAsync(string apiBaseUrl, int month, int year, int venueId)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(apiBaseUrl);

        // Construct the API endpoint with query parameters
        var requestUri = $"raw/AC2PwZSt";

        var response = await client.GetAsync(requestUri);

        response.EnsureSuccessStatusCode();  // Throws if there's an error

        var responseContent = await response.Content.ReadAsStringAsync();
      
        var events = JsonConvert.DeserializeObject<EventVenus>(responseContent); 
    
        var filteredEvents = events.events.Where(e => e.StartDate.Month == month && e.StartDate.Year == year && e.venueId == venueId).ToList();

     var eventDtos = filteredEvents.Select(e =>
         {
             var formattedTime =
                 DateTime.Today.Add(e.StartDate.TimeOfDay).ToString("h:mm tt", CultureInfo.InvariantCulture);
             return new EventDto(
                 e.name, // Assuming you have a 'name' property on the event object
                 e.StartDate.Day,
                 formattedTime);
         }
     ).ToList();

     return eventDtos;

    }
}

public record EventDto(string Name, int StartDateDay, string startTime);

public interface IEventService
{
    Task<List<EventDto>> GetEventsByMonthYearAsync(string apiBaseUrl, int month, int year, int venueId);
}


