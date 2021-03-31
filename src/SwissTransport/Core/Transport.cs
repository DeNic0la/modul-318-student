namespace SwissTransport.Core
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;

    using SwissTransport.Models;

    public class Transport : ITransport, IDisposable
    {
        private static readonly Regex timeRegex = new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");

        public const string WebApiHost = "http://transport.opendata.ch/v1/";

        protected readonly IHttpClient HttpClient =
            new HttpClient(CredentialCache.DefaultNetworkCredentials, WebRequest.DefaultWebProxy);

        public Stations GetStations(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            var uri = new Uri($"{WebApiHost}locations?query={query}&type=station");
            return HttpClient.GetObject(uri, 
                input => JsonConvert.DeserializeObject<Stations>(input, 
                new JsonSerializerSettings 
                {
                    NullValueHandling = NullValueHandling.Ignore 
                }));       
        }

        public StationBoardRoot GetStationBoard(string station, string id)
        {
            if (string.IsNullOrEmpty(station))
            {
                throw new ArgumentNullException(nameof(station));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var uri = new Uri($"{WebApiHost}stationboard?station={station}&id={id}");
            return HttpClient.GetObject(uri,
                input => JsonConvert.DeserializeObject<StationBoardRoot>(input,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
            
        }
        // For the Interface and Tests
        public Connections GetConnections(string fromStation,string toStation)
        {
            return GetConnectionsWithTime(fromStation, toStation);
        }
        
        public Connections GetConnectionsWithTime(string fromStation, string toStation, DateTime? departureDate = null, string departureTime = null)
        {
            string uriString = $"{WebApiHost}connections?from={fromStation}&to={toStation}";
            if (departureTime != null)
            {
                if (timeRegex.IsMatch(departureTime))
                {
                    uriString += "&time=" + departureTime;
                }
            }
            if (departureDate.HasValue)
            {
                try // this should not throw errors but since its with a Optional DateTime. Better Save than sorry
                {
                    uriString += "&date=" + (departureDate ?? DateTime.Now).ToString("yyyy-MM-dd");
                }
                catch (Exception) { }
                
            }
            if (string.IsNullOrEmpty(fromStation))
            {
                throw new ArgumentNullException(nameof(fromStation));
            }

            if (string.IsNullOrEmpty(toStation))
            {
                throw new ArgumentNullException(nameof(toStation));
            }


            var uri = new Uri(uriString);

            return HttpClient.GetObject(uri,
               input => JsonConvert.DeserializeObject<Connections>(input,
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore
               }));
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}