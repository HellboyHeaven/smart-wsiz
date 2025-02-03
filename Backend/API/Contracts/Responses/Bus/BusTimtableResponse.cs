using Core.Models;

namespace API.Contracts.Responses.Bus;

public record BusTimtableResponse(Guid Id, DateOnly Date, TimeOnly Time, BusDirection Direction, uint Route, BusStationResponse Station);
