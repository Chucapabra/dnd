using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace DND
{
    internal class CheckUpdate
    {
        public static async Task Check()
        {
            string[] path = Assembly.GetExecutingAssembly().Location.Split('\\');
            string PathGlobal = string.Join("\\", path, 0, path.Count() - 2);
            string PathUpdater = PathGlobal + "\\Updater\\UpdaterDND.exe";

            string PathVersion = PathGlobal + "\\Version.txt";
            bool InvalidPathVersion = false;
            foreach (var fileName in Directory.GetFiles(PathGlobal))
            {
                if (fileName == PathVersion)
                {
                    InvalidPathVersion = true;
                }
            }
            if (!InvalidPathVersion)
            {
                var MyFile = File.Create(PathVersion);
                MyFile.Close();
            }
            string Version = File.ReadAllText(PathVersion).Replace("\r\n", "");
            string GitUrl = "https://api.github.com/repos/Chucapabra/dnd/releases/latest";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "AutoUpdater");
                    string NewVer = await client.GetStringAsync(GitUrl);

                    using JsonDocument document = JsonDocument.Parse(NewVer);
                    JsonElement root = document.RootElement;
                    string NewVersion = root.GetProperty("tag_name").GetString();
                    if (NewVersion != Version)
                    {
                        try
                        {
                            if (MessageBox.Show("Доступно новое обновление. Хотите обновиться?", "Обновление",
                              MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                Process.Start(PathUpdater);
                                Environment.Exit(0);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка запуска приложения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (HttpRequestException e)
                {

                }
            }

        }

    }
}
