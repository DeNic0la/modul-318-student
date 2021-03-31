using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissTransportGui
{
    class ConnectionEntry
    {
        
        public string Abfahrtsort { get; set; }
        public string Gleis { get; set; }
        public string Linie { get; set; }
        public string Abfahrt { get; set; }

        public string Dauer { get; set; }
        public string Ankunftsort { get; set; }
        public string Ankunft { get; set; }



        public ConnectionEntry() { }
        public ConnectionEntry(string Dauer, string Abfahrtsort, string Gleis, string Abfahrt,
            string Ankunftsort, string Ankunft, string Linie)
        {
            this.Dauer = Dauer;
            this.Abfahrtsort = Abfahrtsort;
            this.Gleis = Gleis;
            this.Abfahrt = Abfahrt;
            this.Ankunftsort = Ankunftsort;
            this.Ankunft = Ankunft;
            this.Linie = Linie;
        }        
    }
}
