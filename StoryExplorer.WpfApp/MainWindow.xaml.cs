using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoryExplorer.WpfApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void loadAdventurer_Click(object sender, RoutedEventArgs e)
		{
			//ViewModel currentViewModel = (ViewModel)DataContext;
			newAdventurer.IsEnabled = false;
			loadAdventurer.IsEnabled = false;
			selectAdventurer.Visibility = Visibility.Visible;
			adventurerProfile.Visibility = Visibility.Visible;
			loadSelect.Visibility = Visibility.Visible;
			loadCancel.Visibility = Visibility.Visible;
		}

		private void loadCancel_Click(object sender, RoutedEventArgs e)
		{
			newAdventurer.IsEnabled = true;
			loadAdventurer.IsEnabled = true;
			selectAdventurer.SelectedItem = null;
			selectAdventurer.Visibility = Visibility.Hidden;
			adventurerProfile.Visibility = Visibility.Hidden;
			loadSelect.IsEnabled = false;
			loadCancel.IsEnabled = false;
			loadSelect.Visibility = Visibility.Hidden;
			loadCancel.Visibility = Visibility.Hidden;
		}

		private void selectAdventurer_DropDownClosed(object sender, EventArgs e)
		{
			if (selectAdventurer.SelectedItem != null)
			{
				loadSelect.IsEnabled = true;
				loadCancel.IsEnabled = true;
			}
		}
	}
}
