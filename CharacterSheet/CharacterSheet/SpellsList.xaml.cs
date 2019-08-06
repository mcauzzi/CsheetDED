using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        SQLManager SQLMan;
        string loadQuery = @"SELECT name, altname, school, subschool, descriptor, spellcraft_dc, COALESCE(level, '-') as level, short_description
                             FROM SPELL
                                 where name like '%{0}%'
                                 and school like '%{1}%'
                                 and COALESCE(level,'-') like '%{2}%'";
        public SpellsList(SQLManager SQLMan)
        {
            InitializeComponent();
            this.SQLMan = SQLMan;
            RoutedEventHandler LoadEvent = (x, y) =>
            {
                LoadLevelClasses();
                LoadData();
            };
            Loaded += LoadEvent;

        }

        private void LoadLevelClasses()
        {
            DataTable temp = new DataTable();
            HashSet<string> classSet = new HashSet<string>();
            try
            {
                temp = SQLMan.GetData("SELECT LEVEL FROM SPELL");
                foreach (DataRow row in temp.Rows)
                {
                    MatchCollection matches = Regex.Matches(row["LEVEL"].ToString(), "[A-Za-z//]+");
                    foreach (Match m in matches)
                    {
                        classSet.Add(m.Value);
                    }
                }
                classSet.Add("-");
                classSet.OrderBy(x => x);
                classComboBox.ItemsSource = classSet;
                classComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore caricamento classi" + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                SpellsGrid.DataContext = SQLMan.GetData(string.Format(loadQuery, NameFilterBox.Text, SchoolFilterBox.Text,
                                                            classComboBox.SelectedValue.ToString() +
                                                        (classComboBox.SelectedValue.ToString() != "-" ? $" {LevelMinFilterBox.Text}" : string.Empty)))
                                                .DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore caricamento Dati Tabella" + ex.Message);
            }
        }

        private void LoadEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadData();
            }

        }

        private void ClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }
    }
}
