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

        // Récuperer la licence et l'afficher
        public Window1()
        {
            InitializeComponent();
            string inputFileName = "licence.txt";
            string fileContents;
            using (StreamReader sr = File.OpenText(inputFileName))
            {
                fileContents = sr.ReadToEnd();
            }
            licenceInput.Text = fileContents;
        }

        // Verifier que la licence saisie est valide et la sauvegarder
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //validation de la licence
            if (string.IsNullOrEmpty(licenceInput.Text))
            {
                MessageBox.Show("Veuillez saisir une licence");
                return;
            }
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "licence", licenceInput.Text },
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync("https://licence.free.beeceptor.com/verif", content);
            if (response.IsSuccessStatusCode)
            {
                // Sauvegarde dans un fichier local
                MessageBox.Show("Licence valide");
                using StreamWriter sw = File.CreateText("licence.txt");
                sw.Write(licenceInput.Text);
                ((MainWindow)Application.Current.MainWindow).userIsRegistered = true;
                Close();
            }
            else
            {
                MessageBox.Show("Licence invalide");
            }

        }
    }
}
