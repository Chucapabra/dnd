using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;

namespace DNDHelper.Modules.Character
{
    class AttributesCharacter
    {
        public static void CallAllMethodInScript()
        {
            UpdateRolls();
            СountAvailableActions();
            StickMethod();
        }


        public static void UpdateRolls()
        {
            Main main = Main.Instance;
            main.character_rollattack_textblock.Text = (ItemBaffsListScript.ItemBaffs[25][0]).ToString();
            main.character_block_textblock.Text = (ItemBaffsListScript.ItemBaffs[26][0]).ToString();
            main.character_dodge_textblock.Text = (ItemBaffsListScript.ItemBaffs[27][0]).ToString();
            main.character_counteraction_textblock.Text = (ItemBaffsListScript.ItemBaffs[28][0]).ToString();
            main.character_fistattack_textblock.Text = (ItemBaffsListScript.ItemBaffs[29][0]).ToString();
            main.character_longrangeattack_textblock.Text = (ItemBaffsListScript.ItemBaffs[30][0]).ToString();
        }

        public static void СountAvailableActions()
        {
            int actions = 1;
            int counterActions = 1;

            if (CharacteristicTable.Buffed(CharacteristicTable.StatName.Agility) >= 20)
            {
                if (CharacteristicTable.Buffed(CharacteristicTable.StatName.Agility) >= 30)
                {
                    actions = 3;
                    counterActions = 3;
                }
                actions = 2;
                counterActions = 2;
            }

            actions += ItemBaffsListScript.ItemBaffs[35][0];
            counterActions += ItemBaffsListScript.ItemBaffs[36][0];

            Main.Instance.Actions_textblock.Text = actions.ToString();
            Main.Instance.Counter_actions_textblock.Text = counterActions.ToString();
        }

        public static void StickMethod()
        {
            double lengthStick = 1;

            int Agility = CharacteristicTable.Buffed(CharacteristicTable.StatName.Agility);
            double addStick = 0;
            if ((Agility - 10) >= 5)
                addStick = (int)((Agility - 10) / 5) * 0.5;

            lengthStick += addStick + ItemBaffsListScript.ItemBaffs[34][0];
            Main.Instance.Movesticks_textblock.Text = lengthStick.ToString();
        }
    }
}
