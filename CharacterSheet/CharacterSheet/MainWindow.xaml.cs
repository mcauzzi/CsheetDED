using System.Windows;

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
            this.IsEnabled = false;
            var form = new LoginForm();
            form.Closed += (obj, args) =>
            {
                SQLMan = form.SQLMan;
            };
            form.Show();
            form.Topmost = true;
            form.Focus();

            form.Closed+=(x,y)=> {
                this.IsEnabled = true;
                this.Activate();
                this.Focus();
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
