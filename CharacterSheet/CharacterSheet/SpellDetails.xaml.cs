using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CharacterSheet
{
    /// <summary>
    /// Logica di interazione per SpellDetails.xaml
    /// </summary>
    public partial class SpellDetails : Window
    {
        private List<Label> statNames=new List<Label>();
        private List<Label> statValue=new List<Label>();
        public SpellDetails(DataRow row)
        {
            InitializeComponent();

            try
            {
                foreach (DataColumn c in row.Table.Columns)
                {
                    if (row[c.ColumnName].ToString() == "Null")
                    {
                        continue;
                    }
                    this.Height += 20;
                    MainGrid.RowDefinitions.Add(new RowDefinition());
                    MainGrid.RowDefinitions.Last().Height = new GridLength(1, GridUnitType.Star);

                    statNames.Add(new Label());
                    statNames.Last().Content = c.ColumnName;
                    Grid.SetColumn(statNames.Last(), 0);
                    Grid.SetRow(statNames.Last(), MainGrid.RowDefinitions.Count);
                    MainGrid.Children.Add(statNames.Last());

                    statValue.Add(new Label());
                    statValue.Last().Content = row[c.ColumnName];
                    Grid.SetColumn(statValue.Last(), 1);
                    Grid.SetRow(statValue.Last(), MainGrid.RowDefinitions.Count);
                    MainGrid.Children.Add(statValue.Last());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
        }
    }
}
