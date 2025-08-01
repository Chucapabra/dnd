﻿using DNDHelper.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable.StatName;

namespace DNDHelper.Modules.Сharacteristics
{
    public class GridCharacteristics
    {
        Main main = Main.Instance;

        public void AddCharacteristic_Click()
        {
            int selectedIndex = main.DataGridCharacterisctics.SelectedIndex;

            if (Base(selectedIndex) < 30)
            {
                AddCharacteristic(selectedIndex, 1);
                FindAVariableCharacteristic(selectedIndex);
            }

            SetChars();
        }

        public void SubtractCharacteristic_Click()
        {
            int selectedIndex = main.DataGridCharacterisctics.SelectedIndex;

            if (Base(selectedIndex) > 0)
            {
                AddCharacteristic(selectedIndex, -1);
                FindAVariableCharacteristic(selectedIndex);
            }

            SetChars();
        }

        private void AddCharacteristic(int selectIndex, int Add)
        {
            switch (selectIndex)
            {
                case >= 0 and <= 1:
                    Base(0) += Add;
                    Base(1) = Base(0);
                    FindAVariableCharacteristic(0);
                    FindAVariableCharacteristic(1);
                    return;
                case >= 3 and <= 5:
                    if (PointsAgilitySkillsNow > 0 || Add < 0)
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
                case >= 21 and <= 24:
                    if (PointsCharismaSkillsNow > 0 || Add < 0)
                        Base(selectIndex) += Add;
                    return;
            }
            if (PointsNow > 0 || Add < 0)
                Base(selectIndex) += Add;
        }


        private void FindAVariableCharacteristic(int selectIndex)
        {
            switch (selectIndex)
            {
                case >= 0 and <= 1:
                    StrengthMethod();
                    break;
                case >= 2 and <= 5:
                    AgilityMethod();
                    PointsAgilitySkillsNow = PointsAgilitySkills - Base(Acrobatics) - Base(SleightOfHand) - Base(Stealth);
                    break;
                case 6:
                    BodyMethod();
                    break;
                case >= 7 and <= 13:
                    IntellectMethod();
                    PointsIntellectSkillsNow = PointsIntellectSkills - Base(Magic) - Base(Religion) - Base(Nature) - Base(History) - Base(Investigation) - Base(Technology);
                    break;
                case >= 14 and <= 19:
                    WisdomMethod();
                    PointsWisdomSkillsNow = PointsWisdomSkills - Base(Medicine) - Base(Perception) - Base(Insight) - Base(Survival) - Base(TOAnimals);
                    break;
                case >= 20 and <= 24:
                    CharismaMethod();
                    PointsCharismaSkillsNow = PointsCharismaSkills - Base(Deception) - Base(Intimidation) - Base(Speech) - Base(Persuasion);
                    break;
            }

            UpdateCharacterisitc(selectIndex);
            PointsNow = Points - Base(Strength) - Base(Agility) - Base(Body) - Base(Intellect) - Base(Wisdom) - Base(Charisma);
            UpdatePointText(true);
        }

        public void UpdatePointText(bool OnOff)
        {
            if (OnOff)
            {
                main.DataGridCharacterisctics.Columns[0].Header = $" Навык  Очков:{PointsNow}";
                DataGridChar[2].Name = $" Ловкость  Очков:{PointsAgilitySkillsNow}";
                DataGridChar[7].Name = $" Интеллект  Очков:{PointsIntellectSkillsNow}";
                DataGridChar[14].Name = $" Мудрость  Очков:{PointsWisdomSkillsNow}";
                DataGridChar[20].Name = $" Харизма  Очков:{PointsCharismaSkillsNow}";
            }
            else
            {
                main.DataGridCharacterisctics.Columns[0].Header = " Навык";
                DataGridChar[2].Name = $" Ловкость";
                DataGridChar[7].Name = $" Интеллект";
                DataGridChar[14].Name = $" Мудрость";
                DataGridChar[20].Name = $" Харизма";
            }

        }

        public void StrengthMethod()
        {

        }

        public void AgilityMethod()
        {
            PointsAgilitySkills = (int)(Base(Agility) * 3 * 0.75);
        }


        public void BodyMethod()
        {

        }

        public void IntellectMethod()
        {
            PointsIntellectSkills = (int)(Base(Intellect) * 3 * 0.75);
        }

        public void WisdomMethod()
        {
            PointsWisdomSkills = (int)(Base(Wisdom) * 3 * 0.75);
        }
        public void CharismaMethod()
        {
            PointsCharismaSkills = (int)(Base(Charisma) * 3 * 0.75);
        }

        public void UpdateCharacterisitc(int index)
        {
            CountCharacterisitc(index);
        }

        public void UpdateCharacterisitc(StatName name)
        {
            CountCharacterisitc((int)name);
        }

        public void UpdateCharacterisitc(StatName[] names)
        {
            foreach (StatName name in names)
                CountCharacterisitc((int)name);
        }

        private void CountCharacterisitc(int index)
        {
            Buffed(index) = Base(index);
            DataGridChar[index].Value = Buffed(index);
            DataGridChar[index].Roll = CalculateRoll(index);
        }

        static public ObservableCollection<Characteristic> DataGridChar = new()
            {
                new Characteristic { Name = " Сила", Value = Buffed(Strength), Roll = CalculateRoll(0) },
                new Characteristic { Name = "     Атлетика", Value = Buffed(Agility), Roll = CalculateRoll(1)},
                new Characteristic { Name = " Ловкость", Value = Buffed(Athlete), Roll = CalculateRoll(2)},
                new Characteristic { Name = "     Акробатика", Value = Buffed(Acrobatics), Roll = CalculateRoll(3)},
                new Characteristic { Name = "     Ловкость рук", Value = Buffed(SleightOfHand), Roll = CalculateRoll(4) },
                new Characteristic { Name = "     Скрытность", Value = Buffed(Stealth), Roll = CalculateRoll(5) },
                new Characteristic { Name = " Телосложение", Value = Buffed(Body), Roll = CalculateRoll(6)},
                new Characteristic { Name = " Интеллект", Value = Buffed(Intellect), Roll = CalculateRoll(7)},
                new Characteristic { Name = "     Магия", Value = Buffed(Magic), Roll = CalculateRoll(8) },
                new Characteristic { Name = "     Религия", Value = Buffed(Religion), Roll = CalculateRoll(9) },
                new Characteristic { Name = "     Природа", Value = Buffed(Nature), Roll = CalculateRoll(10) },
                new Characteristic { Name = "     История", Value = Buffed(History), Roll = CalculateRoll(11)},
                new Characteristic { Name = "     Расследовие", Value = Buffed(Investigation), Roll = CalculateRoll(12)},
                new Characteristic { Name = "     Технолония", Value = Buffed(Technology), Roll = CalculateRoll(13) },
                new Characteristic { Name = " Мудрость", Value = Buffed(Wisdom), Roll = CalculateRoll(14)},
                new Characteristic { Name = "     Медицина", Value = Buffed(Medicine), Roll = CalculateRoll(15)},
                new Characteristic { Name = "     Восприятие", Value = Buffed(Perception), Roll = CalculateRoll(16) },
                new Characteristic { Name = "     Проницательность", Value = Buffed(Insight), Roll = CalculateRoll(17) },
                new Characteristic { Name = "     Выживание", Value = Buffed(Survival), Roll = CalculateRoll(18) },
                new Characteristic { Name = "     Уход за животными", Value = Buffed(TOAnimals), Roll = CalculateRoll(19) },
                new Characteristic { Name = " Харизма", Value = Buffed(Charisma), Roll = CalculateRoll(20)},
                new Characteristic { Name = "     Обман", Value = Buffed(Deception), Roll = CalculateRoll(21) },
                new Characteristic { Name = "     Запугивание", Value = Buffed(Intimidation), Roll = CalculateRoll(22) },
                new Characteristic { Name = "     Выступление", Value = Buffed(Speech), Roll = CalculateRoll(23) },
                new Characteristic { Name = "     Убеждение", Value = Buffed(Persuasion), Roll = CalculateRoll(24) }
            };

        static public void SetChars()
        {
            Main.Instance.DataGridCharacterisctics.ItemsSource = DataGridChar;
        }



        static public int CalculateRoll(int setCharIndex)
        {
            return (int)MathF.Ceiling((float)((Buffed(setCharIndex) - 10) * 0.5));
        }
    }





    public class Characteristic : INotifyPropertyChanged
    {
        private string _name;
        private int _Value;
        private int _roll;

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

        public string ParentAttribute { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
