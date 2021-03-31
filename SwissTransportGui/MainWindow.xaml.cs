using SwissTransport.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using SwissTransport;
using SwissTransport.Models;
using SwissTransportView.Mock;
using System.Text.RegularExpressions;

namespace SwissTransportGui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Thread currentStartStationSearchThread = new Thread(x => Console.WriteLine("ThreadStarted") );
        Transport transport = new Transport();

        public List<StationBoardEntry> stationBoardEntryListToDisplay = new List<StationBoardEntry>();
        List<ConnectionEntry> connectionEntryListToDisplay = new List<ConnectionEntry>();

        private static readonly Regex numberRegex = new Regex("[0-9]");
        private static readonly Regex timeRegex = new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$");



        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBoxStartStation_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateTabControll();
            /*currentStartStationSearchThread.Abort();
            currentStartStationSearchThread = new Thread(updateTabControllStationButton);
            currentStartStationSearchThread.Start();*/
        }
        private void textBoxEndStation_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateTabControll();
            datePickerAbfahrtszeit.BlackoutDates.AddDatesInPast();
        }
        private void updateTabControll() {
            if (tabItemStationBoardButton == null)
                return;
            //Check Start Station
            if (isStationValid((textBoxStartStation.Text))) {
                tabItemStationBoardButton.IsSelected = true;
                if (isStationValid((textBoxEndStation.Text)) && textBoxEndStation.Text != textBoxStartStation.Text)
                    tabItemSearchConnectionButton.IsSelected = true;
                else
                    tabItemSearchStationButton.IsSelected = true;
            }
            else {
                tabItemStationNearbyButton.IsSelected = true;
                tabItemSearchStationButton.IsSelected = true;
            }
            
        }
        private bool isStationValid(string toSearchFor)
        {
            Stations foundStations = transport.GetStations(toSearchFor+",");//TODO: SWAP THIS MockData.GetStations();   //
            foreach (Station s in foundStations.StationList)
            {              
                
                if (s.Name != null 
                    && (s.Name.Equals(toSearchFor, StringComparison.CurrentCultureIgnoreCase) 
                    || s.Name.IndexOf((", "+toSearchFor), StringComparison.OrdinalIgnoreCase) >0 ))
                    return true;
            }            
            return false;
        }

        private void buttonSearchStation_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stations foundStations = transport.GetStations(textBoxStartStation.Text);   
            
            
            tabItemStationDisplayer.IsSelected = true;

            stackPanelStationDisplayer.Children.Clear();
            foreach(Station s in foundStations.StationList)
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
            Mouse.OverrideCursor = Cursors.Wait;
            Stations foundStations = transport.GetStations(textBoxStartStation.Text);
            Station s = foundStations.StationList.First();
            StationBoardRoot sbr = transport.GetStationBoard(s.Name,s.Id);
            tabItemShowStationBoard.IsSelected = true;
            dataGridStationBoard.ItemsSource = null;
            stationBoardEntryListToDisplay.Clear();
            
            foreach (StationBoard sb in sbr.Entries)
            {
                StationBoardEntry stationBoardEntry = new StationBoardEntry( sb.Stop.Departure.ToString("HH:mm"), sb.Category+" "+sb.Number, sb.To);
                stationBoardEntryListToDisplay.Add(stationBoardEntry);
            }

            Mouse.OverrideCursor = null;
            dataGridStationBoard.ItemsSource = stationBoardEntryListToDisplay;
        }

        private void buttonSearchConnection_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            DateTime? dateTimeFromPicker = null;
            string timeFromInput = "";
            if (textBoxAbfahrtszeit.Text != "00:00" && timeRegex.IsMatch(textBoxAbfahrtszeit.Text))
            {
                timeFromInput = textBoxAbfahrtszeit.Text;
                dateTimeFromPicker = datePickerAbfahrtszeit.SelectedDate;
            }
            Connections returnedConnections = transport.GetConnectionsWithTime(textBoxStartStation.Text, textBoxEndStation.Text,dateTimeFromPicker,timeFromInput);
            dataGridConnections.ItemsSource = null;
            connectionEntryListToDisplay.Clear();


            foreach (Connection c in returnedConnections.ConnectionList)
            {
                connectionEntryListToDisplay.Add(
                    new ConnectionEntry(
                        c.Duration,
                        c.From.Station.Name,
                        c.From.Platform,
                        convertDateTimeToString(c.From.Departure,"dd.MM.yy HH:mm"),
                        c.To.Station.Name,
                        convertDateTimeToString(c.To.Arrival,"HH:mm"),
                        c.Line.First()));
            }            

            dataGridConnections.ItemsSource = connectionEntryListToDisplay;
            tabItemShowConnections.IsSelected = true;

            Mouse.OverrideCursor = null;
        }
        private string convertDateTimeToString(DateTime? input,string format)
        {
            if (input.HasValue)
                return format == "" ? (input ?? DateTime.Now).ToString() : (input ?? DateTime.Now).ToString(format);
            return "";
        }

        private void textBoxStartStation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (isStationValid(textBoxStartStation.Text))
                {
                    buttonStationBoard_Click(sender, e);
                }
            }
        }

        private void textBoxEndStation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (isStationValid(textBoxStartStation.Text) && isStationValid(textBoxEndStation.Text))
                {
                    buttonSearchConnection_Click(sender, e);
                }
            }
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
    }
}
