using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DNDHelper.Modules
{
	public class TextboxProcessing
	{
		public void WholeNumbersOnly(object sender, TextCompositionEventArgs e)
		{
			if (!char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}
		public void DoubleNumbersOnly(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as System.Windows.Controls.TextBox;

			if (char.IsDigit(e.Text, 0))
			{
				e.Handled = false;
				return;
			}

			if (e.Text == ",")
			{
				if (!textBox.Text.Contains(","))
				{
					e.Handled = false;
				}
				else
				{
					e.Handled = true;
				}
				return;
			}
			e.Handled = true;
		}
	}
}
