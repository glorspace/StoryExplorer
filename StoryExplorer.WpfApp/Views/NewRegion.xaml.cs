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
	/// Interaction logic for NewRegion.xaml
	/// </summary>
	public partial class NewRegion : Window
	{
		public NewRegion()
		{
			InitializeComponent();
		}

		public NewRegion(string adventurerName) : this()
		{
			var viewModel = (NewRegionViewModel)DataContext;
			viewModel.AdventurerName = adventurerName;
		}

		internal string GetNewRegionName()
		{
			var viewModel = (NewRegionViewModel)DataContext;
			return viewModel.RegionName;
		}

		private void checkForReady()
		{
			if (!String.IsNullOrWhiteSpace(regionName.Text) &&
				!String.IsNullOrWhiteSpace(regionDescription.Text) &&
				!String.IsNullOrWhiteSpace(sceneTitle.Text) &&
				!String.IsNullOrWhiteSpace(sceneDescription.Text))
			{
				create.IsEnabled = true;
			}
			else
			{
				create.IsEnabled = false;
			}
		}

		private void create_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (NewRegionViewModel)DataContext;
			viewModel.RegionName = regionName.Text;
			var region = new Region(regionName.Text, viewModel.AdventurerName);
			var scene = new Scene()
			{
				Title = sceneTitle.Text,
				Description = sceneDescription.Text
			};
			region.AddScene(scene);
			region.Save();

			DialogResult = true;
		}

		private void regionName_KeyUp(object sender, KeyEventArgs e)
		{
			checkForReady();
		}

		private void regionDescription_KeyUp(object sender, KeyEventArgs e)
		{
			checkForReady();
		}

		private void sceneTitle_KeyUp(object sender, KeyEventArgs e)
		{
			checkForReady();
		}

		private void sceneDescription_KeyUp(object sender, KeyEventArgs e)
		{
			checkForReady();
		}
	}
}
