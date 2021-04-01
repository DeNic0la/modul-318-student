using SwissTransport.Core;
using SwissTransport.Models;
using SwissTransportGUI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace SwissTransportGui.Geolocation
{
    class GeoLocationHandler : IGeoLocationUpdateEvent
    {
        public MapWindow mapWindow;
        private Transport transport = new Transport();
        private List<GeoLocationHelper> signedOn = new List<GeoLocationHelper>();
        Thread currentCountdownUntilThrowingError;
        public bool isLoading;

        public GeoLocationHandler()
        {
            isLoading = false;
        }
        public GeoLocationHandler(GeoLocationHelper signOn)
        {
            addSelfToEventHandlerList(signOn);
            isLoading = true;
        }
        public void addSelfToEventHandlerList(GeoLocationHelper signOn)
        {
            currentCountdownUntilThrowingError = new Thread(x =>
            {
                try
                {
                    Thread.Sleep(10000);
                    ((GeoLocationHandler)x).isLoading = false;
                    var response = MessageBox.Show("Es gab einen Fehler beim Ermitteln ihres Standorts," +
                        " stellen sie sicher das sie die Standortfreigabe aktiviert haben." +
                        " Soll ihr Standort weiterhin gesucht werden ? Die Stationen werden angezeigt wenn ihr Standort ermittelt wurde.",
                        "Fehler", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.Yes);
                    if (response == MessageBoxResult.No)
                    {
                        ((GeoLocationHandler)x).removeSelfFromAllEventHandlerList();
                    }
                }
                catch (Exception) { } /* Thread.Sleep can Throw errors
                                        * the MessageBox is just an additional Feature
                                        */
            });
            currentCountdownUntilThrowingError.Start(this);
            signOn.updateEventHandler.Add(this);
            signedOn.Add(signOn);
        }
        public void removeSelfFromAllEventHandlerList()
        {

            foreach (GeoLocationHelper geoLocationHelper in signedOn)
            {
                geoLocationHelper.updateEventHandler.Remove(this);
            }
            try
            {
                currentCountdownUntilThrowingError.Abort();
            }
            catch (Exception) { }
        }
        public void OnGeoLocationUpdate(string latitude, string longitude)
        {
            currentCountdownUntilThrowingError.Abort();
            removeSelfFromAllEventHandlerList();
            Stations nearestStations = transport.GetStationsByLocation(latitude, longitude);
            mapWindow = new MapWindow(nearestStations.StationList);
            mapWindow.Show();

        }
    }
}
