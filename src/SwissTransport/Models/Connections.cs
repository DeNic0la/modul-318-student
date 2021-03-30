namespace SwissTransport.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class Connections
    {
        [JsonProperty("connections")] public List<Connection> ConnectionList { get; set; }
    }

    public class Connection
    {
        [JsonProperty("from")] public ConnectionPoint From { get; set; }

        [JsonProperty("to")] public ConnectionPoint To { get; set; }

        [JsonProperty("duration")] public string Duration { get; set; }
        [JsonProperty("sections")] public List<Section> SectionList { get; set; }

        public string getLine()
        {
            Section s = SectionList.First();
        }
    }
    public class Section
    {
        [JsonProperty("journey")] public Journey journey { get; set; }
        [JsonProperty("walk")] public string Walk { get; set; }
    }
    public class Journey
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("category")] public string Category { get; set; }
        [JsonProperty("categoryCode")] public string CategoryCode { get; set; }
        [JsonProperty("number")] public string Number { get; set; }
        [JsonProperty("operator")] public string Operator { get; set; }
        [JsonProperty("to")] public string FinalDestination { get; set; }
        [JsonProperty("passList")] public List<ConnectionPoint> connectionPointsOnJourney { get; set; }
    }
    public class ConnectionPoint
    {
        [JsonProperty("station")] public Station Station { get; set; }

        public DateTime? Arrival { get; set; }

        public string ArrivalTimestamp { get; set; }

        public DateTime? Departure { get; set; }

        public string DepartureTimestamp { get; set; }

        public int? Delay { get; set; }

        public string Platform { get; set; }

        public string RealtimeAvailability { get; set; }
    }
}