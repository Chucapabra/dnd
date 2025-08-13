using DNDHelper.Modules.Character;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static DNDHelper.Modules.Character.TreeSkills;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable;

namespace DNDHelper.Modules.Settings
{
    public static class DataManager
    {
        public static DataSaveEmpty DataSave = new DataSaveEmpty();
        public static void Save()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };


            var json = JsonSerializer.Serialize(DataSave, options);

            File.WriteAllText("Saves/Test.json", json);
        }

        public static void Load()
        {
            var json = File.ReadAllText("Saves/Test.json");

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


            DataSave.ClassTreeGrid.Clear();
            foreach (var item in dataSave.ClassTreeGrid)
                DataSave.ClassTreeGrid.Add(item);

            DataSave.Inventory.Clear();
            foreach (var item in dataSave.Inventory)
            {
                if(item.Weight != 0)
                item.Weight /= item.Count;
                DataSave.Inventory.Add(item);
            }

            DataSave.CustomSkills.Clear();
            foreach (var item in dataSave.CustomSkills)
                DataSave.CustomSkills.Add(item);

            Character.Skills.ReloadDataGridSkills();
            Level.SetLevel();
            Main.ItemBaffsListScript.UpdateValues();
        }

        public class DataSaveEmpty : INotifyPropertyChanged
        {

            private string _name = "";
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }

            private int _selectedClass = -1;
            public int SelectedClass
            {
                get => _selectedClass;
                set
                {
                    _selectedClass = value;
                    OnPropertyChanged();
                }
            }

            private int _selectedRace = -1;
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

            private int _selectedQualityArmor = -1;
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

            public ObservableCollection<TreeGrid> ClassTreeGrid { get; set; } = new() {
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 }
            };

            public int[] Characterisitics { get; set; } = new int[Enum.GetValues(typeof(StatName)).Length];

            public ObservableCollection<Skills.Skill> CustomSkills { get; set; } = new();

            public ObservableCollection<InventoryLoot.InventoryItem> Inventory { get; set; } = new();

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
 
}
