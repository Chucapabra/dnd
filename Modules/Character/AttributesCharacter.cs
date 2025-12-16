using DNDHelper.Modules.Config;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System.Diagnostics;
using System.Windows.Controls;

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
            main.character_rollattack_textblock.Text = (ItemBaffsListScript.ItemBaffs[25][0] + CharacteristicTable.OtherBaff(25) + Effects.EffectBaffs[26][0]).ToString();
            main.character_block_textblock.Text = (ItemBaffsListScript.ItemBaffs[26][0] + CharacteristicTable.OtherBaff(26) + Effects.EffectBaffs[26][0] + Effects.EffectBaffs[28][0]).ToString();
            main.character_dodge_textblock.Text = (ItemBaffsListScript.ItemBaffs[27][0] + CharacteristicTable.OtherBaff(27) + Effects.EffectBaffs[26][0]).ToString();
            main.character_counteraction_textblock.Text = (ItemBaffsListScript.ItemBaffs[28][0] + CharacteristicTable.OtherBaff(28) + Effects.EffectBaffs[26][0]).ToString();
            main.character_fistattack_textblock.Text = (ItemBaffsListScript.ItemBaffs[29][0] + Effects.EffectBaffs[26][0]).ToString();
            main.character_longrangeattack_textblock.Text = (ItemBaffsListScript.ItemBaffs[30][0] + Effects.EffectBaffs[26][0]).ToString();

            ToolTip toolTipA = new ToolTip{ Content = $"Предметы:{ItemBaffsListScript.ItemBaffs[25][0]} Эффекты{Effects.EffectBaffs[26][0]} Остальное:{CharacteristicTable.OtherBaff(25)}" };
            main.character_rollattackname_textblock.ToolTip = toolTipA;
            ToolTip toolTipB = new ToolTip { Content = $"Предметы:{ItemBaffsListScript.ItemBaffs[26][0]} Эффекты:{Effects.EffectBaffs[26][0] + Effects.EffectBaffs[28][0]} Остальное:{CharacteristicTable.OtherBaff(26)}" };
            main.character_blockname_textblock.ToolTip = toolTipB;
            ToolTip toolTipD = new ToolTip { Content = $"Предметы:{ItemBaffsListScript.ItemBaffs[27][0]} Эффекты:{Effects.EffectBaffs[26][0]} Остальное:{CharacteristicTable.OtherBaff(27)}" };
            main.character_dodgename_textblock.ToolTip = toolTipD;
            ToolTip toolTipC = new ToolTip { Content = $"Предметы:{ItemBaffsListScript.ItemBaffs[28][0]} Эффекты:{Effects.EffectBaffs[26][0]} Остальное:{CharacteristicTable.OtherBaff(28)}" };
            main.character_counteractionname_textblock.ToolTip = toolTipC;
            ToolTip toolTipF = new ToolTip { Content = $"Предметы:{ItemBaffsListScript.ItemBaffs[29][0]} Эффекты:{Effects.EffectBaffs[26][0]}" };
            main.character_fistattackname_textblock.ToolTip = toolTipF;
            ToolTip toolTipL = new ToolTip { Content = $"Предметы:{ItemBaffsListScript.ItemBaffs[30][0]} Эффекты:{Effects.EffectBaffs[26][0]}" };
            main.character_longrangeattackname_textblock.ToolTip = toolTipL;

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
            double lengthStick = Race.SelectedClassData.StandartStickLength;
            double multiplyLength = Race.SelectedClassData.MultiplyStickLength;

            int Agility = CharacteristicTable.Buffed(CharacteristicTable.StatName.Agility);
            double addStick = 0;
            if ((Agility - 10) >= 5)
                addStick = (int)((Agility - 10) / 5) * 0.5;
            lengthStick *= multiplyLength;

            lengthStick += addStick + ItemBaffsListScript.ItemBaffs[34][0];
            double multiplyEffect = Effects.EffectBaffs[29][0] * 0.01;
            if (multiplyEffect != 0)
                lengthStick *= multiplyEffect;
            Main.Instance.Movesticks_textblock.Text = lengthStick.ToString();
        }
    }
}
