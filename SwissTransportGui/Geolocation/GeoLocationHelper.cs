using SwissTransportGui;
using System;
using System.Collections.Generic;
using System.Device.Location;

namespace SwissTransportGUI
{
    class GeoLocationHelper
    {
        private GeoCoordinateWatcher geoWatcher;
        private GeoCoordinate coordinate = null;

        public GeoLocationHelper()
        {
            UpdateEventHandler = new List<IGeoLocationUpdateEvent>();
            geoWatcher = new GeoCoordinateWatcher();
            geoWatcher.MovementThreshold = 20;
            geoWatcher.StatusChanged += OnStatusChange;
            geoWatcher.PositionChanged += OnPositionChange;

            geoWatcher.Start(false);

        }

        public GeoCoordinate GeoCoordinate
        {
            get { return coordinate; }
        }

        /// <summary>
        /// Liste, in welche alle EventHandler eingetragen werden können
        /// </summary>
        public List<IGeoLocationUpdateEvent> UpdateEventHandler { get; set; }

        private void OnStatusChange(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Console.Write(e.Status);
            if (e.Status == GeoPositionStatus.Ready)
            {
                if (!(geoWatcher.Position.Location.IsUnknown))
                {
                    coordinate = geoWatcher.Position.Location;
                    CallUpdateEvent();
                }
            }
        }

        private void OnPositionChange(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (!(e.Position.Location.IsUnknown))
            {
                coordinate = e.Position.Location;
                CallUpdateEvent();
            }
        }

        private void CallUpdateEvent()
        {
            foreach (IGeoLocationUpdateEvent updateEvent in UpdateEventHandler)
            {
                updateEvent.OnGeoLocationUpdate(GeoCoordinate.Latitude.ToString(GoogleMapsHelper.numberFormatInfo), GeoCoordinate.Longitude.ToString(GoogleMapsHelper.numberFormatInfo));
            }
        }
    }
}