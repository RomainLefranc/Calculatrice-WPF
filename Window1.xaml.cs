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

namespace Calculatrice
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            // TODO :
            // verifier qu'il y a une licence en locale
            // si oui : récuperer la licence dans un fichier locale
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO :
            // envoyer une requete vers l'api pour verifier si la licence est valide
            // si oui : stocker la licence dans le fichier locale
            // si non : message erreur
        }
    }
}
