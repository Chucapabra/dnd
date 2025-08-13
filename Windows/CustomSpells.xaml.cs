using DNDHelper.Modules;
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
	/// Логика взаимодействия для CustomSpells.xaml
	/// </summary>
	public partial class CustomSpells : Window
	{
		public CustomSpells()
		{
			InitializeComponent();
		}

		private void Damage_Custom_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextboxProcessing.WholeNumbersOnly(Damage_Custom_textbox, e);
        }

		private void Damage_Custom_textbox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			e.CancelCommand();
			e.Handled = true;
		}
	}
}
