using System.Globalization;

namespace SwissTransportGui
{
    class GoogleMapsHelper
    {
        static public NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
        private const string urlBaseString = "https://www.google.com/maps";
        static public void openLocation(string x, string y)
        {
            string url = $"{urlBaseString}/search/?api=1&query={x},{y}";
            System.Diagnostics.Process.Start(url);
        }
    }
}
