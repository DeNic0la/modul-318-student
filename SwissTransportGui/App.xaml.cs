using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;


namespace SwissTransportGui
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            try
            {
                List<string> favoritsFromFile = new List<string>();
                FileStream filestream = new FileStream("Favorits.txt", FileMode.Open);
                using (TextReader tw = new StreamReader(filestream))
                {
                    bool keepLoopActive = true;
                    while (keepLoopActive)
                    {
                        string line = tw.ReadLine();
                        keepLoopActive = (line != null);
                        if (!string.IsNullOrEmpty(line))
                            favoritsFromFile.Add(line);
                    }
                }
                Favorit.FavoritHelper.init(favoritsFromFile);


            }
            catch (Exception)
            {
                Favorit.FavoritHelper.init(new List<string>());
            }


            //MainWindow mw = new MainWindow();
            //mw.Show();
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            List<string> toSave = Favorit.FavoritHelper.Favorits;
            FileStream filestream = new FileStream("Favorits.txt", FileMode.Create);
            using (TextWriter tw = new StreamWriter(filestream))
            {
                foreach (string s in toSave)
                    tw.WriteLine(s);
            }

        }
    }
}
