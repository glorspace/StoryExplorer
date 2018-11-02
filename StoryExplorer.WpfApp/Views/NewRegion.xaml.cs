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
using StoryExplorer.Domain;

namespace StoryExplorer.WpfApp
{
	/// <summary>
	/// Interaction logic for NewRegion.xaml
	/// </summary>
	public partial class NewRegion : Window
	{
        private NewRegionViewModel viewModel;

        public NewRegion()
		{
			InitializeComponent();
		    viewModel = (NewRegionViewModel)DataContext;
        }

		public NewRegion(string adventurerName) : this()
		{
			viewModel.AdventurerName = adventurerName;
		}

		internal string GetNewRegionName()
		{
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
			viewModel.RegionName = regionName.Text.Trim();
			viewModel.RegionDescription = regionDescription.Text.Trim();
		    viewModel.SceneTitle = sceneTitle.Text.Trim();
		    viewModel.SceneDescription = sceneDescription.Text.Trim();
		    viewModel.CreateRegion();

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
