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
using System.Diagnostics;
using System.IO;

namespace Calculatrice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal? nombre1;
        private decimal? nombre2;
        private string operateur;
        private bool inputEstUnResultat = false;
        private List<string> historique = new List<string>();
        public bool userIsRegistered = false;
       public MainWindow()
        {
            InitializeComponent();

            verifLicence();

        }
        // retour a la derniere operation
        private void Ce(object sender, RoutedEventArgs e)
        {
            if (derniereOperation.Text.Contains("=")) {
                derniereOperation.Text = null;
            }
            input.Text = null;
        }
        // reset de la calculatrice
        private void C(object sender, RoutedEventArgs e)
        {
            derniereOperation.Text = null;
            input.Text = null;
            operateur = null;
            nombre1 = null;
            nombre2 = null;
            historique = null;
            liste.ItemsSource = null;
        }
        // calcul de l'operation
        private void Calcul(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(operateur))
            {
                nombre2 = decimal.Parse(input.Text);
                derniereOperation.Text += nombre2 + "=";
                if (operateur == "+")
                {
                    input.Text = (nombre1 + nombre2).ToString();
                }
                else if (operateur == "-")
                {
                    input.Text = (nombre1 - nombre2).ToString();
                }
                else if (operateur == "/")
                {
                    input.Text = nombre2 == 0 ? "Impossible de diviser par 0" : (nombre1 / nombre2).ToString();
                }
                else if (operateur == "*")
                {
                    input.Text = (nombre1 * nombre2).ToString();
                }
                inputEstUnResultat = true;
                operateur = null;
                nombre1 = null;
                nombre2 = null;
                historique.Add(derniereOperation.Text + input.Text);
                liste.ItemsSource = null;
                liste.ItemsSource = historique;
            }
        }
        // saisie
        private void Add(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            string[] listeOperateur = { "+", "-", "*", "/" };
            if (userIsRegistered)
            {
                if (listeOperateur.Contains(content))
                {
                    if (string.IsNullOrEmpty(operateur))
                    {
                        GestionNombre1(content);
                    }
                    else
                    {
                        GestionNombre2(content, sender, e);
                    }
                }
                else
                {
                    if (inputEstUnResultat)
                    {
                        operateur = null;
                        nombre1 = null;
                        nombre2 = null;
                        input.Text = content;
                        derniereOperation.Text = null;
                        inputEstUnResultat = false;
                    }
                    else
                    {
                        input.Text += content;
                    }
                }
            }
            else
            {
                MessageBox.Show("Votre licence est invalide");
            }

        }
        // supprimer le dernier caractere saisie
        private void Retour(object sender, RoutedEventArgs e)
        {
            if (input.Text != "" && !inputEstUnResultat)
            {
                input.Text = input.Text.Substring(0, input.Text.Length - 1);
            }
        }
        // opposé du nombre saisie
        private void Negate(object sender, RoutedEventArgs e)
        {
            if (input.Text != "")
            {
                input.Text = (decimal.Parse(input.Text) * -1).ToString();
            }
        }
        //
        private void GestionNombre1(string content)
        {
            inputEstUnResultat = false;
            if (string.IsNullOrEmpty(input.Text))
            {
                input.Text = "0";
            }
            operateur = content;
            nombre1 = decimal.Parse(input.Text);
            derniereOperation.Text = input.Text + content;
            input.Text = null;
        }

        private void GestionNombre2(string content, object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(input.Text))
            {
                input.Text = null;
                derniereOperation.Text = nombre1 + content;
                operateur = content;
            }
            else
            {
                Calcul(sender, e);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightAlt)
            {
                menu.Visibility = menu.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            }
        }

        private void Licence(object sender, RoutedEventArgs e)
        {
            Window1 p = new Window1();
            p.ShowDialog();
        }

        private void Historique(object sender, RoutedEventArgs e)
        {
            historiqueView.Visibility = historiqueView.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void verifLicence()
        {
            var inputFileName = "licence.txt";
            string fileContents;
            using (StreamReader sr = File.OpenText(inputFileName))
            {
                fileContents = sr.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(fileContents))
            {
                userIsRegistered = true;
            }
        }
    }

    
}
