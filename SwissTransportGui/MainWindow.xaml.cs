using SwissTransport.Core;
using SwissTransport.Models;
using SwissTransportGui.Geolocation;
using SwissTransportGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SwissTransportGui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Transport transport = new Transport();
        private GeoLocationHandler geoLocationHandler = new GeoLocationHandler();



        public List<StationBoardEntry> stationBoardEntryListToDisplay = new List<StationBoardEntry>();
        List<ConnectionEntry> connectionEntryListToDisplay = new List<ConnectionEntry>();

        private static readonly Regex numberRegex = new Regex("[0-9]");
        private static readonly Regex timeRegex = new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");



        public MainWindow()
        {

            InternetHelper.numberFormatInfo.NumberDecimalSeparator = ".";
            InitializeComponent();

            datePickerAbfahrtszeit.BlackoutDates.AddDatesInPast();


            textBoxStartStation.TextBoxTabIndex = 0;
            textBoxEndStation.TextBoxTabIndex = 1;

            TextBoxAutoComplete.notifyOnStationUpdate notifyOnStationUpdate = updateTabControlls;

            textBoxStartStation.addToNotifyOnStationUpdateList(notifyOnStationUpdate);
            textBoxEndStation.addToNotifyOnStationUpdateList(notifyOnStationUpdate);


        }

        private void updateTabControlls(bool atLeastOneFieldIs)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            if (atLeastOneFieldIs)
            {
                if (textBoxStartStation.isValidStation)
                {
                    tabItemStationBoardButton.IsSelected = true;
                    if (textBoxEndStation.isValidStation)
                        tabItemSearchConnectionButton.IsSelected = true;
                }
            }
            else
            {
                tabItemSearchStationButton.IsSelected = true;
                if (!textBoxStartStation.isValidStation)
                    tabItemStationNearbyButton.IsSelected = true;
            }
        }


        private void buttonSearchStation_Click(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            Mouse.OverrideCursor = Cursors.Wait;
            Stations foundStations = transport.GetStations(textBoxStartStation.Text);


            tabItemStationDisplayer.IsSelected = true;

            stackPanelStationDisplayer.Children.Clear();
            foreach (Station s in foundStations.StationList)
            {
                Button b = new Button();
                stackPanelStationDisplayer.Children.Add(b);
                b.Content = s.Name;
                Thickness margin = b.Margin;
                margin.Top = 5;
                margin.Bottom = 5;
                margin.Left = 20;
                margin.Right = 20;
                b.Margin = margin;
                b.Click += buttonClickPutTextInStationField;
            }
            Mouse.OverrideCursor = null;
        }

        private void buttonClickPutTextInStationField(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            Mouse.OverrideCursor = Cursors.Wait;
            if (sender is Button)
            {
                try
                {
                    textBoxStartStation.Text = ((Button)sender).Content.ToString();
                }
                catch (Exception) { }

            }

            Mouse.OverrideCursor = null;
        }

        private void buttonStationBoard_Click(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            Mouse.OverrideCursor = Cursors.Wait;
            Station s = textBoxStartStation.currentlySelectedStation;
            StationBoardRoot sbr = transport.GetStationBoard(s.Name, s.Id);
            tabItemShowStationBoard.IsSelected = true;
            dataGridStationBoard.ItemsSource = null;
            stationBoardEntryListToDisplay.Clear();

            foreach (StationBoard sb in sbr.Entries)
            {
                StationBoardEntry stationBoardEntry = new StationBoardEntry(sb.Stop.Departure.ToString("HH:mm"), sb.Category + " " + sb.Number, sb.To);
                stationBoardEntryListToDisplay.Add(stationBoardEntry);
            }

            Mouse.OverrideCursor = null;
            dataGridStationBoard.ItemsSource = stationBoardEntryListToDisplay;
        }

        private void buttonSearchConnection_Click(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            Mouse.OverrideCursor = Cursors.Wait;
            DateTime? dateTimeFromPicker = null;
            string timeFromInput = "";
            if (textBoxAbfahrtszeit.Text != "00:00" && timeRegex.IsMatch(textBoxAbfahrtszeit.Text))
            {
                timeFromInput = textBoxAbfahrtszeit.Text;
                dateTimeFromPicker = datePickerAbfahrtszeit.SelectedDate;
            }
            Connections returnedConnections = transport.GetConnections(
                textBoxStartStation.currentlySelectedStation.Name,
                textBoxEndStation.currentlySelectedStation.Name,
                dateTimeFromPicker, timeFromInput);
            dataGridConnections.ItemsSource = null;
            connectionEntryListToDisplay.Clear();


            foreach (Connection c in returnedConnections.ConnectionList)
            {
                connectionEntryListToDisplay.Add(
                    new ConnectionEntry(
                        c.Duration,
                        c.From.Station.Name,
                        c.From.Platform,
                        convertDateTimeToString(c.From.Departure, "dd.MM.yy HH:mm"),
                        c.To.Station.Name,
                        convertDateTimeToString(c.To.Arrival, "HH:mm"),
                        c.Line.First()));
            }

            dataGridConnections.ItemsSource = connectionEntryListToDisplay;
            tabItemShowConnections.IsSelected = true;

            Mouse.OverrideCursor = null;
        }
        private string convertDateTimeToString(DateTime? input, string format)
        {
            if (input.HasValue)
                return format == "" ? (input ?? DateTime.Now).ToString() : (input ?? DateTime.Now).ToString(format);
            return "";
        }

        private void textBoxAbfahrtszeit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = isTextIllegal(e.Text);
            if (textBoxAbfahrtszeit.Text.Length == 2)
            {
                textBoxAbfahrtszeit.Text += ":";
                textBoxAbfahrtszeit.CaretIndex = 3;
            }
        }

        private bool isTextIllegal(string text)
        {
            if (!numberRegex.IsMatch(text))
                return true;
            if (textBoxAbfahrtszeit.Text.Length > 4)
                return true;
            return false;
        }

        private void textBoxAbfahrtszeit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!timeRegex.IsMatch(textBoxAbfahrtszeit.Text))
            {
                textBoxAbfahrtszeit.Text = "";
            }
        }

        private void textBoxAbfahrtszeit_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxAbfahrtszeit.Text == "00:00")
            {
                textBoxAbfahrtszeit.Text = "";
            }
        }

        private void textBoxAbfahrtszeit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!InternetHelper.hasInternetConnection())
                {
                    MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                    return;
                }
                textBoxAbfahrtszeit_LostFocus(sender, e);
                if (textBoxStartStation.isValidStation && textBoxEndStation.isValidStation)
                {
                    buttonSearchConnection_Click(sender, e);
                }
            }
        }

        private void buttonStationsNearby_Click(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            if (this.geoLocationHandler.isLoading)
            {
                MessageBox.Show("Ihr Standort wird bereits ermittelt, bitte warten sie einen Moment");
                return;
            }
            else if (this.geoLocationHandler.mapWindow == null)
            {
                geoLocationHandler = new GeoLocationHandler(new GeoLocationHelper());
            }
            else
            {
                this.geoLocationHandler.mapWindow.Show();
            }

        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            try
            {
                if (tabItemShowConnections.IsSelected && dataGridConnections.SelectedCells.Count > 0)
                {
                    foreach (DataGridCellInfo cellInfo in dataGridConnections.SelectedCells)
                    {
                        ConnectionEntry ce = ((ConnectionEntry)cellInfo.Item);
                        System.Diagnostics.Process.Start(
                                "mailto:mail@hier.eingeben" + "?subject=Verbindung nach "
                                + ce.Abfahrtsort + "&body=Von: " + ce.Abfahrtsort
                                + ", Nach: " + ce.Ankunftsort + ", Abfahrt: " + ce.Abfahrt
                                + " Ankunft: " + ce.Ankunft + ", Gleis: " + ce.Gleis
                            );
                        return;
                    }
                }
                else if (tabItemShowStationBoard.IsSelected && dataGridStationBoard.SelectedCells.Count > 0)
                {

                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Für diese Funktion muss eine Verbindung markiert sein");
            }

        }
    }
}
