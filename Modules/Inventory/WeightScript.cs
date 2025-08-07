using DNDHelper.Modules.Config;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using static DNDHelper.Modules.Inventory.InventoryLoot;

namespace DNDHelper.Modules.Inventory
{
    internal class WeightScript
    {
        Main main = Main.Instance;
        public static int UsedItemWeight = 0;

        public WeightScript()
        {
            main.backpack_plus_tb.TextChanged += Backpack_plus_tb_TextChanged;
        }

        private void Backpack_plus_tb_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CountWeightCharacter();
        }

        public static void CountWeightItems()
        {
            int weightItem = 0;
            int weightArmor = 0;
            foreach (var item in InventoryItems)
            {
                if (item.CountWeight == true)
                    if (item.KD > 0)
                        weightArmor += item.Weight;
                    else
                        weightItem += item.Weight;
            }
            if (!Backpack.IsDivideArmorWeight)
                if (weightItem > 0)
                    UsedItemWeight = (int)(weightItem / Backpack.DivideItemWeight) + weightArmor;
                else
                    UsedItemWeight = weightItem + weightArmor;
            else
                UsedItemWeight = (int)(weightItem + weightArmor) / Backpack.DivideItemWeight;
            CountWeightCharacter();
        }

        public static void CountWeightCharacter()
        {
            int addWeight = 0;
            if (Main.Instance.backpack_plus_tb.Text != "")
                addWeight = int.Parse(Main.Instance.backpack_plus_tb.Text);
            int MaxWeight = (int)MathF.Round(((CharacteristicTable.Buffed(0) * 30) + Race.SelectedClassData.AddWeight + Backpack.AddWeight) * (float)Race.SelectedClassData.MultiplyWeight + addWeight);
            int remainedWeight = MaxWeight - UsedItemWeight;
            Main.Instance.сharacter_weight_textblock.Text = $"Вес: {remainedWeight}/{MaxWeight}";
        }
    }
}
