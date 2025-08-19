using DNDHelper.Modules.Config;
using DNDHelper.Modules.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DNDHelper.Modules.Settings
{
    internal class SetRepository
    {
        Windows.Settings settings = Windows.Settings.Instance;
        public static string SelectedRepository
        {
            get => DataManager.DataSave.SelectedRepository;
            set
            {
                DataManager.DataSave.SelectedRepository = value;
            }
        }

        List<Repository> repositories = new()
        {
            new Repository { Name = "Санекхуек", Link = "https://raw.githubusercontent.com/Chucapabra/dnd/refs/heads/Settings"  },
            new Repository { Name = "Гунер", Link = "https://raw.githubusercontent.com/GunterSuperPenguin/dnd-configs-gunter/refs/heads/main"  },
            new Repository { Name = "Раб", Link = "https://raw.githubusercontent.com/Chucapabra/dnd/refs/heads/wh40k"  }
        };

        public SetRepository()
        {
            settings.SelectedRepository.Click += SelectedRepository_Click;

            foreach (Repository repository in repositories)
                settings.ListProfiles.Items.Add(repository.Name);



        }

        public static void UpdateListBox()
        {
            for (int i = 0; i < Windows.Settings.Instance.ListProfiles.Items.Count; i++)
            {
                var container = Windows.Settings.Instance.ListProfiles.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (container != null)
                    if (container.Content == SelectedRepository)
                    {
                        container.Foreground = Brushes.Green;
                    }
                    else if (container != null)
                        container.Foreground = new SolidColorBrush(Settings.SelectedTheme[1]);
            }
        }

        private void SelectedRepository_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (settings.ListProfiles.SelectedIndex != -1)
            {
                SelectedRepository = repositories[settings.ListProfiles.SelectedIndex].Name;
                for (int i = 0; i < settings.ListProfiles.Items.Count; i++)
                {
                    var container = settings.ListProfiles.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (container != null)
                        if (container.Content == SelectedRepository)
                        {
                            container.Foreground = Brushes.Green;
                            FileСonnection();
                        }
                        else if (container != null)
                            container.Foreground = new SolidColorBrush(Settings.SelectedTheme[1]);
                }

            }
        }



        public static string FileSpells = "";
        public static string FileRace = "";
        public static string FileClass = "";
        public static string FileTypeDamage = "";
        public static string FileMultiplyGlobal = "";

        public async Task FileСonnection()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            string Repos = repositories[settings.ListProfiles.SelectedIndex].Link;


            string castFileUrl = $"{Repos}/Casts";
            string raceFileUrl = $"{Repos}/Race.json";
            string classFileUrl = $"{Repos}/Class.json";
            string typeDamageFileUrl = $"{Repos}/TypesDamage.json";
            string globalMultiplyUrl = $"{Repos}/GlobalMultiply.json";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    async Task<string> SafeGetAsync(string url)
                    {
                        try
                        {
                            return await client.GetStringAsync(url);
                        }
                        catch (HttpRequestException)
                        {
                            return "";
                        }
                    }

                    var tasks = new List<Task<string>>
                    {
    SafeGetAsync(castFileUrl),
    SafeGetAsync(raceFileUrl),
    SafeGetAsync(classFileUrl),
    SafeGetAsync(globalMultiplyUrl),
    SafeGetAsync(typeDamageFileUrl)
                    };

                    await Task.WhenAll(tasks);

                    FileSpells = tasks[0].Result;
                    FileRace = tasks[1].Result;
                    FileClass = tasks[2].Result;
                    FileMultiplyGlobal = tasks[3].Result;
                    FileTypeDamage = tasks[4].Result;
                    CheckCacheFiles();
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Ошибка соединения: {e}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        public static void CheckCacheFiles()
        {
            string path = $"Cache/{SelectedRepository}/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var spell_ = CheckCacheFile("FileSpells.txt", FileSpells, path);
            var race_ = CheckCacheFile("FileRace.json", FileRace, path);
            var class_ = CheckCacheFile("FileClass.json", FileClass, path);
            var typeDamage_ = CheckCacheFile("FileTypeDamage.json", FileTypeDamage, path);
            var multiplyGlobal_ = CheckCacheFile("FileMultiplyGlobal.json", FileMultiplyGlobal, path);
            if (spell_ || race_ || class_ || typeDamage_ || multiplyGlobal_) ;
        }

        public static bool CheckCacheFile(string name, string content, string path)
        {
            string pathFile = Path.Combine(path, name);
            if (content.Length > 3)
            {
                if (!File.Exists(pathFile))
                {
                    var file = File.Create(pathFile);
                    file.Close();
                }

                if (File.ReadAllText(pathFile) != content)
                {
                    File.WriteAllText(pathFile, content);
                    return true;
                }
            }
            else if (File.Exists(pathFile))
                File.Delete(pathFile);

            return false;
        }
    }

    public class Repository
    {
        public string Name { get; set; }

        public string Link { get; set; }
    }
}
