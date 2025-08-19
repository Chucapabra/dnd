using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using DarkThem = DNDHelper.Modules.Settings.Settings;
using DNDHelper.Modules.Inventory;
using DNDHelper.Modules.MagicSpells;
using DNDHelper.Modules.Settings;

namespace DNDHelper.Windows
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public static Settings Instance { get; set; }

        public Settings()
        {
            InitializeComponent();
            Instance = this;

            SetRepository setRepository = new SetRepository();

            Loaded += Settings_Loaded;
        }

        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            SetRepository.UpdateListBox();
        }

        private void ComboBoxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null)
            {
                int selectionIndex = comboBox.SelectedIndex;
                object selectionItem = ComboBoxTheme.SelectedItem;

                var paletteHelper = new PaletteHelper();
                Theme theme = paletteHelper.GetTheme();
                switch (selectionIndex)
                {
                    case 0:
                        DarkThem.IsDarkTheme = false;
                        DarkThem.SelectedTheme = DarkThem.LightTheme;
                        theme.SetBaseTheme(BaseTheme.Light);
                        break;
                    case 1:
                        DarkThem.IsDarkTheme = true;
                        DarkThem.SelectedTheme = DarkThem.DarkTheme;
                        theme.SetBaseTheme(BaseTheme.Dark);
                        break;
                }
                Main.Instance.SetTheme(DarkThem.SelectedTheme[0], DarkThem.SelectedTheme[1]);
                paletteHelper.SetTheme(theme);
                MagicSpells.RepositoryLoad();
                SetRepository.UpdateListBox();

                // Обновление шрифта в инвенторе
                foreach (var item in InventoryLoot.InventoryItems)
                {
                    item.CountWeight = item.CountWeight;
                    item.CountKD = item.CountKD;
                }

            }
        }


        private void AddProfile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
