using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CharacterSheet
{
    /// <summary>
    /// Logica di interazione per SpellsList.xaml
    /// </summary>
    public partial class SpellsList : Window
    {
        public List<DataRow> SelectedSpells { private set; get; }=new List<DataRow>();
        private readonly SQLManager SQLMan;
        string loadQuery = @"SELECT name, altname, school, subschool, descriptor, spellcraft_dc, COALESCE(level, '-') as level, short_description
                             FROM SPELL
                                 where name like '%{0}%'
                                 and school like '%{1}%'
                                 and COALESCE(level,'-') like '%{2}%'"; //TODO:Sostituire con query parametrica
        public SpellsList(SQLManager SQLMan)
        {
            InitializeComponent();
            this.SQLMan = SQLMan;
            Loaded += (x, y) =>
            {
                LoadLevelClasses();
                LoadData();
            };
        }

        private void LoadLevelClasses()
        {
            DataTable temp = new DataTable();
            HashSet<string> classSet = new HashSet<string>();
            try
            {
                temp = SQLMan.SelectQuery("SELECT LEVEL FROM SPELL");
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
                SpellsGrid.DataContext = SQLMan.SelectQuery(string.Format(loadQuery, NameFilterBox.Text, SchoolFilterBox.Text,
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

        private void SpellsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRow selRow = (SpellsGrid.SelectedItem as DataRowView)?.Row;
            var parsList = new List<QueryParameter> {new QueryParameter("spellName", selRow["NAME"], typeof(string))};
            DataRow detailsRow = SQLMan.ParameterizedSelectQuery("SELECT * FROM spell WHERE name=@spellName",parsList).Rows[0];//TODO: Prendere solo i dettagli necessari

            if (detailsRow == null)
            {
                return;
            }
            var form = new SpellDetails(detailsRow);
            form.Show();
        }

        private void SpellsGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            if (GetVisualParentByType(
                (FrameworkElement)e.OriginalSource, typeof(DataGridRow)) is DataGridRow row)
            {
                row.IsSelected = !row.IsSelected;
                SelectSpells(row);
            }

            e.Handled = true;
        }
        private void SpellsGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!(e.OriginalSource is DataGridRow) || e.LeftButton != MouseButtonState.Pressed) return;
            DataGridRow row = e.OriginalSource as DataGridRow;
            row.IsSelected = !row.IsSelected;
            SelectSpells(row);
            e.Handled = true;
        }
        private void SelectSpells(DataGridRow row)
        {
            if (row.IsSelected)
            {
                row.Background = new SolidColorBrush(Colors.LightGreen); //TODO:Non cambia se viene cambiata il datasource della tabella
                SelectedSpells.Add(((DataRowView)row.Item).Row);
            }
            else
            {
                row.Background = null;
                SelectedSpells.Remove(((DataRowView)row.Item).Row);
            }
        }
        //TODO: da spostare in classe helper
        public static DependencyObject GetVisualParentByType(DependencyObject startObject, Type type)
        {
            DependencyObject parent = startObject;
            while (parent != null)
            {
                if (type.IsInstanceOfType(parent))
                    break;
                else
                    parent = VisualTreeHelper.GetParent(parent);
            }

            return parent;
        }
    }
}
