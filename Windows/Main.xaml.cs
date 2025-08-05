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
using DNDHelper.Modules;
using DNDHelper.Modules.Config;
using DNDHelper.Modules.Diary;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Сharacteristics;
using Microsoft.Win32;
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
			InitializeClasses();


            Resources["StandartBackColor"] = new SolidColorBrush(DarkThem.Theme[0]);
			Resources["StandartForeColor"] = new SolidColorBrush(DarkThem.Theme[1]);

			MaxHealth_textblock.Text = "100";
			CurrentHealth_textblock.Text = "-99";


        }
		public void InitializeClasses()
		{
			Instance = this;

			Race @class = new Race();
			PlayerClass playerClass = new();
			InventoryLoot inventoryLoot = new();
            Characteristics = new();
            GridCharacteristics.SetChars();
            Characteristics.UpdateAllCharacterisitc();
        }

		

		public void SetTheme(Color Background, Color Foreground)
		{
			Resources["StandartBackColor"] = new SolidColorBrush(Background);
			Resources["StandartForeColor"] = new SolidColorBrush(Foreground);
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
			{
				Resources["IsEdit"] = Visibility.Hidden;
				Characteristics.UpdatePointText(false);
				character_name_textblock.Visibility = Visibility.Visible;
				character_name_textbox.Visibility = Visibility.Hidden;
				character_race_textblock.Visibility = Visibility.Visible;
				character_race_combobox.Visibility = Visibility.Hidden;
				character_class_textblock.Visibility = Visibility.Visible;
				character_class_combobox.Visibility = Visibility.Hidden;
			}
			else
			{
				Resources["IsEdit"] = Visibility.Visible;
				Characteristics.UpdatePointText(true);
				character_name_textblock.Visibility = Visibility.Collapsed;
				character_name_textbox.Visibility = Visibility.Visible;
				character_race_textblock.Visibility = Visibility.Collapsed;
				character_race_combobox.Visibility = Visibility.Visible;
				character_class_textblock.Visibility = Visibility.Collapsed;
				character_class_combobox.Visibility = Visibility.Visible;
			}
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

		private void backpack_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Backpack backpack = new();
			backpack.ChangeComboBoxQuantity();
		}

		// Медяки
		private void Cuprum_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}
			TextboxProcessing.WholeNumbersOnly(Cuprum_tb, e);
		}
		private void Cuprum_tb_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}
		// Серебреники
		private void Silver_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}
			TextboxProcessing.WholeNumbersOnly(Silver_tb, e);
		}
		private void Silver_tb_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}
		// Золотые
		private void Gold_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}
			TextboxProcessing.WholeNumbersOnly(Gold_tb, e);
		}
		private void Gold_tb_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}
		// Плюс к броне
		private void backpack_plus_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}
			TextboxProcessing.WholeNumbersOnly(backpack_plus_tb, e);
		}
		private void backpack_plus_tb_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}
		// Загрузка изображения
		private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			OpenFileDialog LoadImageCharacter = new();
			LoadImageCharacter.Filter = "Изображения (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
			if (LoadImageCharacter.ShowDialog() == true)
			{
				try
				{
					BitmapImage bitmap = new();
					bitmap.BeginInit();
					bitmap.UriSource = new Uri(LoadImageCharacter.FileName);
					bitmap.EndInit();
					ImageCharacter.Source = bitmap;
					ImageIconCharacter.Visibility = Visibility.Collapsed;
					ImageCharacter.Visibility = Visibility.Visible;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
				}

			}
		}
		// Изменение ХП
		private void CurrentHealth_textblock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CurrentHealth_textblock.Visibility = Visibility.Collapsed;
			CurrentHealth_textbox.Visibility = Visibility.Visible;
			CurrentHealth_textbox.Focus();
		}
		private void CurrentHealth_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.WholeNumbersOnly(CurrentHealth_textbox, e);
		}
		private void CurrentHealth_tb_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

		private void CurrentHealth_textbox_KeyDown(object sender, KeyEventArgs e)
		{
			var textBox = sender as TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}
			if (e.Key == Key.Enter)
			{
				string text = CurrentHealth_textbox.Text;
				CurrentHealth_textblock.Text = text;
				if (text.Length == 0 || CurrentHealth_textblock.Text == "-")
				{
					CurrentHealth_textblock.Text = "0";
					CurrentHealth_textbox.Text = "0";
				}
				CurrentHealth_textblock.Visibility = Visibility.Visible;
				CurrentHealth_textbox.Visibility = Visibility.Collapsed;
				if (Convert.ToInt32(CurrentHealth_textblock.Text) <= 0)
				{
					СriticalRoll_Text_textblock.Visibility = Visibility.Visible;
					СriticalRoll_GreaterOrEqual_textblock.Visibility = Visibility.Visible;
					СriticalRoll_Number_textblock.Visibility = Visibility.Visible;
					СriticalRoll_Number_textblock.Text = Convert.ToString((Math.Abs(Convert.ToInt32(CurrentHealth_textblock.Text)) * 100) / (Convert.ToInt32(MaxHealth_textblock.Text)) / 5 + 10);
				}
				else
				{
					СriticalRoll_Text_textblock.Visibility = Visibility.Hidden;
					СriticalRoll_GreaterOrEqual_textblock.Visibility = Visibility.Hidden;
					СriticalRoll_Number_textblock.Visibility = Visibility.Hidden;
				}

			}
		}

		private void ResetHealth_Click(object sender, RoutedEventArgs e)
		{
			СriticalRoll_Text_textblock.Visibility = Visibility.Hidden;
			СriticalRoll_GreaterOrEqual_textblock.Visibility = Visibility.Hidden;
			СriticalRoll_Number_textblock.Visibility = Visibility.Hidden;
			CurrentHealth_textblock.Text = MaxHealth_textblock.Text;
			CurrentHealth_textbox.Text = MaxHealth_textblock.Text;
			if (CurrentHealth_textbox.Focus())
			{
				CurrentHealth_textbox.Text = MaxHealth_textblock.Text;
			}


		}
		// Счёт урона
		private void CountDamage_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show("213");
        }
		
		private void shield_health_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}
			TextboxProcessing.WholeNumbersOnly(shield_health_textbox, e);
		}
		private void shield_health_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

		private void BaffKD_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var textBox = sender as TextBox;
			int caretIndex = textBox.CaretIndex;
			string newText = textBox.Text.Replace(" ", "");
			if (textBox.Text != newText)
			{
				textBox.Text = newText;
				textBox.CaretIndex = Math.Min(caretIndex, newText.Length);
			}
			TextboxProcessing.WholeNumbersOnly(BaffKD_textbox, e);
		}
		private void BaffKD_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextboxProcessing.WholeNumbersOnly(sender, e);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            // Проверяем вставку
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (text.Any(c => !char.IsDigit(c)))
                {
                    e.CancelCommand(); // Блокируем вставку
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
