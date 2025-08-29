using DNDHelper.Modules.Config;
using DNDHelper.Modules.Settings;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace DNDHelper.Modules.Character
{
    internal class Effects
    {
        public static Dictionary<string, Dictionary<int, List<Effect>>> data;

        private static ObservableCollection<EffectTable> effectsList => DataManager.DataSave.EffectTables;
        private static Main main = Main.Instance;

        public static List<int[]> EffectBaffs = new(50);
        public static string[] StatNameRus = new[]
        {
            "сила", "атлетика",
            "ловкость", "акробатика", "ловкость_рук", "скрытность",
            "телосложение",
            "интеллект", "магия", "религия",
            "природа", "история",
            "расcледование", "технология",
            "мудрость", "медецина",
            "восприятие", "проницательность", "выживание", "обращение_с_животными",
            "харизма", "обман",
            "запугивание", "выступление", "убеждение", 
            "кд", "остальным_роллам", "всем_роллам", "блок", "множитель_палки"
        };
        public Effects()
        {
            main.DataGridStatusEffects.ItemsSource = effectsList;

            main.AddEffect.Click += AddEffect_Click;
            main.DeleteEffect.Click += DeleteEffect_Click;
            main.DataGridStatusEffects.ContextMenuOpening += DataGridStatusEffects_ContextMenuOpening;

            effectsList.CollectionChanged += EffectsList_CollectionChanged;

            ClearItemBaffs();
            Update();
        }

        private void EffectsList_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DataManager.Save();
        }

        private void DataGridStatusEffects_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            int selectedIndex = main.DataGridStatusEffects.SelectedIndex;
            if (selectedIndex != -1)
            {
                main.AddEffect.Visibility = Visibility.Visible;
                main.DeleteEffect.Visibility = Visibility.Visible;
            }
            else
            {
                main.AddEffect.Visibility = Visibility.Visible;
                main.DeleteEffect.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteEffect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int selectedIndex = main.DataGridStatusEffects.SelectedIndex;
            if (selectedIndex != -1)
            {
                effectsList.RemoveAt(selectedIndex);
                UpdateLevelEffect();
            }
        }

        private void AddEffect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            effectsList.Add(new EffectTable());
        }

        public static void Update()
        {
             string pathFile = Main.PathMain + $"Cache/{DataManager.DataSave.SelectedRepository}/Effects.json";
            if (File.Exists(pathFile))
            {
                string json = File.ReadAllText(pathFile);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                try
                {
                    data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<int, List<Effect>>>>(json, options);
                }
                catch
                {
                    data = null;
                }
            }
        }

        private static void ClearItemBaffs()
        {
            EffectBaffs.Clear();
            for (int i = 0; i < 30; i++)
                EffectBaffs.Add(new int[] { 0, 0 });
        }

        public static void UpdateLevelEffect()
        {
            ClearItemBaffs();

            if (data != null)
                foreach (var item in effectsList)
                {
                    string nameEffect = item.SelectedEffect;
                    if (nameEffect != null)
                    {
                        var effect = data[nameEffect];
                        var levelEffect = effect[item.Level][0];
                        foreach (var baff in levelEffect.StandartStats[0].Keys)
                        {
                            int index = Array.IndexOf(StatNameRus, baff.ToLower());
                            if (index != -1)
                            {
                                int A = levelEffect.StandartStats[0][baff][0];
                                int B = levelEffect.StandartStats[0][baff][1];
                                if (index < 25)
                                {
                                    EffectBaffs[index][0] += A;
                                    EffectBaffs[index][1] += B;
                                }
                                else if (index != 29)
                                {
                                    EffectBaffs[index][0] += A + B;
                                    if (index == 27)
                                        EffectBaffs[26][0] += A + B;
                                }
                                else
                                    if (A < EffectBaffs[index][0] || EffectBaffs[index][0] == 0)
                                    EffectBaffs[index][0] = A;
                            }
                        }
                    }
                }
            Debug.WriteLine(EffectBaffs[29][0]);
            Main.Characteristics.UpdateAllCharacterisitc();
            AttributesCharacter.UpdateRolls();
            AttributesCharacter.StickMethod();
            KDScript.CountKD();
        }


        public static void AddingLevelEffect(int number)
        {
            int selectedIndex = main.DataGridStatusEffects.SelectedIndex;
            if(selectedIndex != -1) 
            {
                effectsList[selectedIndex].Level += number;
            }
        }
    }

    public class Effect
    {
        public List<Dictionary<string, int[]>> StandartStats { get; set; } = new List<Dictionary<string, int[]>>();

        public string ToolTip { get; set; } = "";
    }

    public class EffectTable : INotifyPropertyChanged
    {
        private int maxLevel = 0;

        private string _selectedEffect { get; set; }
        public string SelectedEffect
        {
            get => _selectedEffect;
            set
            {
                _selectedEffect = value;
                Level = 0;
                CheckEffect();
                OnPropertyChanged();
            }
        }

        private int _level { get; set; } = 0;
        public int Level
        {
            get => _level;
            set
            {
                if (value >= 0 && value <= maxLevel || DataManager.IsLoad)
                {
                    _level = value;
                    UpdateLevel();
                    Effects.UpdateLevelEffect();
                    OnPropertyChanged();
                }
            }
        }

        private string _levelString { get; set; } = "0/0";
        public string LevelString
        {
            get => _levelString;
            set
            {
                _levelString = value;
                OnPropertyChanged();
            }
        }

        private string _toolTip { get; set; } = "";
        public string ToolTip
        {
            get => _toolTip;
            set
            {
                _toolTip = value;
                OnPropertyChanged();
            }
        }


        private void CheckEffect()
        {
            if (_selectedEffect != null)
            {
                if (Effects.data != null)
                {
                    var effect = Effects.data[_selectedEffect];
                    if (effect != null)
                    {
                        maxLevel = effect.Count - 1;

                    }
                }
            }
            else
                maxLevel = 0;
            UpdateLevel();
        }

        private void UpdateLevel()
        {
            LevelString = $"{_level}/{maxLevel}";

            if (_selectedEffect != null)
            {
                if (Effects.data != null)
                {
                    var effect = Effects.data[_selectedEffect];
                    if (effect != null)
                    {
                        ToolTip = effect[Level][0].ToolTip;
                    }
                }
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            DataManager.Save();
        }
    }
}
