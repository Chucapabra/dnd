using DND;
using DNDHelper.Modules;
using DNDHelper.Modules.Character;
using DNDHelper.Modules.Config;
using DNDHelper.Modules.Diary;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.MagicSpells;
using DNDHelper.Modules.Settings;
using DNDHelper.Modules.Сharacteristics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
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
using static System.Net.Mime.MediaTypeNames;
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
		public static TreeSkills TreeSkillsScript;
		public static ItemBaffsListScript ItemBaffsListScript;
		public static string PathMain = "";       



        public Main()
		{
			string[] _pathMain = Assembly.GetExecutingAssembly().Location.Split('\\');
            PathMain = string.Join("\\", _pathMain, 0, _pathMain.Count() - 1) + "\\";

            CheckUpdate.Check();
			DataManager.FindPathSaves();
			InitializeComponent();


			InitializeClasses();
			DataManager.ReadSaves();
			Resources["StandartBackColor"] = new SolidColorBrush(DarkThem.SelectedTheme[0]);
			Resources["StandartForeColor"] = new SolidColorBrush(DarkThem.SelectedTheme[1]);


            Closed += Main_Closed;
		}

        private void Main_Closed(object? sender, EventArgs e)
        {
			DataManager.Save(true);
        }

        public void InitializeClasses()
		{
			Instance = this;
			SavesMenu savesMenu = new SavesMenu();
			DataContext = DataManager.DataSave;

			
			Race @class = new();
			PlayerClass playerClass = new();
			ItemBaffsListScript = new();
			InventoryLoot inventoryLoot = new();
			Skills skills = new();
			Level level = new();
			MagicSpells magicSpells = new();
			DiaryManager diaryManager = new DiaryManager();
			TreeSkillsScript = new();
			Characteristics = new();
			Characteristics.UpdateAllCharacterisitc();
			MagicSearch magicSearch = new MagicSearch();
            TypeArmorBaffs typeArmorBaffs = new();


            WeightScript weightScript = new WeightScript();
			Health health = new Health();

			AttributesCharacter.CallAllMethodInScript();

		}

		public void SetTheme(Color Background, Color Foreground)
		{
			Resources["StandartBackColor"] = new SolidColorBrush(Background);
			Resources["StandartForeColor"] = new SolidColorBrush(Foreground);
		}
        // Меню
        private void UpdateConfingsInHands_Click(object sender, RoutedEventArgs e)
        {
			SetRepository.FileСonnection();
        }

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
				TreeSkillsScript.UpdateDisplayPoints();
				character_name_textblock.Visibility = Visibility.Visible;
				character_name_textbox.Visibility = Visibility.Hidden;
				character_race_textblock.Visibility = Visibility.Visible;
				character_race_combobox.Visibility = Visibility.Hidden;
				character_class_textblock.Visibility = Visibility.Visible;
				character_class_combobox.Visibility = Visibility.Hidden;
				Grid.SetColumnSpan(DataGridCurrentSpells, 4);
				Grid.SetColumnSpan(CurrentSpellDescription, 4);
                DataGridAllSpells.Visibility = Visibility.Collapsed;
				AllSpellDescription.Visibility = Visibility.Collapsed;
				DataGridCurrentSpellsSpellName.Width = 600;
			}
			else
			{
				Resources["IsEdit"] = Visibility.Visible;
				Characteristics.UpdatePointText(true);
				TreeSkillsScript.UpdateDisplayPoints();
				character_name_textblock.Visibility = Visibility.Collapsed;
				character_name_textbox.Visibility = Visibility.Visible;
				character_race_textblock.Visibility = Visibility.Collapsed;
				character_race_combobox.Visibility = Visibility.Visible;
				character_class_textblock.Visibility = Visibility.Collapsed;
				character_class_combobox.Visibility = Visibility.Visible;
				DataGridCurrentSpellsSpellName.Width = 200;
				Grid.SetColumnSpan(DataGridCurrentSpells, 2);
				Grid.SetColumnSpan(CurrentSpellDescription, 2);
				Grid.SetRowSpan(DataGridAllSpells, 4);
				DataGridAllSpells.Visibility = Visibility.Visible;
				AllSpellDescription.Visibility = Visibility.Collapsed;				
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
		// Древо Развитие
		private void AddTreeLevel_Click(object sender, RoutedEventArgs e)
		{
			TreeSkillsScript.AddTreeLevel();
		}

		private void SubtractTreeLevel_Click(object sender, RoutedEventArgs e)
		{
			TreeSkillsScript.SubstractTreeLevel();
		}
		// Дневник

		private void DiaryTB_SelectionChanged(object sender, RoutedEventArgs e)
		{
			diaryTB.Focus();
			TextChanges textChanges = new();
			textChanges.UpdateFontSizeDisplay();
		}

		private void DiaryTB_MouseMove(object sender, MouseEventArgs e)
		{
			diaryTB.Focus();
		}

		private void DiaryTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (diaryTB.Selection.IsEmpty)
			{
				TextChanges textChanges = new();
				textChanges.UpdateFontSizeDisplay();
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
		private void CurrentHealth_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{

		}
		private void CurrentHealth_tb_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
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
			TextboxProcessing.WholeNumbersOnly(shield_health_textbox, e);
		}
		private void shield_health_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

		private void BaffKD_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.WholeNumbersOnly(BaffKD_textbox, e);
		}
		private void BaffKD_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

		// Инвентарь
		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.WholeNumbersOnly(sender, e);
		}

		private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(typeof(string)))
			{
				string text = (string)e.DataObject.GetData(typeof(string));
				if (text.Any(c => !char.IsDigit(c)))
				{
					e.CancelCommand();
				}
			}
			else
			{
				e.CancelCommand();
			}
		}

		private void ComboBoxDataGridItemBaffsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ItemBaffsListScript.UpdateValues();
		}
		// Множитель урона магии
		private void DamageMagic_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}

		private void DamageMagic_textblock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DamageMagic_textblock.Visibility = Visibility.Collapsed;
			DamageMagic_textbox.Visibility = Visibility.Visible;
			DamageMagic_textbox.Text = DamageMagic_textblock.Text;
			DamageMagic_textbox.Focus();
		}

		private void DamageMagic_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.DoubleNumbersOnly(DamageMagic_textbox, e);
		}

		private void DamageMagic_textbox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				DamageMagic_textblock.Visibility = Visibility.Visible;
				DamageMagic_textbox.Visibility = Visibility.Collapsed;
				if (DamageMagic_textbox.Text.Length == 0 ||
					DamageMagic_textbox.Text == "-0" ||
					DamageMagic_textbox.Text == "-" ||
					DamageMagic_textbox.Text == "," ||
					DamageMagic_textbox.Text == "-0,"
					)
				{
                    DataManager.DataSave.MultiplyMagicDamage = 0;
					DamageHealth_textblock.Text = "0";
				}
                DataManager.DataSave.MultiplyMagicDamage = double.Parse(DamageMagic_textbox.Text);
			}

			MagicSpells.RepositoryLoad();
		}

		private void character_name_textbox_TextChanged(object sender, TextChangedEventArgs e)
		{
			character_name_textblock.Text = character_name_textbox.Text;
            DataManager.ReadSaves();
        }
		// Магические пули
		private void MagicBulletCurrent_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}
		private void MagicBulletCurrent_textblock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MagicBulletCurrent_textblock.Visibility = Visibility.Collapsed;
			MagicBulletCurrent_textbox.Visibility = Visibility.Visible;
			MagicBulletCurrent_textbox.Text = MagicBulletCurrent_textblock.Text;
			MagicBulletCurrent_textbox.Focus();
		}
		private void MagicBulletCurrent_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.WholeNumbersOnly(MagicBulletCurrent_textbox, e);
		}
		private void MagicBulletCurrent_textbox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				MagicBulletCurrent_textblock.Visibility = Visibility.Visible;
				MagicBulletCurrent_textbox.Visibility = Visibility.Collapsed;
				if (MagicBulletCurrent_textbox.Text.Length == 0 ||
					MagicBulletCurrent_textbox.Text == "-0" ||
					MagicBulletCurrent_textbox.Text == "-" ||
					MagicBulletCurrent_textbox.Text == "00" ||
					Convert.ToInt32(MagicBulletCurrent_textbox.Text) < 0
					)
				{
                    DataManager.DataSave.SubtractMagicBullet = 0;
					MagicBulletCurrent_textblock.Text = "0";
				}
				else
                DataManager.DataSave.SubtractMagicBullet = MagicSpells.MaxMagicBullet - Convert.ToInt32(MagicBulletCurrent_textbox.Text);
			}
		}


    }
}
