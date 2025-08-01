using DNDHelper.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using static DNDHelper.Modules.Сharacteristics.CharacteristicTable.StatName;

namespace DNDHelper.Modules.Сharacteristics
{
    public class GridCharacteristics
	{
        Main main = Main.Instance;

        public void AddCharacteristic_Click()
        {
            int selectedIndex = main.DataGridCharacterisctics.SelectedIndex;

            if (CharacteristicTable.Base(selectedIndex) < 30)
            {
                CharacteristicTable.Base(selectedIndex)++;
                DataGridChar[selectedIndex].StandartValue = CharacteristicTable.Base(selectedIndex);
            }

            SetChars();
        }

        public void SubtractCharacteristic_Click()
        {
            int selectedIndex = main.DataGridCharacterisctics.SelectedIndex;

            if (CharacteristicTable.Base(selectedIndex) > 0)
            {
                CharacteristicTable.Base(selectedIndex)--;
                DataGridChar[selectedIndex].StandartValue = CharacteristicTable.Base(selectedIndex);
            }

            SetChars();
        }
 
        static public ObservableCollection<Characteristic> DataGridChar = new()
            {
                new Characteristic { Name = " Сила", BaffValue = CharacteristicTable.Buffed(Strength), Roll = CalculateRoll(CharacteristicTable.Buffed(Strength)), StandartValue = CharacteristicTable.Base(Strength), ParentAttribute = "" },
                new Characteristic { Name = "     Атлетика", BaffValue = CharacteristicTable.Buffed(Agility), Roll = CalculateRoll(CharacteristicTable.Buffed(Agility)), StandartValue = CharacteristicTable.Base(Agility)},
                new Characteristic { Name = " Ловкость", BaffValue = CharacteristicTable.Buffed(Athlete), Roll = CalculateRoll(CharacteristicTable.Buffed(Athlete)), StandartValue = CharacteristicTable.Base(Athlete) },
                new Characteristic { Name = "     Акробатика", BaffValue = CharacteristicTable.Buffed(Acrobatics), Roll = CalculateRoll(CharacteristicTable.Buffed(Acrobatics)), StandartValue = CharacteristicTable.Base(Acrobatics) },
                new Characteristic { Name = "     Ловкость рук", BaffValue = CharacteristicTable.Buffed(SleightOfHand), Roll = CalculateRoll(CharacteristicTable.Buffed(SleightOfHand)), StandartValue = CharacteristicTable.Base(SleightOfHand) },
                new Characteristic { Name = "     Скрытность", BaffValue = CharacteristicTable.Buffed(Stealth), Roll = CalculateRoll(CharacteristicTable.Buffed(Stealth)), StandartValue = CharacteristicTable.Base(Stealth) },
                new Characteristic { Name = " Телосложение", BaffValue = CharacteristicTable.Buffed(Body), Roll = CalculateRoll(CharacteristicTable.Buffed(Body)), StandartValue = CharacteristicTable.Base(Body)},
                new Characteristic { Name = " Интеллект", BaffValue = CharacteristicTable.Buffed(Intellect), Roll = CalculateRoll(CharacteristicTable.Buffed(Intellect)), StandartValue = CharacteristicTable.Base(Intellect)},
                new Characteristic { Name = "     Магия", BaffValue = CharacteristicTable.Buffed(Magic), Roll = CalculateRoll(CharacteristicTable.Buffed(Magic)), StandartValue = CharacteristicTable.Base(Magic) },
                new Characteristic { Name = "     Религия", BaffValue = CharacteristicTable.Buffed(Religion), Roll = CalculateRoll(CharacteristicTable.Buffed(Religion)), StandartValue = CharacteristicTable.Base(Religion) },
                new Characteristic { Name = "     Природа", BaffValue = CharacteristicTable.Buffed(Nature), Roll = CalculateRoll(CharacteristicTable.Buffed(Nature)), StandartValue = CharacteristicTable.Base(Nature) },
                new Characteristic { Name = "     История", BaffValue = CharacteristicTable.Buffed(History), Roll = CalculateRoll(CharacteristicTable.Buffed(History)), StandartValue = CharacteristicTable.Base(History)},
                new Characteristic { Name = "     Расследовие", BaffValue = CharacteristicTable.Buffed(Investigation), Roll = CalculateRoll(CharacteristicTable.Buffed(Investigation)), StandartValue = CharacteristicTable.Base(Investigation)},
                new Characteristic { Name = "     Технолония", BaffValue = CharacteristicTable.Buffed(Technology), Roll = CalculateRoll(CharacteristicTable.Buffed(Technology)), StandartValue = CharacteristicTable.Base(Technology) },
                new Characteristic { Name = " Мудрость", BaffValue = CharacteristicTable.Buffed(Wisdom), Roll = CalculateRoll(CharacteristicTable.Buffed(Wisdom)), StandartValue = CharacteristicTable.Base(Wisdom)},
                new Characteristic { Name = "     Медицина", BaffValue = CharacteristicTable.Buffed(Medicine), Roll = CalculateRoll(CharacteristicTable.Buffed(Medicine)), StandartValue = CharacteristicTable.Base(Medicine)},
                new Characteristic { Name = "     Восприятие", BaffValue = CharacteristicTable.Buffed(Perception), Roll = CalculateRoll(CharacteristicTable.Buffed(Perception)), StandartValue = CharacteristicTable.Base(Perception) },
                new Characteristic { Name = "     Проницательность", BaffValue = CharacteristicTable.Buffed(Insight), Roll = CalculateRoll(CharacteristicTable.Buffed(Insight)), StandartValue = CharacteristicTable.Base(Insight) },
                new Characteristic { Name = "     Выживание", BaffValue = CharacteristicTable.Buffed(Survival), Roll = CalculateRoll(CharacteristicTable.Buffed(Survival)), StandartValue = CharacteristicTable.Base(Survival) },
                new Characteristic { Name = "     Уход за животными", BaffValue = CharacteristicTable.Buffed(TOAnimals), Roll = CalculateRoll(CharacteristicTable.Buffed(TOAnimals)), StandartValue = CharacteristicTable.Base(TOAnimals) },
                new Characteristic { Name = " Харизма", BaffValue = CharacteristicTable.Buffed(Charisma), Roll = CalculateRoll(CharacteristicTable.Buffed(Charisma)), StandartValue = CharacteristicTable.Base(Charisma)},
                new Characteristic { Name = "     Обман", BaffValue = CharacteristicTable.Buffed(Deception), Roll = CalculateRoll(CharacteristicTable.Buffed(Deception)), StandartValue = CharacteristicTable.Base(Deception) },
                new Characteristic { Name = "     Запугивание", BaffValue = CharacteristicTable.Buffed(Intimidation), Roll = CalculateRoll(CharacteristicTable.Buffed(Intimidation)), StandartValue = CharacteristicTable.Base(Intimidation) },
                new Characteristic { Name = "     Выступление", BaffValue = CharacteristicTable.Buffed(Speech), Roll = CalculateRoll(CharacteristicTable.Buffed(Speech)), StandartValue = CharacteristicTable.Base(Speech) },
                new Characteristic { Name = "     Убеждение", BaffValue = CharacteristicTable.Buffed(Persuasion), Roll = CalculateRoll(CharacteristicTable.Buffed(Persuasion)), StandartValue = CharacteristicTable.Base(Persuasion) }
            };
        
        static public void SetChars()
        {
            Main.Instance.DataGridCharacterisctics.ItemsSource = DataGridChar;
        }



        static public int CalculateRoll(int valueChar)
        {
            return (int)MathF.Ceiling((float)((valueChar - 10) * 0.5));
        }
    }





    public class Characteristic : INotifyPropertyChanged
    {
        private string _name;
        private int _baffValue;
        private int _roll;
        private int _standartValue;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int BaffValue
        {
            get => _baffValue;
            set
            {
                _baffValue = value;
                Roll = GridCharacteristics.CalculateRoll(_baffValue);
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

        public int StandartValue
        {
            get => _standartValue;
            set
            {
                _standartValue = value;
                BaffValue = value;
                Roll = GridCharacteristics.CalculateRoll(_baffValue);
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
