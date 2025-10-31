using DNDHelper.Modules.Config;
using DNDHelper.Modules.Inventory;
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
using System.Windows;
using System.Windows.Controls;

namespace DNDHelper.Modules.Character
{   
    public class DamageCalculator
    {
        private ObservableCollection<TypeDamage> typeDamages { get; set; } = new();
        Main main = Main.Instance;
        public static Dictionary<string, List<TypeDamageConfig>> data = new();


        public DamageCalculator() 
        {
            main.DamageGrid.ItemsSource = typeDamages;

            main.DamageGrid.ContextMenuOpening += DamageGrid_ContextMenuOpening;
            main.AddTypeDamage.Click += AddTypeDamage_Click;
            main.DeleteTypeDamage.Click += DeleteTypeDamage_Click;
            main.CountDamage_button.MouseLeftButtonDown += CountDamage_button_MouseLeftButtonDown;

            Update();
        }


        public static void Update()
        {
            string pathFile = Main.PathMain + $"Cache/{DataManager.DataSave.SelectedRepository}/TypeDamage.json";
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
                    data = JsonSerializer.Deserialize<Dictionary<string, List<TypeDamageConfig>>>(json, options);
                }
                catch (Exception ex) 
                {
                    data = new();
                }

            }
        }

        private void CountDamage_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Calculate();
        }

        private void DamageGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            int selectedIndex = main.DamageGrid.SelectedIndex;
            if (selectedIndex != -1)
            {
                main.AddTypeDamage.Visibility = Visibility.Visible;
                main.DeleteTypeDamage.Visibility = Visibility.Visible;
            }
            else
            {
                main.AddTypeDamage.Visibility = Visibility.Visible;
                main.DeleteTypeDamage.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteTypeDamage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int selectedIndex = main.DamageGrid.SelectedIndex;
            if (selectedIndex != -1)
                typeDamages.RemoveAt(selectedIndex);
        }

        private void AddTypeDamage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            typeDamages.Add(new TypeDamage());
        }



        private void Calculate()
        {
            int TotalDamage = 0;
            int KDArmor = KDScript.ArmorKD;
            int KDBody = KDScript.BodyKD;
            int KDMental = KDScript.MentalKD;
            int UsedKDArmor = 0;
            int USedKDBody = 0;
            int UsedKDMental = 0;

            Dictionary<string, int> effects = new() { {"Кровотечение", 0}, {"Шок", 0}, {"Оглушение", 0}, { "Горение", 0 } };

            foreach (TypeDamage typeDamage in typeDamages)
            {
                if (data.Keys.Contains(typeDamage.NameType.Content.ToString()) != false)
                {
                    var config = data[typeDamage.NameType.Content.ToString()][0];

                    string name = typeDamage.NameType.Content.ToString().ToLower();
                    string type = config.Type;

                    double damage = typeDamage.Damage;
                    double baff = config.BaffDamage;
                    double multiplyArmor = config.MultiplyArmorKD;
                    int damageInEffect = config.DamageInEffect;

                    double multiplyDamageRace = 1;
                    int indexDamageType = 0;
                    if (type == "Физ")
                    {
                        multiplyDamageRace = Race.SelectedClassData.MultiplyPhisDamage;
                        indexDamageType = 1;
                    }
                    else
                    {
                        multiplyDamageRace = Race.SelectedClassData.MultiplyMagDamage;
                        indexDamageType = 0;
                    }


                    if (typeDamage.NameType.Content.ToString() != "ментальный")
                    {

                        // Счет магического щита
                        if (Main.Instance.shield_health_textbox.Text != "" && Main.Instance.shield_health_textbox.Text != "0" && DataManager.DataSave.SelectedTypeMagicShield != -1 && Health.DamageMagicShield != Health.MaxMagicShield)
                            if (DataManager.DataSave.SelectedTypeMagicShield == indexDamageType || DataManager.DataSave.SelectedTypeMagicShield == 2)
                            {
                                int hpSield = Health.MaxMagicShield - Health.DamageMagicShield;

                                if (damage < hpSield)
                                {
                                    Health.DamageMagicShield = Health.DamageMagicShield + (int)damage;
                                    continue;
                                }
                                else
                                {
                                    damage -= hpSield;
                                    Health.DamageMagicShield = Health.MaxMagicShield;
                                }
                            }


                        if (typeDamage.NameType.Content.ToString() != "без дебаффов")
                        {
                            // Броня
                            double multiplyChipping = 1;
                            double multiplyOther = 1;
                            if (TypeArmorBaffs.SelectedArmor > 1 && type == "Физ")
                            {
                                if (TypeArmorBaffs.SelectedArmor == 2)
                                {
                                    multiplyChipping = 0.5;
                                    multiplyOther = 0.75;
                                }
                                else if (TypeArmorBaffs.SelectedArmor == 3)
                                {
                                    multiplyChipping = 0.25;
                                    multiplyOther = 0.5;
                                }

                                if (name == "рубящий")
                                {
                                    damage *= multiplyChipping;
                                    name = "дробящий";
                                    multiplyOther = 1;
                                }
                            }



                            // Счет магического и физичекого урона
                            double _kd = (KDArmor * multiplyArmor);

                            damage *= multiplyDamageRace;
                            if (typeDamage.CheckBox) damage *= baff;

                            if (UsedKDArmor == KDArmor)
                                multiplyOther = 1;
                            calculateDamage(ref damage, ref UsedKDArmor, (int)_kd);
                            if (DataManager.DataSave.SelectedRepository != "Жирный")
                                damage *= multiplyOther;


                            calculateDamage(ref damage, ref USedKDBody, KDBody);

                        }


                    }
                    else // Счет ментального урона
                        if (UsedKDMental < KDMental)
                        calculateDamage(ref damage, ref UsedKDMental, KDMental);

                    calculateEffects(name, damage, damageInEffect, ref effects);

                    TotalDamage += (int)double.Round(damage);
                }
            }

            string receivedEffect = "";
            foreach (var effectName in effects.Keys)
            {
                int effect = effects[effectName];
                if (effect != 0) receivedEffect += $"\r\n{effectName}: {effect}";
            }
            if (receivedEffect != "")
                main.DamageHealth_textblock.ToolTip = receivedEffect.Remove(0,2);

            main.DamageHealth_textblock.Text = TotalDamage.ToString();
            Health.Damage += TotalDamage;
            Health.HealthUpdate();
        }

        // Счет урона
        private void calculateDamage(ref double damage, ref int usedKD, int kd)
        {
            int _kd = kd - usedKD;
            if (damage < _kd)
            {
                usedKD += (int)double.Round(damage);
                damage = 0;
            }
            else
            {
                damage -= _kd;
                usedKD = kd;
            }
        }

        private void calculateEffects(string nameDamage, double damage, int damageInEffect, ref Dictionary<string, int> effects)
        {
            // {"Кровотечение", 0}, {"Шок", 0}, {"Оглушение", 0}, { "Горение", 0 }
            switch (nameDamage)
            {
                case "рубящий":
                    effects["Кровотечение"] += (int)(damage / damageInEffect);
                    break;
                case "электрический":
                    effects["Шок"] += (int)(damage / damageInEffect);
                    break;
                case "дробящий":
                    if(Health.MaxHealth * 0.1 < damage)
                    effects["Оглушение"] = 1;
                    break;
                case "огненный":
                    int stage = 0;
                    if(damage >= 20 * GlobalMultiply.data.GlobalMultiply)
                        stage = 1;
                    if (damage >= 40 * GlobalMultiply.data.GlobalMultiply)
                        stage = 2;
                    if (damage >= 250 * GlobalMultiply.data.GlobalMultiply)
                        stage = 3;
                    if (damage >= 400 * GlobalMultiply.data.GlobalMultiply)
                        stage = 4;

                    if(effects["Горение"] < stage)
                    effects["Горение"] = stage;
                    break;
            }
        }
    }

    public class TypeDamage : INotifyPropertyChanged
    {
        private Dictionary<string, string> typeDamageToolTip = new()
        {
            { "Божественный", " Нечисть или нежить?" },
            { "Колющий", " Блок разбит?" }
        };


        private ComboBoxItem _nameType;
        public ComboBoxItem NameType
        {
            get => _nameType;
            set
            {
                _nameType = value;
                if (_nameType.Content != null)
                    if (typeDamageToolTip.Keys.Contains(_nameType.Content))
                    {
                        VisibilityCheckBox = Visibility.Visible;
                        TextCheckBox = typeDamageToolTip[_nameType.Content.ToString()];
                    }
                    else 
                    {
                        VisibilityCheckBox = Visibility.Collapsed;
                        CheckBox = false;
                    }

                OnPropertyChanged();
            }
        }

        public int Damage { get; set; } = 0;

        public bool CheckBox { get; set; } = false;

        private string _textCheckBox = "";
        public string TextCheckBox
        {
            get => _textCheckBox;
            set
            {
                _textCheckBox = value;
                OnPropertyChanged();
            }
        }

        private Visibility _visibilityCheckBox = Visibility.Collapsed;
        public Visibility VisibilityCheckBox
        {
            get => _visibilityCheckBox;
            set
            {
                _visibilityCheckBox = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TypeDamageConfig
    {
        [JsonPropertyName("Тип")]
        public string Type { get; set; } = "";

        [JsonPropertyName("МентальноеКД")]
        public int IsMentKD { get; set; } = 0;

        [JsonPropertyName("МножительБрони")]
        public double MultiplyArmorKD { get; set; } = 1;

        [JsonPropertyName("ЕслиЧтотоПроизошлоТоДамагУмножаетсяНа")]
        public double BaffDamage { get; set; } = 1;



        [JsonPropertyName("ЛибоЭффектКаждыйДамаг")]
        public int DamageInEffect { get; set; } = 0;


        [JsonPropertyName("ПроцентУронаОтЗдоровьяЧтобыНаложилсяЭффект")]
        public int ProcentInEffect { get; set; } = 0;

        [JsonPropertyName("ИмяЭффекта")]
        public string NameEffect { get; set; } = "";
    }
}
