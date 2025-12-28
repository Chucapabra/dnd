using DNDHelper.Modules.Character;
using DNDHelper.Modules.Config;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.MagicSpells;
using DNDHelper.Modules.Settings;
using DNDHelper.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Linq;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable.StatName;

namespace DNDHelper.Modules.Сharacteristics
{
    public class GridCharacteristics
    {
        static public ObservableCollection<Characteristic> DataGridCharOld = new()
            {
                new Characteristic { Name = " Сила", Value = Buffed(Strength), Roll = CalculateRoll(0), Index = new int[]{0, 0 }  },
                new Characteristic { Name = "     Атлетика", Value = Buffed(Agility), Roll = CalculateRoll(1), Index = new int[]{1, 1 } },
                new Characteristic { Name = " Ловкость", Value = Buffed(Athlete), Roll = CalculateRoll(2), Index = new int[]{2, 2 } },
                new Characteristic { Name = "     Акробатика", Value = Buffed(Acrobatics), Roll = CalculateRoll(3), Index = new int[]{3,  3} },
                new Characteristic { Name = "     Ловкость рук", Value = Buffed(SleightOfHand), Roll = CalculateRoll(4), Index = new int[]{4, 4 } },
                new Characteristic { Name = "     Скрытность", Value = Buffed(Stealth), Roll = CalculateRoll(5), Index = new int[]{5,  5} },
                new Characteristic { Name = " Телосложение", Value = Buffed(Body), Roll = CalculateRoll(6), Index = new int[]{6,  6}},
                new Characteristic { Name = " Интеллект", Value = Buffed(Intellect), Roll = CalculateRoll(7) , Index = new int[]{7, 7 }},
                new Characteristic { Name = "     Магия", Value = Buffed(Magic), Roll = CalculateRoll(8) , Index = new int[]{8, 8 }},
                new Characteristic { Name = "     Религия", Value = Buffed(Religion), Roll = CalculateRoll(9) , Index = new int[]{9, 9 } },
                new Characteristic { Name = "     Природа", Value = Buffed(Nature), Roll = CalculateRoll(10)  , Index = new int[]{10,10  }},
                new Characteristic { Name = "     История", Value = Buffed(History), Roll = CalculateRoll(11) , Index = new int[]{11, 11 }},
                new Characteristic { Name = "     Расследование", Value = Buffed(Investigation), Roll = CalculateRoll(12), Index = new int[]{12,12  }},
                new Characteristic { Name = "     Технология", Value = Buffed(Technology), Roll = CalculateRoll(13) , Index = new int[]{13, 13 }},
                new Characteristic { Name = " Мудрость", Value = Buffed(Wisdom), Roll = CalculateRoll(14), Index = new int[]{14, 14 }},
                new Characteristic { Name = "     Медицина", Value = Buffed(Medicine), Roll = CalculateRoll(15), Index = new int[]{15, 15 }},
                new Characteristic { Name = "     Восприятие", Value = Buffed(Perception), Roll = CalculateRoll(16) , Index = new int[]{16, 16 }},
                new Characteristic { Name = "     Проницательность", Value = Buffed(Insight), Roll = CalculateRoll(17) , Index =new int[]{17,17  }},
                new Characteristic { Name = "     Выживание", Value = Buffed(Survival), Roll = CalculateRoll(18) , Index = new int[]{18, 18 }},
                new Characteristic { Name = "     Уход за животными", Value = Buffed(TOAnimals), Roll = CalculateRoll(19) , Index = new int[]{19,19  }},
                new Characteristic { Name = " Харизма", Value = Buffed(Charisma), Roll = CalculateRoll(20), Index = new int[]{20, 20 }},
                new Characteristic { Name = "     Обман", Value = Buffed(Deception), Roll = CalculateRoll(21) , Index = new int[]{21,21  }},
                new Characteristic { Name = "     Запугивание", Value = Buffed(Intimidation), Roll = CalculateRoll(22) , Index =new int[]{22, 22 }},
                new Characteristic { Name = "     Выступление", Value = Buffed(Speech), Roll = CalculateRoll(23) , Index = new int[]{23, 23 }},
                new Characteristic { Name = "     Убеждение", Value = Buffed(Persuasion), Roll = CalculateRoll(24), Index = new int[]{24, 24 } }          
        };
      
        static public ObservableCollection<Characteristic> DataGridCharNew = new()
            {
                new Characteristic { Name = " Сила", Value = Buffed(Strength), Roll = CalculateRoll(0), Index = new int[]{0, 0 }  },
                new Characteristic { Name = "     Атлетика", Value = Buffed(Agility), Roll = CalculateRoll(1), Index = new int[]{1, 1 } },
                new Characteristic { Name = " Ловкость", Value = Buffed(Athlete), Roll = CalculateRoll(2), Index = new int[]{2, 2 } },
                new Characteristic { Name = "     Акробатика", Value = Buffed(Acrobatics), Roll = CalculateRoll(3), Index = new int[]{3,  3} },
                new Characteristic { Name = "     Ловкость рук", Value = Buffed(SleightOfHand), Roll = CalculateRoll(4), Index = new int[]{4, 4 } },
                new Characteristic { Name = "     Скрытность", Value = Buffed(Stealth), Roll = CalculateRoll(5), Index = new int[]{5,  5} },
                new Characteristic { Name = " Телосложение", Value = Buffed(Body), Roll = CalculateRoll(6), Index = new int[]{6,  6}},
                new Characteristic { Name = "     Иммунитет", Value = Buffed(Immunity), Roll = CalculateRoll(26), Index = new int[]{26, 7 }},
                new Characteristic { Name = "     Баланс", Value = Buffed(Balance), Roll = CalculateRoll(27), Index = new int[]{27, 8 }},
                new Characteristic { Name = "     Стойкость", Value = Buffed(Resistance), Roll = CalculateRoll(28), Index = new int[]{28, 9 }},
                new Characteristic { Name = "     Закалка", Value = Buffed(Tempering), Roll = CalculateRoll(29) , Index = new int[]{29, 10 }},
                new Characteristic { Name = " Интеллект", Value = Buffed(Intellect), Roll = CalculateRoll(7) , Index = new int[]{7, 11 }},
                new Characteristic { Name = "     Магия", Value = Buffed(Magic), Roll = CalculateRoll(8) , Index = new int[]{8, 12 }},
                new Characteristic { Name = "     Религия", Value = Buffed(Religion), Roll = CalculateRoll(9) , Index = new int[]{9, 13 } },
                new Characteristic { Name = "     Природа", Value = Buffed(Nature), Roll = CalculateRoll(10)  , Index = new int[]{10,14  }},
                new Characteristic { Name = "     История", Value = Buffed(History), Roll = CalculateRoll(11) , Index = new int[]{11, 15 }},
                new Characteristic { Name = "     Расследование", Value = Buffed(Investigation), Roll = CalculateRoll(12), Index = new int[]{12,16  }},
                new Characteristic { Name = "     Технология", Value = Buffed(Technology), Roll = CalculateRoll(13) , Index = new int[]{13, 17 }},
                new Characteristic { Name = " Мудрость", Value = Buffed(Wisdom), Roll = CalculateRoll(14), Index = new int[]{14, 18 }},
                new Characteristic { Name = "     Медицина", Value = Buffed(Medicine), Roll = CalculateRoll(15), Index = new int[]{15, 19 }},
                new Characteristic { Name = "     Восприятие", Value = Buffed(Perception), Roll = CalculateRoll(16) , Index = new int[]{16, 20 }},
                new Characteristic { Name = "     Проницательность", Value = Buffed(Insight), Roll = CalculateRoll(17) , Index =new int[]{17,21  }},
                new Characteristic { Name = "     Выживание", Value = Buffed(Survival), Roll = CalculateRoll(18) , Index = new int[]{18, 22 }},
                new Characteristic { Name = "     Уход за животными", Value = Buffed(TOAnimals), Roll = CalculateRoll(19) , Index = new int[]{19,23  }},
                new Characteristic { Name = " Харизма", Value = Buffed(Charisma), Roll = CalculateRoll(20), Index = new int[]{20, 24 }},
                new Characteristic { Name = "     Обман", Value = Buffed(Deception), Roll = CalculateRoll(21) , Index = new int[]{21,25  }},
                new Characteristic { Name = "     Запугивание", Value = Buffed(Intimidation), Roll = CalculateRoll(22) , Index =new int[]{22, 26 }},
                new Characteristic { Name = "     Выступление", Value = Buffed(Speech), Roll = CalculateRoll(23) , Index = new int[]{23, 27 }},
                new Characteristic { Name = "     Убеждение", Value = Buffed(Persuasion), Roll = CalculateRoll(24), Index = new int[]{24, 28 } },
                new Characteristic { Name = "     Очарование", Value = Buffed(Charm), Roll = CalculateRoll(25), Index = new int[]{25,29  }}
            };

        static public ObservableCollection<Characteristic> DataGridChar = DataGridCharNew;

        Main main = Main.Instance;

        public GridCharacteristics()
        {
            Main.Instance.DataGridCharacterisctics.ItemsSource = DataGridChar;
            Debug.WriteLine("s");
        }

        public void AddCharacteristic_Click()
        {
            int[] selectedIndex = DataGridChar[main.DataGridCharacterisctics.SelectedIndex].Index;

            
            if (Base(selectedIndex[0]) <= 30)
            {
                AddCharacteristic(selectedIndex[0], 1);
                FindAVariableCharacteristic(selectedIndex[0]);
                UpdateCharacterisitc(selectedIndex[1]);
                DataManager.Save();
            }
        }

        public void SubtractCharacteristic_Click()
        {
            int[] selectedIndex = DataGridChar[main.DataGridCharacterisctics.SelectedIndex].Index;

            if (Base(selectedIndex[0]) > 3)
            {
                AddCharacteristic(selectedIndex[0], -1);
                FindAVariableCharacteristic(selectedIndex[0]);
                UpdateCharacterisitc(selectedIndex[1]);
                DataManager.Save();
            }
        }

        private void AddCharacteristic(int selectIndex, int Add)
        {
            switch (selectIndex)
            {
                case >= 0 and <= 1:
                    if ((PointsNow > 0 || Add < 0) && (Base(selectIndex) > 5 || Add > 0))
                    {
                        Base(0) += Add;
                        Base(1) = Base(0);
                        FindAVariableCharacteristic(0);
                        FindAVariableCharacteristic(1);
                        UpdateCharacterisitc(0);
                        UpdateCharacterisitc(1);
                    }
                    return;
                case >= 3 and <= 5:
                    if (PointsAgilitySkillsNow > 0 || Add < 0)
                        Base(selectIndex) += Add;
                    return;
                case >= 26 and <= 29:
                    if (PointsBodySkillsNow > 0 || Add < 0)
                        Base(selectIndex) += Add;
                    return;
                case >= 8 and <= 13:
                    if (PointsIntellectSkillsNow > 0 || Add < 0)
                        Base(selectIndex) += Add;
                    return;
                case >= 15 and <= 19:
                    if (PointsWisdomSkillsNow > 0 || Add < 0)
                        Base(selectIndex) += Add;
                    return;
                case >= 21 and <= 25:
                    if (PointsCharismaSkillsNow > 0 || Add < 0)
                        Base(selectIndex) += Add;
                    return;
            }
            if ((PointsNow > 0 || Add < 0) && (Base(selectIndex) > 5 || Add > 0))
                Base(selectIndex) += Add;
        }


        public void FindAVariableCharacteristic(int selectIndex)
        {
            switch (selectIndex)
            {
                case 0:
                    StrengthMethod();
                    break;
                case >= 2 and <= 5:
                    AgilityMethod();
                    PointsAgilitySkillsNow = PointsAgilitySkills - Base(Acrobatics) - Base(SleightOfHand) - Base(Stealth);
                    break;
                case >= 26 and <= 29 or 6:
                    BodyMethod();
                    PointsBodySkillsNow = PointsBodySkills - Base(Balance) - Base(Tempering) - Base(Resistance) - Base(Immunity);
                    break;
                case >= 7 and <= 13:
                    IntellectMethod();
                    PointsIntellectSkillsNow = PointsIntellectSkills - Base(Magic) - Base(Religion) - Base(Nature) - Base(History) - Base(Investigation) - Base(Technology);
                    break;
                case >= 14 and <= 19:
                    WisdomMethod();
                    PointsWisdomSkillsNow = PointsWisdomSkills - Base(Medicine) - Base(Perception) - Base(Insight) - Base(Survival) - Base(TOAnimals);
                    break;
                case >= 20 and <= 25:
                    CharismaMethod();
                    PointsCharismaSkillsNow = PointsCharismaSkills - Base(Deception) - Base(Intimidation) - Base(Speech) - Base(Persuasion) - Base(Charm);
                    break;
            }
            PointsNow = Race.SelectedClassData.AddPoints + LevelBaffs.AddPoints + Points - Base(Strength) - Base(Agility) - Base(Body) - Base(Intellect) - Base(Wisdom) - Base(Charisma);
            UpdatePointText(main.EditMode_button.IsChecked);
        }
            
        public void UpdatePointText(bool OnOff)
        {
            int add = 0;
            if (DataGridChar.Count > 25)
                add = 4;

            if (OnOff)
            {
                main.DataGridCharacterisctics.Columns[0].Header = $" Навык  Очков:{PointsNow}";
                DataGridChar[2].Name = $" Ловкость  Очков:{PointsAgilitySkillsNow}";
                DataGridChar[6].Name = $" Телосложение  Очков:{PointsBodySkillsNow}";
                DataGridChar[7 + add].Name = $" Интеллект  Очков:{PointsIntellectSkillsNow}";
                DataGridChar[14 + add].Name = $" Мудрость  Очков:{PointsWisdomSkillsNow}";
                DataGridChar[20 + add].Name = $" Харизма  Очков:{PointsCharismaSkillsNow}";
            }
            else
            {
                main.DataGridCharacterisctics.Columns[0].Header = " Навык";
                DataGridChar[2].Name = $" Ловкость";
                DataGridChar[6].Name = $" Телосложение";
                DataGridChar[7 + add].Name = $" Интеллект";
                DataGridChar[14 + add].Name = $" Мудрость";
                DataGridChar[20 + add].Name = $" Харизма";
            }

        }

        public void UpdateCharacterisitc(int index)
        {
            CountCharacterisitc(DataGridChar[index].Index[0], DataGridChar[index].Index[1]);
        }

        public void UpdateCharacterisitc(StatName name)
        {
            CountCharacterisitc(DataGridChar[(int)name].Index[0], DataGridChar[(int)name].Index[1]);
        }

        public void UpdateCharacterisitc(StatName[] names)
        {
            foreach (StatName name in names)
                CountCharacterisitc(DataGridChar[(int)name].Index[0], DataGridChar[(int)name].Index[1]);
        }

        public void UpdateAllCharacterisitc()
        {
            for (int i = 0; i < 30; i++)
            {
                if (DataGridChar.Count > i)
                    CountCharacterisitc(DataGridChar[i].Index[0], DataGridChar[i].Index[1]);
            }
        }

        private void CountCharacterisitc(int index, int indexGUI)
        {
            if (DataGridChar.Count > indexGUI)
            {
                Buffed(index) = Base(index) + OtherBaff(index) + Race.Stats[index].Value + PlayerClass.Stats[index].Value + TreeSkills.Stats[index].Value + ItemBaffsListScript.ItemBaffs[index][0] + Effects.EffectBaffs[index][0];
                DataGridChar[indexGUI].Value = Buffed(index);
                DataGridChar[indexGUI].Roll = CalculateRoll(index);
                FindAVariableCharacteristic(index);

                DataGridChar[indexGUI].ToolTip = $"{DataGridChar[indexGUI].Name.Replace(" ", "")}:  Пассивная:{10 + DataGridChar[indexGUI].Roll}\r\n" +
                                              $"СТАТ: Базовая:{Base(index)} Раса:{Race.Stats[index].Value} Класс:{PlayerClass.Stats[index].Value} Древо развитие:{TreeSkills.Stats[index].Value} Предметы:{ItemBaffsListScript.ItemBaffs[index][0]} Эффекты:{Effects.EffectBaffs[index][0]} Остальное:{OtherBaff(index)}\r\n" +
                                              $"РОЛЛ: Базовый:{(int)MathF.Floor((float)((Buffed(index) - 10) * 0.5))} Раса:{Race.Stats[index].Roll} Класс:{PlayerClass.Stats[index].Roll} Древо развитие:{TreeSkills.Stats[index].Roll} Предметы:{ItemBaffsListScript.ItemBaffs[index][1]} Эффекты:{Effects.EffectBaffs[index][1] + Effects.EffectBaffs[27][0]}";
            }
        }

        static public int CalculateRoll(int setCharIndex)
        {
                int baffEffect = Effects.EffectBaffs[setCharIndex][1];
                if (baffEffect == 0)
                    baffEffect = Effects.EffectBaffs[32][0];

                return (int)MathF.Floor((float)((Buffed(setCharIndex) - 10) * 0.5) + Race.Stats[setCharIndex].Roll + PlayerClass.Stats[setCharIndex].Roll + TreeSkills.Stats[setCharIndex].Roll + ItemBaffsListScript.ItemBaffs[setCharIndex][1] + baffEffect);
        }

        static public void UpdateOtherDeBaff()
        {
            OtherBaff(Agility) = TypeArmorBaffs.Agility;
            OtherBaff(30) = TypeArmorBaffs.Attack + WeightScript.Attack;
            OtherBaff(31) = TypeArmorBaffs.Block;
            OtherBaff(32) = TypeArmorBaffs.Dodge + WeightScript.Dodge;
            OtherBaff(33) = TypeArmorBaffs.CounterAction;

            AttributesCharacter.UpdateRolls();
            Main.Characteristics.UpdateCharacterisitc(Agility);
        }

        // Методы характеристик
        public void StrengthMethod()
        {
            // Бафф к харизме
            int strength = Base(Strength) + Race.Stats[0].Value + PlayerClass.Stats[0].Value;
            OtherBaff(Athlete) = Race.Stats[0].Value + PlayerClass.Stats[0].Value;
            UpdateCharacterisitc(Athlete);
            if (strength > 0)
            {
                int addRollCharisma = (int)(strength / 5) - 2;
                if (addRollCharisma < 0)
                    addRollCharisma = 0;
                OtherBaff(Charisma) = addRollCharisma;
                UpdateCharacterisitc(Charisma);
            }

            WeightScript.CountWeightCharacter();
        }

        public void AgilityMethod()
        {
            PointsAgilitySkills = (int)((Base(Agility) + Race.Stats[2].Value + PlayerClass.Stats[2].Value + TreeSkills.Stats[2].Value) * 3 * 0.7);

            AttributesCharacter.СountAvailableActions();
            AttributesCharacter.StickMethod();
        }


        public void BodyMethod()
        {
            PointsBodySkills = (int)((Base(Body) + Race.Stats[6].Value + PlayerClass.Stats[6].Value + TreeSkills.Stats[6].Value) * 4 * 0.7);
            Health.HealthUpdate();
            KDScript.CountKD();
        }

        public void IntellectMethod()
        {
            PointsIntellectSkills = (int)((Base(Intellect) + Race.Stats[7].Value + PlayerClass.Stats[7].Value + TreeSkills.Stats[7].Value) * 6 * 0.7);

            KDScript.CountMentalKD();

            if (PlayerClass.SelectedClassData.CharacteristicMagic.ToLower() == "и")
                PlayerClass.UpdateCharacteristicMagic();
        }

        public void WisdomMethod()
        {
            PointsWisdomSkills = (int)((Base(Wisdom) + Race.Stats[14].Value + PlayerClass.Stats[14].Value + TreeSkills.Stats[14].Value) * 5 * 0.7);


            MagicSpells.MagicSpells.UpdateMagicBullet();
            if (PlayerClass.SelectedClassData.CharacteristicMagic.ToLower() == "м")
                PlayerClass.UpdateCharacteristicMagic();
        }
        public void CharismaMethod()
        {
            PointsCharismaSkills = (int)((Base(Charisma) + Race.Stats[20].Value + PlayerClass.Stats[20].Value + OtherBaff(Charisma) + TreeSkills.Stats[20].Value) * 5 * 0.7);

            if (PlayerClass.SelectedClassData.CharacteristicMagic.ToLower() == "х")
                PlayerClass.UpdateCharacteristicMagic();
        }
    }





    public class Characteristic : INotifyPropertyChanged
    {
        private string _name;
        private int _Value;
        private int _roll;
        private string _toolTip;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Value
        {
            get => _Value;
            set
            {
                _Value = value;
                OnPropertyChanged();
            }
        }

        public int Roll
        {
            get => _roll;
            set
            {
                _roll = value;
                OnPropertyChanged();
            }
        }

        public int[] Index = new int[2];




        public string ToolTip
        {
            get => _toolTip;
            set
            {
                _toolTip = value;
                OnPropertyChanged();
            }
        }

        public string ParentAttribute { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
