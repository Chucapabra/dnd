using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace DNDHelper.Modules
{
	public class TextboxProcessing
	{
		public static void WholeNumbersOnly(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as System.Windows.Controls.TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}
			if (string.IsNullOrEmpty(e.Text))
			{
				e.Handled = false;
				return;
			}

			if (char.IsDigit(e.Text, 0))
			{
				e.Handled = false;
				return;
			}
			if (e.Text == "-")
			{
				if (textBox.SelectionStart == 0 && !textBox.Text.Contains("-"))
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
		public static void DoubleNumbersOnly(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as System.Windows.Controls.TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}

			if (string.IsNullOrEmpty(e.Text))
			{
				e.Handled = false;
				return;
			}

			if (e.Text == "-")
			{
				bool isAtStart = textBox.SelectionStart == 0;
				bool hasMinus = textBox.Text.Contains("-");
				e.Handled = !(isAtStart && !hasMinus);
				return;
			}

			if (e.Text == ",")
			{
				bool hasComma = textBox.Text.Contains(",");
				bool insertingBeforeMinus =
					textBox.SelectionStart > 0 &&
					textBox.Text[textBox.SelectionStart - 1] == '-';
				e.Handled = hasComma || insertingBeforeMinus;
				return;
			}

			if (char.IsDigit(e.Text, 0))
			{
				string simulatedText =
					textBox.Text.Substring(0, textBox.SelectionStart) +
					e.Text +
					textBox.Text.Substring(textBox.SelectionStart);
				string numPart = simulatedText.StartsWith("-") ? simulatedText.Substring(1) : simulatedText;
				if (numPart.StartsWith("0") && numPart.Length > 1 && numPart[1] != ',')
				{
					e.Handled = true;
					return;
				}

				e.Handled = false;
				return;
			}
			e.Handled = true;
		}
		public static void IsTextTrimmed()
		{

		}
	}
}
