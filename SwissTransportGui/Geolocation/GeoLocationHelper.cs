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
        /// <summary>
        /// Liste, in welche alle EventHandler eingetragen werden können
        /// </summary>
        public List<IGeoLocationUpdateEvent> updateEventHandler { get; set; }

        public GeoLocationHelper()
        {
            updateEventHandler = new List<IGeoLocationUpdateEvent>();
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
            try
            {
                foreach (IGeoLocationUpdateEvent updateEvent in updateEventHandler)
                {
                    updateEvent.OnGeoLocationUpdate(GeoCoordinate.Latitude.ToString(InternetHelper.numberFormatInfo), GeoCoordinate.Longitude.ToString(InternetHelper.numberFormatInfo));
                }
            }
            catch (Exception) { }/*this Function is called from the GeoWatcher
                                  * because of this this Function can sometimes throw errors
                                  * when removing EventHandlers */

        }
    }
}