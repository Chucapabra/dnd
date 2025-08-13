using DNDHelper.Modules.Settings;
using DNDHelper.Windows;

namespace DNDHelper.Modules.Character
{

    class Level
    {
        public Level()
        {
            Main.Instance.pluslevel.Click += Pluslevel_Click;
            Main.Instance.minuslevel.Click += Minuslevel_Click;
            Main.Instance.сharacter_level_textblcok.Text = LevelBaffs.Level.ToString();
        }

        private void Minuslevel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (LevelBaffs.Level > 1)
            {
                LevelBaffs.Level--;
                SetLevel();
            }
        }

        private void Pluslevel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (LevelBaffs.Level < 20)
            {
                LevelBaffs.Level++;
                SetLevel();
            }
        }

        public static void SetLevel()
        {
            LevelBaffs.AddPoints = 0;
            LevelBaffs.MaxLevelTree = 2;
            LevelBaffs.PointsTree = 0;

            switch (LevelBaffs.Level)
            {
                case 1:
                    break;
                case 2:
                    LevelBaffs.AddPoints = 1;
                    break;
                case 3:
                    LevelBaffs.AddPoints = 1;
                    LevelBaffs.PointsTree = 1;
                    break;
                case 4:
                    LevelBaffs.AddPoints = 2;
                    LevelBaffs.PointsTree = 1;
                    break;
                case 5:
                    LevelBaffs.AddPoints = 2;
                    LevelBaffs.PointsTree = 2;
                    break;
                case 6:
                    LevelBaffs.AddPoints = 3;
                    LevelBaffs.PointsTree = 2;
                    break;
                case 7:
                    LevelBaffs.AddPoints = 3;
                    LevelBaffs.PointsTree = 3;
                    LevelBaffs.MaxLevelTree = 3;
                    break;
                case 8:
                    LevelBaffs.AddPoints = 4;
                    LevelBaffs.PointsTree = 3;
                    LevelBaffs.MaxLevelTree = 3;
                    break;
                case 9:
                    LevelBaffs.AddPoints = 5;
                    LevelBaffs.PointsTree = 3;
                    LevelBaffs.MaxLevelTree = 3;
                    break;
                case 10:
                    LevelBaffs.AddPoints = 5;
                    LevelBaffs.PointsTree = 4;
                    LevelBaffs.MaxLevelTree = 3;
                    break;
                case 11:
                    LevelBaffs.AddPoints = 6;
                    LevelBaffs.PointsTree = 4;
                    LevelBaffs.MaxLevelTree = 3;
                    break;
                case 12:
                    LevelBaffs.AddPoints = 8;
                    LevelBaffs.PointsTree = 4;
                    LevelBaffs.MaxLevelTree = 3;
                    break;
                case 13:
                    LevelBaffs.AddPoints = 8;
                    LevelBaffs.PointsTree = 5;
                    LevelBaffs.MaxLevelTree = 4;
                    break;
                case 14:
                    LevelBaffs.AddPoints = 10;
                    LevelBaffs.PointsTree = 5;
                    LevelBaffs.MaxLevelTree = 4;
                    break;
                case 15:
                    LevelBaffs.AddPoints = 10;
                    LevelBaffs.PointsTree = 6;
                    LevelBaffs.MaxLevelTree = 4;
                    break;
                case 16:
                    LevelBaffs.AddPoints = 12;
                    LevelBaffs.PointsTree = 6;
                    LevelBaffs.MaxLevelTree = 4;
                    break;
                case 17:
                    LevelBaffs.AddPoints = 14;
                    LevelBaffs.PointsTree = 6;
                    LevelBaffs.MaxLevelTree = 4;
                    break;
                case 18:
                    LevelBaffs.AddPoints = 14;
                    LevelBaffs.PointsTree = 7;
                    LevelBaffs.MaxLevelTree = 5;
                    break;
                case 19:
                    LevelBaffs.AddPoints = 17;
                    LevelBaffs.PointsTree = 7;
                    LevelBaffs.MaxLevelTree = 5;
                    break;
                case 20:
                    LevelBaffs.AddPoints = 17;
                    LevelBaffs.PointsTree = 8;
                    LevelBaffs.MaxLevelTree = 5;
                    break;
            }



            Main.Characteristics.FindAVariableCharacteristic(28);
            Main.Instance.сharacter_level_textblcok.Text = LevelBaffs.Level.ToString();
            Main.TreeSkillsScript.UpdateTreeLevel();
        }
    }


    public static class LevelBaffs
    {
        public static int Level
        {
            get => DataManager.DataSave.Level;
            set => DataManager.DataSave.Level = value;
        }

        public static int PointsTree = 0;

        public static int MaxLevelTree = 2;

        public static int AddPoints = 0;
    }
}
