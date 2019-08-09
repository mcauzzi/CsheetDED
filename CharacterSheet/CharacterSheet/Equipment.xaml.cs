using System.Windows;

namespace CharacterSheet
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class Equipment : Window
    {
        SQLManager SQLMan;
        //TODO:Aggiungere controlli WPF separatì per ogni tipo di equip
        public Equipment(SQLManager SQLMan)
        {
            InitializeComponent();
            this.SQLMan = SQLMan;
        }
    }
}
