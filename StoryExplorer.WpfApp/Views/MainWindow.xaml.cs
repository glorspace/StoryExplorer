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
			MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
			newAdventurer.IsEnabled = false;
			loadAdventurer.IsEnabled = false;
			viewModel.LoadAdventurerElementsVisibility = Visibility.Visible;
			BindingOperations.GetBindingExpressionBase(loadAdventurerElements, StackPanel.VisibilityProperty).UpdateTarget();
		}

		private void loadCancel_Click(object sender, RoutedEventArgs e)
		{
			MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
			newAdventurer.IsEnabled = true;
			loadAdventurer.IsEnabled = true;
			selectAdventurer.SelectedItem = null;
			loadSelect.IsEnabled = false;
			viewModel.LoadAdventurerElementsVisibility = Visibility.Hidden;
			BindingOperations.GetBindingExpressionBase(loadAdventurerElements, StackPanel.VisibilityProperty).UpdateTarget();
		}

		private void selectAdventurer_DropDownClosed(object sender, EventArgs e)
		{
			if (selectAdventurer.SelectedItem != null)
			{
				loadSelect.IsEnabled = true;
			}
		}

		private void loadSelect_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
			// instantiate new window
		}
	}
}
