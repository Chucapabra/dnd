using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

		private void CastSearch_Type_cb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			switch (main.CastSearch_Type_cb.SelectedIndex)
			{
				case 0:
					main.DataGridAllSpells.ItemsSource =  MagicSpells.AllCasts;
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts;
					break;
				case 1:

					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
				case 5:
					break;
				default:
					main.DataGridAllSpells.ItemsSource = MagicSpells.AllCasts;
					main.DataGridCurrentSpells.ItemsSource = MagicSpells.CurrentCasts;
					break;
			}
		}

		private void CastSearch_textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			var CastSearchAll = MagicSpells.AllCasts.Where((x) => x.SpellName.ToLower().Contains(main.CastSearch_textbox.Text.ToLower()));
			var CastSearchCurrent = MagicSpells.CurrentCasts.Where((x) => x.SpellName.ToLower().Contains(main.CastSearch_textbox.Text.ToLower()));

			main.DataGridAllSpells.ItemsSource = CastSearchAll;
			main.DataGridCurrentSpells.ItemsSource = CastSearchCurrent;

		}
	}
}
