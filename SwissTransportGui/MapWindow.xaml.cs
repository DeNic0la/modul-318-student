using SwissTransport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;



namespace SwissTransportGui
{
    /// <summary>
    /// Interaktionslogik für MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {

        private List<Station> displayedStations;
        public MapWindow(List<Station> stations)
        {
            InitializeComponent();
            displayedStations = stations;

            foreach (Station s in stations)
            {
                if (double.IsNaN(s.Coordinate.XCoordinate)
                    || (s.Coordinate.YCoordinate == 0.0 && s.Coordinate.XCoordinate == 0.0))
                    continue;
                Button b = new Button();
                stackPanelButtons.Children.Add(b);
                b.Content = s.Name;
                Thickness margin = b.Margin;
                margin.Top = 5;
                margin.Bottom = 5;
                margin.Left = 20;
                margin.Right = 20;
                b.Margin = margin;
                b.Click += openStationButton_Click;
            }
        }

        private void openStationButton_Click(object sender, RoutedEventArgs e)
        {

            Station s = displayedStations.ElementAt(stackPanelButtons.Children.IndexOf((Button)sender));
            GoogleMapsHelper.openLocation(s.Coordinate.XCoordinate.ToString(GoogleMapsHelper.numberFormatInfo), s.Coordinate.YCoordinate.ToString(GoogleMapsHelper.numberFormatInfo));
        }
    }
}
