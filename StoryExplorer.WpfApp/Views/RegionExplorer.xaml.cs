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
	/// Interaction logic for RegionExplorer.xaml
	/// </summary>
	public partial class RegionExplorer : Window
	{
		private Window previousWindow;
		private bool goBack;
		private bool isOwner = true;
		private string regionDescriptionCache = String.Empty;

		public RegionExplorer()
		{
			InitializeComponent();
		}

		public RegionExplorer(Window previous, Adventurer adventurer, Region region) : this()
		{
			previousWindow = previous;

			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.Adventurer = adventurer;
			viewModel.Region = region;
			viewModel.Mode = RegionMode.Author;

			Title = "Story Explorer: [" + region.Name + "]";

			regionName.Content = region.Name;
			regionDescription.Text = region.Description;

			if (adventurer.Name != region.OwnerName)
			{
				isOwner = false;
				editRegionDescription.Visibility = Visibility.Collapsed;
				manageAuthors.Visibility = Visibility.Collapsed;
				mode.IsChecked = false;
				mode.Visibility = Visibility.Collapsed;
				viewModel.Mode = RegionMode.Explorer;
			}
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

		private void exit_Click(object sender, RoutedEventArgs e)
		{
			hideExplorerControls();
			showRegionMenuControls();
		}

		private void enter_Click(object sender, RoutedEventArgs e)
		{
			hideRegionMenuControls();
			showExplorerControls();

			var viewModel = (RegionExplorerViewModel)DataContext;

			viewModel.Adventurer.CurrentRegion = viewModel.Region;
			viewModel.Adventurer.CurrentRegionName = viewModel.Region.Name;

			if (viewModel.Adventurer.CurrentPosition == null)
			{
				viewModel.Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			}

			viewModel.Adventurer.Save();

			viewModel.Mode = mode.IsChecked.Value ? RegionMode.Author : RegionMode.Explorer;

			RefreshSceneElements();
		}

		private void showRegionMenuControls()
		{
			if (isOwner)
			{
				editRegionDescription.Visibility = Visibility.Visible;
				manageAuthors.Visibility = Visibility.Visible;
				mode.Visibility = Visibility.Visible;
			}
			back.Visibility = Visibility.Visible;
			enter.Visibility = Visibility.Visible;
		}

		private void hideRegionMenuControls()
		{
			editRegionDescription.Visibility = Visibility.Collapsed;
			manageAuthors.Visibility = Visibility.Collapsed;
			back.Visibility = Visibility.Collapsed;
			mode.Visibility = Visibility.Collapsed;
			enter.Visibility = Visibility.Collapsed;
		}

		private void showExplorerControls()
		{
			exit.Visibility = Visibility.Visible;
			explorerControls.Visibility = Visibility.Visible;
		}

		private void hideExplorerControls()
		{
			exit.Visibility = Visibility.Collapsed;
			explorerControls.Visibility = Visibility.Hidden;
		}

		private void RefreshSceneElements()
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.CurrentScene = viewModel.Region.GetScene(viewModel.Adventurer.CurrentPosition);
			BindingOperations.GetBindingExpressionBase(sceneTitle, Label.ContentProperty).UpdateTarget();
			BindingOperations.GetBindingExpressionBase(sceneDescription, TextBlock.TextProperty).UpdateTarget();

			viewModel.CurrentScene.AllowableMoves = viewModel.Region.GetAllowableMoves(viewModel.CurrentScene);

			RefreshDirectionalButtonColors(viewModel);
			if (viewModel.Mode == RegionMode.Explorer)
				RefreshDirectionalButtonEnables(viewModel);
			else
				EnableAllDirectionalButtons();
		}

		private void RefreshDirectionalButtonColors(RegionExplorerViewModel viewModel)
		{
			north.Background = viewModel.CurrentScene.AllowableMoves.Contains(Direction.North) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFB1CFAE")) : new SolidColorBrush(Colors.Gray);
			east.Background = viewModel.CurrentScene.AllowableMoves.Contains(Direction.East) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFB1CFAE")) : new SolidColorBrush(Colors.Gray);
			south.Background = viewModel.CurrentScene.AllowableMoves.Contains(Direction.South) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFB1CFAE")) : new SolidColorBrush(Colors.Gray);
			west.Background = viewModel.CurrentScene.AllowableMoves.Contains(Direction.West) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFB1CFAE")) : new SolidColorBrush(Colors.Gray);
			up.Background = viewModel.CurrentScene.AllowableMoves.Contains(Direction.Up) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFB1CFAE")) : new SolidColorBrush(Colors.Gray);
			down.Background = viewModel.CurrentScene.AllowableMoves.Contains(Direction.Down) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFB1CFAE")) : new SolidColorBrush(Colors.Gray);
		}

		private void RefreshDirectionalButtonEnables(RegionExplorerViewModel viewModel)
		{
			north.IsEnabled = viewModel.CurrentScene.AllowableMoves.Contains(Direction.North);
			east.IsEnabled = viewModel.CurrentScene.AllowableMoves.Contains(Direction.East);
			south.IsEnabled = viewModel.CurrentScene.AllowableMoves.Contains(Direction.South);
			west.IsEnabled = viewModel.CurrentScene.AllowableMoves.Contains(Direction.West);
			up.IsEnabled = viewModel.CurrentScene.AllowableMoves.Contains(Direction.Up);
			down.IsEnabled = viewModel.CurrentScene.AllowableMoves.Contains(Direction.Down);

		}

		private void EnableAllDirectionalButtons()
		{
			north.IsEnabled = true;
			east.IsEnabled = true;
			south.IsEnabled = true;
			west.IsEnabled = true;
			up.IsEnabled = true;
			down.IsEnabled = true;
		}

		private void DisableAllDirectionalButtons()
		{
			north.IsEnabled = false;
			east.IsEnabled = false;
			south.IsEnabled = false;
			west.IsEnabled = false;
			up.IsEnabled = false;
			down.IsEnabled = false;
		}

		private bool AttemptMove(Direction direction)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			if (viewModel.Region.GetScene(viewModel.Adventurer.CurrentPosition.Peek(direction)) != null)
			{
				viewModel.Adventurer.CurrentPosition.Move(direction);
				return true;
			}
			else
			{
				if (viewModel.Mode == RegionMode.Author)
				{
					if (MessageBox.Show("This scene has not yet been written. Would you like to create it now?", "Scene Creation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						// launch new scene creation dialog
						// set up an if such that if the DialogResult = Ok, then move, else do nothing.
						return true;
					}
				}
				return false;				
			}
		}

		private void back_Click(object sender, RoutedEventArgs e)
		{
			goBack = true;
			Close();
		}

		private void north_Click(object sender, RoutedEventArgs e)
		{
			if (AttemptMove(Direction.North))
			{
				RefreshSceneElements();
			}
		}

		private void east_Click(object sender, RoutedEventArgs e)
		{

			if (AttemptMove(Direction.East))
			{
				RefreshSceneElements();
			}
		}

		private void south_Click(object sender, RoutedEventArgs e)
		{

			if (AttemptMove(Direction.South))
			{
				RefreshSceneElements();
			}
		}

		private void west_Click(object sender, RoutedEventArgs e)
		{

			if (AttemptMove(Direction.West))
			{
				RefreshSceneElements();
			}
		}

		private void up_Click(object sender, RoutedEventArgs e)
		{

			if (AttemptMove(Direction.Up))
			{
				RefreshSceneElements();
			}
		}

		private void down_Click(object sender, RoutedEventArgs e)
		{

			if (AttemptMove(Direction.Down))
			{
				RefreshSceneElements();
			}
		}

		private void regionDescriptionEdit_KeyUp(object sender, KeyEventArgs e)
		{
			saveDescription.IsEnabled = true;
		}

		private void closeDescriptionEditor()
		{
			editControls.Visibility = Visibility.Collapsed;

			regionDescription.Visibility = Visibility.Visible;
			showRegionMenuControls();
		}

		private void cancelDescription_Click(object sender, RoutedEventArgs e)
		{
			closeDescriptionEditor();
		}

		private void saveDescription_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.Region.Description = regionDescriptionEdit.Text;
			regionDescription.Text = viewModel.Region.Description;
			viewModel.Region.Save();

			closeDescriptionEditor();
		}

		private void editRegionDescription_Click(object sender, RoutedEventArgs e)
		{
			regionDescriptionCache = regionDescription.Text;
			regionDescription.Visibility = Visibility.Collapsed;
			hideRegionMenuControls();

			editControls.Visibility = Visibility.Visible;
			regionDescriptionEdit.Text = regionDescription.Text;
		}

		private void editSceneTitle_Click(object sender, RoutedEventArgs e)
		{
			sceneTitle.Visibility = Visibility.Collapsed;

			sceneTitleTextBox.Visibility = Visibility.Visible;
			saveSceneTitle.Visibility = Visibility.Visible;
			cancelSceneTitle.Visibility = Visibility.Visible;

			editSceneDescription.IsEnabled = false;

			DisableAllDirectionalButtons();

			exit.IsEnabled = false;

			var viewModel = (RegionExplorerViewModel)DataContext;

			sceneTitleTextBox.Text = viewModel.CurrentScene.Title;
		}

		private void sceneTitleTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			saveSceneTitle.IsEnabled = true;
		}

		private void closeSceneTitleEditor()
		{
			sceneTitleTextBox.Visibility = Visibility.Collapsed;
			saveSceneTitle.Visibility = Visibility.Collapsed;
			cancelSceneTitle.Visibility = Visibility.Collapsed;

			sceneTitle.Visibility = Visibility.Visible;
			EnableAllDirectionalButtons();
			exit.IsEnabled = true;
			editSceneDescription.IsEnabled = true;
			saveSceneTitle.IsEnabled = false;
		}

		private void saveSceneTitle_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;

			viewModel.CurrentScene.Title = sceneTitleTextBox.Text;
			viewModel.Region.Save();
			BindingOperations.GetBindingExpressionBase(sceneTitle, Label.ContentProperty).UpdateTarget();

			closeSceneTitleEditor();
		}

		private void cancelSceneTitle_Click(object sender, RoutedEventArgs e)
		{
			closeSceneTitleEditor();
		}
	}
}
