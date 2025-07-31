using DNDHelper.Modules.Diary;
using DNDHelper.Modules.Сharacteristics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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

using DarkThem = DNDHelper.Modules.Settings.Settings;

namespace DNDHelper.Windows
{
	/// <summary>
	/// Логика взаимодействия для Main.xaml
	/// </summary>
	public partial class Main : Window
	{


		public static Main Instance;
        public static GridCharacteristics Characteristics;

        public Main()
		{
			InitializeComponent();
			LoadData();
			fontSizeComboBox.SelectedValue = 12.0; // Размер шрифта в дневнике по умолчанию

			Instance = this;

            Resources["IsEdit"] = Visibility.Hidden;

            Characteristics = new();
            GridCharacteristics.SetChars();

            Resources["StandartBackColor"] = new SolidColorBrush(DarkThem.Theme[0]);
            Resources["StandartForeColor"] = new SolidColorBrush(DarkThem.Theme[1]);


        }
		public class InventoryItem
		{
			public string Название { get; set; }
			public int Вес { get; set; }
			public string Качество { get; set; } // Для ComboBox
			public double Рубящий { get; set; }
			public double Колющий { get; set; }
			public double Дробяший { get; set; }
			public double Тяжесть { get; set; }
			public double КД { get; set; }
			public int Количество { get; set; }
			public double СчётВеса { get; set; }
			public string Описание { get; set; }
			public double СчётКД { get; set; }
			public double СчётКДШлема { get; set; }
		}
		public ObservableCollection<InventoryItem> InventoryItems { get; set; }
		private void LoadData()
		{
			InventoryItems = new ObservableCollection<InventoryItem>();
			InventoryItems.Add(new InventoryItem
			{
				Название = "Меч рыцаря",
				Вес = 1,
				Качество = "Хорошо", // Убедитесь, что это значение есть в вашем списке для ComboBox
				Рубящий = 15,
				Колющий = 10,
				Дробяший = 0,
				Тяжесть = 0.5,
				КД = 1.2,
				Количество = 1,
				СчётВеса = 1.5,
				Описание = "Старый добрый меч.",
				СчётКД = 0,
				СчётКДШлема = 0
			});

			InventoryItems.Add(new InventoryItem
			{
				Название = "Щит деревянный",
				Вес = 3,
				Качество = "Удовлетворительно",
				Рубящий = 0,
				Колющий = 0,
				Дробяший = 5,
				Тяжесть = 1.0,
				КД = 2.5,
				Количество = 1,
				СчётВеса = 3.0,
				Описание = "Простой деревянный щит.",
				СчётКД = 0,
				СчётКДШлема = 0
			});
			DataGridInventory.ItemsSource = InventoryItems;
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
		private void EditMode_click(object sender, RoutedEventArgs e)
		{
            if (EditMode_button.IsChecked == false)
                Resources["IsEdit"] = Visibility.Hidden;
            else
                Resources["IsEdit"] = Visibility.Visible;
        }

		private void Settings_Click(object sender, RoutedEventArgs e)
		{
			Settings settings = new();
			settings.ShowDialog();
	
		}
        // Характеристики
        private void AddCharacteristic_Click(object sender, RoutedEventArgs e)
        {
            Characteristics.AddCharacteristic_Click();
        }

        private void SubtractCharacteristic_Click(object sender, RoutedEventArgs e)
        {
            Characteristics.SubtractCharacteristic_Click();
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



		private void DiaryTB_SelectionChanged(object sender, RoutedEventArgs e)
		{
			TextChanges textChanges = new();
			textChanges.UpdateFontSizeDisplay();
		}

		private void DiaryTB_MouseMove(object sender, MouseEventArgs e)
		{
			if (diaryTB.Selection.IsEmpty)
			{
				TextChanges textChanges = new();
				textChanges.UpdateFontSizeDisplay();
			}
		}

		private void DiaryTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (diaryTB.Selection.IsEmpty)
			{
				TextChanges textChanges = new();
				textChanges.UpdateFontSizeComboBox();
			}
		}
	}
}
