using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissTransportGui
{
    class ConnectionEntry
    {
        public string Dauer { get; set; }
        public string Abfahrtsort { get; set; }
        public string Gleis { get; set; }
        public string Abfahrt { get; set; }
        public string Ankunftsort { get; set; }
        public string Ankunft { get; set; }

        //---
        public string ArrivalTimeStamp { get; set; }
        public string DepartureTimeStamp { get; set; }
        public string Delay { get; set; }
        public string RealTimeAvailability { get; set; }

        public ConnectionEntry() { }
        public ConnectionEntry(string Dauer, string Abfahrtsort,string Gleis,string Abfahrt,
            string Ankunftsort, string Ankunft, string arrivalTimestamp)
        {
            this.Dauer = Dauer;
            this.Abfahrtsort = Abfahrtsort;
            this.Gleis = Gleis;
            this.Abfahrt = Abfahrt;
            this.Ankunftsort = Ankunftsort;
            this.Ankunft = Ankunft;

        }
        public ConnectionEntry(string Dauer, string Abfahrtsort, string Gleis, string Abfahrt,
            string Ankunftsort, string Ankunft, string ArrivalTimeStamp, string DepartureTimeStamp,
            string Delay, string RealTimeAvailability)
        {
            this.Dauer = Dauer;
            this.Abfahrtsort = Abfahrtsort;
            this.Gleis = Gleis;
            this.Abfahrt = Abfahrt;
            this.Ankunftsort = Ankunftsort;
            this.Ankunft = Ankunft;
            this.ArrivalTimeStamp = ArrivalTimeStamp;
            this.DepartureTimeStamp = DepartureTimeStamp;
            this.Delay = Delay;
            this.RealTimeAvailability = RealTimeAvailability;

        }
    }
}
