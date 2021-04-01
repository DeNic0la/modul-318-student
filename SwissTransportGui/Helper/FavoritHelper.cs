using System.Collections.Generic;

namespace SwissTransportGui.Favorit
{
    class FavoritHelper
    {
        public static List<string> Favorits;
        public static void init(List<string> favorits)
        {
            Favorits = favorits;
        }
    }
}
