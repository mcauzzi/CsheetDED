using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Logica di interazione per SpellsList.xaml
    /// </summary>
    public partial class SpellsList : Window
    {
        DataTable SpellTable;
        public SpellsList()
        {
            InitializeComponent();
           
            using (var reader = new StreamReader("AlexSpells.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.BadDataFound = null;
                csv.Configuration.Delimiter = ";";
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                using (var dr = new CsvDataReader(csv))
                {
                    SpellTable = new DataTable();
                    SpellTable.Load(dr);
                    SpellsGrid.DataContext = SpellTable.DefaultView;
                }
            }
        }

    }
}
