using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
	/// Логика взаимодействия для About.xaml
	/// </summary>
	public partial class About : Window
	{
		public About()
		{
			InitializeComponent();
			string[] _pathMain = Assembly.GetExecutingAssembly().Location.Split('\\');
			string pathVersion = string.Join("\\", _pathMain, 0, _pathMain.Count() - 2) + "\\Version.txt";
			string version = File.ReadAllText(pathVersion);
			VersionTextBlock.Text = version;
		}
	}
}
