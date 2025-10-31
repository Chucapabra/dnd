using DNDHelper.Modules.Character;
using DNDHelper.Modules.Config;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DNDHelper.Modules.Settings
{
    class GlobalMultiply
    {
        public static globalMultiplyClass data = new globalMultiplyClass();

        public static void Update()
        {
            string pathFile = Main.PathMain + $"Cache/{DataManager.DataSave.SelectedRepository}/MultiplyGlobal.json";
            if (File.Exists(pathFile))
            {
                Debug.WriteLine("sds");
                string json = File.ReadAllText(pathFile);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                try
                {
                    data = JsonSerializer.Deserialize<globalMultiplyClass>(json, options);
                    Debug.WriteLine(data.GlobalMultiply);
                }
                catch
                {
                    data = new();
                }
            }
            else
                data = new();
            Health.HealthUpdate();
        }


        public class globalMultiplyClass
        {
            public double GlobalMultiply { get; set; } = 1;

            public double MultiplyMentKD { get; set; } = 1;
        }
    }
}
