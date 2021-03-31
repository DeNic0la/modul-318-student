using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissTransportGui
{
    class GoogleMapsHelper
    {
        static public NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
        private const string urlBaseString = "https://www.google.com/maps";
        static public void openLocation(string x,string y)
        {
            string url = $"{urlBaseString}/search/?api=1&query={x},{y}";
            System.Diagnostics.Process.Start(url);
        // Example: https://www.google.com/maps/search/?api=1&query=47.5951518,-122.3316393
        }
    }
}
