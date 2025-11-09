using DNDHelper.Modules.Character;
using DNDHelper.Modules.Settings;
using DNDHelper.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static DNDHelper.Modules.Inventory.ItemBaffsListScript;

namespace DNDHelper.Modules.Inventory
{
    public class InventoryLoot
    {
        Main main = Main.Instance;

        public static ObservableCollection<InventoryItem> InventoryItems => DataManager.DataSave.Inventory;

        public InventoryLoot()
        {
            main.DataGridInventory.ItemsSource = InventoryItems;


            main.DataGridInventory.BeginningEdit += DataGridInventory_BeginningEdit;
            main.DataGridInventory.CellEditEnding += DataGridInventory_CellEditEnding; ;
            main.DataGridInventory.PreviewMouseLeftButtonDown += DataGridInventory_PreviewMouseLeftButtonDown;
            main.DataGridInventory.PreviewMouseRightButtonDown += DataGridInventory_PreviewMouseRightButtonDown;
            main.DataGridInventory.SelectionChanged += DataGridInventory_SelectionChanged;
            main.DescriptionTextBox.TextChanged += DescriptionTextBox_TextChanged;
            main.DataGridInventory.ContextMenuOpening += DataGridInventory_ContextMenuOpening;
            main.AddMenuItem.Click += AddMenuItem_Click;
            main.DeleteMenuItem.Click += DeleteMenuItem_Click;
            main.CopyMenuItem.Click += CopyMenuItem_Click;
            main.PasteMenuItem.Click += PasteMenuItem_Click;
            main.weight_count_checkbox.Click += Weight_count_checkbox_Checked; ;
            main.kd_count_checkbox.Click += Kd_count_checkbox_Checked; ;
            main.kdhelmet_count_checkbox.Click += Kdhelmet_count_checkbox_Checked;
            main.equipped_checkbox.Click += Equipped_checkbox_Click;

            InventoryItems.CollectionChanged += InventoryItems_CollectionChanged;
        }

        private void InventoryItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DataManager.Save();
        }

        private void DataGridInventory_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            int selectedIndex = main.DataGridInventory.SelectedIndex;
            if (selectedIndex != -1)
            {
                main.AddMenuItem.Visibility = Visibility.Visible;
                main.DeleteMenuItem.Visibility = Visibility.Visible;
                main.CopyMenuItem.Visibility = Visibility.Visible;
            }
            else
            {
                main.AddMenuItem.Visibility = Visibility.Visible;
                main.DeleteMenuItem.Visibility = Visibility.Collapsed;
                main.CopyMenuItem.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = main.DataGridInventory.SelectedIndex;
            if (selectedIndex != -1)
            {
                int selectedCount = main.DataGridInventory.SelectedItems.Count;
                MessageBoxResult result;
                if (selectedCount == 1)
                    result = MessageBox.Show($"Вы точно хотите удалить предмет: {InventoryItems[selectedIndex].Name}", "Удалить предмет", MessageBoxButton.YesNo);
                else
                    result = MessageBox.Show($"Вы точно хотите удалить {selectedCount} предметов", "Удалить предметы", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    for (int i = 0; i < selectedCount; i++)
                    {
                        InventoryItems.RemoveAt(main.DataGridInventory.SelectedIndex);
                    }
            }
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            InventoryItems.Add(new InventoryItem());
        }

        private void CopyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = main.DataGridInventory.SelectedIndex;
            List<InventoryItem> selectedItems = new();
            if (selectedIndex != -1)
            {
                int selectedCount = main.DataGridInventory.SelectedItems.Count;
                foreach (var selectedItem in main.DataGridInventory.SelectedItems)
                {
                    int index = main.DataGridInventory.Items.IndexOf(selectedItem);
                    var item = InventoryItems[index];
                    selectedItems.Add(item);
                }
            }

            string jsontext = JsonSerializer.Serialize(selectedItems);
            Clipboard.SetText(jsontext);
        }

        private void PasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string pasteText = Clipboard.GetText();
            try
            {
                var pasteItems = JsonSerializer.Deserialize<List<InventoryItem>>(pasteText);
                pasteItems.Reverse();
                foreach (var item in pasteItems)
                {
                    item.Weight /= item.Count;
                    InventoryItems.Add(item);
                }
            }
            catch { }
            ;

        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (main.DataGridInventory.SelectedItems.Count > 0)
            {
                var item = InventoryItems[main.DataGridInventory.SelectedIndex];
                item.Description = main.DescriptionTextBox.Text;
            }
        }

        private void Weight_count_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if (main.DataGridInventory.SelectedItems.Count > 0)
            {
                var item = InventoryItems[main.DataGridInventory.SelectedIndex];
                item.CountWeight = main.weight_count_checkbox.IsChecked.Value;
            }
            WeightScript.CountWeightItems();
        }

        private void Kd_count_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if (main.DataGridInventory.SelectedItems.Count > 0)
            {
                var item = InventoryItems[main.DataGridInventory.SelectedIndex];
                item.CountKD = main.kd_count_checkbox.IsChecked.Value;
            }
        }

        private void Kdhelmet_count_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if (main.DataGridInventory.SelectedItems.Count > 0)
            {
                var item = InventoryItems[main.DataGridInventory.SelectedIndex];
                item.CountKDHelmet = main.kdhelmet_count_checkbox.IsChecked.Value;
            }
        }

        private void Equipped_checkbox_Click(object sender, RoutedEventArgs e)
        {
            if (main.DataGridInventory.SelectedItems.Count > 0)
            {
                var item = InventoryItems[main.DataGridInventory.SelectedIndex];
                item.Equipped = main.equipped_checkbox.IsChecked.Value;
            }
        }


        private int _lastSelectedItem = 0;

        private void DataGridInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (main.DataGridInventory.SelectedItems.Count > 0)
            {
                if (_lastSelectedItem != main.DataGridInventory.SelectedIndex)
                {
                    _lastSelectedItem = main.DataGridInventory.SelectedIndex;
                    main.DataGridInventory.CommitEdit();                 
                }


                var item = InventoryItems[main.DataGridInventory.SelectedIndex];
                if (item != null)
                {                   
                    main.DescriptionTextBox.IsReadOnly = false;
                    main.weight_count_checkbox.IsEnabled = true;
                    main.kd_count_checkbox.IsEnabled = true;
                    main.kdhelmet_count_checkbox.IsEnabled = true;
                    main.equipped_checkbox.IsEnabled = true;

                    main.weight_count_checkbox.IsChecked = item.CountWeight;
                    main.kd_count_checkbox.IsChecked = item.CountKD;
                    main.kdhelmet_count_checkbox.IsChecked = item.CountKDHelmet;
                    main.equipped_checkbox.IsChecked = item.Equipped;
                    main.DescriptionTextBox.Text = item.Description;
                    ItemBaffsList.Clear();
                    foreach (var baff in item.Baffs)
                        ItemBaffsList.Add(baff);
                }
            }
            else
            {
                main.DescriptionTextBox.Text = "";
                main.DescriptionTextBox.IsReadOnly = true;
                ItemBaffsList.Clear();

                main.weight_count_checkbox.IsEnabled = false;
                main.kd_count_checkbox.IsEnabled = false;
                main.kdhelmet_count_checkbox.IsEnabled = false;
                main.equipped_checkbox.IsEnabled = false;
                main.weight_count_checkbox.IsChecked = false;
                main.kd_count_checkbox.IsChecked = false;
                main.kdhelmet_count_checkbox.IsChecked = false;
                main.equipped_checkbox.IsChecked = false;
            }
		}

        private void DataGridInventory_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cell = FindParent<DataGridCell>(e.OriginalSource as DependencyObject);
            main.DataGridInventory.IsReadOnly = false;

            if (cell != null && !cell.IsEditing)
            {
                bool isCtrlPressed = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
                bool isShiftPressed = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

                if (e.ClickCount == 1)
                {
                    if (isCtrlPressed || isShiftPressed)
                    {
                        main.DataGridInventory.Focus();
                        main.DataGridInventory.IsReadOnly = true;
                        return;
                    }
                    main.DataGridInventory.SelectedItem = cell.DataContext;
                    e.Handled = true;
                }
            }
            else if (cell == null && e.ClickCount == 2)
            {
                main.DataGridInventory.CommitEdit();
                main.DataGridInventory.SelectedIndex = -1;
            }
            else
                main.DataGridInventory.SelectedIndex = -1;
        }
        private void DataGridInventory_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cell = FindParent<DataGridCell>(e.OriginalSource as DependencyObject);

            if (cell == null)
                main.DataGridInventory.SelectedIndex = -1;

        }

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (child != null && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }

        private void DataGridInventory_BeginningEdit(object? sender, System.Windows.Controls.DataGridBeginningEditEventArgs e)
        {
            ShowOriginalValue(e.Column.DisplayIndex, e.Row.GetIndex());
        }

        private void DataGridInventory_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditingElement is System.Windows.Controls.TextBox textBox)
            {
                string newValue = textBox.Text;

                ShowBaffValue(e.Column.DisplayIndex, e.Row.GetIndex(), newValue);
            }
        }

        private void ShowOriginalValue(int indexColumn, int indexRow)
        {
            switch (indexColumn)
            {
                case 1:
                    if (InventoryItems[indexRow].Weight != 0)
                        InventoryItems[indexRow].Weight = InventoryItems[indexRow].Weight / InventoryItems[indexRow].Count;
                    break;
                case 3:
                    InventoryItems[indexRow].Chopping = InventoryItems[indexRow].Chopping / QualityToDouble(InventoryItems[indexRow].Quality);
                    break;
                case 4:
                    InventoryItems[indexRow].Stabbing = InventoryItems[indexRow].Stabbing / QualityToDouble(InventoryItems[indexRow].Quality);
                    break;
                case 5:
                    InventoryItems[indexRow].Crushing = (InventoryItems[indexRow].Crushing - InventoryItems[indexRow].DamageSeverity) / QualityToDouble(InventoryItems[indexRow].Quality);
                    break;
                case 7:
                    InventoryItems[indexRow].KD = InventoryItems[indexRow].KD / QualityToDouble(InventoryItems[indexRow].Quality);
                    break;

            }
        }

        private void ShowBaffValue(int indexColumn, int indexRow, string newValue)
        {
            newValue = newValue.Replace(" ", "");
            if (newValue != "" && newValue != "-")
                switch (indexColumn)
                {
                    case 1:
                        InventoryItems[indexRow].Weight = int.Parse(newValue) * InventoryItems[indexRow].Count;
                        break;
                    case 3:
                        InventoryItems[indexRow].Chopping = int.Parse(newValue) * QualityToDouble(InventoryItems[indexRow].Quality);
                        break;
                    case 4:
                        InventoryItems[indexRow].Stabbing = int.Parse(newValue) * QualityToDouble(InventoryItems[indexRow].Quality);
                        break;
                    case 5:
                        InventoryItems[indexRow].Crushing = (int.Parse(newValue) * QualityToDouble(InventoryItems[indexRow].Quality) + InventoryItems[indexRow].DamageSeverity);
                        break;
                    case 7:
                        InventoryItems[indexRow].KD = int.Parse(newValue) * QualityToDouble(InventoryItems[indexRow].Quality);
                        KDScript.CountKD();
                        break;
                }

		}

        public static double QualityToDouble(string quality, bool IsDebaff = false)
        {
            float Baff = 1f;
            float Debaff = 1f;
            switch (quality)
            {
                case "Прото":
                    Baff = 0.5f;
                    Debaff = 1.5f;
                    break;

                case "Плохое":
                    Baff = 0.75f;
                    Debaff = 1.25f;
                    break;

                case "Стандартное":
                    Baff = 1.0f;
                    Debaff = 1.0f;
                    break;

                case "Высокое":
                    Baff = 1.25f;
                    Debaff = 0.75f;
                    break;

                case "Великолепное":
                    Baff = 1.5f;
                    Debaff = 0.5f;
                    break;

                case "Мастерское":
                    Baff = 2.0f;
                    Debaff = 0.25f;
                    break;

                case "Мифическое":
                    Baff = 3.0f;
                    Debaff = 0f;
                    break;

                default:
                    break;
            }
            if(!IsDebaff) return Baff;
            else return Debaff;
        }

        public static int CalculateHeavinessDamage(int HeavinessNumber, string Quality)
        {
            decimal Baff = 0;
            switch (Quality)
            {

                case "Прото":
                    Baff = 1;
                    break;

                case "Плохое":
                    Baff = 2;
                    break;

                case "Стандартное":
                    Baff = 3;
                    break;

                case "Высокое":
                    Baff = 4;
                    break;

                case "Великолепное":
                    Baff = 5;
                    break;

                case "Мастерское":
                    Baff = 6;
                    break;

                case "Мифическое":
                    Baff = 7;
                    break;

                default:
                    break;
            }

            int HeavinessDamage = 0;

            if (DataManager.DataSave.SelectedRepository == SetRepository.repositories[0].Name)
                HeavinessDamage = (int)(Baff * HeavinessNumber);
            else
                HeavinessDamage = (int)((double)(5 * Baff * HeavinessNumber));  
            return HeavinessDamage;
        }


        public class InventoryItem : INotifyPropertyChanged
        {
            public string _name = "";
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }

            private int _weight;
            public int Weight
            {
                get => _weight;
                set
                {
                    _weight = value;
                    WeightScript.CountWeightItems();
                    OnPropertyChanged();
                }
            }

            private string _quality = "Нет";
            public string Quality
            {
                get => _quality;
                set
                {
                    ReturnAllValue();
                    SetQualityBaffs(0, _quality);
                    _quality = value;
                    UpdateAllValue();
                    SetQualityBaffs(1, _quality);
                    KDScript.CountKD();
                    OnPropertyChanged();

				}
            }


            private int _wholeChopping;
            private double _chopping;
            public int WholeChopping
            {
                get => _wholeChopping;
                set
                {
                    _wholeChopping = value;
                    OnPropertyChanged();
                }
            }
            public double Chopping
            {
                get => _chopping;
                set
                {
                    _chopping = value;
                    WholeChopping = (int)_chopping;
                    OnPropertyChanged();
                }
            }


            private int _wholeStabbing;
            private double _stabbing;
            public int WholeStabbing
            {
                get => _wholeStabbing;
                set
                {
                    _wholeStabbing = value;
                    OnPropertyChanged();
                }
            }
            public double Stabbing
            {
                get => _stabbing;
                set
                {
                    _stabbing = value;
                    WholeStabbing = (int)_stabbing;
                    OnPropertyChanged();
                }
            }


            private int _wholeCrushing;
            private double _crushing;
            public int WholeCrushing
            {
                get => _wholeCrushing;
                set
                {
                    _wholeCrushing = value;
                    OnPropertyChanged();
                }
            }
            public double Crushing
            {
                get => _crushing;
                set
                {
                    _crushing = value;
                    WholeCrushing = (int)_crushing;
                    OnPropertyChanged();
                }
            }


            private int _severity;
            public int DamageSeverity { get; set; }
            public int Severity
            {
                get => _severity;
                set
                {
                    _crushing = (_crushing - DamageSeverity) / QualityToDouble(_quality);
                    _severity = value;
                    DamageSeverity = CalculateHeavinessDamage(_severity, _quality);
                    Crushing = _crushing * QualityToDouble(_quality) + DamageSeverity;
                    OnPropertyChanged();
                }
            }



            private int _wholekd;
            private double _kd;
            public int WholeKD
            {
                get => _wholekd;
                set
                {
                    _wholekd = value;
                    OnPropertyChanged();
                }
            }
            public double KD
            {
                get => _kd;
                set
                {
                    _kd = value;
                    WholeKD = (int)_kd;
                    if (Main.Instance.backpack_cb.SelectedIndex >= 3 && Main.Instance.backpack_quantity_cb.SelectedIndex != 0)
                        WeightScript.CountWeightItems();
                    OnPropertyChanged();
                }
            }


            private int _count = 1;
            public int Count
            {
                get => _count;
                set
                {
                    if (value == 0)
                        value = 1;
                    _weight = _weight / _count;
                    _count = value;
                    Weight = _weight * _count;
                    OnPropertyChanged();
                }
            }

            private Brush _foreColorWeight = new SolidColorBrush(Settings.Settings.SelectedTheme[1]);
            [JsonIgnore]
            public Brush ForeColorWeight
            {
                get => _foreColorWeight;
                set
                {
                    _foreColorWeight = value;
                    OnPropertyChanged();
                }
            }

            private bool _countWeight = true;
            public bool CountWeight
            {
                get => _countWeight;
                set
                {
                    _countWeight = value;
                    if (_countWeight)
                        ForeColorWeight = new SolidColorBrush(Settings.Settings.SelectedTheme[1]);
                    else
                        ForeColorWeight = Brushes.Firebrick;
                    OnPropertyChanged();
                }
            }
            public string Description { get; set; }


            private Brush _foreColorKD = new SolidColorBrush(Settings.Settings.SelectedTheme[1]);
            [JsonIgnore]
            public Brush ForeColorKD
            {
                get => _foreColorKD;
                set
                {
                    _foreColorKD = value;
                    OnPropertyChanged();
                }
            }

            private bool _countKD = true;
            public bool CountKD
            {
                get => _countKD;
                set
                {
                    _countKD = value;
                    SetCountKD();
                    OnPropertyChanged();
                }
            }

            private bool _countKDHelmet = false;
            public bool CountKDHelmet
            {
                get => _countKDHelmet;
                set
                {
                    _countKDHelmet = value;
                    SetCountKD();
                    OnPropertyChanged();
                }
            }

            private Brush _foreColorKDEquipped = new SolidColorBrush(Settings.Settings.SelectedTheme[1]);
            [JsonIgnore]
            public Brush ForeColorKDEquipped
            {
                get => _foreColorKDEquipped;
                set
                {
                    _foreColorKDEquipped = value;
                    OnPropertyChanged();
                }
            }

            private bool _equipped = true;
            public bool Equipped
            {
                get => _equipped;
                set
                {
                    _equipped = value;
                    if (_equipped)
                        ForeColorKDEquipped = new SolidColorBrush(Settings.Settings.SelectedTheme[1]);
                    else
                        ForeColorKDEquipped = Brushes.Firebrick;
                    Main.ItemBaffsListScript.UpdateValues();
                    KDScript.CountKD();
                    OnPropertyChanged();
                }
            }

            private void SetCountKD()
            {
                if (_countKD == false && _countKDHelmet == false)
                    ForeColorKD = Brushes.Firebrick;
                if (_countKD == true && _countKDHelmet == false)
                    ForeColorKD = new SolidColorBrush(Settings.Settings.SelectedTheme[1]);
                if (_countKD == false && _countKDHelmet == true)
                    ForeColorKD = Brushes.Orange;
                if (_countKD == true && _countKDHelmet == true)
                    ForeColorKD = Brushes.YellowGreen;
                KDScript.CountKD();
            }

            public ObservableCollection<ItemBaff> Baffs { get; set; } = new();

            private void ReturnAllValue()
            {
                if (_chopping != 0)
                    _chopping = _chopping / QualityToDouble(_quality);
                if (_stabbing != 0)
                    _stabbing = _stabbing / QualityToDouble(_quality);
                if (_crushing != 0 || DamageSeverity != 0)
                    _crushing = (_crushing - DamageSeverity) / QualityToDouble(_quality);
                if (_kd != 0)
                    _kd = _kd / QualityToDouble(_quality);
            }
            private void UpdateAllValue()
            {
                DamageSeverity = CalculateHeavinessDamage(_severity, _quality);
                if (_chopping != 0)
                    Chopping = _chopping * QualityToDouble(_quality);
                if (_stabbing != 0)
                    Stabbing = _stabbing * QualityToDouble(_quality);
                if (_crushing != 0 || DamageSeverity != 0)
                    Crushing = (_crushing * QualityToDouble(_quality)) + DamageSeverity;
                if (_kd != 0)
                    KD = _kd * QualityToDouble(_quality);
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                DataManager.Save();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
