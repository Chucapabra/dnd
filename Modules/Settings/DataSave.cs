using DNDHelper.Modules.Character;
using DNDHelper.Modules.Config;
using DNDHelper.Modules.Diary;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static DNDHelper.Modules.Character.TreeSkills;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable;

namespace DNDHelper.Modules.Settings
{
    class SavesMenu
    {
        Main main = Main.Instance;

        private int SelectedIndex = -1;

        public SavesMenu()
        {
            main.AddCharacter.Click += AddCharacter_Click;
            main.ImageIconCharacter.MouseLeftButtonDown += ImageIconCharacter_MouseLeftButtonDown;
            main.ImageCharacter.MouseLeftButtonDown += ImageCharacter_MouseLeftButtonDown;
        }

        private void ImageCharacter_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SaveImage();
        }

        private void SaveImage()
        {
            OpenFileDialog LoadImageCharacter = new()
            {
                Filter = "Изображения (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"
            };

            if (LoadImageCharacter.ShowDialog() == true)
            {
                try
                {
                    string fileExtension = Path.GetExtension(LoadImageCharacter.FileName).ToLower();
                    var formats = new[] { ".png", ".jpg", ".jpeg" };
                    foreach (var format in formats)
                    {
                        var oldFile = Path.Combine(DataManager.SelectedSave, $"image{format}");
                        if (File.Exists(oldFile))
                            File.Delete(oldFile);
                    }
                    if (!Directory.Exists(DataManager.SelectedSave))
                        Directory.CreateDirectory(DataManager.SelectedSave);

                    string destinationPath = Path.Combine(DataManager.SelectedSave, $"image{fileExtension}");
                    File.Copy(LoadImageCharacter.FileName, destinationPath, true);

                    byte[] imageBytes = File.ReadAllBytes(destinationPath);
                    using (MemoryStream ms = new(imageBytes))
                    {
                        BitmapImage bitmap = new();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        bitmap.Freeze();

                        main.ImageCharacter.Source = bitmap;
                    }

                    main.ImageIconCharacter.Visibility = Visibility.Collapsed;
                    main.ImageCharacter.Visibility = Visibility.Visible;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                }
            }
        }
        private void ImageIconCharacter_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SaveImage();
        }

        private void AddCharacter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataManager.Create();
        }

    }

    public static class DataManager
    {
        private static string pathSaves = "Saves/";

        public static string SelectedSave = "";

        public static DataSaveEmpty DataSave = new DataSaveEmpty();

        public static void FindPathSaves()
        {
            string[] path = Assembly.GetExecutingAssembly().Location.Split('\\');
            pathSaves = string.Join("\\", path, 0, path.Count() - 2) + "\\Saves\\";
        }
        public static void WorkingProgram(bool visibilityBool)
        {
            Visibility visibility = Visibility.Visible;
            if (!visibilityBool)
                visibility = Visibility.Hidden;

            Main.Instance.WorkProgam1.Visibility = visibility;
            Main.Instance.WorkProgam2.Visibility = visibility;
            Main.Instance.WorkProgam3.Visibility = visibility;
            Main.Instance.WorkProgam4.Visibility = visibility;
            Main.Instance.WorkProgam5.Visibility = visibility;
            Main.Instance.WorkProgam6.Visibility = visibility;
            Main.Instance.WorkProgam7.Visibility = visibility;
            Main.Instance.сharacter_weight_textblock.Visibility = visibility;
            Main.Instance.DataGridCharacterisctics.Visibility = visibility;
            if (visibilityBool)
                Main.Instance.WorkProgam8.Visibility = Visibility.Collapsed;
            else
                Main.Instance.WorkProgam8.Visibility = Visibility.Visible;
        }


        public static void ReadSaves()
        {
            if (!Directory.Exists(pathSaves))
                Directory.CreateDirectory(pathSaves);
            string[] allfolders = Directory.GetDirectories(pathSaves);
            List<string> savefolders = new();
            foreach (string folder in allfolders)
            {
                var folders = folder.Split('/');
                if (folders[folders.Length - 1].Contains("Save") && !folders[folders.Length - 1].Contains('.'))
                    savefolders.Add(folder);
            }


            Main.Instance.CharactersMenu.Items.Clear();

            foreach (string folder in savefolders)
            {
                var json = File.ReadAllText($"{folder}/Config.json");
                if (json != null)
                {
                    var deleteItem = new MenuItem { Header = "Удалить", Tag = folder };
                    deleteItem.Click += DeleteItem_Click;
                    var deleteContextMenu = new ContextMenu();
                    deleteContextMenu.Items.Add(deleteItem);


                    var dataSave = JsonSerializer.Deserialize<DataSaveEmpty>(json);
                    var newMenuItem = new MenuItem { Header = dataSave.Name, ContextMenu = deleteContextMenu, Tag = folder };
                    newMenuItem.Click += Load_Click;
                    Main.Instance.CharactersMenu.Items.Add(newMenuItem);
                }
            }

        }


        public static void Create()
        {
            string[] deleteChars = { ".", ":", " " };
            string dateTime = DateTime.Now.ToString();
            foreach (string c in deleteChars)
                dateTime = dateTime.Replace(c, "");
            string newDirectory = $"{pathSaves}Save{dateTime}";
            string newFile = $"{newDirectory}/Config.json";

            Directory.CreateDirectory(newDirectory);
            Directory.CreateDirectory($"{newDirectory}/Notes");
            var file = File.Create(newFile);
            file.Close();



            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var newDataSave = new DataSaveEmpty();
            var json = JsonSerializer.Serialize(newDataSave, options);
            File.WriteAllText(newFile, json);

            ReadSaves();
            Load(newDirectory);
        }

        private static void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItemDelete = sender as MenuItem;
            var path = menuItemDelete.Tag.ToString();
            Delete(path);
        }

        public static void Delete(string path)
        {
            if (path != SelectedSave)
            {
                var json = File.ReadAllText($"{path}/Config.json");
                var dataSave = JsonSerializer.Deserialize<DataSaveEmpty>(json);
                var messageBox = MessageBox.Show($"Вы уверены, что хотите удалить персонажа {dataSave.Name}", "Удаление персонажа", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBox == MessageBoxResult.Yes)
                {

                    Directory.Delete(path, true);
                    ReadSaves();
                }
            }
            else
                MessageBox.Show("Это сохранение открыто", "Тыеб", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static bool IsLoad = true;




        private static readonly SmartDebouncer _debouncer = new();
        public static void Save(bool Force = false)
        {
            if (SelectedSave.Length == 0 || IsLoad)
                return;

            if (!Force)
                _debouncer.ExecuteDebounced(async () =>
                {
                    save();
                    ReadSaves();
                });
            else
                ForceSaveDataAsync();
        }

        private static void ForceSaveDataAsync()
        {
            _debouncer.Reset();
            save();
        }

        private static void save()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            Debug.WriteLine($"{SelectedSave}/Config.json");
            var json = JsonSerializer.Serialize(DataSave, options);

            File.WriteAllText($"{SelectedSave}/Config.json", json);
        }
        private static void Load_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var clickedItem = sender as MenuItem;
            Load(clickedItem.Tag.ToString());
        }

        public static void Load(string path)
        {
            if (SelectedSave.Length > 0) Save(true);


            IsLoad = true;

            SelectedSave = path;

            var json = File.ReadAllText($"{SelectedSave}/Config.json");

            var dataSave = JsonSerializer.Deserialize<DataSaveEmpty>(json);


            DataSave.SelectedRepository = dataSave.SelectedRepository;
            SetRepository.UpdateRepository();

            WorkingProgram(true);

            DataSave.Name = dataSave.Name;
            DataSave.SelectedRace = dataSave.SelectedRace;
            DataSave.SelectedClass = dataSave.SelectedClass;
            DataSave.AddWeight = dataSave.AddWeight;
            DataSave.Level = dataSave.Level;
            DataSave.Damage = dataSave.Damage;
            DataSave.SelectedQualityArmor = dataSave.SelectedQualityArmor;
            DataSave.Copper = dataSave.Copper;
            DataSave.Silver = dataSave.Silver;
            DataSave.Gold = dataSave.Gold;
            DataSave.Characterisitics = dataSave.Characterisitics;
            DataSave.SubtractMagicBullet = dataSave.SubtractMagicBullet;
            DataSave.MultiplyMagicDamage = dataSave.MultiplyMagicDamage;
            DataSave.CatalyzerSelect = dataSave.CatalyzerSelect;
            DataSave.CatalyzerQualitySelect = dataSave.CatalyzerQualitySelect;
            DataSave.MagicSchool = dataSave.MagicSchool;



            DataSave.ClassTreeGrid.Clear();
            foreach (var item in dataSave.ClassTreeGrid)
                DataSave.ClassTreeGrid.Add(item);

            DataSave.Inventory.Clear();
            foreach (var item in dataSave.Inventory)
            {
                if (item.Weight != 0)
                    item.Weight /= item.Count;
                DataSave.Inventory.Add(item);
            }
            DataSave.SelectedBackpack = dataSave.SelectedBackpack;
            DataSave.BackpackQuantity = dataSave.BackpackQuantity;

            DataSave.CustomSkills.Clear();
            foreach (var item in dataSave.CustomSkills)
                DataSave.CustomSkills.Add(item);

            DataSave.CurrentCastsNames.Clear();
            foreach (var item in dataSave.CurrentCastsNames)
                DataSave.CurrentCastsNames.Add(item);

            Character.Skills.ReloadDataGridSkills();
            Level.SetLevel();
            Main.ItemBaffsListScript.UpdateValues();
            DiaryManager.LoadNotes();
            LoadImage();
            Main.Instance.diaryTB.Document.Blocks.Clear();
            SetRepository.FileСonnection();

            IsLoad = false;

            PlayerClass.UpdateCharacteristicMagic();
            WeightScript.CountWeightItems();
        }

        private static void LoadImage()
        {
            var formats = new[] { ".png", ".jpg", ".jpeg" };
            string imagePath = null;

            foreach (var format in formats)
            {
                var currentPath = Path.Combine(SelectedSave, $"image{format}");
                if (File.Exists(currentPath))
                {
                    imagePath = currentPath;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(imagePath))
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    using (MemoryStream ms = new(imageBytes))
                    {
                        BitmapImage bitmap = new();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        bitmap.Freeze();

                        Main.Instance.ImageCharacter.Source = bitmap;
                    }

                    Main.Instance.ImageIconCharacter.Visibility = Visibility.Collapsed;
                    Main.Instance.ImageCharacter.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                }
            }
            else
            {
                Main.Instance.ImageCharacter.Source = null;
                Main.Instance.ImageIconCharacter.Visibility = Visibility.Visible;
                Main.Instance.ImageCharacter.Visibility = Visibility.Collapsed;
            }
        }

    }

    public class DataSaveEmpty : INotifyPropertyChanged
    {

        private string _selectedRepository = "";
        public string SelectedRepository
        {
            get => _selectedRepository;
            set
            {
                _selectedRepository = value;
                OnPropertyChanged();
            }
        }

        private string _name = "Имя";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _selectedClass = 0;
        public int SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                OnPropertyChanged();
            }
        }

        private int _selectedRace = 0;
        public int SelectedRace
        {
            get => _selectedRace;
            set
            {
                _selectedRace = value;
                OnPropertyChanged();
            }
        }


        private int _selectedBackpack = 0;
        public int SelectedBackpack
        {
            get => _selectedBackpack;
            set
            {
                _selectedBackpack = value;
                OnPropertyChanged();
            }
        }

        private int _backpackQuantity = 0;
        public int BackpackQuantity
        {
            get => _backpackQuantity;
            set
            {
                _backpackQuantity = value;
                OnPropertyChanged();
            }
        }

        private string _addWeight = "0";
        public string AddWeight
        {
            get => _addWeight;
            set
            {
                _addWeight = value;
                OnPropertyChanged();
            }
        }

        private int _selectedQualityArmor = 0;
        public int SelectedQualityArmor
        {
            get => _selectedQualityArmor;
            set
            {
                _selectedQualityArmor = value;
                OnPropertyChanged();
            }
        }

        private string _copper = "0";
        public string Copper
        {
            get => _copper;
            set
            {
                _copper = value;
                OnPropertyChanged();
            }
        }

        private string _silver = "0";
        public string Silver
        {
            get => _silver;
            set
            {
                _silver = value;
                OnPropertyChanged();
            }
        }

        private string _gold = "0";
        public string Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                OnPropertyChanged();
            }
        }

        private int _level { get; set; } = 1;
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }
        private int _damage = 0;
        public int Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                OnPropertyChanged();
            }
        }
        private int _subtractMagicBullet = 0;
        public int SubtractMagicBullet
        {
            get => _subtractMagicBullet;
            set
            {
                _subtractMagicBullet = value;
                MagicSpells.MagicSpells.UpdateMagicBullet();
                OnPropertyChanged();
            }
        }

        private double _multiplyMagicDamage = 1;
        public double MultiplyMagicDamage
        {
            get => _multiplyMagicDamage;
            set
            {
                _multiplyMagicDamage = value;
                Main.Instance.DamageMagic_textblock.Text = value.ToString();
                Main.Instance.DamageMagic_textbox.Text = value.ToString();
                OnPropertyChanged();
            }
        }

        private int _magicSchool = 0;
        public int MagicSchool
        {
            get => _magicSchool;
            set
            {
                _magicSchool = value;
                OnPropertyChanged();
            }
        }

        private int _catalyzerSelect = 1;
        public int CatalyzerSelect
        {
            get => _catalyzerSelect;
            set
            {
                _catalyzerSelect = value;
                OnPropertyChanged();
            }
        }

        private int _catalyzerQualitySelect = 2;
        public int CatalyzerQualitySelect
        {
            get => _catalyzerQualitySelect;
            set
            {
                _catalyzerQualitySelect = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<TreeGrid> ClassTreeGrid { get; set; } = new() {
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 }
            };

        public int[] Characterisitics { get; set; } = { 5, 5, 5, 3, 3, 3, 5, 5, 3, 3, 3, 3, 3, 3, 5, 3, 3, 3, 3, 3, 5, 3, 3, 3, 3 };

        public ObservableCollection<Skills.Skill> CustomSkills { get; set; } = new();

        public ObservableCollection<InventoryLoot.InventoryItem> Inventory { get; set; } = new();

        public ObservableCollection<string> CurrentCastsNames { get; set; } = new ObservableCollection<string>();


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            DataManager.Save();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



    public class SmartDebouncer
    {
        private DateTime _lastRequestTime = DateTime.MinValue;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private Func<Task> _pendingAction = null;
        private readonly object _lock = new object();

        // Основной метод для отложенного выполнения
        public void ExecuteDebounced(Func<Task> action)
        {
            lock (_lock)
            {
                // Сохраняем действие
                _pendingAction = action;

                // Отменяем предыдущую отложенную задачу
                _cts.Cancel();
                _cts = new CancellationTokenSource();

                var now = DateTime.UtcNow;
                _lastRequestTime = now;

                var token = _cts.Token;
                _ = Task.Run(async () =>
                {
                    await Task.Delay(2500, token);

                    if (!token.IsCancellationRequested)
                    {
                        await ExecutePendingAction();
                    }
                }, token);
            }
        }

        // Принудительное сохранение (немедленное выполнение)
        public async Task ForceSaveAsync()
        {
            Func<Task> actionToExecute = null;

            lock (_lock)
            {
                // Отменяем отложенную задачу
                _cts.Cancel();
                _cts = new CancellationTokenSource();

                // Берем текущее действие для выполнения
                actionToExecute = _pendingAction;
                _pendingAction = null;
            }

            if (actionToExecute != null)
            {
                await actionToExecute();
            }
        }

        // Сброс всех таймеров и ожиданий
        public void Reset()
        {
            lock (_lock)
            {
                _cts.Cancel();
                _cts = new CancellationTokenSource();
                _pendingAction = null;
                _lastRequestTime = DateTime.MinValue;
            }
        }

        // Проверка есть ли отложенные действия
        public bool HasPendingActions()
        {
            lock (_lock)
            {
                return _pendingAction != null;
            }
        }

        // Внутренний метод выполнения
        private async Task ExecutePendingAction()
        {
            Func<Task> actionToExecute = null;

            lock (_lock)
            {
                actionToExecute = _pendingAction;
                _pendingAction = null;
            }

            if (actionToExecute != null)
            {
                await actionToExecute();
            }
        }
    }

}
