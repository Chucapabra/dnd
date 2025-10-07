using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDHelper.Modules.MagicSpells
{
    internal class MagicCurse
    {
        public MagicCurse()
        {
            
        }
		public static void ColdBoosting(CurseMagic curseMagicInstanceColdBoosting)
		{
			if (curseMagicInstanceColdBoosting.ColdBoosting_Consumables_cb.SelectedIndex < 0)
			{
				return;
			}
			double totaldammage;
			double consumables = 1.41;
			string damage = curseMagicInstanceColdBoosting.ColdBoosting_DammageSpell_textbox.Text;
			if (string.IsNullOrWhiteSpace(damage) || damage == "-")
			{
				damage = "0";
			}
			switch (curseMagicInstanceColdBoosting.ColdBoosting_Consumables_cb.SelectedIndex)
			{
				case 0:
					consumables = 1.41;
					break;
				case 1:
					consumables = 2.03;
					break;
				case 2:
					consumables = 2.29;
					break;
				case 3:
					consumables = 2.81;
					break;
			}
			if (string.IsNullOrEmpty(curseMagicInstanceColdBoosting.ColdBoosting_SpellName_cb.Text))
			{
				totaldammage = Math.Floor(Convert.ToDouble(damage) * consumables * 0.89 * MagicSpells.muliplyStaff * MagicSpells.muliplyTypeStaff * Modules.Settings.DataManager.DataSave.MultiplyMagicDamage);
			}
			else
			{
				totaldammage = Math.Floor(Convert.ToDouble(damage) * consumables * 0.89);
			}
			if (curseMagicInstanceColdBoosting.ColdBoosting_totaldammage_textblock != null)
			{
				curseMagicInstanceColdBoosting.ColdBoosting_totaldammage_textblock.Text = Convert.ToString(totaldammage);
			}
		}
		public static void GiftOfHealing(CurseMagic curseMagicInstanceGiftOfHealing)
		{
			double consumables = 1.41;
			string quantity = curseMagicInstanceGiftOfHealing.GiftOfHealing_quantity_textbox.Text;
			if (string.IsNullOrWhiteSpace(quantity) || quantity == "-")
			{
				quantity = "0";
			}
			switch (curseMagicInstanceGiftOfHealing.GiftOfHealing_Consumables_cb.SelectedIndex)
			{
				case 0:
					consumables = 1.41;
					break;
				case 1:
					consumables = 2.03;
					break;
				case 2:
					consumables = 2.29;
					break;
				case 3:
					consumables = 2.81;
					break;
			}
			double totalgift = Math.Floor(22 * (Convert.ToDouble(quantity) + 1) * consumables * MagicSpells.muliplyStaff * MagicSpells.muliplyTypeStaff * Modules.Settings.DataManager.DataSave.MultiplyMagicDamage);
			if (curseMagicInstanceGiftOfHealing.GiftOfHealing_total_textblock != null)
			{
				curseMagicInstanceGiftOfHealing.GiftOfHealing_total_textblock.Text = Convert.ToString(totalgift);
			}
		}
		public static void GiftOfDark(CurseMagic curseMagicInstanceGiftOfDark)
		{
			double consumables = 1.41;
			string quantity = curseMagicInstanceGiftOfDark.GiftOfDark_quantity_textbox.Text;
			if (string.IsNullOrWhiteSpace(quantity) || quantity == "-")
			{
				quantity = "0";
			}
			switch (curseMagicInstanceGiftOfDark.GiftOfDark_Consumables_cb.SelectedIndex)
			{
				case 0:
					consumables = 1.41;
					break;
				case 1:
					consumables = 2.03;
					break;
				case 2:
					consumables = 2.29;
					break;
				case 3:
					consumables = 2.81;
					break;
			}
			double totalgift = Math.Floor((Convert.ToDouble(quantity) + 1) * 30 * consumables * MagicSpells.muliplyStaff * MagicSpells.muliplyTypeStaff * Modules.Settings.DataManager.DataSave.MultiplyMagicDamage);
			if (curseMagicInstanceGiftOfDark.GiftOfDark_total_textblock != null)
			{
				curseMagicInstanceGiftOfDark.GiftOfDark_total_textblock.Text = Convert.ToString(totalgift);
			}
		}
	}
}
