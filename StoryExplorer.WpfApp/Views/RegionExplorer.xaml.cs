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
		#region Private Fields

		private readonly Window previousWindow;
		private readonly bool isOwner = true;
		private bool goBack;

		#endregion

		#region Constructors

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

			Title = "Story Explorer: [" + region.Name + "]";

			regionName.Content = region.Name;
			regionDescription.Text = region.Description;

			if (adventurer.Name != region.OwnerName)
			{
				isOwner = false;
				editRegionDescription.Visibility = Visibility.Collapsed;
				manageAuthors.Visibility = Visibility.Collapsed;
				
				if (!region.DesignatedAuthors.Contains(adventurer.Name))
				{
					mode.IsChecked = false;
					mode.Visibility = Visibility.Collapsed;
				}
			}
		}

		#endregion

		#region Private Methods

		private void SetRegionMode(RegionExplorerViewModel viewModel)
		{
			viewModel.Mode = mode.IsChecked.HasValue && mode.IsChecked.Value ? RegionMode.Author : RegionMode.Explorer;

			if (viewModel.Mode == RegionMode.Author)
			{
				editSceneTitle.Visibility = Visibility.Visible;
				editSceneDescription.Visibility = Visibility.Visible;
			}
			else if (viewModel.Mode == RegionMode.Explorer)
			{
				editSceneTitle.Visibility = Visibility.Collapsed;
				editSceneDescription.Visibility = Visibility.Collapsed;
			}
		}

		private void ShowRegionMenuControls()
		{
			if (isOwner)
			{
				editRegionDescription.Visibility = Visibility.Visible;				
			}
			regionMenuControls.Visibility = Visibility.Visible;
			back.Visibility = Visibility.Visible;
		}

		private void HideRegionMenuControls()
		{
			editRegionDescription.Visibility = Visibility.Collapsed;
			regionMenuControls.Visibility = Visibility.Collapsed;
			back.Visibility = Visibility.Collapsed;
		}

		private void ShowExplorerControls()
		{
			exitRegion.Visibility = Visibility.Visible;
			explorerControls.Visibility = Visibility.Visible;
		}

		private void HideExplorerControls()
		{
			exitRegion.Visibility = Visibility.Collapsed;
			explorerControls.Visibility = Visibility.Hidden;
		}

		private void RefreshSceneElements()
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.RefreshCurrentScene();

			sceneTitle.GetBindingExpression(ContentProperty)?.UpdateTarget();
			sceneDescription.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();

			RefreshDirectionalButtonColors(viewModel);
			if (viewModel.Mode == RegionMode.Explorer)
				RefreshDirectionalButtonVisibility(viewModel);
			else
				ShowDirectionalButtons();
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

		private void RefreshDirectionalButtonVisibility(RegionExplorerViewModel viewModel)
		{
			north.Visibility = ConvertBooleanToVisibility(viewModel.CurrentScene.AllowableMoves.Contains(Direction.North));
			east.Visibility = ConvertBooleanToVisibility(viewModel.CurrentScene.AllowableMoves.Contains(Direction.East));
			south.Visibility = ConvertBooleanToVisibility(viewModel.CurrentScene.AllowableMoves.Contains(Direction.South));
			west.Visibility = ConvertBooleanToVisibility(viewModel.CurrentScene.AllowableMoves.Contains(Direction.West));
			up.Visibility = ConvertBooleanToVisibility(viewModel.CurrentScene.AllowableMoves.Contains(Direction.Up));
			down.Visibility = ConvertBooleanToVisibility(viewModel.CurrentScene.AllowableMoves.Contains(Direction.Down));

		}

		private Visibility ConvertBooleanToVisibility(bool allowableMove)
		{
			return allowableMove ? Visibility.Visible : Visibility.Hidden;
		}

		private void ShowDirectionalButtons()
		{
			north.Visibility = Visibility.Visible;
			east.Visibility = Visibility.Visible;
			south.Visibility = Visibility.Visible;
			west.Visibility = Visibility.Visible;
			up.Visibility = Visibility.Visible;
			down.Visibility = Visibility.Visible;
		}

		private void HideDirectionalButtons()
		{
			north.Visibility = Visibility.Hidden;
			east.Visibility = Visibility.Hidden;
			south.Visibility = Visibility.Hidden;
			west.Visibility = Visibility.Hidden;
			up.Visibility = Visibility.Hidden;
			down.Visibility = Visibility.Hidden;
		}

		private bool AttemptMove(Direction direction)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			if (viewModel.AttemptMove(direction))
			{
				return true;
			}
			else
			{
				if (viewModel.Mode == RegionMode.Author)
				{
					if (MessageBox.Show("This scene has not yet been written. Would you like to create it now?", "Scene Creation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						OpenNewSceneEditor(direction);
					}
				}
				return false;				
			}
		}

		private void OpenRegionDescriptionEditor()
		{
			regionDescriptionTextBox.Text = regionDescription.Text;
			regionDescription.Visibility = Visibility.Collapsed;
			HideRegionMenuControls();
			regionDescriptionEditor.Visibility = Visibility.Visible;
		}

		private void CloseRegionDescriptionEditor()
		{
			regionDescriptionEditor.Visibility = Visibility.Collapsed;
			ShowRegionMenuControls();
			regionDescription.Visibility = Visibility.Visible;
		}

		private void OpenSceneTitleEditor()
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			sceneTitleTextBox.Text = viewModel.CurrentScene.Title;

			sceneTitleViewer.Visibility = Visibility.Collapsed;
			editSceneDescription.Visibility = Visibility.Collapsed;
			HideDirectionalButtons();
			exitRegion.Visibility = Visibility.Collapsed;

			sceneTitleEditor.Visibility = Visibility.Visible;
		}

		private void CloseSceneTitleEditor()
		{
			sceneTitleEditor.Visibility = Visibility.Collapsed;
			saveSceneTitle.IsEnabled = false;
			sceneTitleViewer.Visibility = Visibility.Visible;
			editSceneTitle.Visibility = Visibility.Visible;
			editSceneDescription.Visibility = Visibility.Visible;
			ShowDirectionalButtons();
			exitRegion.Visibility = Visibility.Visible;
			
		}

		private void OpenSceneDescriptionEditor()
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			sceneDescriptionTextBox.Text = viewModel.CurrentScene.Description;

			sceneDescriptionViewer.Visibility = Visibility.Collapsed;
			sceneDescriptionEditor.Visibility = Visibility.Visible;
			editSceneTitle.Visibility = Visibility.Collapsed;
			HideDirectionalButtons();
			exitRegion.Visibility = Visibility.Collapsed;
		}

		private void CloseSceneDescriptionEditor()
		{
			sceneDescriptionEditor.Visibility = Visibility.Collapsed;
			saveSceneDescription.IsEnabled = false;
			sceneDescriptionViewer.Visibility = Visibility.Visible;
			editSceneDescription.Visibility = Visibility.Visible;
			editSceneTitle.Visibility = Visibility.Visible;
			ShowDirectionalButtons();
			exitRegion.Visibility = Visibility.Visible;
		}

		private void OpenNewSceneEditor(Direction direction)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.CreateNewScene(direction);			

			sceneTitleViewer.Visibility = Visibility.Collapsed;
			sceneDescriptionViewer.Visibility = Visibility.Collapsed;
			exitRegion.Visibility = Visibility.Collapsed;
			HideDirectionalButtons();
			newSceneTitleEditor.Visibility = Visibility.Visible;
			newSceneDescriptionEditor.Visibility = Visibility.Visible;
		}

		private void CloseNewSceneEditor()
		{
			newSceneTitleEditor.Visibility = Visibility.Collapsed;
			newSceneDescriptionEditor.Visibility = Visibility.Collapsed;
			exitRegion.Visibility = Visibility.Visible;
			RefreshSceneElements();
			sceneTitleViewer.Visibility = Visibility.Visible;
			sceneDescriptionViewer.Visibility = Visibility.Visible;
		}

		private void RefreshDesignatedAuthorsEditor()
		{
			var viewModel = (RegionExplorerViewModel)DataContext;

			if (viewModel.NonAuthors.Count == 0)
			{
				addDesignatedAuthorsControls.Visibility = Visibility.Collapsed;
			}
			else
			{
				addDesignatedAuthorsControls.Visibility = Visibility.Visible;
			}

			if (viewModel.Region.DesignatedAuthors.Count == 0)
			{
				removeDesignatedAuthorsControls.Visibility = Visibility.Collapsed;
			}
			else
			{
				removeDesignatedAuthorsControls.Visibility = Visibility.Visible;
			}

			nonAuthors.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			designatedAuthors.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
		}

		#endregion

		#region Event Handlers

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

		private void exitRegion_Click(object sender, RoutedEventArgs e)
		{
			HideExplorerControls();
			ShowRegionMenuControls();
		}

		private void enterRegion_Click(object sender, RoutedEventArgs e)
		{
			HideRegionMenuControls();
			ShowExplorerControls();

			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.InitializeAdventurer();
			SetRegionMode(viewModel);

			RefreshSceneElements();
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
			saveRegionDescription.IsEnabled = true;
		}

		private void cancelRegionDescription_Click(object sender, RoutedEventArgs e)
		{
			CloseRegionDescriptionEditor();
		}

		private void saveRegionDescription_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.SetRegionDescription(regionDescriptionTextBox.Text.Trim());
			regionDescription.Text = viewModel.Region.Description;

			CloseRegionDescriptionEditor();
		}

		private void editRegionDescription_Click(object sender, RoutedEventArgs e)
		{
			OpenRegionDescriptionEditor();
		}

		private void editSceneTitle_Click(object sender, RoutedEventArgs e)
		{
			OpenSceneTitleEditor();
		}

		private void sceneTitleTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			saveSceneTitle.IsEnabled = true;
		}

		private void saveSceneTitle_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.SetCurrentSceneTitle(sceneTitleTextBox.Text.Trim());
			sceneTitle.GetBindingExpression(ContentProperty)?.UpdateTarget();
			CloseSceneTitleEditor();
		}

		private void cancelSceneTitle_Click(object sender, RoutedEventArgs e)
		{
			CloseSceneTitleEditor();
		}

		private void editSceneDescription_Click(object sender, RoutedEventArgs e)
		{
			OpenSceneDescriptionEditor();
		}

		private void sceneDescriptionTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			saveSceneDescription.IsEnabled = true;
		}

		private void saveSceneDescription_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.SetCurrentSceneDescription(sceneDescriptionTextBox.Text.Trim());
			sceneDescription.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
			CloseSceneDescriptionEditor();
		}

		private void cancelSceneDescription_Click(object sender, RoutedEventArgs e)
		{
			CloseSceneDescriptionEditor();
		}

		private void addDesignatedAuthor_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.AddDesignatedAuthor((string)nonAuthors.SelectedItem);
			RefreshDesignatedAuthorsEditor();
			addDesignatedAuthor.IsEnabled = false;
		}

		private void removeDesignatedAuthor_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.RemoveDesignatedAuthor((string)designatedAuthors.SelectedItem);
			RefreshDesignatedAuthorsEditor();
			removeDesignatedAuthor.IsEnabled = false;
		}

		private void nonAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			addDesignatedAuthor.IsEnabled = true;
		}

		private void designatedAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			removeDesignatedAuthor.IsEnabled = true;
		}

		private void manageAuthors_Click(object sender, RoutedEventArgs e)
		{
			HideRegionMenuControls();
			regionDescriptionViewer.Visibility = Visibility.Collapsed;
			designatedAuthorsEditor.Visibility = Visibility.Visible;
			RefreshDesignatedAuthorsEditor();
		}

		private void exitDesignatedAuthorsEditor_Click(object sender, RoutedEventArgs e)
		{
			designatedAuthorsEditor.Visibility = Visibility.Collapsed;
			regionDescriptionViewer.Visibility = Visibility.Visible;
			ShowRegionMenuControls();
		}

		private void newSceneDescriptionTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			saveNewScene.IsEnabled = !String.IsNullOrWhiteSpace(newSceneTitleTextBox.Text) && !String.IsNullOrWhiteSpace(newSceneDescriptionTextBox.Text);
		}

		private void newSceneTitleTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			saveNewScene.IsEnabled = !String.IsNullOrWhiteSpace(newSceneTitleTextBox.Text) && !String.IsNullOrWhiteSpace(newSceneDescriptionTextBox.Text);
		}

		private void saveNewScene_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.CurrentScene.Title = newSceneTitleTextBox.Text.Trim();
			viewModel.CurrentScene.Description = newSceneDescriptionTextBox.Text.Trim();
			viewModel.SaveNewScene();
			CloseNewSceneEditor();
		}

		private void cancelNewScene_Click(object sender, RoutedEventArgs e)
		{
			CloseNewSceneEditor();
		}

		#endregion
	}
}
