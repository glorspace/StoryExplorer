using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using StoryExplorer.Repository.Models;
using StoryExplorer.WpfApp.ViewModels;

namespace StoryExplorer.WpfApp.Views
{
    /// <summary>
    /// Interaction logic for RegionEntry.xaml
    /// </summary>
    public partial class RegionEntry : Window
	{
		private Window previousWindow;
	    private RegionEntryViewModel viewModel;
        private bool goBack = false;

        public RegionEntry()
		{
			InitializeComponent();
		    viewModel = (RegionEntryViewModel)DataContext;
        }
		public RegionEntry(Window previous, Adventurer adventurer) : this()
		{
			previousWindow = previous;
            
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
			var newRegionWindow = new NewRegion(viewModel.Adventurer.Name);
			var regionCreated = newRegionWindow.ShowDialog();
			if (regionCreated.HasValue && regionCreated.Value)
			{
				var newRegionName = newRegionWindow.GetNewRegionName();
				newRegionWindow.Close();				
				viewModel.RefreshRegionList();
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
			var region = (Region)selectRegion.SelectedItem;
			this.Hide();
			RegionExplorer regionMenuWindow = new RegionExplorer(this, viewModel.Adventurer, region);
			regionMenuWindow.Show();
		}
	}
}
