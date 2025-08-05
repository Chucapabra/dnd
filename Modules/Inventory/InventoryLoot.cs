using DNDHelper.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme;

namespace DNDHelper.Modules.Inventory
{
    internal class InventoryLoot
    {
        Main main = Main.Instance;

        public static ObservableCollection<InventoryItem> InventoryItems { get; set; }

        public InventoryLoot()
        {
            InventoryItems = new ObservableCollection<InventoryItem>();
            InventoryItems.Add(new InventoryItem
            {
                Name = "Меч рыцаря",
                Weight = 1,
                Chopping = 15,
                Stabbing = 10,
                Crushing = 0,
                Severity = 1,
                KD = 5,
				Quality = "Прото",
				Count = 1,
                CountWeight = true,
                Description = "Старый добрый меч.",
                CountKD = true,
                CountKDHelmet = true
            });

            InventoryItems.Add(new InventoryItem
            {
                Name = "Щит деревянный",
                Weight = 3,
                Quality = "Прото",
                Chopping = 0,
                Stabbing = 0,
                Crushing = 5,
                Severity = 1,
                KD = 2.5,
                Count = 1,
                CountWeight = true,
                Description = "Простой деревянный щит.",
                CountKD = true,
                CountKDHelmet = true
            });
            main.DataGridInventory.ItemsSource = InventoryItems;


            main.DataGridInventory.BeginningEdit += DataGridInventory_BeginningEdit;
            main.DataGridInventory.CellEditEnding += DataGridInventory_CellEditEnding; ;
            main.DataGridInventory.PreviewMouseLeftButtonDown += DataGridInventory_PreviewMouseDoubleClick;
            main.DataGridInventory.SelectionChanged += DataGridInventory_SelectionChanged;
            main.DescriptionTextBox.TextChanged += DescriptionTextBox_TextChanged;
        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (main.DataGridInventory.SelectedItems.Count > 0)
            {
                var item = main.DataGridInventory.SelectedItems[0] as InventoryItem;
                item.Description = main.DescriptionTextBox.Text;
            }
        }

        private void DataGridInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (main.DataGridInventory.SelectedItems.Count > 0)
            {
                var item = main.DataGridInventory.SelectedItems[0] as InventoryItem;
                if (item != null)
                    main.DescriptionTextBox.Text = item.Description;
            }
        }

        private void DataGridInventory_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
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
                    break;
            }
        }

        public static double QualityToDouble(string quality)
        {
            float Baff = 1f;
            switch (quality)
            {
                case "Прото":
                    Baff = 0.5f;
                    break;

                case "Плохое":
                    Baff = 0.75f;
                    break;

                case "Стандартное":
                    Baff = 1.0f;
                    break;

                case "Высокое":
                    Baff = 1.25f;
                    break;

                case "Великолепное":
                    Baff = 1.5f;
                    break;

                case "Мастерское":
                    Baff = 2.0f;
                    break;

                case "Мифическое":
                    Baff = 3.0f;
                    break;

                default:
                    break;
            }
            return Baff;
        }

        public static int CalculateHeavinessDamage(int HeavinessNumber, string Quality, double GlobalMultiply)
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

            int HeavinessDamage = (int)((double)(5 * Baff * HeavinessNumber) * GlobalMultiply);
            return HeavinessDamage;
        }


        public class InventoryItem : INotifyPropertyChanged
        {
            public string Name { get; set; }

            private int _weight;
            public int Weight
            {
                get => _weight;
                set
                {
                    _weight = value;
                    OnPropertyChanged();
                }
            }

            private string _quality;
            public string Quality
            {
                get => _quality;
                set
                {
                    ReturnAllValue();
                    _quality = value;
                    UpdateAllValue();
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
                    DamageSeverity = CalculateHeavinessDamage(_severity, _quality, 1);
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
                    OnPropertyChanged();
                }
            }


            private int _count;
            public int Count
            {
                get => _count;
                set
                {
                    if (_weight != 0 && _count != 0)
                        _weight = _weight / _count;
                    else
                        _weight = 0;
                    _count = value;
                    Weight = _weight * _count;
                    OnPropertyChanged();
                }
            }
            public bool CountWeight { get; set; }
            public string Description { get; set; }
            public bool CountKD { get; set; }
            public bool CountKDHelmet { get; set; }


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
                DamageSeverity = CalculateHeavinessDamage(_severity, _quality, 1);
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
