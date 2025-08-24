using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DNDHelper.Modules.ReferenceBooks
{
	internal class LoadingReferenceBook
	{
		Main main = Main.Instance;
		public LoadingReferenceBook()
		{
			main.ListBoxUrls.SelectionChanged += ListBoxUrls_SelectionChanged;
			main.ReferenceBook.NavigationCompleted += ReferenceBook_NavigationCompleted;
			main.ReferenceBook.NavigationStarting += ReferenceBook_NavigationStarting;
		}

		private void ReferenceBook_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
		{
			if (e.IsSuccess)
			{
				main.LoadingIcon.Visibility = Visibility.Hidden;
			}
		}

		private void ReferenceBook_NavigationStarting(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
		{
			main.LoadinTextUrl.Text = "Загрузка страницы...";
			main.LoadingIcon.Visibility = Visibility.Visible;
		}


		private void ListBoxUrls_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (main.ListBoxUrls.SelectedItem != null && Config.ManagerUrls._urlData != null)
			{
				string selectedKey = main.ListBoxUrls.SelectedItem.ToString();

				if (Config.ManagerUrls._urlData.TryGetValue(selectedKey, out string url))
				{
					if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
					{
						Debug.WriteLine(uriResult);
						main.ReferenceBook.Source = uriResult;
					}
				}
			}
		}
	}
}
