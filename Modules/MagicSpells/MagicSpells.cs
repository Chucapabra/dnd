using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme;

namespace DNDHelper.Modules.MagicSpells
{
    internal class MagicSpells
    {
		Main main = Main.Instance;
		static List<Cast> casts_ = new List<Cast>();
		public MagicSpells() 
		{
			main.AddCustomSpellCurrent.Click += AddCustomSpellCurrent_Click;
			main.MoveSpellAll.Click += MoveSpellAll_Click;
			main.DataGridAllSpells.SelectionChanged += DataGridAllSpells_SelectionChanged;
			RepositoryLoad();
			main.DataGridAllSpells.ItemsSource = casts_;
		}

		private void DataGridAllSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var cast = main.DataGridAllSpells.Items[main.DataGridAllSpells.SelectedIndex] as Cast;
			main.AllSpellDescription.Text = cast.SpellDescription;
		}

		public static void RepositoryLoad()
		{
			var casts = File.ReadAllLines("Casts.txt");
			casts_.Clear();
			foreach (var line in casts)
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
				cast.SpellDamage = Convert.ToInt32(separator[2 + add].Replace("*", ""));
				//cast.SpellRoll = Convert.ToInt32(separator[3 + add]);
				cast.SpellLevel = separator[3 + add];
				cast.SpellDescription = separator[4 + add];
				casts_.Add(cast);
			}
		}
		private void MoveSpellAll_Click(object sender, RoutedEventArgs e)
		{

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
	} 
}
