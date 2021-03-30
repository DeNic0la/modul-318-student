using Newtonsoft.Json;
using SwissTransport.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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