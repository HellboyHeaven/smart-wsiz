namespace API.Contracts.Requests.Bus;


public record BusStationRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}