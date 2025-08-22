using DNDHelper.Modules.Settings;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme;

namespace DNDHelper.Modules.MagicSpells
{
	internal class MagicSpells
    {
		Main main = Main.Instance;
		public static ObservableCollection<Cast> AllCasts = new ObservableCollection<Cast>();
		public static ObservableCollection<Cast> CurrentCasts = new ObservableCollection<Cast>();
		public MagicSpells() 
		{
			main.AddCustomSpellCurrent.Click += AddCustomSpellCurrent_Click;
			main.MoveSpellAll.Click += MoveSpellAll_Click;
			main.DataGridAllSpells.SelectionChanged += DataGridAllSpells_SelectionChanged;
			main.DataGridAllSpells.PreviewMouseLeftButtonDown += DataGridAllSpells_PreviewMouseLeftButtonDown;
			main.DataGridCurrentSpells.SelectionChanged += DataGridCurrentSpells_SelectionChanged;
			main.DataGridCurrentSpells.PreviewMouseLeftButtonDown += DataGridCurrentSpells_PreviewMouseLeftButtonDown;
            main.CatalyzerQualitySelect_cb.SelectionChanged += CatalyzerQualitySelect_cb_SelectionChanged;
            main.CatalyzerSelect_cb.SelectionChanged += CatalyzerSelect_cb_SelectionChanged; 
            main.DataGridAllSpells.ItemsSource = AllCasts;
			main.DataGridCurrentSpells.ItemsSource = CurrentCasts;
			
		}

        private object _lastSelectedItemAll;
		private int _clickCountAll = 0;
		private object _lastSelectedItemCurrent;
		private int _clickCountCurrent = 0;
		public static int MagicBulletsCurrent;
		public static int MagicBulletsMax;
		private void DataGridCurrentSpells_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var row = ItemsControl.ContainerFromElement((System.Windows.Controls.DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

			if (row == null)
			{
				_lastSelectedItemCurrent = null;
				_clickCountCurrent = 0;
				return;
			}

			if (Equals(_lastSelectedItemCurrent, row.Item))
			{
				_clickCountCurrent++;
			}
			else
			{
				_lastSelectedItemCurrent = row.Item;
				_clickCountCurrent = 1;
			}

			if (row.IsSelected)
			{
				if (_clickCountCurrent == 2)
				{
					main.DataGridCurrentSpells.SetValue(Grid.RowSpanProperty, 4);
					main.CurrentSpellDescription.Visibility = Visibility.Collapsed;
					e.Handled = true;
				}
				else if (_clickCountCurrent == 3)
				{
					if (main.DataGridCurrentSpells.SelectedIndex != -1)
					{
						main.DataGridCurrentSpells.SetValue(Grid.RowSpanProperty, 2);
						main.CurrentSpellDescription.Visibility = Visibility.Visible;
						var cast = main.DataGridCurrentSpells.Items[main.DataGridCurrentSpells.SelectedIndex] as Cast;
						main.CurrentSpellDescription.Text = cast.SpellDescription;
					}

					e.Handled = true;
					_clickCountCurrent = 1;
				}
			}
		}

		private void DataGridCurrentSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (main.DataGridCurrentSpells.SelectedIndex != -1)
			{
				main.DataGridCurrentSpells.SetValue(Grid.RowSpanProperty, 2);
				main.CurrentSpellDescription.Visibility = Visibility.Visible;
				var cast = main.DataGridCurrentSpells.Items[main.DataGridCurrentSpells.SelectedIndex] as Cast;
				main.CurrentSpellDescription.Text = cast.SpellDescription;
			}
		}

		private void DataGridAllSpells_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var row = ItemsControl.ContainerFromElement((System.Windows.Controls.DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

			if (row == null)
			{
				_lastSelectedItemAll = null;
				_clickCountAll = 0;
				return;
			}

			if (Equals(_lastSelectedItemAll, row.Item))
			{
				_clickCountAll++;
			}
			else
			{
				_lastSelectedItemAll = row.Item;
				_clickCountAll = 1;
			}

			if (row.IsSelected)
			{
				if (_clickCountAll == 2)
				{
					main.DataGridAllSpells.SetValue(Grid.RowSpanProperty, 4);
					main.AllSpellDescription.Visibility = Visibility.Collapsed;
					e.Handled = true;
				}
				else if (_clickCountAll == 3)
				{
					if (main.DataGridAllSpells.SelectedIndex != -1)
					{
						main.DataGridAllSpells.SetValue(Grid.RowSpanProperty, 2);
						main.AllSpellDescription.Visibility = Visibility.Visible;
						var cast = main.DataGridAllSpells.Items[main.DataGridAllSpells.SelectedIndex] as Cast;
						main.AllSpellDescription.Text = cast.SpellDescription;
					}

					e.Handled = true;
					_clickCountAll = 1;
				}
			}
		}
		private void DataGridAllSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ 
			if (main.DataGridAllSpells.SelectedIndex != -1)
			{
				main.DataGridAllSpells.SetValue(Grid.RowSpanProperty, 2);
				main.AllSpellDescription.Visibility = Visibility.Visible;
				var cast = main.DataGridAllSpells.Items[main.DataGridAllSpells.SelectedIndex] as Cast;
				main.AllSpellDescription.Text = cast.SpellDescription;
			}
		}

		public static void RepositoryLoad()
		{
			string spells = $"Cache/{DataManager.DataSave.SelectedRepository}/Spells.txt";
			if (!File.Exists(spells))
				return;


			var casts = File.ReadAllLines(spells);
			AllCasts.Clear();
			int count = 0;
			foreach (var line in casts)
			{
				try
				{
					Cast cast = new();
					var separator = line.Split('|');
					int add = 0;
					if (separator[0].Length == 1)
					{
						switch (separator[0].ToString())
						{
							case "1":
								cast.colorbrush = Brushes.Firebrick;
								break;
							case "2":
								cast.colorbrush = Brushes.SkyBlue;
								break;
							case "3":
								cast.colorbrush = Brushes.BlueViolet;
								break;
						}
						add = 1;
					}
					cast.SpellName = separator[0 + add];
					//cast.SpellType = separator[1 + add];
					cast.SpellSchool = NumberToSchool(separator[1 + add].ToString());

					if (cast.SpellName.Contains("@"))
						cast.colorbrushnature = Brushes.Green;

					cast.SpellDamage = CheckDamageSpell(separator[2 + add]);
					//cast.SpellRoll = Convert.ToInt32(separator[3 + add]);
					cast.SpellLevel = separator[3 + add];
					cast.SpellDescription = separator[4 + add];
					AllCasts.Add(cast);
				}
				catch (Exception error) 
				{
					count++;
					Debug.WriteLine(line);
				}

			}
			Debug.WriteLine(count.ToString());
		}



		private static float muliplyStaff;
        private static float muliplyTypeStaff;
        private void CatalyzerQualitySelect_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (main.CatalyzerQualitySelect_cb.SelectedIndex)
            {
				case 0:
                    muliplyStaff = 0.5f;
                    break;
                case 1:
                    muliplyStaff = 0.75f;
                    break;
                case 2:
                    muliplyStaff = 1f;
                    break;
                case 3:
                    muliplyStaff = 1.25f;
                    break;
                case 4:
                    muliplyStaff = 1.5f;
                    break;
                case 5:
                    muliplyStaff = 2;
                    break;
                case 6:
                    muliplyStaff = 3;
                    break;
				default:
					muliplyStaff = 1;
					break;
            }

			RepositoryLoad();
        }
        private void CatalyzerSelect_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (main.CatalyzerSelect_cb.SelectedIndex)
            {
                case 0:
                    muliplyTypeStaff = 1.5f;
                    break;
                default:
                    muliplyTypeStaff = 1;
                    break;
            }

			RepositoryLoad();
        }
        private static int CheckDamageSpell(string damageString)
        {
			if (damageString.Contains("*")) return int.Parse(damageString.Replace("*", ""));

			int damage = int.Parse(damageString);
            return (int)(damage * muliplyStaff * muliplyTypeStaff);
        }
		private static string NumberToSchool(string number)
		{
            switch (number)
            {
                case "-1":
                    return   "Заговор";
                case "0":
                    return   "Ограждения";
                case "1":
                    return   "Вызова";
                case "2":
                    return   "Прорицания";
                case "3":
                    return   "Очарования";
                case "4":
                    return   "Воплощения";                    
                case "5":
                    return   "Иллюзии";                     
                case "6":
                    return   "Некромантии";
                case "7":
                    return   "Преобразования";                     
                case "8":
                    return   "Нет";                     
                default:
                    return   "Нет";                    
            }
		}

        private void MoveSpellAll_Click(object sender, RoutedEventArgs e)
		{
			if (main.DataGridAllSpells.SelectedIndex == -1)
			{
				return;
			}
			int indexAllCast = main.DataGridAllSpells.SelectedIndex;
			CurrentCasts.Add(AllCasts[indexAllCast]);
			AllCasts.RemoveAt(indexAllCast);
		}

		private void AddCustomSpellCurrent_Click(object sender, RoutedEventArgs e)
		{
			
			bool isCustomSpellsOpen = Application.Current.Windows.OfType<CustomSpells>().Any();
			if (isCustomSpellsOpen)
			{
				Application.Current.Windows.OfType<CustomSpells>().First().Activate();
			}
			else
			{
				CustomSpells customSpells = new();
				customSpells.Show();
			}
		}

	}
	public class Cast
	{
		public string SpellName { get; set; }
		public string SpellType { get; set; }
		public string SpellSchool { get; set; }
		public int SpellDamage { get; set; }
		public int SpellRoll { get; set; }
		public string SpellLevel { get; set; }
		public string SpellDescription { get; set; }
		public Brush colorbrush { get; set; } = new SolidColorBrush(Settings.Settings.SelectedTheme[1]);
		public Brush colorbrushnature { get; set; } = new SolidColorBrush(Settings.Settings.SelectedTheme[1]);
	} 
}
