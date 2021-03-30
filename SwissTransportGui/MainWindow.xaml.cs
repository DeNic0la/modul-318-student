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
            updateTabControllStationButton();
            /*currentStartStationSearchThread.Abort();
            currentStartStationSearchThread = new Thread(updateTabControllStationButton);
            currentStartStationSearchThread.Start();*/
        }
        private void updateTabControllStationButton() {
            if (tabItemStationBoardButton == null)
                return;
            if (isStationValid((textBoxStartStation.Text.Trim()))) {
                tabItemStationBoardButton.IsSelected = true;
            }
            else {
                tabItemStationNearbyButton.IsSelected = true;
            }
            
        }
        private bool isStationValid(string toSearchFor)
        {
            Stations foundStations = MockData.GetStations();   //transport.GetStations(toSearchFor.Text+",");//TODO: SWAP THIS
            foreach (Station s in foundStations.StationList)
            {
                if (s.Name.Equals(toSearchFor, StringComparison.CurrentCultureIgnoreCase))
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
            StationBoardRoot sb = transport.GetStationBoard(s.Name,s.Id);
            dataGridStationBoard.ItemsSource = sb.Entries;
            tabItemShowStationBoard.IsSelected = true;
            
        }
    }
}
