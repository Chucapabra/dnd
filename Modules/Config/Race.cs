using DNDHelper.Modules.Character;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Settings;
using DNDHelper.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using static DNDHelper.Modules.Inventory.InventoryLoot;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable;

namespace DNDHelper.Modules.Config
{
    class Race
    {
        private static Dictionary<string, List<RaceData>> data;

        public static RaceData SelectedClassData = new();
        public static List<Stat> Stats = new List<Stat>();

        public Race()
        {
            Main.Instance.character_race_combobox.SelectionChanged += character_race_combobox_SelectionChanged;

            ClearStats();
        }

        public static void Update()
        {
            string pathFile = Main.PathMain + $"Cache/{DataManager.DataSave.SelectedRepository}/Race.json";
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
                    data = JsonSerializer.Deserialize<Dictionary<string, List<RaceData>>>(json, options);
                    Main.Instance.character_race_combobox.ItemsSource = data.Keys;
                }
                catch
                {
                    data = new();
                }
            }
            else
            {
                Main.Instance.character_race_combobox.ItemsSource = null;
                Main.Instance.character_race_textblock.Text = "[[Раса]]";
            }
        }



        private static void ClearStats() 
        {
            Stats.Clear();
            for (int i = 0; i < 30; i++)
            {
                Stats.Add(new Stat { Value = 0, Roll = 0 });
            }
        }

        private void character_race_combobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateRace();
        }

        private static void UpdateRace()
        {
            if (Main.Instance.character_race_combobox.SelectedValue != null)
            {
                ClearStats();

                string selectText = Main.Instance.character_race_combobox.SelectedValue.ToString();
                SelectedClassData = data[selectText][0];

                foreach (var item in SelectedClassData.StandartStats[0])
                {
                    int index = Array.IndexOf(StatNameRus, item.Key.ToLower());

                    if (index != -1)
                    {
                        int[] stat = SelectedClassData.StandartStats[0][item.Key];
                        Stats[index].Value = stat[0];
                        Stats[index].Roll = stat[1];
                    }
                }

                Main.Characteristics.UpdateAllCharacterisitc();
                Skills.ReloadDataGridSkills();

                Main.Instance.character_race_textblock.Text = selectText;

            }
        }

    }

    public class Stat
    {
        public int Value { get; set; } = 0;

        public int Roll { get; set; } = 0;
    }

    internal class RaceData
    {
        [JsonPropertyName("ДопОчки_Навыков")]
        public int AddPoints { get; set; } = 0;
        [JsonPropertyName("ДопКД")]
        public int AddKD { get; set; } = 0;
        [JsonPropertyName("основная_длина_палки")]
        public double StandartStickLength { get; set; } = 1;
        [JsonPropertyName("Множитель_палки")]
        public double MultiplyStickLength { get; set; } = 1;
        [JsonPropertyName("Плюс_веса")]
        public int AddWeight { get; set; } = 0;
        [JsonPropertyName("Множитель_веса")]
        public double MultiplyWeight { get; set; } = 1;
        [JsonPropertyName("Множитель_магУрона")]
        public double MultiplyMagDamage { get; set; } = 1;
        [JsonPropertyName("Множитель_физУрона")]
        public double MultiplyPhisDamage { get; set; } = 1;

        [JsonPropertyName("Статы_Стандарт")]
        public List<Dictionary<string, int[]>> StandartStats { get; set; } = new List<Dictionary<string, int[]>>();

        [JsonPropertyName("Способности")]
        public List<string> Skills { get; set; } = new List<string>();

        [JsonPropertyName("Касты")]
        public List<string> Casts { get; set; } = new List<string>();
    }


}
