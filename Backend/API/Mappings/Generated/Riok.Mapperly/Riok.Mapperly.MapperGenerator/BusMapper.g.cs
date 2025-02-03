﻿// <auto-generated />
#nullable enable
namespace API.Mappings
{
    public partial class BusMapper
    {
        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::Core.Models.BusStation Map(global::API.Contracts.Requests.Bus.BusStationRequest request)
        {
            var target = new global::Core.Models.BusStation();
            target.Id = request.Id;
            target.Name = request.Name;
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::API.Contracts.Responses.Bus.BusStationResponse Map(global::Core.Models.BusStation entity)
        {
            var target = new global::API.Contracts.Responses.Bus.BusStationResponse(entity.Id, entity.Name);
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::System.Collections.Generic.IEnumerable<global::API.Contracts.Responses.Bus.BusStationResponse> Map(global::System.Collections.Generic.IEnumerable<global::Core.Models.BusStation> entities)
        {
            return global::System.Linq.Enumerable.Select(entities, x => Map(x));
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::Core.Models.BusTimetable Map(global::API.Contracts.Requests.Bus.BusTimetableRequest request)
        {
            var target = new global::Core.Models.BusTimetable();
            target.Route = request.Route;
            target.Id = request.Id;
            target.Direction = request.Direction;
            target.Date = request.Date;
            target.Time = request.Time;
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::API.Contracts.Responses.Bus.BusTimtableResponse Map(global::Core.Models.BusTimetable entity)
        {
            var target = new global::API.Contracts.Responses.Bus.BusTimtableResponse(
                entity.Id,
                entity.Date,
                entity.Time,
                entity.Direction,
                entity.Route,
                Map(entity.Station)
            );
            return target;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
        public partial global::System.Collections.Generic.IEnumerable<global::API.Contracts.Responses.Bus.BusTimtableResponse> Map(global::System.Collections.Generic.IEnumerable<global::Core.Models.BusTimetable> entities)
        {
            return global::System.Linq.Enumerable.Select(entities, x => Map(x));
        }
    }
}