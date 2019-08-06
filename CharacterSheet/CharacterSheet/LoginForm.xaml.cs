using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CharacterSheet
{
    /// <summary>
    /// Logica di interazione per LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public string UserName { private set; get; }
        public string Password { private set; get; }
        public SQLManager SQLMan { private set; get; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            UserName = UserNameBox.Text;
            Password = passwordBox.Password;

            if (string.IsNullOrEmpty(UserName))
            {
                UserName = "loginApp";
                Password = "Prova12345";
            }

            try
            {
                SQLMan = new SQLManager(UserName, Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }
    }
}
