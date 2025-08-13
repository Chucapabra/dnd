using DNDHelper.Modules.Character;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DNDHelper.Modules.Settings
{
    public class DataSave
    {
        public string Name => Main.Instance.character_name_textbox.Text;
        public int SelectedClass => Main.Instance.character_class_combobox.SelectedIndex;
        public int SelectedRace => Main.Instance.character_race_combobox.SelectedIndex;

        public int[] Characterisitics => CharacteristicTable._baseStats;

        public int SelectedBackpack => Main.Instance.backpack_cb.SelectedIndex;
        public int BackpackQuantity => Main.Instance.backpack_quantity_cb.SelectedIndex;
        public int AddWeight => int.Parse(Main.Instance.backpack_plus_tb.Text);

        public int SelectedQualityArmor => Main.Instance.armor_type_cb.SelectedIndex;

        public string Copper => Main.Instance.Cuprum_tb.Text;
        public string Silver => Main.Instance.Silver_tb.Text;
        public string Gold => Main.Instance.Gold_tb.Text;


        public int TreeLevel0 => TreeSkills.TreeGrids[0].TreeLevel;
        public int TreeLevel1 => TreeSkills.TreeGrids[1].TreeLevel;
        public int TreeLevel2 => TreeSkills.TreeGrids[2].TreeLevel;

        public List<Skills.Skill> CustomSkills => Skills.CustomSkills.ToList();

        public List<InventoryLoot.InventoryItem> Inventory => InventoryLoot.InventoryItems.ToList();
    }

    public static class DataManager
    {
        public static DataSave dataSave = new DataSave();

        public static void Save()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };


            var json = JsonSerializer.Serialize(dataSave, options);

            if (!File.Exists("Saves/Test.json"))
                File.Create("Saves/Test.json");
            File.WriteAllText("Saves/Test.json", json);
        }

    }


}
