using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;
using System.IO;

namespace Calculatrice
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public Window1()
        {
            InitializeComponent();
            var inputFileName = "licence.txt";
            string fileContents;
            using (StreamReader sr = File.OpenText(inputFileName))
            {
                fileContents = sr.ReadToEnd();
            }
            licenceInput.Text = fileContents;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "licence", licenceInput.Text },
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await client.PostAsync("https://licence.free.beeceptor.com/verif", content);

            string responseString = response.StatusCode.ToString();
            if (responseString == "OK")
            {
                MessageBox.Show("Licence valide");
                string fileName = "licence.txt";

                using StreamWriter sw = File.CreateText(fileName);
                sw.Write(licenceInput.Text);
                ((MainWindow)Application.Current.MainWindow).userIsRegistered = true;
                this.Close();
            }

        }
    }
}
