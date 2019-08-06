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
using System.Windows.Shapes;

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
