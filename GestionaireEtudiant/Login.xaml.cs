using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using GestionaireEtudiant;

namespace GestionaireEtudiant
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Password;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                byte[] hashedPassword = HashPassword(password);
                var user = db.Users.SingleOrDefault(u => u.username == username
                                                       && hashedPassword == u.password
                                                       && u.password != null);

                if (user != null)
                {
                    var newWindow = new MainWindow();
                    newWindow.Show();

                    // Fermeture de la fenêtre actuelle 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                }
            }
        }


        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }



        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lorsque le champ reçoit le focus masquer le hint
            usernameHint.Visibility = Visibility.Collapsed;
        }

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            // Si le champ est vide et perd le focus reafficher le hint
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                usernameHint.Visibility = Visibility.Visible;
            }
        }

        // Gestion du focus sur le champ Password
        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lorsque le champ reçoit le focus  masquer le hint
            passwordHint.Visibility = Visibility.Collapsed;
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            // Si le champ est vide et perd le focus  rEafficher le hint
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                passwordHint.Visibility = Visibility.Visible;
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Masquer ou afficher le hint en fonction du contenu du PasswordBox
            passwordHint.Visibility = string.IsNullOrEmpty(txtPassword.Password)
                                      ? Visibility.Visible
                                      : Visibility.Collapsed;
        }


    }
}
