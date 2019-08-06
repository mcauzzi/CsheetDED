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

namespace CharacterSheet
{
    //TODO: Quel DB fa parecchio schifo, mettere apposto i dati

    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLManager SQLMan;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (x, y) =>
              {
                  var form = new LoginForm();
                  form.Closed += (obj, args) =>
                  {
                      SQLMan = form.SQLMan;
                  };
                  form.Show();
              };
        }

        private void SpellsButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new SpellsList(SQLMan);
            form.Closed += (x, y)=>{ };
            form.Show();
        }

        private void EquipButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new Equipment(SQLMan);
            form.Show();
        }
    }
}
