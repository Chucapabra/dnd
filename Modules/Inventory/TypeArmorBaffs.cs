using DNDHelper.Modules.Character;
using DNDHelper.Modules.Settings;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDHelper.Modules.Inventory
{
    internal class TypeArmorBaffs
    {
        Main main = Main.Instance;

        public TypeArmorBaffs()
        {
            main.armor_type_cb.SelectionChanged += Armor_type_cb_SelectionChanged;
        }

        private void Armor_type_cb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SetArmor(main.armor_type_cb.SelectedIndex);
        }

        public static int SelectedArmor = 0;

        public static int Agility = 0;
        public static int Attack = 0;
        public static int Block = 0;
        public static int Dodge = 0;
        public static int CounterAction = 0;

        public static void SetArmor(int index)
        {
            SelectedArmor = index;

            Agility = 0;
            Attack = 0;
            Block = 0;
            Dodge = 0;
            CounterAction = 0;

            switch (index)
            {
                case 1:
                    Dodge = 2;
                    break;
                case 2:
                    Agility = -4;
                    Attack = -2;                  
                    CounterAction = -2;

                    if(DataManager.DataSave.SelectedRepository == SetRepository.repositories[0].Name)
                        Block = 2;
                    else
                        Block = 1;
                    break;
                case 3:
                    Agility = -8;
                    Attack = -4;
                    Block = 2;
                    Dodge = -8;
                    CounterAction = -4;
                    break;
            }

            GridCharacteristics.UpdateOtherDeBaff();
        }
    }
}
