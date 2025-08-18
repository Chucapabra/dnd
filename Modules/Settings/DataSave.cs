using DNDHelper.Modules.Character;
using DNDHelper.Modules.Diary;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
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

        public static void ReadSaves()
        {
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

        public static void Save()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };


            var json = JsonSerializer.Serialize(DataSave, options);

            File.WriteAllText($"{SelectedSave}/Config.json", json);
            ReadSaves();
        }

        private static void Load_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var clickedItem = sender as MenuItem;
            Load(clickedItem.Tag.ToString());
        }

        public static void Load(string path)
        {
            SelectedSave = path;

            var json = File.ReadAllText($"{SelectedSave}/Config.json");

            var dataSave = JsonSerializer.Deserialize<DataSaveEmpty>(json);
            DataSave.Name = dataSave.Name;
            DataSave.SelectedRace = dataSave.SelectedRace;
            DataSave.SelectedClass = dataSave.SelectedClass;
            DataSave.SelectedBackpack = dataSave.SelectedBackpack;
            DataSave.BackpackQuantity = dataSave.BackpackQuantity;
            DataSave.AddWeight = dataSave.AddWeight;
            DataSave.Level = dataSave.Level;
            DataSave.Damage = dataSave.Damage;
            DataSave.SelectedQualityArmor = dataSave.SelectedQualityArmor;
            DataSave.Copper = dataSave.Copper;
            DataSave.Silver = dataSave.Silver;
            DataSave.Gold = dataSave.Gold;
            DataSave.Characterisitics = dataSave.Characterisitics;
            DataSave.SubtractMagicBullet = dataSave.SubtractMagicBullet;



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

            DataSave.CustomSkills.Clear();
            foreach (var item in dataSave.CustomSkills)
                DataSave.CustomSkills.Add(item);

            Character.Skills.ReloadDataGridSkills();
            Level.SetLevel();
            Main.ItemBaffsListScript.UpdateValues();
            DiaryManager.LoadNotes();
            Main.Instance.diaryTB.Document.Blocks.Clear();
        }

    }

    public class DataSaveEmpty : INotifyPropertyChanged
    {

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

        public int Level { get; set; } = 1;
        public int Damage { get; set; } = 0;
        public int SubtractMagicBullet { get; set; } = 0;

        public ObservableCollection<TreeGrid> ClassTreeGrid { get; set; } = new() {
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 }
            };

        public int[] Characterisitics { get; set; } = new int[Enum.GetValues(typeof(StatName)).Length];

        public ObservableCollection<Skills.Skill> CustomSkills { get; set; } = new();

        public ObservableCollection<InventoryLoot.InventoryItem> Inventory { get; set; } = new();
        
        //public ObservableCollection

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
