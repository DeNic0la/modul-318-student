using Newtonsoft.Json;
using SwissTransport.Models;
using System.IO;

namespace SwissTransportView.Mock
{
    public class MockData
    {
        public static Stations GetStations()
        {
            using (StreamReader reader = new StreamReader("Mock/stations.json"))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Stations>(
                    json,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            }
        }
    }
}