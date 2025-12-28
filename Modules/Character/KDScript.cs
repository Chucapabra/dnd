using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNDHelper.Modules.Config;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Settings;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using static DNDHelper.Modules.Inventory.InventoryLoot;

namespace DNDHelper.Modules.Character
{
    class KDScript
    {
        public static int MentalKD = 0;
        public static int BodyKD = 0;
        public static int ArmorKD = 0;
        public static int HeadKD = 0;
        public static int DebaffKD = 0;

        public static void UpdateAllKD()
        {
            CountMentalKD();
        }
        public static void CountMentalKD()
        {
            int intellectBaff = (int)((CharacteristicTable.Buffed(CharacteristicTable.StatName.Intellect) * 2) * GlobalMultiply.data.MultiplyMentKD);

            MentalKD = intellectBaff + ItemBaffsListScript.ItemBaffs[38][0];

            Main.Instance.MentalKD_TextBlock.Text = MentalKD.ToString();
        }

        public static void CountKD()
        {
            BodyKD = (int)(CharacteristicTable.Buffed(CharacteristicTable.StatName.Body) * GlobalMultiply.data.GlobalMultiply) + Race.SelectedClassData.AddKD;


            int[] itemsKD = CountItemsKD();
            ArmorKD = itemsKD[0];
            HeadKD = itemsKD[1] + BodyKD;
            DebaffKD = Effects.EffectBaffs[30][0];

            int generalKD = BodyKD + ArmorKD + DebaffKD;
            Main.Instance.KD_TextBlock.Text = generalKD.ToString();
            Main.Instance.HeadKD_TextBlock.Text = HeadKD.ToString();
        }

        private static int[] CountItemsKD()
        {
            int[] KD = new int[2];

            foreach (var item in InventoryItems)
            {
                if (item.Equipped)
                {
                    if (item.CountKD)
                        KD[0] += item.WholeKD;
                    if (item.CountKDHelmet)
                        KD[1] += item.WholeKD;
                }
            }

            return KD;
        }
    }
}
