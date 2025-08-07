using System.Windows.Media;


namespace DNDHelper.Modules.Settings
{


    public static class Settings
    {
        public static bool IsDarkTheme = false;
        public static Color[] SelectedTheme = { Color.FromRgb(50, 50, 50), Color.FromRgb(240, 240, 240) };
        public static Color[] DarkTheme = { Color.FromRgb(50, 50, 50), Color.FromRgb(240, 240, 240) };
        public static Color[] LightTheme = { Color.FromRgb(240, 240, 240), Color.FromRgb(50, 50, 50) };
    }
}
