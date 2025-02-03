using API.Contracts.Requests.Bus;
using API.Contracts.Responses.Bus;
using API.Mappings;
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class BusTimetableController(IRepository<BusStation> stationRepository)
    : ApiControllerBase<BusTimetable, BusTimtableResponse, BusTimetableRequest, BusMapper, DateOnly?>
{
    public override async Task<IActionResult> Get([FromQuery] DateOnly? date, [FromServices] GetAllEntitiesHandler<BusTimetable> action)
    {
        var command = new GetAllQuery<BusTimetable>(
            filter: t => (date == null || t.Date == date),
            includes: q => q.Include(t => t.Station));

        var response = await action.Handle(command);
        var result = Mapper.Map(response);
        return Ok(result);
    }

    public override async Task<IActionResult> Get(Guid id, [FromServices] GetEntityByIdHandler<BusTimetable> action)
    {
        var command = new GetByIdQuery<BusTimetable>(Id: id, includes: q => q.Include(t => t.Station));
        var response = await action.Handle(command);
        var result = Mapper.Map(response);
        return Ok(result);
    }

    public override async Task<IActionResult> Add([FromBody] BusTimetableRequest request, [FromServices] CreateEntityHandler<BusTimetable> action)
    {
        var entity = Mapper.Map(request);
        entity.Station = await stationRepository.GetByIdAsync(request.StationId);
        Console.WriteLine(entity.Station.Name);
        var command = new CreateCommand<BusTimetable>(entity);
        await action.Handle(command);
        return Ok("successful");
    }

    public override async Task<IActionResult> Update([FromBody] BusTimetableRequest request, [FromServices] UpdateEntityHandler<BusTimetable> action)
    {
        var entity = Mapper.Map(request);
        entity.Station = await stationRepository.GetByIdAsync(request.StationId);
        var command = new UpdateCommand<BusTimetable>(entity);
        await action.Handle(command);
        return Ok("successful");
    }
}
