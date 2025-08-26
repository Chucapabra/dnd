using DNDHelper.Modules.Config;
using DNDHelper.Modules.Settings;
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
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace DNDHelper.Modules.Character
{
    internal class Effects
    {
        public static Dictionary<string, Dictionary<int, List<Effect>>> data;

        private static ObservableCollection<EffectTable> effectsList = new();
        Main main = Main.Instance;
        public Effects()
        {
            main.DataGridStatusEffects.ItemsSource = effectsList;
            effectsList.Add(new EffectTable());


            //Update();
        }

        public static void Update()
        {
            //   string pathFile = Main.PathMain + $"Cache/{DataManager.DataSave.SelectedRepository}/Effects.json";
            string pathFile = Main.PathMain + $"Effects.json";
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
                    foreach (var item in data.Keys)
                    {

                    }
                }
                catch
                {

                }
            }
            else
            {

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
                CheckMaxLevel();
                OnPropertyChanged();
            }
        }

        private int _level { get; set; } = 0;
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                UpdateLevel();
                OnPropertyChanged();
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

        public string ToolTip { get; set; } = "";


        private void CheckMaxLevel()
        {
            if (_selectedEffect != null)
            {
                var effect = Effects.data[_selectedEffect];
                if (effect != null)
                {
                    maxLevel = effect.Count;
                }
            }
            else
                maxLevel = 0;
            UpdateLevel();
        }

        private void UpdateLevel()
        {
            LevelString = $"{_level}/{maxLevel}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
