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
		private Window previousWindow;
		private bool goBack;
		private bool isOwner = true;

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

			regionName.Content = region.Name;
			regionDescription.Text = region.Description;

			if (adventurer.Name != region.OwnerName)
			{
				isOwner = false;
				edit.Visibility = Visibility.Collapsed;
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
			exit.Visibility = Visibility.Collapsed;
			explorerControls.Visibility = Visibility.Hidden;

			if (isOwner)
			{
				edit.Visibility = Visibility.Visible;
				manageAuthors.Visibility = Visibility.Visible;
				mode.Visibility = Visibility.Visible;
			}
			back.Visibility = Visibility.Visible;
			enter.Visibility = Visibility.Visible;
		}

		private void enter_Click(object sender, RoutedEventArgs e)
		{
			edit.Visibility = Visibility.Collapsed;
			manageAuthors.Visibility = Visibility.Collapsed;
			back.Visibility = Visibility.Collapsed;
			mode.Visibility = Visibility.Collapsed;
			enter.Visibility = Visibility.Collapsed;

			exit.Visibility = Visibility.Visible;
			explorerControls.Visibility = Visibility.Visible;

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
	}
}