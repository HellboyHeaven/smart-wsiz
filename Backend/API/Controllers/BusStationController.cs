using API.Contracts.Requests.Bus;
using API.Contracts.Responses.Bus;
using API.Mappings;
using Core.Models;

namespace API.Controllers;


public class BusStationController() : ApiControllerBase<BusStation, BusStationResponse, BusStationRequest, BusMapper>()
{

}

