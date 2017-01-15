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
	/// Interaction logic for RegionMenu.xaml
	/// </summary>
	public partial class RegionMenu : Window
	{
		private Window previousWindow;

		public RegionMenu()
		{
			InitializeComponent();
		}

		public RegionMenu(Window previous, Adventurer adventurer, Region region) : this()
		{
			previousWindow = previous;

			var viewModel = (RegionMenuViewModel)DataContext;
			viewModel.Adventurer = adventurer;
			viewModel.Region = region;

			regionName.Content = region.Name;
			regionDescription.Text = region.Description;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			previousWindow.Close();
		}
	}
}
