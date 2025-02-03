using API.Contracts.Requests.Bus;
using API.Contracts.Responses.Bus;
using Core.Interfaces;
using Core.Models;
using Riok.Mapperly.Abstractions;

namespace API.Mappings;



[Mapper]
public partial class BusMapper :
    IMapper<BusStation, BusStationResponse, BusStationRequest>,
    IMapper<BusTimetable, BusTimtableResponse, BusTimetableRequest>

{

    #region Bus
    public partial BusStation Map(BusStationRequest request);
    public partial BusStationResponse Map(BusStation entity);
    public partial IEnumerable<BusStationResponse> Map(IEnumerable<BusStation> entities);

    public partial BusTimetable Map(BusTimetableRequest request);
    public partial BusTimtableResponse Map(BusTimetable entity);
    public partial IEnumerable<BusTimtableResponse> Map(IEnumerable<BusTimetable> entities);

    #endregion


}