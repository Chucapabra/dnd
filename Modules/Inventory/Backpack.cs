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
		static public int AddWeight = 0;
        static public int DivideItemWeight = 1;

        public Backpack() 
		{
            main.backpack_quantity_cb.SelectionChanged += Backpack_quantity_cb_SelectionChanged;

        }

        private void Backpack_quantity_cb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
			UpdateAddWeight();
        }

		private void UpdateAddWeight()
		{
			AddWeight = addBackpackWeight * main.backpack_quantity_cb.SelectedIndex;

            if (main.backpack_quantity_cb.SelectedIndex != 0) DivideItemWeight = divideItemWeight;
            else DivideItemWeight = 1;

            WeightScript.CountWeightItems();
		}

        int[] quantitymax1 = new int[] { 0, 1 };
		int[] quantitymax2 = new int[] { 0, 1, 2 };
		int[] quantitymax3 = new int[] {0, 1, 2, 3};
		int addBackpackWeight = 0;
        int divideItemWeight = 1;

        public void ChangeComboBoxQuantity()
		{
			switch (main.backpack_cb.SelectedIndex)
			{
                case 0:
                    divideItemWeight = 1;
                    addBackpackWeight = 0;
                    main.backpack_quantity_cb.ItemsSource = new int[0];
                    main.backpack_quantity_cb.SelectedIndex = 0;
                    break;
                case 1:
                    divideItemWeight = 1;
                    addBackpackWeight = 30;
                    main.backpack_quantity_cb.ItemsSource = quantitymax3;
					main.backpack_quantity_cb.SelectedIndex = 0;
					break;
				case 2:
                    divideItemWeight = 1;
                    addBackpackWeight = 30;
                    main.backpack_quantity_cb.ItemsSource = quantitymax2;
					main.backpack_quantity_cb.SelectedIndex = 0;
                    break;
				case 3:
                    divideItemWeight = 2;
                    addBackpackWeight = 0;
                    main.backpack_quantity_cb.ItemsSource = quantitymax1;
					main.backpack_quantity_cb.SelectedIndex = 0;
                    break;
				case 4:
                    divideItemWeight = 2;
                    main.backpack_quantity_cb.ItemsSource = quantitymax3;
					main.backpack_quantity_cb.SelectedIndex = 0;
                    break;
				case 5:
                    divideItemWeight = 2;
                    addBackpackWeight = 60;
                    main.backpack_quantity_cb.ItemsSource = quantitymax2;
					main.backpack_quantity_cb.SelectedIndex = 0;
                    break;
				case 6:
                    divideItemWeight = 2;
                    addBackpackWeight = 100;
                    main.backpack_quantity_cb.ItemsSource = quantitymax2;
					main.backpack_quantity_cb.SelectedIndex = 0;
                    break;
				case 7:
                    divideItemWeight = 3;
                    addBackpackWeight = 100;
                    main.backpack_quantity_cb.ItemsSource = quantitymax2;
					main.backpack_quantity_cb.SelectedIndex = 0;
                    break;
				default:
					break;
			}

        }
	}
}
