﻿namespace SwissTransportGui
{
    public class StationBoardEntry
    {
        public string Abfahrt { get; set; }
        public string Name { get; set; }
        public string Nach { get; set; }
        public StationBoardEntry(string Abfahrt, string Name, string Nach)
        {
            this.Abfahrt = Abfahrt;
            this.Name = Name;
            this.Nach = Nach;
        }
        public StationBoardEntry()
        {

        }
    }
}
