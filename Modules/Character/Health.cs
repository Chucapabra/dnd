using DNDHelper.Modules.Config;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.Settings;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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


        public static int DamageMagicShield
        {
            get => DataManager.DataSave.DamageMagicShield;
            set
            {
                DataManager.DataSave.DamageMagicShield = value;
            }
        }

        public static int MaxMagicShield
        {
            get => int.Parse(DataManager.DataSave.MaxMagicShield);
            set
            {
                DataManager.DataSave.MaxMagicShield = value.ToString();
            }
        }

        public Health() 
        {
            HealthUpdate();
            main.CurrentHealth_textblock.MouseLeftButtonDown += CurrentHealth_textblock_MouseLeftButtonDown;
            main.CurrentHealth_textbox.KeyDown += CurrentHealth_textblock_KeyDown;
            main.CurrentHealth_textbox.PreviewTextInput += CurrentHealth_textbox_PreviewTextInput;
            main.ResetHealth.Click += ResetHealth_Click;
        }

        private void ResetHealth_Click(object sender, RoutedEventArgs e)
        {
            Damage = 0;
            DamageMagicShield = 0;
            HealthUpdate();
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
            MaxHealth = (int)Math.Floor((body * 10 * GlobalMultiply.data.GlobalMultiply) * PlayerClass.SelectedClassData.MultiplyHealth);
            if (DataManager.DataSave.SelectedRepository != SetRepository.repositories[1].Name)
            {
                int addАfromTheLevel = (int)Math.Floor(MaxHealth * 0.1) * DataManager.DataSave.Level;
                MaxHealth += addАfromTheLevel;
            }
            MaxHealth += ItemBaffsListScript.ItemBaffs[37][0];
            int currentHealth = MaxHealth - Damage;
            CountCriticalRoll(currentHealth, MaxHealth);

            string currentMagicShield = "";
            string maxMagicShield = "";
            if(Main.Instance.shield_health_textbox.Text != "" && MaxMagicShield != 0 && DataManager.DataSave.SelectedTypeMagicShield != -1)
            {
                maxMagicShield = $" {MaxMagicShield}"; ;
                currentMagicShield = $" {MaxMagicShield - DamageMagicShield}";

                Main.Instance.CurrentShieldHealth_textblock.Text = currentMagicShield;
                Main.Instance.MaxShieldHealth_textblock.Text = maxMagicShield;
                
                Brush colorFore = Brushes.White;
                switch (DataManager.DataSave.SelectedTypeMagicShield)
                {
                    case 0:
                        colorFore = Brushes.LightBlue;
                        break;
                    case 1:
                        colorFore = Brushes.Orange;
                        break;
                    case 2:
                        colorFore = Brushes.ForestGreen;
                        break;
                }

                Main.Instance.CurrentShieldHealth_textblock.Foreground = colorFore;
                Main.Instance.MaxShieldHealth_textblock.Foreground = colorFore;
            }
            else
            {
                Main.Instance.CurrentShieldHealth_textblock.Text = "";
                Main.Instance.MaxShieldHealth_textblock.Text = "";
            }

            Main.Instance.CurrentHealth_textblock.Text = currentHealth.ToString();        
            Main.Instance.MaxHealth_textblock.Text = MaxHealth.ToString();  
        }
    }
}
