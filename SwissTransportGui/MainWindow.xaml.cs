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

namespace SwissTransportGui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Thread currentStartStationSearchThread = new Thread(x => Console.WriteLine("ThreadStarted") );
        Transport transport = new Transport();
        

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
                if (s.Name != null && s.Name.Equals(toSearchFor, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }            
            return false;
        }

        private void buttonSearchStation_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void buttonClickPutTextInStationField(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                try
                {
                    textBoxStartStation.Text = ((Button)sender).Content.ToString();
                }
                catch (Exception) { }
                
            }


        }

        private void buttonStationBoard_Click(object sender, RoutedEventArgs e)
        {
            Stations foundStations = transport.GetStations(textBoxStartStation.Text);
            Station s = foundStations.StationList.First();
            StationBoardRoot sbr = transport.GetStationBoard(s.Name,s.Id);
            dataGridStationBoard.ItemsSource = sbr.Entries;
            tabItemShowStationBoard.IsSelected = true;
            List<StationBoardEntry> stationBoardEntryListToDisplay = new List<StationBoardEntry>();
            //dataGridStationBoard.ItemsSource = stationBoardEntryListToDisplay;
            foreach (StationBoard sb in sbr.Entries)
            {
                StationBoardEntry stationBoardEntry = new StationBoardEntry( sb.Stop.Departure.ToString("dd.MM.yyyy. HH:mm"), sb.Category+" "+sb.Number, sb.To);
                stationBoardEntryListToDisplay.Add(stationBoardEntry);
            }
            
        }

        private void buttonSearchConnection_Click(object sender, RoutedEventArgs e)
        {
            Connections connections = transport.GetConnections(textBoxStartStation.Text, textBoxEndStation.Text);
            List<ConnectionEntry> connectionEntries = new List<ConnectionEntry>();
            
            connections.ConnectionList.ForEach(x => connectionEntries.Add(new ConnectionEntry(
                x.Duration, 
                x.From.Station.Name, 
                x.From.Platform,
                x.From.Departure.HasValue ? x.From.Departure.ToString() : "",
                x.To.Station.Name,
                x.To.Arrival.HasValue ? x.To.Arrival.ToString() : "",
                "")));

            dataGridConnections.ItemsSource = connectionEntries;
            tabItemShowConnections.IsSelected = true;


        }
    }
}
