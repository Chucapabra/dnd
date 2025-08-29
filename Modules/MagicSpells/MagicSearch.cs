using DNDHelper.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using static DNDHelper.Modules.Character.Skills;

namespace DNDHelper.Modules.MagicSpells
{
	internal class MagicSearch
	{
		private static Main main = Main.Instance;
		private static int rollSortState = 0;
		private static string currentSortMember = null;
		public MagicSearch()
		{
			main.CastSearch_textbox.TextChanged += CastSearch_textbox_TextChanged;
			main.CastSearch_Type_cb.SelectionChanged += SelectionChanged;
            main.SchoolSelect_Search_cb.SelectionChanged += SelectionChanged;
			main.Damage_Search_cb.SelectionChanged += SelectionChanged;
			main.Roll_Search_cb.SelectionChanged += SelectionChanged;
			main.LevelCast_Search_cb.SelectionChanged += SelectionChanged;
            main.NatureCast_Search_cb.Click += NatureCast_Search_cb_Checked;
			main.DataGridCurrentSpells.Sorting += DataGridCurrentSpells_Sorting;
			main.DataGridAllSpells.Sorting += DataGridAllSpells_Sorting;
		}

		private void DataGridAllSpells_Sorting(object sender, System.Windows.Controls.DataGridSortingEventArgs e)
		{
			e.Handled = true;

			var grid = (System.Windows.Controls.DataGrid)sender;


			if (currentSortMember != e.Column.SortMemberPath)
			{
				currentSortMember = e.Column.SortMemberPath;
				rollSortState = 0;
			}

			rollSortState = (rollSortState == 1) ? 2 : 1;

			foreach (var c in grid.Columns)
				if (!ReferenceEquals(c, e.Column))
					c.SortDirection = null;

			e.Column.SortDirection = (rollSortState == 1)
				? ListSortDirection.Ascending
				: ListSortDirection.Descending;

			CastsFilter();
		}

		private void DataGridCurrentSpells_Sorting(object sender, System.Windows.Controls.DataGridSortingEventArgs e)
		{
			DataGridAllSpells_Sorting(sender, e);
		}

		private static List<Cast> filtersSpellAll = new List<Cast>();

		public static void CastsFilter()
		{
			filtersSpellAll.Clear();
			foreach (var filter in MagicSpells.AllCasts)
			{
				filtersSpellAll.Add(filter);
			}

			FilterName();
			FilterType();
			FilterSchool();
			FilterDamage();
			FilterRoll();
			FilterLevel();
			FilterNature();


			MagicSpells.CurrentCasts.Clear();
		    MagicSpells.CurrentAllCasts.Clear();
			foreach (var filter in filtersSpellAll)
				if (MagicSpells.CurrentCastsNames.Contains(filter.SpellName))
					MagicSpells.CurrentCasts.Add(filter);
				else
					MagicSpells.CurrentAllCasts.Add(filter);


		}

		private static void FilterName()
		{
			filtersSpellAll = filtersSpellAll.Where((x) => x.SpellName.ToLower().Contains(main.CastSearch_textbox.Text.ToLower())).ToList();

			if (currentSortMember == nameof(Cast.SpellName))
			{
				if (rollSortState == 1)
					filtersSpellAll = filtersSpellAll.OrderBy(x => x.SpellName).ToList();
				else if (rollSortState == 2)
					filtersSpellAll = filtersSpellAll.OrderByDescending(x => x.SpellName).ToList();
			}
		}
		private static void FilterType()
		{
			if (main.CastSearch_Type_cb.SelectedIndex != 0)
				filtersSpellAll = filtersSpellAll.Where((x) => x.NumberType == (main.CastSearch_Type_cb.SelectedIndex - 1).ToString()).ToList();
		}
		private static void FilterSchool()
		{

			if (main.SchoolSelect_Search_cb.SelectedIndex != 0)
				filtersSpellAll = filtersSpellAll.Where((x) => x.SpellSchool == MagicSpells.NumberToSchool((main.SchoolSelect_Search_cb.SelectedIndex - 1).ToString())).ToList();

			if (currentSortMember == nameof(Cast.SpellSchool))
			{
				if (rollSortState == 1)
					filtersSpellAll = filtersSpellAll.OrderBy(x => x.SpellSchool).ToList();
				else if (rollSortState == 2)
					filtersSpellAll = filtersSpellAll.OrderByDescending(x => x.SpellSchool).ToList();
			}
		}
		private static void FilterDamage()
		{
            if (currentSortMember == nameof(Cast.SpellDamage))
            {
                if (rollSortState == 1)
                    filtersSpellAll = filtersSpellAll.OrderBy(x => x.SpellDamage).ToList();
                else if (rollSortState == 2)
                    filtersSpellAll = filtersSpellAll.OrderByDescending(x => x.SpellDamage).ToList();
            }

            if (main.Damage_Search_cb.SelectedIndex == 0)
				return;

			if (main.Damage_Search_cb.SelectedIndex == 1)
				filtersSpellAll = filtersSpellAll.Where(x => x.SpellDamage > 0).ToList();
			else if (main.Damage_Search_cb.SelectedIndex == 2)
				filtersSpellAll = filtersSpellAll.Where(x => x.SpellDamage == 0).ToList();

		}
		private static void FilterRoll()
		{
			if (main.Roll_Search_cb.SelectedIndex != 0)
			{
				if (main.Roll_Search_cb.SelectedIndex == 1)
					filtersSpellAll = filtersSpellAll.Where(x => x.SpellRoll > 0).ToList();
				else if (main.Roll_Search_cb.SelectedIndex == 2)
					filtersSpellAll = filtersSpellAll.Where(x => x.SpellRoll < 0).ToList();
				else if (main.Roll_Search_cb.SelectedIndex == 3)
					filtersSpellAll = filtersSpellAll.Where(x => x.SpellRoll == 0).ToList();
			}

			if (currentSortMember == nameof(Cast.SpellRoll))
			{
				if (rollSortState == 1)
					filtersSpellAll = filtersSpellAll.OrderBy(x => x.SpellRoll).ToList();
				else if (rollSortState == 2)
					filtersSpellAll = filtersSpellAll.OrderByDescending(x => x.SpellRoll).ToList();
            }


		}
		private static void FilterLevel()
		{

			if (main.LevelCast_Search_cb.SelectedIndex != 0)
				filtersSpellAll = filtersSpellAll.Where((x) => x.SpellLevel == (main.LevelCast_Search_cb.SelectedIndex - 1).ToString()).ToList();

			if (currentSortMember == nameof(Cast.SpellLevel))
			{
				if (rollSortState == 1)
					filtersSpellAll = filtersSpellAll.OrderBy(x => x.SpellLevel).ToList();
				else if (rollSortState == 2)
					filtersSpellAll = filtersSpellAll.OrderByDescending(x => x.SpellLevel).ToList();
			}
		}

		private static void FilterNature()
		{
			if (main.NatureCast_Search_cb.IsChecked.Value)
				filtersSpellAll = filtersSpellAll.Where((x) => x.IsNature).ToList();
		}

		private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			CastsFilter();
		}

		private void CastSearch_textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			CastsFilter();
		}

		private void NatureCast_Search_cb_Checked(object sender, RoutedEventArgs e)
		{
			CastsFilter();
		}
	}
}
