using Microsoft.AspNetCore.Mvc;
using TEG.Service;

namespace TEG.App.Controllers;
[ApiController]
[Route("[controller]")]
public class VenusController : ControllerBase
{
    private readonly IVenueService _venueService;

    public VenusController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    [HttpGet("Getvenues")]
    public async Task<IActionResult> GetVenues()
    {
        var venues = await _venueService.GetVenues("https://pastebin.com/"); 
        return Ok(venues);
    }
}

