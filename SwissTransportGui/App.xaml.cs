﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using static System.Environment;

namespace SwissTransportGui
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        string path;

        public App()
        {
            // This functions are Likely to Throw Errors on other Windows-Versions
            try
            {
                path = Environment.GetFolderPath(SpecialFolder.ApplicationData);
            }
            catch (Exception)
            {
                try
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                }
                catch (Exception) { }
            }

            try
            {

                List<string> favoritsFromFile = new List<string>();
                FileStream filestream = new FileStream(path + "/Favorits.txt", FileMode.Open);
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
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {


            List<string> toSave = Favorit.FavoritHelper.Favorits;
            FileStream filestream = new FileStream(path + "/Favorits.txt", FileMode.Create);
            using (TextWriter tw = new StreamWriter(filestream))
            {
                foreach (string s in toSave)
                    tw.WriteLine(s);
            }

        }
    }
}
