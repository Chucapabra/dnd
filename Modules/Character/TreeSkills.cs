using DNDHelper.Modules.Config;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DNDHelper.Modules.Character
{
    public class TreeSkills
    {
        Main main = Main.Instance;

        int points = 0;

        public static List<Stat> Stats = new List<Stat>();

        public ObservableCollection<TreeGrid> treeGrids = new() {
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 }
            };

        public TreeSkills()
        {
            main.DataGridTreeDevelopment.ItemsSource = treeGrids;
            ClearStats();
            UpdateTreeLevel();           
        }

        private void ClearStats()
        {
            Stats.Clear();
            for (int i = 0; i < 26; i++)
            {
                Stats.Add(new Stat { Value = 0, Roll = 0 });
            }
        }

        public void AddTreeLevel()
        {
            int selectedIndex = main.DataGridTreeDevelopment.SelectedIndex;

            if (points > 0)
            {
                treeGrids[selectedIndex].TreeLevel++;
                UpdateTreeLevel();
            }
        }

        public void SubstractTreeLevel()
        {
            int selectedIndex = main.DataGridTreeDevelopment.SelectedIndex;

            if (treeGrids[selectedIndex].TreeLevel > 0)
            {
                treeGrids[selectedIndex].TreeLevel--;
                UpdateTreeLevel();
            }
        }

        public void UpdateTreeLevel()
        {
            int usedPoints = 0;
            foreach (TreeGrid treeGrid in treeGrids)
                usedPoints += treeGrid.TreeLevel;

            points = LevelBaffs.PointsTree - usedPoints;
            UpdateDisplayPoints();

            UpdateAddStats();

        }
        
        public void UpdateDisplayPoints()
        {
            if (main.EditMode_button.IsChecked)
                main.DataGridTreeDevelopment.Columns[0].Header = "Название Очков: " + points;
        }

        public void UpdateAddStats()
        {
            int i = 0;
            if (PlayerClass.SelectedClassData.ClassTrees.Count > 0)
            foreach (var item in PlayerClass.SelectedClassData.ClassTrees[0])
            {
                    for (int j = 0; j <= treeGrids[i].TreeLevel; j++)
                        foreach (var stage in PlayerClass.SelectedClassData.ClassTrees[0][item.Key][0][j][0].StandartStats)
                            Debug.WriteLine(stage.Count);
                i++;
            }
        }

        public void SetClassTree()
        {
            int[] levels = new int[] { treeGrids[0].TreeLevel, treeGrids[1].TreeLevel, treeGrids[2].TreeLevel }; 

            treeGrids.Clear();
            int i = 0;
            foreach (var item in PlayerClass.SelectedClassData.ClassTrees[0])
            {
                treeGrids.Add(new TreeGrid() { TreeName = item.Key, TreeLevel = levels[i] } );
                i++;
            }
            
        }



        public class TreeGrid : INotifyPropertyChanged
        {
            public string TreeName { get; set; }


            private int _treeLevel = 0;
            public int TreeLevel
            {
                get => _treeLevel;
                set
                {
                    _treeLevel = value;
                    OnPropertyChanged();
                }
            }


            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
