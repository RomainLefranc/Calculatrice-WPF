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


namespace Calculatrice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal? nb1;
        private decimal? nb2;
        private string op;
        private bool result = false;
        private List<string> items = new List<string>();
       public MainWindow()
        {
            InitializeComponent();
            // TODO :
            // verifier que l'utilisateur est inscrit
            // si non : l'utilisateur peut pas utiliser la caclulatrice
            // si oui :
            // verifier que la licence est toujours valide
            // si oui : autoriser l'utilisateur a utiliser la calculatrice
            // si non : supprimer la licence et l'utilisateur peut pas utiliser la caclulatrice
        }
        // retour a la derniere operation
        private void Ce(object sender, RoutedEventArgs e)
        {
            if (lastOp.Text.Contains("=")) {
                lastOp.Text = null;
            }
            input.Text = null;
        }
        // reset de la calculatrice
        private void C(object sender, RoutedEventArgs e)
        {
            lastOp.Text = null;
            input.Text = null;
            op = null;
            nb1 = null;
            nb2 = null;
            items = null;
            liste.ItemsSource = null;
        }
        // calcul de l'operation
        private void Calcul(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(op))
            {
                nb2 = decimal.Parse(input.Text);
                lastOp.Text += nb2 + "=";
                if (op == "+")
                {
                    input.Text = (nb1 + nb2).ToString();
                }
                else if (op == "-")
                {
                    input.Text = (nb1 - nb2).ToString();
                }
                else if (op == "/")
                {
                    input.Text = nb2 == 0 ? "Impossible de diviser par 0" : (nb1 / nb2).ToString();
                }
                else if (op == "*")
                {
                    input.Text = (nb1 * nb2).ToString();
                }
                result = true;
                op = null;
                nb1 = null;
                nb2 = null;
                items.Add(lastOp.Text + input.Text);
                liste.ItemsSource = null;
                liste.ItemsSource = items;
            }
        }
        // saisie
        private void Add(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            string[] operation = { "+", "-", "*", "/" };
            if (operation.Contains(content))
            {
                if (string.IsNullOrEmpty(op))
                {
                    GestionOp1(content);
                }
                else
                {
                    GestionOp2(content, sender, e);
                }
            }
            else
            {
                if (result) {
                    op = null;
                    op1 = null;
                    op2 = null;
                    input.Text = content;
                    lastOp.Text = null;
                    result = false;
                }
                else
                {
                    input.Text += content;
                }
            }
        }
        // supprimer le dernier caractere saisie
        private void Retour(object sender, RoutedEventArgs e)
        {
            if (input.Text != "" && !result)
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
            result = false;
            if (string.IsNullOrEmpty(input.Text))
            {
                input.Text = "0";
            }
            op = content;
            op1 = decimal.Parse(input.Text);
            lastOp.Text = input.Text + content;
            input.Text = null;
        }

        private void GestionNombre2(string content, object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(input.Text))
            {
                input.Text = null;
                lastOp.Text = op1 + content;
                op = content;
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
    }
}
