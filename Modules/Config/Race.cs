using DNDHelper.Windows;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable;

namespace DNDHelper.Modules.Config
{
    class Race
    {
        Dictionary<string, List<CharacterData>> data;
        public static CharacterData SelectedCharacterData;

        List<Stat> statsEmpty = new List<Stat>();
        public static List<Stat> Stats = new List<Stat>();
        public void Test()
        {
            string json = File.ReadAllText("Race.json");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            Main.Instance.character_race_combobox.SelectionChanged += character_race_combobox_SelectionChanged;


            data = JsonSerializer.Deserialize<Dictionary<string, List<CharacterData>>>(json, options);



            Main.Instance.character_race_combobox.ItemsSource = data.Keys;


            for (int i = 0; i < 26; i++)
            {
                Stats.Add(new Stat { Value = 0, Roll = 0 });
            }

        }

        private void character_race_combobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Stats.Clear();
            for (int i = 0; i < 26; i++)
            {
                Stats.Add(new Stat { Value = 0, Roll = 0 });
            }

            string selectText = Main.Instance.character_race_combobox.SelectedValue.ToString();
            SelectedCharacterData = data[selectText][0];

            foreach (var item in SelectedCharacterData.StandartStats[0])
            {
                int index = Array.IndexOf(StatNameRus, item.Key.ToLower());

                if (index != -1)
                {
                    int[] stat = SelectedCharacterData.StandartStats[0][item.Key];
                    Stats[index].Value = stat[0];
                    Stats[index].Roll = stat[1];
                }
            }

            Main.Characteristics.UpdateAllCharacterisitc();

        }
    }



    public class Stat
    {
        public int Value { get; set; } = 0;

        public int Roll { get; set; } = 0;
    }


    public class CharacterData
    {
        [JsonPropertyName("ДопОчки_Навыков")]
        public int AddPoints { get; set; }
        [JsonPropertyName("ДопКД")]
        public int AddKD { get; set; }
        [JsonPropertyName("основная_длина_палки")]
        public double StandartStickLength { get; set; }
        [JsonPropertyName("Множитель_палки")]
        public double MultiplyStickLength { get; set; }
        [JsonPropertyName("Плюс_веса")]
        public int AddWeight { get; set; }
        [JsonPropertyName("Множитель_веса")]
        public double MultiplyWeight { get; set; }
        [JsonPropertyName("Множитель_магУрона")]
        public double MultiplyMagDamage { get; set; }
        [JsonPropertyName("Множитель_физУрона")]
        public double MultiplyPhisDamage { get; set; }


        [JsonPropertyName("Статы_Стандарт")]
        public List<Dictionary<string, int[]>> StandartStats { get; set; } = new List<Dictionary<string, int[]>>();

        [JsonPropertyName("Способности")]
        public List<string> Skills { get; set; } = new List<string>();

        [JsonPropertyName("Касты")]
        public List<string> Casts { get; set; } = new List<string>();
    }


}
