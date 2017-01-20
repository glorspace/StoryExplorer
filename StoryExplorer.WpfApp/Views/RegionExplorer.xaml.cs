﻿using StoryExplorer.DataModel;
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
		private readonly Window previousWindow;
		private bool goBack;
		private readonly bool isOwner = true;

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

			viewModel.Adventurer.CurrentRegion = viewModel.Region;
			viewModel.Adventurer.CurrentRegionName = viewModel.Region.Name;

			if (viewModel.Adventurer.CurrentPosition == null)
			{
				viewModel.Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			}

			viewModel.Adventurer.Save();

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

			RefreshSceneElements();
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
			viewModel.CurrentScene = viewModel.Region.GetScene(viewModel.Adventurer.CurrentPosition);
			//BindingOperations.GetBindingExpressionBase(sceneTitle, Label.ContentProperty).UpdateTarget();
			//BindingOperations.GetBindingExpressionBase(sceneDescription, TextBlock.TextProperty).UpdateTarget();
			sceneTitle.GetBindingExpression(ContentProperty)?.UpdateTarget();
			sceneDescription.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();

			viewModel.CurrentScene.AllowableMoves = viewModel.Region.GetAllowableMoves(viewModel.CurrentScene);

			RefreshDirectionalButtonColors(viewModel);
			if (viewModel.Mode == RegionMode.Explorer)
				RefreshDirectionalButtonEnables(viewModel);
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
						var scene = new Scene
						{
							Coordinates = viewModel.Adventurer.CurrentPosition.Peek(direction),
							Title = String.Empty,
							Description = String.Empty
						};
						viewModel.Region.Map.Add(scene);
						viewModel.Adventurer.CurrentPosition.Move(direction);

						RefreshSceneElements();
						OpenSceneTitleEditor();
						OpenSceneDescriptionEditor();
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
			saveRegionDescription.IsEnabled = true;
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

		private void cancelRegionDescription_Click(object sender, RoutedEventArgs e)
		{
			CloseRegionDescriptionEditor();
		}

		private void saveRegionDescription_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.Region.Description = regionDescriptionTextBox.Text;
			regionDescription.Text = viewModel.Region.Description;
			viewModel.Region.Save();

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
			sceneTitleViewer.Visibility = Visibility.Visible;
			saveSceneTitle.IsEnabled = false;

			if (sceneDescriptionEditor.Visibility == Visibility.Visible)
			{
				editSceneTitle.Visibility = Visibility.Collapsed;
			}
			else
			{
				editSceneTitle.Visibility = Visibility.Visible;
				editSceneDescription.Visibility = Visibility.Visible;
				ShowDirectionalButtons();
				exitRegion.Visibility = Visibility.Visible;
			}
		}

		private void saveSceneTitle_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;

			viewModel.CurrentScene.Title = sceneTitleTextBox.Text;
			viewModel.Region.Save();
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
			sceneDescriptionViewer.Visibility = Visibility.Visible;
			saveSceneDescription.IsEnabled = false;

			if (sceneTitleEditor.Visibility == Visibility.Visible)
			{
				editSceneDescription.Visibility = Visibility.Collapsed;
			}
			else
			{
				editSceneDescription.Visibility = Visibility.Visible;
				editSceneTitle.Visibility = Visibility.Visible;
				ShowDirectionalButtons();
				exitRegion.Visibility = Visibility.Visible;
			}
		}

		private void sceneDescriptionTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			saveSceneDescription.IsEnabled = true;
		}

		private void saveSceneDescription_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (RegionExplorerViewModel)DataContext;
			viewModel.CurrentScene.Description = sceneDescriptionTextBox.Text;
			viewModel.Region.Save();
			sceneDescription.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();

			CloseSceneDescriptionEditor();
		}

		private void cancelSceneDescription_Click(object sender, RoutedEventArgs e)
		{
			CloseSceneDescriptionEditor();
		}
	}
}
