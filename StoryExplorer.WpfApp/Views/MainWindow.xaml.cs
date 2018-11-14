using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using StoryExplorer.WpfApp.ViewModels;
using StoryExplorer.Repository.Models;

namespace StoryExplorer.WpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
        private MainWindowViewModel viewModel;

        public MainWindow()
		{
			InitializeComponent();
		    viewModel = (MainWindowViewModel)DataContext;
        }

		private void loadCancel_Click(object sender, RoutedEventArgs e)
		{
			selectAdventurer.SelectedItem = null;
			login.IsEnabled = false;
		}

		private void selectAdventurer_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			login.IsEnabled = true;
			adventurerPassword.IsEnabled = true;
			adventurerPassword.Password = String.Empty;
		}

		private void login_Click(object sender, RoutedEventArgs e)
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
		}

		private void resetPasswordError()
		{
			passwordLabel.Content = "Password:";
			passwordLabel.Foreground = new SolidColorBrush(Colors.White);
			adventurerPassword.BorderBrush = new SolidColorBrush(Colors.LightGray);
		}

		private void adventurerPassword_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter)
			{
				resetPasswordError();
			}
		}

		private void adventurerPassword_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			resetPasswordError();
		}

		private void newAdventurer_Click(object sender, RoutedEventArgs e)
		{
			var newAdventurerWindow = new NewAdventurer();
			var adventurerCreated = newAdventurerWindow.ShowDialog();
			if (adventurerCreated.HasValue && adventurerCreated.Value)
			{
				var newAdventurerName = newAdventurerWindow.GetNewAdventurerName();
				newAdventurerWindow.Close();

                viewModel.RefreshAdventurerList();
				foreach (var adventurer in viewModel.AllSavedAdventurers)
				{
					if (adventurer.Name == newAdventurerName)
					{
						selectAdventurer.SelectedItem = adventurer;
						break;
					}
				}

				BindingOperations.GetBindingExpressionBase(selectAdventurer, ItemsControl.ItemsSourceProperty).UpdateTarget();
			}
		}

		public void refreshAdventurers()
		{
		    viewModel.RefreshAdventurerList();
            BindingOperations.GetBindingExpressionBase(selectAdventurer, ItemsControl.ItemsSourceProperty).UpdateTarget();
		}
	}
}
