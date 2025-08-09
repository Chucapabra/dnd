using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DNDHelper.Modules.Diary
{
	public class TextChanges
	{
		Main main = Main.Instance;
		public static bool isUpdatingFontSize = false;
		public void UpdateFontSizeDisplay()
		{
			if (isUpdatingFontSize || main.diaryTB == null) return;

			isUpdatingFontSize = true;

			try
			{
				TextPointer caretPos = main.diaryTB.CaretPosition;
				if (caretPos == null) return;

				object fontSizeValue = DependencyProperty.UnsetValue;


				if (!main.diaryTB.Selection.IsEmpty)
				{
					fontSizeValue = main.diaryTB.Selection.GetPropertyValue(TextElement.FontSizeProperty);
				}
				else 
				{
					TextRange range = new(caretPos, caretPos);
					fontSizeValue = range.GetPropertyValue(TextElement.FontSizeProperty);
				}
				main.fontSizeComboBox.SelectionChanged -= FontSizeComboBox_SelectionChanged;

				if (fontSizeValue != DependencyProperty.UnsetValue && fontSizeValue is double fontSize)
				{
					main.fontSizeComboBox.SelectedValue = fontSize;
				}
				else
				{
					main.fontSizeComboBox.SelectedValue = null;
				}
				main.fontSizeComboBox.SelectionChanged += FontSizeComboBox_SelectionChanged;
			}
			finally
			{
				isUpdatingFontSize = false;
			}
		}
		private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (isUpdatingFontSize || !(main.fontSizeComboBox.SelectedItem is double fontSize))
			{
				return;
			}

			if (!main.diaryTB.Selection.IsEmpty)
			{
				main.diaryTB.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
			}
			else
			{
				TextRange currentRange = new TextRange(main.diaryTB.CaretPosition, main.diaryTB.CaretPosition);
				currentRange.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
			}
		}
		public void ApplyFontSize(double fontSize)
		{
			if (main.diaryTB.Selection.IsEmpty)
			{
				TextRange range = new(main.diaryTB.CaretPosition, main.diaryTB.CaretPosition);
				range.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
			}
			else
			{
				main.diaryTB.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
			}
		}

	}
}
