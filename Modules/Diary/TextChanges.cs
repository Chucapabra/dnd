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
		public bool isUpdatingFontSize = false;
		public void UpdateFontSizeDisplay()
		{

			if (isUpdatingFontSize || main.diaryTB == null) return;

			isUpdatingFontSize = true;

			try
			{
				TextPointer caretPos = main.diaryTB.CaretPosition;
				if (caretPos == null) return;

				if (!main.diaryTB.Selection.IsEmpty)
				{
					caretPos = main.diaryTB.Selection.Start;
				}

				TextRange range = new(caretPos, caretPos);
				object fontSizeValue = range.GetPropertyValue(TextElement.FontSizeProperty);

				if (fontSizeValue != DependencyProperty.UnsetValue && fontSizeValue is double fontSize)
				{
					main.fontSizeComboBox.SelectionChanged -= FontSizeComboBox_SelectionChanged;
					main.fontSizeComboBox.SelectedValue = fontSize;
					main.fontSizeComboBox.SelectionChanged += FontSizeComboBox_SelectionChanged;
				}
			}
			finally
			{
				isUpdatingFontSize = false;
			}
		}
        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextChanges textChanges = new();
            if (textChanges.isUpdatingFontSize || !(main.fontSizeComboBox.SelectedItem is double fontSize)) return;

            if (!main.diaryTB.Selection.IsEmpty)
            {
                main.diaryTB.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            }
        }
        public void UpdateFontSizeComboBox()
		{
			if (main.diaryTB.CaretPosition == null) return;

			TextPointer caretPos = main.diaryTB.CaretPosition;

			TextRange range = new(caretPos, caretPos);

			object fontSizeValue = range.GetPropertyValue(TextElement.FontSizeProperty);

			if (fontSizeValue != DependencyProperty.UnsetValue && fontSizeValue is double fontSize)
			{
				main.fontSizeComboBox.SelectionChanged -= FontSizeComboBox_SelectionChanged;
				main.fontSizeComboBox.SelectedValue = fontSize;
				main.fontSizeComboBox.SelectionChanged += FontSizeComboBox_SelectionChanged;
			}
		}
	}
}
