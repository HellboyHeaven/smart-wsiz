using Core.Models;

namespace API.Contracts.Requests.Bus;

public record BusTimetableRequest()
{
    public Guid Id { get; set; }
    public required BusDirection Direction { get; set; }
    public required Guid StationId { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly Time { get; set; }
    public required uint Route { get; set; }
}