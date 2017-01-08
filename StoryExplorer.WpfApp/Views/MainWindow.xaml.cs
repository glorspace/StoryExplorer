using StoryExplorer.DataModel;
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
			loadLogin.IsEnabled = false;
			viewModel.LoadAdventurerElementsVisibility = Visibility.Hidden;
			BindingOperations.GetBindingExpressionBase(loadAdventurerElements, StackPanel.VisibilityProperty).UpdateTarget();
		}

		private void selectAdventurer_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			loadLogin.IsEnabled = true;
			adventurerPassword.IsEnabled = true;
			adventurerPassword.Password = String.Empty;
		}

		private void loadLogin_Click(object sender, RoutedEventArgs e)
		{
			var adventurer = (Adventurer)selectAdventurer.SelectedItem;
			if (adventurerPassword.Password == adventurer.Password)
			{
				this.Hide();
				AdventurerMenu adventurerMenuWindow = new AdventurerMenu(this, adventurer);
				adventurerMenuWindow.Show();
			} else
			{
				passwordLabel.Content = "Password Incorrect! Please try again...";
				passwordLabel.Foreground = new SolidColorBrush(Colors.Red);
				adventurerPassword.BorderBrush = new SolidColorBrush(Colors.Red);
			}
			// instantiate new window
		}

		private void adventurerPassword_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			passwordLabel.Content = "Password:";
			passwordLabel.Foreground = new SolidColorBrush(Colors.White);
			adventurerPassword.BorderBrush = new SolidColorBrush(Colors.LightGray);
		}
	}
}
