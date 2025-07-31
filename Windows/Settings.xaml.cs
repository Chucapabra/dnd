using ModernWpf;
using System;
using System.Collections.Generic;
using System.Linq;
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
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using DarkThem = DNDHelper.Modules.Settings.Settings;

namespace DNDHelper.Windows
{
	/// <summary>
	/// Логика взаимодействия для Settings.xaml
	/// </summary>
	public partial class Settings : Window
	{
		public Settings()
		{
			InitializeComponent();
		}

		private void ComboBoxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;

			if (comboBox != null)
			{
				int selectionIndex = comboBox.SelectedIndex;
				object selectionItem = ComboBoxTheme.SelectedItem;
				
				Main main = new();
				var paletteHelper = new PaletteHelper();
				Theme theme = paletteHelper.GetTheme();
				switch (selectionIndex)
				{
					case 0:
						DarkThem.IsDarkTheme = false;
						Main.Instance.SetTheme(DarkThem.Theme[1], DarkThem.Theme[0]);
                        theme.SetBaseTheme(BaseTheme.Light);
						break;
					case 1:
						DarkThem.IsDarkTheme = true;
                        Main.Instance.SetTheme(DarkThem.Theme[0], DarkThem.Theme[1]);
                        theme.SetBaseTheme(BaseTheme.Dark);
						break;
				}
				paletteHelper.SetTheme(theme);
			}
        }

		private void AddProfile_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
