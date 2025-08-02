using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDHelper.Modules.Inventory
{
	public class Backpack
	{
		Main main = Main.Instance;

		int[] quantitymax1 = new int[] { 0, 1 };
		int[] quantitymax2 = new int[] { 0, 1, 2 };
		int[] quantitymax3 = new int[] {0, 1, 2, 3};

		public void ChangeComboBoxQuantity()
		{
			switch (main.backpack_cb.SelectedIndex)
			{
				case 0:
					main.backpack_quantity_cb.ItemsSource = quantitymax3;
					main.backpack_quantity_cb.SelectedIndex = 0;
					break;
				case 1:
					main.backpack_quantity_cb.ItemsSource = quantitymax2;
					main.backpack_quantity_cb.SelectedIndex = 0;
					break;
				case 2:
					main.backpack_quantity_cb.ItemsSource = quantitymax1;
					main.backpack_quantity_cb.SelectedIndex = 0;
					break;
				case 3:
					main.backpack_quantity_cb.ItemsSource = quantitymax3;
					main.backpack_quantity_cb.SelectedIndex = 0;
					break;
				case 4:
					main.backpack_quantity_cb.ItemsSource = quantitymax2;
					main.backpack_quantity_cb.SelectedIndex = 0;
					break;
				case 5:
					main.backpack_quantity_cb.ItemsSource = quantitymax2;
					main.backpack_quantity_cb.SelectedIndex = 0;
					break;
				case 6:
					main.backpack_quantity_cb.ItemsSource = quantitymax2;
					main.backpack_quantity_cb.SelectedIndex = 0;
					break;
				default:
					break;
			}
		}
	}
}
