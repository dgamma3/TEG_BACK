namespace TEG.Domain;

public class Event
{
    public int id { get; set; }
    public string name { get; set; } 
    public DateTime StartDate { get; set; }
    public int venueId { get; set; }
    // ... other properties
}