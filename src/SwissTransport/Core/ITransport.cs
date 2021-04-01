namespace SwissTransport.Core
{

    using SwissTransport.Models;
    using System;

    public interface ITransport
    {
        Stations GetStations(string query);

        StationBoardRoot GetStationBoard(string station, string id);

        Connections GetConnections(string fromStation, string toStation, DateTime? departureDate = null, string departureTime = null);
    }
}