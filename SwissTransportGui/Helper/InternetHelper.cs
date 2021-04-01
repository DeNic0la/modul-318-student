using System.Globalization;
using System.Net;

namespace SwissTransportGui
{
    class InternetHelper
    {
        static public NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
        private const string urlBaseString = "https://www.google.com/maps";
        static public void openLocation(string x, string y)
        {
            string url = $"{urlBaseString}/search/?api=1&query={x},{y}";
            System.Diagnostics.Process.Start(url);
        }
        static public bool hasInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
