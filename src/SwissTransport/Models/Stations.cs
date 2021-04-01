namespace SwissTransport.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Coordinate
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("x")] public double XCoordinate { get; set; }

        [JsonProperty("y")] public double YCoordinate { get; set; }
    }

    public class Station
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("score")] public int? Score { get; set; }

        [JsonProperty("coordinate")] public Coordinate Coordinate { get; set; }

        [JsonProperty("distance")] public double? Distance { get; set; }
    }

    public class Stations
    {
        [JsonProperty("stations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Station> StationList { get; set; }
    }
}