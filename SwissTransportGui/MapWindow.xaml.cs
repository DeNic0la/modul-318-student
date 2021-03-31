using SwissTransport.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;



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
