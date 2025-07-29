using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
using DNDHelper.Modules.Specifications;

using DarkThem = DNDHelper.Modules.Settings.Settings;

namespace DNDHelper.Windows
{
	/// <summary>
	/// Логика взаимодействия для Main.xaml
	/// </summary>
	public partial class Main : Window
	{
        private readonly TestGrid testGrid;

		public static Main Instance;

        public Main()
		{
			InitializeComponent();
			fontSizeComboBox.SelectedValue = 12.0; // Размер шрифта в дневнике по умолчанию

			Instance = this;

            testGrid = new TestGrid
            {
                Characteristics = new List<Characteristics>()
                {
                    new Characteristics
                    {
                        Name = "A",
                        Value = "21",

                    },
                    new Characteristics
                    {
                        Name = "B",
                        Value = "B",
                    }
                }
            };

            DataContext = testGrid;

            Resources["StandartBackColor"] = new SolidColorBrush(DarkThem.Theme[0]);
            Resources["StandartForeColor"] = new SolidColorBrush(DarkThem.Theme[1]);
        }

        public void SetTheme(Color Background, Color Foreground)
        {
            Resources["StandartBackColor"] = new SolidColorBrush(Background);
            Resources["StandartForeColor"] = new SolidColorBrush(Foreground);
			MessageBox.Show("asdasdasdsasa");
		}
		// Меню
        private void UrlTusha(object sender, RoutedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://docs.google.com/document/d/1rUMWdTp645Zy80d09ZMoJ6dmcw9lAP25tqxsdNkftaY/edit?usp=sharing") { UseShellExecute = true });
		}
		private void UrlGunter(object sender, RoutedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://sites.google.com/view/rulesdndgunter/%D0%BF%D1%80%D0%B0%D0%B2%D0%B8%D0%BB%D0%B0") { UseShellExecute = true });
		}
		private void UrlWeapons(object sender, RoutedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://docs.google.com/document/d/1QRy-6NgwGB81BE0QE1DT_mO7DUGCf-PWrM9csBKEuno/edit?usp=sharing") { UseShellExecute = true });
		}
		private void UrlRaces(object sender, RoutedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://docs.google.com/document/d/1V5B_7he-2i8BzMviCW9aqQoo831FIbKfi0uRwVEJ4VY/edit?usp=sharing") { UseShellExecute = true });
		}
		private void UrlClasses(object sender, RoutedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://docs.google.com/document/d/19zM6JnIa5TNag2Adf22SV7ZI7uZUzzWZLvZbKvLylB0/edit?usp=sharing") { UseShellExecute = true });
		}

		private void Settings_Click(object sender, RoutedEventArgs e)
		{
			Settings settings = new Settings();
			settings.Show();	
		}
		// Дневник
		private void CreateNote_Click(object sender, RoutedEventArgs e)
		{
			FlowDocument flowDocument = diaryTB.Document;

			string text = new TextRange(
				flowDocument.ContentStart,
				flowDocument.ContentEnd
				).Text;
			MessageBox.Show(text);
		}

		// Размер шрифта надо починить!
		private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (fontSizeComboBox.SelectedItem is double fontSize && diaryTB.Selection != null)
			{
				diaryTB.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
			}
		}

		private void DiaryTB_SelectionChanged(object sender, RoutedEventArgs e)
		{
			UpdateFontSizeComboBox();
		}

		private void UpdateFontSizeComboBox()
		{
			if (diaryTB.CaretPosition == null) return;
			
			TextRange range = new TextRange(diaryTB.CaretPosition, diaryTB.CaretPosition);
			object fontSizeValue = range.GetPropertyValue(TextElement.FontSizeProperty);

			if (fontSizeValue is double fontSize)
			{
				fontSizeComboBox.SelectedValue = fontSize;
			}
			else
			{
				fontSizeComboBox.SelectedValue = 12.0; 
			}
		}

		private void DiaryTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			UpdateFontSizeComboBox();
		}
	}
}
