using DNDHelper.Modules.Settings;
using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DNDHelper.Modules.Config
{
	internal class ManagerUrls
	{
		public static Dictionary<string, string> _urlData;
		public static void LoadUrls()
		{
			Windows.Main main = Windows.Main.Instance;
			string pathFile = Main.PathMain + $"Cache/{DataManager.DataSave.SelectedRepository}/urls.json";
			if (File.Exists(pathFile))
			{
				
				string json = File.ReadAllText(pathFile);
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
					WriteIndented = true
				};
				try
				{
					main.ListBoxUrls.Items.Clear();
					_urlData = JsonSerializer.Deserialize<Dictionary<string, string>>(json, options);
					if (_urlData != null)
					{
						foreach (var item in _urlData)
						{
							main.ListBoxUrls.Items.Add(item.Key);
						}
					}
				}
				catch {  }
			}
		}
	}
	
}
