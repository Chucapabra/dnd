using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DNDHelper.Modules.MagicSpells
{
	internal class MagicSearch
	{
		Main main = Main.Instance;
		public MagicSearch()
		{
			main.CastSearch_textbox.TextChanged += CastSearch_textbox_TextChanged;
			main.CastSearch_Type_cb.SelectionChanged += CastSearch_Type_cb_SelectionChanged; ;
		}
		List<Cast> filtersSpellAll = new List<Cast>();
		List<Cast> filtersSpellCurrent = new List<Cast>();
		private void CastsFilter()
		{
			filtersSpellAll.Clear();
			foreach (var filter in MagicSpells.AllCasts)
			{
				filtersSpellAll.Add(filter);
			}
			filtersSpellAll = filtersSpellAll.Where((x) => x.SpellName.ToLower().Contains(main.CastSearch_textbox.Text.ToLower())).ToList();
			//filtersSpellCurrent = filtersSpellCurrent.Where((x) => x.SpellName.ToLower().Contains(main.CastSearch_textbox.Text.ToLower()));

			//main.Dat
			main.DataGridAllSpells.ItemsSource = filtersSpellAll;
		}

		private void CastSearch_Type_cb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			switch (main.CastSearch_Type_cb.SelectedIndex)
			{
				case 0:
					main.DataGridAllSpells.ItemsSource =  MagicSpells.AllCasts;
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts;
					break;
				case 1:
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts.Where(c => c.colorbrush == new SolidColorBrush(Settings.Settings.SelectedTheme[1]));
					main.DataGridAllSpells.ItemsSource = MagicSpells.AllCasts.Where(c => c.colorbrush == new SolidColorBrush(Settings.Settings.SelectedTheme[1]));
					break;
				case 2:
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts.Where(c => c.colorbrush == Brushes.SkyBlue);
					main.DataGridAllSpells.ItemsSource = MagicSpells.AllCasts.Where(c => c.colorbrush == Brushes.SkyBlue);
					break;
				case 3:
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts.Where(c => c.colorbrush == Brushes.Firebrick);
					main.DataGridAllSpells.ItemsSource = MagicSpells.AllCasts.Where(c => c.colorbrush == Brushes.Firebrick);
					break;
				case 4:
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts.Where(c => c.colorbrush == Brushes.BlueViolet);
					main.DataGridAllSpells.ItemsSource = MagicSpells.AllCasts.Where(c => c.colorbrush == Brushes.BlueViolet);
					break;
				case 5:
					main.DataGridAllSpells.ItemsSource = MagicSpells.AllCasts;
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts;
					break;
				default:
					main.DataGridAllSpells.ItemsSource = MagicSpells.AllCasts;
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts;
					break;
			}
		}

		private void CastSearch_textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			CastsFilter();
		}
	}
}
