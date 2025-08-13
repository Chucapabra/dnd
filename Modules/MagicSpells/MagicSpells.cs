using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DNDHelper.Modules.MagicSpells
{
    internal class MagicSpells
    {
		Main main = Main.Instance;

		public MagicSpells() 
		{
			main.AddCustomSpellCurrent.Click += AddCustomSpellCurrent_Click;
			main.MoveSpellAll.Click += MoveSpellAll_Click;
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
}
