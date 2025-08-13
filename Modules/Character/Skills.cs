using DNDHelper.Modules.Config;
using DNDHelper.Modules.Settings;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DNDHelper.Modules.Character
{
    public class Skills
    {
        Main main = Main.Instance;

        public static ObservableCollection<Skill> ListSkills { get; set; } = new();
        public static ObservableCollection<Skill> CustomSkills => DataManager.DataSave.CustomSkills;

        public Skills() 
        {
            main.DataGridChartherCharacterAbilities.ItemsSource = ListSkills;
            main.DataGridChartherCharacterAbilities.BeginningEdit += DataGridChartherCharacterAbilities_BeginningEdit;
            main.DataGridChartherCharacterAbilities.PreviewMouseLeftButtonDown += DataGridChartherCharacterAbilities_PreviewMouseLeftButtonDown; ;
            main.DataGridChartherCharacterAbilities.SelectionChanged += DataGridChartherCharacterAbilities_SelectionChanged;
            main.DataGridChartherCharacterAbilities.ContextMenuOpening += DataGridChartherCharacterAbilities_ContextMenuOpening;
            main.Ability_TextBox.TextChanged += Ability_TextBox_TextChanged;
            main.Ability_TextBox.PreviewTextInput += Ability_TextBox_PreviewTextInput;
            main.AddMenuSkill.Click += AddMenuSkill_Click;
            main.DeleteMenuSkill.Click += DeleteMenuSkill_Click;
        }

        private void Ability_TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if(e.Text.Contains('|'))
                e.Handled = true;
        }

        private void Ability_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int selectedIndex = main.DataGridChartherCharacterAbilities.SelectedIndex;
            if (selectedIndex != -1 && ListSkills[selectedIndex].Origin == "Своя")
            {
                ListSkills[selectedIndex].Description = main.Ability_TextBox.Text;
                SaveCustomSkills();
            }
        }

        private void DeleteMenuSkill_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = main.DataGridChartherCharacterAbilities.SelectedIndex;
            if (selectedIndex != -1 && ListSkills[selectedIndex].Origin == "Своя")
            {
                ListSkills.RemoveAt(selectedIndex);
                SaveCustomSkills();
            }
        }

        private void AddMenuSkill_Click(object sender, RoutedEventArgs e)
        {
            ListSkills.Add(new());
            SaveCustomSkills();
        }

        private void DataGridChartherCharacterAbilities_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            int selectedIndex = main.DataGridChartherCharacterAbilities.SelectedIndex;
        }

        private void DataGridChartherCharacterAbilities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            main.Ability_TextBox.IsReadOnly = true;
            int selectedIndex = main.DataGridChartherCharacterAbilities.SelectedIndex;
            if (selectedIndex != -1)
            {
                main.Ability_TextBox.Text = ListSkills[selectedIndex].Description;
                if(ListSkills[selectedIndex].Origin == "Своя")
                    main.Ability_TextBox.IsReadOnly = false;
            }
            else          
                main.Ability_TextBox.Text = "Выбирите способность";
        }

        private void DataGridChartherCharacterAbilities_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cell = FindParent<DataGridCell>(e.OriginalSource as DependencyObject);

            if (cell != null && !cell.IsEditing)
            {
                if (e.ClickCount == 1)
                {
                    main.DataGridChartherCharacterAbilities.CommitEdit();
                    main.DataGridChartherCharacterAbilities.SelectedItem = cell.DataContext;
                    e.Handled = true;
                }
            }
            else if (cell == null) 
            {
                main.DataGridChartherCharacterAbilities.CommitEdit();
                main.DataGridChartherCharacterAbilities.SelectedIndex = -1;
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (child != null && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }

        private void DataGridChartherCharacterAbilities_BeginningEdit(object? sender, System.Windows.Controls.DataGridBeginningEditEventArgs e)
        {
            var skill = e.Row.DataContext as Skill;
            if(e.Column.DisplayIndex == 1 || skill.Origin != "Своя")
                e.Cancel = true;
        }

        private static void SaveCustomSkills()
        {
            CustomSkills.Clear();
            foreach (var skill in ListSkills)
                if (skill.Origin == "Своя")
                    CustomSkills.Add(skill);
        }


        public static void ReloadDataGridSkills()
        {
            ListSkills.Clear();
            if (Main.Instance.character_race_combobox.SelectedIndex != -1)
            {
                var RaceSkills = Race.SelectedClassData.Skills;
                var NameRace = Main.Instance.character_race_combobox.SelectedValue.ToString();
           
                foreach (var skill in RaceSkills)
                {
                    string[] sk = skill.Split('|');
                    ListSkills.Add(new Skill { Name = sk[0], Origin = NameRace, Description = sk[1] });
                }
            }

            if (Main.Instance.character_class_combobox.SelectedIndex != -1)
            {
                var ClassSkills = PlayerClass.SelectedClassData.Skills;
                var ClassTreesSkills = TreeSkills.Skills;
                ClassTreesSkills.Reverse();
                var NameClass = Main.Instance.character_class_combobox.SelectedValue.ToString();
                // объединение скиллов
                foreach (var skill in ClassSkills)
                    ClassTreesSkills.Add(skill);
                foreach (var skill in ClassTreesSkills)
                {
                    int index = ListSkills.ToList().FindIndex(x => { return x.Name == skill.Split('|')[0]; });
                    if (index == -1)
                    {
                        string[] sk = skill.Split('|');
                        ListSkills.Add(new Skill { Name = sk[0], Origin = NameClass, Description = sk[1] });
                    }
                }

            }





            foreach (var skill in CustomSkills)
            {
                ListSkills.Add(new Skill { Name = skill.Name, Origin = skill.Origin, Description = skill.Description });
            }
        }

        public class Skill
        {
            public string Name { get; set; } = "Способность";

            public string Origin { get; set; } = "Своя";

            public string Description { get; set; } = "Описание";
        }
    }
}
