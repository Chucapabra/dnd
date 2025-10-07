using DNDHelper.Modules;
using DNDHelper.Modules.MagicSpells;
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
using static DNDHelper.Modules.Character.Skills;

namespace DNDHelper.Windows
{
	/// <summary>
	/// Логика взаимодействия для CurseMagic.xaml
	/// </summary>
	public partial class CurseMagic : Window
	{
		public static CurseMagic curseMagic = new();
		
		public CurseMagic()
		{
			InitializeComponent();
			ColdBoosting_SpellName_cb.ItemsSource = MagicSpells.AllCasts.Where(c => c.SpellDamage > 0);
			ColdBoosting_SpellName_cb.DisplayMemberPath = "SpellName";
		}

		private void DammageSpell_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

		private void DammageSpell_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.WholeNumbersOnly(ColdBoosting_DammageSpell_textbox, e);
			ColdBoosting_SpellName_cb.SelectedIndex = -1;
		}

		private void DammageSpell_textbox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Modules.MagicSpells.MagicCurse.ColdBoosting(this);
		}

		private void Consumables_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Modules.MagicSpells.MagicCurse.ColdBoosting(this);
		}

		private void ColdBoosting_SpellName_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ColdBoosting_SpellName_cb.SelectedItem is Cast selectedSpell)
			{
				ColdBoosting_DammageSpell_textbox.Text = selectedSpell.SpellDamage.ToString();
			}
		}

		private void GiftOfHealing_quantity_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

		private void GiftOfHealing_quantity_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.WholeNumbersOnly(GiftOfHealing_quantity_textbox, e);
			Modules.MagicSpells.MagicCurse.GiftOfHealing(this);
		}

		private void GiftOfHealing_quantity_textbox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Modules.MagicSpells.MagicCurse.GiftOfHealing(this);
		}

		private void GiftOfHealing_Consumables_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Modules.MagicSpells.MagicCurse.GiftOfHealing(this);
		}

		private void GiftOfDark_quantity_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.WholeNumbersOnly(GiftOfDark_quantity_textbox, e);
		}

		private void GiftOfDark_quantity_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

		private void GiftOfDark_Consumables_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Modules.MagicSpells.MagicCurse.GiftOfDark(this);
		}

		private void GiftOfDark_quantity_textbox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Modules.MagicSpells.MagicCurse.GiftOfDark(this);
		}
	}
}
