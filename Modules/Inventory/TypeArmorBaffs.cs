using DNDHelper.Modules.Character;
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

        public void SetArmor(int index)
        {
            CharacteristicTable.OtherBaff(CharacteristicTable.StatName.Agility) = 0;
            CharacteristicTable.OtherBaff(25) = 0;
            CharacteristicTable.OtherBaff(26) = 0;
            CharacteristicTable.OtherBaff(27) = 0;
            CharacteristicTable.OtherBaff(28) = 0;

            switch (index)
            {
                case 1:
                    CharacteristicTable.OtherBaff(27) = 2;
                    break;
                case 2:
                    CharacteristicTable.OtherBaff(CharacteristicTable.StatName.Agility) = -4;
                    CharacteristicTable.OtherBaff(25) = -2;
                    CharacteristicTable.OtherBaff(26) = 2;
                    CharacteristicTable.OtherBaff(28) = -2;
                    break;
                case 3:
                    CharacteristicTable.OtherBaff(CharacteristicTable.StatName.Agility) = -8;
                    CharacteristicTable.OtherBaff(25) = -4;
                    CharacteristicTable.OtherBaff(26) = 4;
                    CharacteristicTable.OtherBaff(27) = -8;
                    CharacteristicTable.OtherBaff(28) = -4;
                    break;
            }

            AttributesCharacter.UpdateRolls();
            Main.Characteristics.UpdateAllCharacterisitc();
        }
    }
}
