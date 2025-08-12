using DNDHelper.Modules.Character;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static DNDHelper.Modules.Inventory.InventoryLoot;

namespace DNDHelper.Modules.Inventory
{
    public class ItemBaffsListScript
    {

        Main main = Main.Instance;
        public static ObservableCollection<ItemBaff> ItemBaffsList { get; set; } = new();
        public static List<int[]> ItemBaffs = new(50);
        public ItemBaffsListScript() 
        {
            main.DataGridItemBaffsList.ItemsSource = ItemBaffsList;

            main.DataGridItemBaffsList.BeginningEdit += DataGridItemBaffsList_BeginningEdit; ;
            main.DataGridItemBaffsList.CellEditEnding += DataGridItemBaffsList_CellEditEnding; ;
            main.DataGridItemBaffsList.PreviewMouseLeftButtonDown += DataGridItemBaffsList_PreviewMouseButtonDown;
            main.DataGridItemBaffsList.PreviewMouseRightButtonDown += DataGridItemBaffsList_PreviewMouseButtonDown;
            main.DataGridItemBaffsList.ContextMenuOpening += DataGridItemBaffsList_ContextMenuOpening;
            main.AddMenuItemBaff.Click += AddMenuItemBaff_Click;
            main.DeleteMenuItemBaff.Click += DeleteMenuItemBaff_Click;

            ClearItemBaffs();
        }

        private void DataGridItemBaffsList_BeginningEdit(object? sender, DataGridBeginningEditEventArgs e)
        {
            ShowOriginalValue(e.Column.DisplayIndex, e.Row.GetIndex());
        }

        private void DataGridItemBaffsList_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditingElement is System.Windows.Controls.TextBox textBox)
            {
                string newValue = textBox.Text;

                ShowBaffValue(e.Column.DisplayIndex, e.Row.GetIndex(), newValue);
                
            }
        }

        private void ShowOriginalValue(int indexColumn, int indexRow)
        {
            int selectedIndex = main.DataGridInventory.SelectedIndex;
            switch (indexColumn)
            {
                case 1:
                    ItemBaffsList[indexRow].AddValue = ItemBaffsList[indexRow].AddValue / (float)QualityToDouble(InventoryItems[selectedIndex].Quality);
                    break;
                case 2:
                    ItemBaffsList[indexRow].AddRoll = ItemBaffsList[indexRow].AddRoll / (float)QualityToDouble(InventoryItems[selectedIndex].Quality);
                    break;
            }
        }

        private void ShowBaffValue(int indexColumn, int indexRow, string newValue)
        {
            int selectedIndex = main.DataGridInventory.SelectedIndex;
            newValue = newValue.Replace(" ", "");
            if (newValue != "" && newValue != "-")
                switch (indexColumn)
                {
                    case 1:
                        ItemBaffsList[indexRow].AddValue = int.Parse(newValue) * (float)QualityToDouble(InventoryItems[selectedIndex].Quality);
                        break;
                    case 2:
                        ItemBaffsList[indexRow].AddRoll = int.Parse(newValue) * (float)QualityToDouble(InventoryItems[selectedIndex].Quality);
                        break;

                }
            UpdateValues();
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
            if(main.DataGridInventory.SelectedIndex == -1)
            {
                main.AddMenuItemBaff.Visibility = Visibility.Collapsed;
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
                UpdateValues();
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
                {                    
                    if (item.Equipped)
                        foreach (var baff in item.Baffs)
                        {
                            if (baff.SelectedIndex != -1)
                            {
                                ItemBaffs[baff.SelectedIndex][0] = baff.WholeAddValue;
                                ItemBaffs[baff.SelectedIndex][1] = baff.WholeAddRoll;
                            }
                        }
                }
                Main.Characteristics.UpdateAllCharacterisitc();
                Health.HealthUpdate();
                AttributesCharacter.CallAllMethodInScript();
                KDScript.UpdateAllKD();
            }
        }

        private void ClearItemBaffs()
        {
            ItemBaffs.Clear();
            for (int i = 0;i < 50;i++)
                ItemBaffs.Add(new int[] {0, 0});
        }

        public static void SetQualityBaffs(int Stage, string Quality)
        {
            if(Stage == 0)
                foreach(var baff in ItemBaffsList)
                {
                    baff.AddValue = baff.AddValue / (float)QualityToDouble(Quality);
                    baff.AddRoll = baff.AddRoll / (float)QualityToDouble(Quality);
                }
            else
                foreach (var baff in ItemBaffsList)
                {
                    baff.AddValue = baff.AddValue * (float)QualityToDouble(Quality);
                    baff.AddRoll = baff.AddRoll / (float)QualityToDouble(Quality);
                }
        }

        public class ItemBaff : INotifyPropertyChanged
        {
            public int SelectedIndex { get; set; } = -1;

            private int _wholeAddValue;
            private double _addValue;
            public int WholeAddValue
            {
                get => _wholeAddValue;
                set
                {
                    _wholeAddValue = value;
                    OnPropertyChanged();
                }
            }
            public double AddValue
            {
                get => _addValue;
                set
                {
                    _addValue = value;
                    _wholeAddValue = (int)_addValue;
                    OnPropertyChanged();
                }
            }
            private int _wholeAddRoll;
            private double _addRoll;
            public int WholeAddRoll
            {
                get => _wholeAddRoll;
                set
                {
                    _wholeAddRoll = value;
                    OnPropertyChanged();
                }
            }
            public double AddRoll
            {
                get => _addRoll;
                set
                {
                    _addRoll = value;
                    _wholeAddRoll = (int)_addRoll;
                    OnPropertyChanged();
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

//Ролл атаки - 25
//Ролл блока - 26
//Ролл уворота -  27
//Ролл контрдействия - 28
//Ролл атаки кулаками - 29
//Ролл дальнобойным - 30
//Вес - 31
//Здоровье- 32
//Ментальное КД - 33
//Палка Хода - 34
//Действие - 35
//Контрдействие - 36