using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using static DNDHelper.Modules.Inventory.InventoryLoot;

namespace DNDHelper.Modules.Inventory
{
    public class ItemBaffsListScript
    {

        Main main = Main.Instance;
        public static ObservableCollection<ItemBaff> ItemBaffsList { get; set; } = new();
        public static List<int[]> ItemBaffs = new(25);
        public ItemBaffsListScript() 
        {
            main.DataGridItemBaffsList.ItemsSource = ItemBaffsList;
            main.DataGridItemBaffsList.PreviewMouseLeftButtonDown += DataGridItemBaffsList_PreviewMouseButtonDown;
            main.DataGridItemBaffsList.PreviewMouseRightButtonDown += DataGridItemBaffsList_PreviewMouseButtonDown;
            main.DataGridItemBaffsList.ContextMenuOpening += DataGridItemBaffsList_ContextMenuOpening;
            main.AddMenuItemBaff.Click += AddMenuItemBaff_Click;
            main.DeleteMenuItemBaff.Click += DeleteMenuItemBaff_Click;

            ClearItemBaffs();
        }

        private void DataGridItemBaffsList_PreviewMouseButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cell = FindParent<DataGridCell>(e.OriginalSource as DependencyObject);
            


            if (cell == null)
                main.DataGridItemBaffsList.SelectedIndex = -1;
        }

        private void DataGridItemBaffsList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            int selectedIndex = main.DataGridItemBaffsList.SelectedIndex;
            if (selectedIndex != -1)
            {
                main.AddMenuItemBaff.Visibility = Visibility.Visible;
                main.DeleteMenuItemBaff.Visibility = Visibility.Visible;
            }
            else
            {
                main.AddMenuItemBaff.Visibility = Visibility.Visible;
                main.DeleteMenuItemBaff.Visibility = Visibility.Collapsed;
            }
        }
        private void AddMenuItemBaff_Click(object sender, RoutedEventArgs e)
        {
            ItemBaffsList.Add(new ItemBaff());
        }

        private void DeleteMenuItemBaff_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = main.DataGridItemBaffsList.SelectedIndex;
            if (selectedIndex != -1)
            {
                int selectedCount = main.DataGridItemBaffsList.SelectedItems.Count;
                MessageBoxResult result;
                if (selectedCount == 1)
                    result = MessageBox.Show($"Вы точно хотите удалить Бафф", "Удалить Бафф", MessageBoxButton.YesNo);
                else
                    result = MessageBox.Show($"Вы точно хотите удалить Баффы", "Удалить Баффы", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    for (int i = 0; i < selectedCount; i++)
                    {
                        ItemBaffsList.RemoveAt(main.DataGridItemBaffsList.SelectedIndex);
                    }
            }
        }

        public void UpdateValues()
        {
            int selectedItemIndex = main.DataGridInventory.SelectedIndex;
            if (selectedItemIndex != -1)
            {
                InventoryItems[selectedItemIndex].Baffs.Clear();
                foreach (var baff in ItemBaffsList)
                    if (baff.AddValue != 0 || baff.AddRoll != 0)
                        InventoryItems[selectedItemIndex].Baffs.Add(baff);

                ClearItemBaffs();
                foreach (var item in InventoryItems)
                    foreach (var baff in InventoryItems[selectedItemIndex].Baffs)
                    {
                        ItemBaffs[baff.SelectedIndex][0] = baff.AddValue;
                        ItemBaffs[baff.SelectedIndex][1] = baff.AddRoll;
                    }
                GridCharacteristics.Instance.UpdateAllCharacterisitc();
            }
        }

        private void ClearItemBaffs()
        {
            ItemBaffs.Clear();
            for (int i = 0;i < 50;i++)
                ItemBaffs.Add(new int[] {0, 0});
        }

        public class ItemBaff
        {
            public int SelectedIndex { get; set; } = -1;
            public int AddValue { get; set; }
            public int AddRoll { get; set; }

        }
    }
}
