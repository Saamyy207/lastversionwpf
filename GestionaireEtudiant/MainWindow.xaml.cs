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

namespace GestionaireEtudiant
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            // Default View
            MainContent.Content = new GesitionEtudiant();
            // MainContent.Content = new GestionFiliere();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newWindow = new Login();
            newWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new GesitionEtudiant();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new GestionFiliere();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Stats();
        }
    }
}
