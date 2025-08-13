using DNDHelper.Modules.Config;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Settings;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DNDHelper.Modules.Character
{
    internal class Health
    {
        public static int Damage
        {
            get => DataManager.DataSave.Damage;
            set
            {
                DataManager.DataSave.Damage = value;
            }
        }
        public static int MaxHealth = 0;
        Main main = Main.Instance;
        public Health() 
        {
            HealthUpdate();
            main.CurrentHealth_textblock.MouseLeftButtonDown += CurrentHealth_textblock_MouseLeftButtonDown;
            main.CurrentHealth_textbox.KeyDown += CurrentHealth_textblock_KeyDown;
            main.CurrentHealth_textbox.PreviewTextInput += CurrentHealth_textbox_PreviewTextInput;
        }

        private void CurrentHealth_textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextboxProcessing.WholeNumbersOnly(main.CurrentHealth_textbox, e);
        }

        private void CurrentHealth_textblock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            main.CurrentHealth_textblock.Visibility = Visibility.Collapsed;
            main.CurrentHealth_textbox.Visibility = Visibility.Visible;
            main.CurrentHealth_textbox.Text = main.CurrentHealth_textblock.Text;
            main.CurrentHealth_textbox.Focus();
        }

        private void CurrentHealth_textblock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string text = main.CurrentHealth_textbox.Text;
                main.CurrentHealth_textblock.Text = text;
                if (text.Length == 0 || main.CurrentHealth_textblock.Text == "-")
                {
                    main.CurrentHealth_textblock.Text = "0";
                    main.CurrentHealth_textbox.Text = "0";
                }
                main.CurrentHealth_textblock.Visibility = Visibility.Visible;
                main.CurrentHealth_textbox.Visibility = Visibility.Collapsed;

                int currectHealth = Convert.ToInt32(main.CurrentHealth_textblock.Text);
                Damage = MaxHealth - currectHealth;
                HealthUpdate();
            }
        }



        public static void CountCriticalRoll(int currentHealth, int maxHealth)
        {
            Main main = Main.Instance;
            if (currentHealth < 0 && maxHealth != 0)
            {
                main.СriticalRoll_Text_textblock.Visibility = Visibility.Visible;
                main.СriticalRoll_GreaterOrEqual_textblock.Visibility = Visibility.Visible;
                main.СriticalRoll_Number_textblock.Visibility = Visibility.Visible;

                main.СriticalRoll_Number_textblock.Text = Convert.ToString((Math.Abs(currentHealth) * 100) / maxHealth / 5 + 10);
            }
            else
            {
                main.СriticalRoll_Text_textblock.Visibility = Visibility.Hidden;
                main.СriticalRoll_GreaterOrEqual_textblock.Visibility = Visibility.Hidden;
                main.СriticalRoll_Number_textblock.Visibility = Visibility.Hidden;
            }
        } 

        public static void HealthUpdate()
        {
            int body = CharacteristicTable.Buffed(CharacteristicTable.StatName.Body);
            MaxHealth = (int)Math.Floor(((body * 10) * PlayerClass.SelectedClassData.MultiplyHealth) + ItemBaffsListScript.ItemBaffs[32][0]);
            int currentHealth = MaxHealth - Damage;
            CountCriticalRoll(currentHealth, MaxHealth);

            Main.Instance.CurrentHealth_textblock.Text = currentHealth.ToString();
            Main.Instance.MaxHealth_textblock.Text = MaxHealth.ToString();
        }
    }
}
