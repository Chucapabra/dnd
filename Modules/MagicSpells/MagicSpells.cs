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
		static ObservableCollection<Cast> AllCasts = new ObservableCollection<Cast>();
		static ObservableCollection<Cast> CurrentCasts = new ObservableCollection<Cast>();
		public MagicSpells() 
		{
			main.AddCustomSpellCurrent.Click += AddCustomSpellCurrent_Click;
			main.MoveSpellAll.Click += MoveSpellAll_Click;
			main.DataGridAllSpells.SelectionChanged += DataGridAllSpells_SelectionChanged;
			main.DataGridAllSpells.PreviewMouseLeftButtonDown += DataGridAllSpells_PreviewMouseLeftButtonDown;
			RepositoryLoad();
			main.DataGridAllSpells.ItemsSource = AllCasts;
			main.DataGridCurrentSpells.ItemsSource = CurrentCasts;
			
		}
		private object _lastSelectedItem;
		public static int MagicBulletsCurrent;
		public static int MagicBulletsMax;
		private void DataGridAllSpells_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var row = ItemsControl.ContainerFromElement((System.Windows.Controls.DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

			if (row == null)
			{
				_lastSelectedItem = null;
				return;
			}

			if (row.IsSelected && Equals(_lastSelectedItem, row.Item))
			{
				main.DataGridAllSpells.SetValue(Grid.RowSpanProperty, 1);
				main.AllSpellDescription.Visibility = Visibility.Collapsed	;
				e.Handled = true;
			}
			else
			{
				_lastSelectedItem = row.Item;
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
			var casts = File.ReadAllLines("Casts.txt");
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
					switch (cast.SpellSchool = separator[1 + add].ToString())
					{
						case "-1":
							cast.SpellSchool = "Заговор";
							break;
						case "0":
							cast.SpellSchool = "Ограждения";
							break;
						case "1":
							cast.SpellSchool = "Вызова";
							break;
						case "2":
							cast.SpellSchool = "Прорицания";
							break;
						case "3":
							cast.SpellSchool = "Очарования";
							break;
						case "4":
							cast.SpellSchool = "Воплощения";
							break;
						case "5":
							cast.SpellSchool = "Иллюзии";
							break;
						case "6":
							cast.SpellSchool = "Некромантии";
							break;
						case "7":
							cast.SpellSchool = "Преобразования";
							break;
						case "8":
							cast.SpellSchool = "Нет";
							break;
						default:
							cast.SpellSchool = "Нет";
							break;
					}
					if (cast.SpellName.Contains("@"))
					{
						cast.colorbrushnature = Brushes.Green;
					}
					cast.SpellDamage = Convert.ToInt32(separator[2 + add].Replace("*", ""));
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
		
		private void MoveSpellAll_Click(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("1231");
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
			CustomSpells customSpells = new();
			customSpells.ShowDialog();
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
