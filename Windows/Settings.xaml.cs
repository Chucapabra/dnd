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
				switch (selectionIndex)
				{
					case 0:
						ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
						break;
					case 1:
						ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
						break;
				}
			}
        }

		private void AddProfile_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
