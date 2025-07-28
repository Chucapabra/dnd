using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DNDHelper.Windows
{
	/// <summary>
	/// Логика взаимодействия для Main.xaml
	/// </summary>
	public partial class Main : Window
	{

		public Main()
		{
			InitializeComponent();
			
		}

		private void UrlTusha(object sender, RoutedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://docs.google.com/document/d/1rUMWdTp645Zy80d09ZMoJ6dmcw9lAP25tqxsdNkftaY/edit?usp=sharing") { UseShellExecute = true });
		}

		private void Settings_Click(object sender, RoutedEventArgs e)
		{
			Settings settings = new Settings();
			settings.Show();
		}
	}
}
