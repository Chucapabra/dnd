using DNDHelper.Modules.Config;
using DNDHelper.Modules.Сharacteristics;
using DNDHelper.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace DNDHelper.Modules.Character
{
    public class TreeSkills
    {
        Main main = Main.Instance;

        int points = 0;

        public static List<Stat> Stats = new List<Stat>();

        public static List<string> Skills = new List<string>();
        public static int AddKD = 0;

        public ObservableCollection<TreeGrid> treeGrids = new() {
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 },
            new TreeGrid { TreeName = "", TreeLevel = 0 }
            };

        public TreeSkills()
        {
            main.DataGridTreeDevelopment.ItemsSource = treeGrids;
            ClearStats();    
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

            if (points > 0 && treeGrids[selectedIndex].TreeLevel < 5)
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
            ClearStats();
            Skills.Clear();
            AddKD = 0;

            int i = 0;
            if (PlayerClass.SelectedClassData.ClassTrees.Count > 0)
            foreach (var item in PlayerClass.SelectedClassData.ClassTrees[0])
            {
                    for (int j = 0; j < treeGrids[i].TreeLevel; j++)
                        foreach (var stage in PlayerClass.SelectedClassData.ClassTrees[0][item.Key][0][j][0])
                            FindAddStats(stage.Key, stage.Value);
                i++;
            }

            Main.Characteristics.UpdateAllCharacterisitc();
            Character.Skills.ReloadDataGridSkills();
        }

        private void FindAddStats(string key, object value)
        {
            int index = Array.IndexOf(CharacteristicTable.StatNameRus, key.ToLower());
            if (index != -1)
            {
                if (value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    var array = element.Deserialize<int[]>();
                    if (array != null && array.Length >= 2)
                    {
                        Stats[index].Value += array[0];
                        Stats[index].Roll += array[1];
                        return;
                    }
                }

            }

            switch (key.ToLower())
            {
                case "способности":
                    if (value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                    {
                        var array = element.Deserialize<string[]>();
                        if (array != null)
                            foreach (var item in array)
                                Skills.Add(item);
                    }
                    break;
                case "допкд":
                    if (value is JsonElement elementInt)
                        if (elementInt.ValueKind == JsonValueKind.Number && elementInt.TryGetInt32(out int number))
                            AddKD += number;
                    break;
                case "множительуронамагии":
                    break;
                case "дебаф_к_пулям":
                    break;
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
            
            UpdateTreeLevel();
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
