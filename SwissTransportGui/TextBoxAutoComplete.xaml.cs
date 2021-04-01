using SwissTransport.Core;
using SwissTransport.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SwissTransportGui
{
    /// <summary>
    /// Interaktionslogik für TextBoxAutoComplete.xaml
    /// </summary>
    public partial class TextBoxAutoComplete : UserControl
    {
        Transport transport = new Transport();
        private int notUpdatedFor = 0;
        private Regex regexAllowedChars = new Regex("^(?:[A-Za-zäöüÖÄÜ]+)(?:[A-Za-z0-9äöüÖÄÜ _]*)$");
        public int TextBoxTabIndex
        {
            set
            {
                textBoxInput.TabIndex = value;
            }
        }

        public delegate void notifyOnStationUpdate(bool result);
        private List<notifyOnStationUpdate> notifyOnStationUpdateList = new List<notifyOnStationUpdate>();

        public Station currentlySelectedStation;

        private bool _isValidStation;
        public bool isValidStation
        {
            get
            {
                return _isValidStation;
            }
            set
            {
                if (value)
                {
                    starIcon.Source = Favorit.FavoritHelper.Favorits.Contains(currentlySelectedStation.Name)
                        ? new BitmapImage(new Uri(@"/starOn.png", UriKind.Relative))
                        : new BitmapImage(new Uri(@"/starOff.png", UriKind.Relative));
                    listBoxItemDisplay.Height = 0;
                }
                _isValidStation = value;
                foreach (notifyOnStationUpdate notify in notifyOnStationUpdateList)
                {
                    notify(value);
                }


            }
        }
        private List<Station> lastQueryedStations;
        private List<string> _displayed;
        public List<string> displayed
        {
            get
            {
                return _displayed;
            }
            set
            {
                _displayed = value;
                updateListBox(_displayed);
            }
        }
        public TextBoxAutoComplete()
        {
            InitializeComponent();
        }
        public string Text
        {
            get
            {
                return textBoxInput.Text;
            }
            set
            {
                textBoxInput.Text = value;
            }
        }
        public void addToNotifyOnStationUpdateList(notifyOnStationUpdate toAdd)
        {
            notifyOnStationUpdateList.Add(toAdd);
        }

        private void updateListBox(List<string> newSource)
        {
            if (newSource != null && newSource.Count > 0)
            {
                int newHeight = newSource.Count * 21;
                listBoxItemDisplay.ItemsSource = newSource;
                listBoxItemDisplay.Height = newHeight > 100 ? 100 : newHeight;
            }
            else
            {
                listBoxItemDisplay.Height = 0;
            }

        }
        private bool containsIllegalChar(string toTest)
        {
            return !regexAllowedChars.IsMatch(toTest);
        }
        private void textBoxInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                e.Handled = true;
                return;
            }

            if (containsIllegalChar(e.Text))
            {
                e.Handled = true;
                return;
            }
            isValidStation = false;
            string newText = textBoxInput.Text.Insert(textBoxInput.CaretIndex, e.Text);
            List<string> stillValidItems = new List<string>();
            if (_displayed != null && _displayed.Count > 0)
            {
                stillValidItems = getValidItems(_displayed, newText);
            }
            if (stillValidItems.Count >= 5 && notUpdatedFor < 3)
            {
                notUpdatedFor++;
                displayed = stillValidItems;
                return;
            }
            notUpdatedFor = 0;
            List<Station> foundStations = transport.GetStations(newText).StationList;
            lastQueryedStations = foundStations;
            List<string> newItems = new List<string>();
            foreach (string s in Favorit.FavoritHelper.Favorits)
            {
                if (s != null && s.Contains(newText))
                    newItems.Add(s);
            }
            foreach (Station s in foundStations)
            {
                if (!newItems.Contains(s.Name))
                    newItems.Add(s.Name);
            }
            displayed = newItems;

        }
        private List<string> getValidItems(List<string> baseList, string searchText)
        {
            List<string> toReturn = new List<string>();
            foreach (string s in baseList)
            {
                if (s == null)
                    continue;//This is in very Rare cases null
                if (s.IndexOf(searchText) != -1)
                    toReturn.Add(s);

            }
            return toReturn;
        }

        private void textBoxInput_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (!InternetHelper.hasInternetConnection())
            {
                return;
            }
            listBoxItemDisplay.Height = 0;
            if (string.IsNullOrEmpty(textBoxInput.Text))
                return;
            try
            {
                Station match = lastQueryedStations.Find(x => x.Name.Equals(textBoxInput.Text));
                if (match != null)
                {
                    currentlySelectedStation = match;
                    isValidStation = true;
                    return;
                }
            }
            catch (Exception) { }
            
            List<Station> matchingStations = transport.GetStations(textBoxInput.Text).StationList;
            if (matchingStations.Count <= 0)
                return;
            Station foundStation = matchingStations.Find(x => x.Name.Equals(textBoxInput.Text));
            if (foundStation != null)
            {
                currentlySelectedStation = foundStation;
                isValidStation = true;
                return;
            }




        }

        private void textBoxInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            isValidStation = false;
            List<string> newItems = new List<string>();
            foreach (string s in Favorit.FavoritHelper.Favorits)
            {
                if (s != null && s.Contains(textBoxInput.Text ?? ""))
                    newItems.Add(s);
            }
            updateListBox(_displayed);
        }

        private void textBoxInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }

            switch (e.Key)
            {
                case Key.Up:
                    if (0 <= listBoxItemDisplay.SelectedIndex - 1)
                        listBoxItemDisplay.SelectedIndex -= 1;
                    break;
                case Key.Down:
                    if (listBoxItemDisplay.Items.Count >= listBoxItemDisplay.SelectedIndex + 1)
                        listBoxItemDisplay.SelectedIndex += 1;
                    break;
                case Key.Enter:
                    if (listBoxItemDisplay.SelectedItem != null)
                    {
                        textBoxInput.Text = listBoxItemDisplay.SelectedItem.ToString();
                        currentlySelectedStation = lastQueryedStations.Find(x => x.Name.Equals(textBoxInput.Text)) ??
                            transport.GetStations(textBoxInput.Text).StationList.Find(x => x.Name.Equals(textBoxInput.Text));// In Case a Favorit gets Selected
                        isValidStation = true;


                        var request = new TraversalRequest(FocusNavigationDirection.Next);
                        request.Wrapped = true;
                        textBoxInput.MoveFocus(request);
                    }

                    break;
                default:
                    break;
            }
            if (listBoxItemDisplay.Height > 0 && listBoxItemDisplay.SelectedItem != null)
                listBoxItemDisplay.ScrollIntoView(listBoxItemDisplay.SelectedItem);
        }

        private void buttonShowStationOnMap_Click(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            if (isValidStation)
            {
                Coordinate c = currentlySelectedStation.Coordinate;
                InternetHelper.openLocation(
                    c.XCoordinate.ToString(InternetHelper.numberFormatInfo),
                    c.YCoordinate.ToString(InternetHelper.numberFormatInfo));
            }
            else
            {
                textBoxInput.Focus();
            }
        }

        private void butttonAddStationToFavorits_Click(object sender, RoutedEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            if (isValidStation)
            {
                if (Favorit.FavoritHelper.Favorits.Contains(currentlySelectedStation.Name))
                {
                    Favorit.FavoritHelper.Favorits.Remove(currentlySelectedStation.Name);
                    starIcon.Source = new BitmapImage(new Uri(@"/starOff.png", UriKind.Relative));
                }
                else
                {
                    Favorit.FavoritHelper.Favorits.Add(currentlySelectedStation.Name);
                    starIcon.Source = new BitmapImage(new Uri(@"/starOn.png", UriKind.Relative));
                }
            }
        }

        private void listBoxItemDisplay_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!InternetHelper.hasInternetConnection())
            {
                MessageBox.Show("Es ist keine Internetverbindung vorhanden");
                return;
            }
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                textBoxInput.Text = item.DataContext.ToString();
                textBoxInput_LostFocus(this, e);
            }
        }
    }
}
