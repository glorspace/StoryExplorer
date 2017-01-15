using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StoryExplorer.WpfApp
{
	/// <summary>
	/// Interaction logic for RegionEntry.xaml
	/// </summary>
	public partial class RegionEntry : Window
	{
		private Window previousWindow;
		private bool goBack = false;

		public RegionEntry()
		{
			InitializeComponent();
		}
		public RegionEntry(Window previous, Adventurer adventurer) : this()
		{
			previousWindow = previous;

			var viewModel = (RegionEntryViewModel)DataContext;
			viewModel.Adventurer = adventurer;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			if (goBack)
			{
				previousWindow.Show();
			}
			else
			{
				previousWindow.Close();
			}
		}

		private void back_Click(object sender, RoutedEventArgs e)
		{
			goBack = true;
			Close();
		}

		private void selectRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			load.IsEnabled = true;
		}

		private void newRegion_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionEntryViewModel)DataContext;
			var newRegionWindow = new NewRegion(viewModel.Adventurer.Name);
			var regionCreated = newRegionWindow.ShowDialog();
			if (regionCreated.HasValue && regionCreated.Value)
			{
				var newRegionName = newRegionWindow.GetNewRegionName();
				newRegionWindow.Close();				
				viewModel.AllSavedRegions = Region.GetAllSavedRegions();
				foreach (var region in viewModel.AllSavedRegions)
				{
					if (region.Name == newRegionName)
					{
						selectRegion.SelectedItem = region;
						break;
					}
				}

				BindingOperations.GetBindingExpressionBase(selectRegion, ItemsControl.ItemsSourceProperty).UpdateTarget();
			}
		}

		private void load_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionEntryViewModel)DataContext;
			var region = (Region)selectRegion.SelectedItem;
			this.Hide();
			RegionMenu regionMenuWindow = new RegionMenu(this, viewModel.Adventurer, region);
			regionMenuWindow.Show();
		}
	}
}
