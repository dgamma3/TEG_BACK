using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TEG.Service;

namespace TEG.App.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : Controller
{
    private readonly ILogger<EventsController> _logger;
    private readonly IEventService _eventService;

    public EventsController(ILogger<EventsController> logger, IEventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
    }

    // public IActionResult Index()
    // {
    //     return View();
    // }
    //
    // public IActionResult Privacy()
    // {
    //     return View();
    // }
    //
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
    //
    
    [HttpGet("GetEvents")]
    public async Task<IActionResult> GetEvents([FromQuery] int month, [FromQuery] int year, [FromQuery] int venueId)
    {
        var events = await _eventService.GetEventsByMonthYearAsync("https://pastebin.com/", month, year, venueId); 
        return Ok(events);
    }
}