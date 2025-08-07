using DNDHelper.Modules.Config;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDHelper.Modules.Character
{
    internal class Health
    {
        public static int Damage = 0;

        public Health() 
        {
            HealthUpdate();
        }

        public static void CountCriticalRoll(int currentHealth, int maxHealth)
        {

        } 

        public static void HealthUpdate()
        {
            int body = CharacteristicTable.Buffed(CharacteristicTable.StatName.Body);
            int maxHealth = (int)Math.Floor(((body * 10) * PlayerClass.SelectedClassData.MultiplyHealth) + ItemBaffsListScript.ItemBaffs[32][0]);
            int currentHealth = maxHealth - Damage;
            CountCriticalRoll(currentHealth, maxHealth);

            Main.Instance.CurrentHealth_textblock.Text = currentHealth.ToString();
            Main.Instance.MaxHealth_textblock.Text = maxHealth.ToString();
        }
    }
}
