using DNDHelper.Modules.Character;
using DNDHelper.Modules.Settings;
using DNDHelper.Windows;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable;

namespace DNDHelper.Modules.Config
{
    internal class PlayerClass
    {
        private static Dictionary<string, List<ClassData>> data;

        public static ClassData SelectedClassData = new ClassData();
        public static List<Stat> Stats = new List<Stat>();

        public PlayerClass()
        {

            Main.Instance.character_class_combobox.SelectionChanged += character_class_combobox_SelectionChanged;


            ClearStats();
        }

        public static void Update()
        {
            string pathFile = $"Cache/{DataManager.DataSave.SelectedRepository}/Class.json";
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
                    data = JsonSerializer.Deserialize<Dictionary<string, List<ClassData>>>(json, options);
                    Main.Instance.character_class_combobox.ItemsSource = data.Keys;
                }
                catch
                {

                }

                

            }
            else
            {
                Main.Instance.character_class_combobox.ItemsSource = null;
                Main.Instance.character_class_textblock.Text = "[[Класс]]";
            }
        }

        private void character_class_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Main.Instance.character_class_combobox.SelectedValue != null)
            {
                ClearStats();

                string selectText = Main.Instance.character_class_combobox.SelectedValue.ToString();
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

                Main.TreeSkillsScript.SetClassTree();

                Main.Characteristics.UpdateAllCharacterisitc();
                Health.HealthUpdate();
                Main.Instance.character_class_textblock.Text = selectText;
            }
        }

        private void ClearStats()
        {
            Stats.Clear();
            for (int i = 0; i < 26; i++)
            {
                Stats.Add(new Stat { Value = 0, Roll = 0 });
            }
        }
    }

    //SelectedClassData.ClassTrees[0][Имя ветви][0][номер ступени][0].Skills.Count
    public class ClassData
    {
        [JsonPropertyName("МножительХП")]
        public double MultiplyHealth { get; set; } = 1;

        [JsonPropertyName("ДопКД")]
        public int AddKD { get; set; } = 0;

        [JsonPropertyName("ДеБаф_к_пулям")]
        public int AddMagBullet { get; set; } = 0;

        [JsonPropertyName("множительуронамагии")]
        public double MultiplyMagDamage { get; set; } = 1;

        [JsonPropertyName("Статы_Стандарт")]
        public List<Dictionary<string, int[]>> StandartStats { get; set; } = new List<Dictionary<string, int[]>>();

        [JsonPropertyName("Способности")]
        public List<string> Skills { get; set; } = new List<string>();

        [JsonPropertyName("Касты")]
        public List<string> Casts { get; set; } = new List<string>();

        // Ветви: список словарей, где ключ — название ветви, значение — список ступеней
        [JsonPropertyName("Ветви")]
        public List<Dictionary<string, List<Dictionary<int, List<Dictionary<string, object>>>>>> ClassTrees { get; set; } = new();
    }

    public class StagesTree
    {
        public Dictionary<string, List<StageTree>> ClassTrees { get; set; } = new Dictionary<string, List<StageTree>>();
    }
    public class StageTree
    {
        public List<Dictionary<string, object>> Baffs { get; set; } = new();
    }
}


