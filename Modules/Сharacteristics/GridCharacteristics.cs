using DNDHelper.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DNDHelper.Modules.Сharacteristics
{
    public class GridCharacteristics
	{
        Main main = Main.Instance;

        public void AddCharacteristic_Click()
        {
            int selectedIndex = main.DataGridCharacterisctics.SelectedIndex;

            if (DataGridChar[selectedIndex].StandartValue < 30)
                DataGridChar[selectedIndex].StandartValue += 1;

            SetChars();
        }

        public void SubtractCharacteristic_Click()
        {
            int selectedIndex = main.DataGridCharacterisctics.SelectedIndex;

            if (DataGridChar[selectedIndex].StandartValue > 0)
                DataGridChar[selectedIndex].StandartValue -= 1;

            SetChars();
        }

        static public ObservableCollection<Characteristic> DataGridChar = new()
            {
                new Characteristic { Name = " Сила", BaffValue = CharacteristicTable.Strength_, Roll = CalculateRoll(CharacteristicTable.Strength_), StandartValue = CharacteristicTable.Strength },
                new Characteristic { Name = "     Атлетика", BaffValue = CharacteristicTable.Agility_, Roll = CalculateRoll(CharacteristicTable.Agility_), StandartValue = CharacteristicTable.Agility},
                new Characteristic { Name = " Ловкость", BaffValue = CharacteristicTable.Athlete_, Roll = CalculateRoll(CharacteristicTable.Athlete_), StandartValue = CharacteristicTable.Athlete },
                new Characteristic { Name = "     Акробатика", BaffValue = CharacteristicTable.Acrobatics_, Roll = CalculateRoll(CharacteristicTable.Acrobatics_), StandartValue = CharacteristicTable.Acrobatics },
                new Characteristic { Name = "     Ловкость рук", BaffValue = CharacteristicTable.SleightOfHand_, Roll = CalculateRoll(CharacteristicTable.SleightOfHand_), StandartValue = CharacteristicTable.SleightOfHand },
                new Characteristic { Name = "     Скрытность", BaffValue = CharacteristicTable.Stealth_, Roll = CalculateRoll(CharacteristicTable.Stealth_), StandartValue = CharacteristicTable.Stealth },
                new Characteristic { Name = " Телосложение", BaffValue = CharacteristicTable.Body_, Roll = CalculateRoll(CharacteristicTable.Body_), StandartValue = CharacteristicTable.Body},
                new Characteristic { Name = " Интеллект", BaffValue = CharacteristicTable.Intellect_, Roll = CalculateRoll(CharacteristicTable.Intellect_), StandartValue = CharacteristicTable.Intellect},
                new Characteristic { Name = "     Магия", BaffValue = CharacteristicTable.Magic_, Roll = CalculateRoll(CharacteristicTable.Magic_), StandartValue = CharacteristicTable.Magic },
                new Characteristic { Name = "     Религия", BaffValue = CharacteristicTable.Religion_, Roll = CalculateRoll(CharacteristicTable.Religion_), StandartValue = CharacteristicTable.Religion },
                new Characteristic { Name = "     Природа", BaffValue = CharacteristicTable.Nature_, Roll = CalculateRoll(CharacteristicTable.Nature_), StandartValue = CharacteristicTable.Nature },
                new Characteristic { Name = "     История", BaffValue = CharacteristicTable.History_, Roll = CalculateRoll(CharacteristicTable.History_), StandartValue = CharacteristicTable.History},
                new Characteristic { Name = "     Расследовие", BaffValue = CharacteristicTable.Investigation_, Roll = CalculateRoll(CharacteristicTable.Investigation_), StandartValue = CharacteristicTable.Investigation},
                new Characteristic { Name = "     Технолония", BaffValue = CharacteristicTable.Technology_, Roll = CalculateRoll(CharacteristicTable.Technology_), StandartValue = CharacteristicTable.Technology },
                new Characteristic { Name = " Мудрость", BaffValue = CharacteristicTable.Wisdom_, Roll = CalculateRoll(CharacteristicTable.Wisdom_), StandartValue = CharacteristicTable.Wisdom},
                new Characteristic { Name = "     Медицина", BaffValue = CharacteristicTable.Medicine_, Roll = CalculateRoll(CharacteristicTable.Medicine_), StandartValue = CharacteristicTable.Medicine},
                new Characteristic { Name = "     Восприятие", BaffValue = CharacteristicTable.Perception_, Roll = CalculateRoll(CharacteristicTable.Perception_), StandartValue = CharacteristicTable.Perception },
                new Characteristic { Name = "     Проницательность", BaffValue = CharacteristicTable.Insight_, Roll = CalculateRoll(CharacteristicTable.Insight_), StandartValue = CharacteristicTable.Insight },
                new Characteristic { Name = "     Выживание", BaffValue = CharacteristicTable.Survival_, Roll = CalculateRoll(CharacteristicTable.Survival_), StandartValue = CharacteristicTable.Survival },
                new Characteristic { Name = "     Уход за животными", BaffValue = CharacteristicTable.TOAnimals_, Roll = CalculateRoll(CharacteristicTable.TOAnimals_), StandartValue = CharacteristicTable.TOAnimals },
                new Characteristic { Name = " Харизма", BaffValue = CharacteristicTable.Charisma_, Roll = CalculateRoll(CharacteristicTable.Charisma_), StandartValue = CharacteristicTable.Charisma},
                new Characteristic { Name = "     Обман", BaffValue = CharacteristicTable.Deception_, Roll = CalculateRoll(CharacteristicTable.Deception_), StandartValue = CharacteristicTable.Deception },
                new Characteristic { Name = "     Запугивание", BaffValue = CharacteristicTable.Intimidation_, Roll = CalculateRoll(CharacteristicTable.Intimidation_), StandartValue = CharacteristicTable.Intimidation },
                new Characteristic { Name = "     Выступление", BaffValue = CharacteristicTable.Speech_, Roll = CalculateRoll(CharacteristicTable.Speech_), StandartValue = CharacteristicTable.Speech },
                new Characteristic { Name = "     Убеждение", BaffValue = CharacteristicTable.Persuasion_, Roll = CalculateRoll(CharacteristicTable.Persuasion_), StandartValue = CharacteristicTable.Persuasion }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
