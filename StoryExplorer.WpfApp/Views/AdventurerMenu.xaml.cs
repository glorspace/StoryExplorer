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
using StoryExplorer.DataModel;

namespace StoryExplorer.WpfApp
{
	/// <summary>
	/// Interaction logic for AdventurerMenu.xaml
	/// </summary>
	public partial class AdventurerMenu : Window
	{
		private MainWindow mainWindow;
		private bool goBack = false;

		public AdventurerMenu()
		{
			InitializeComponent();
		}

		public AdventurerMenu(MainWindow mainWindow, Adventurer adventurer) : this()
		{
			this.mainWindow = mainWindow;

			var viewModel = (AdventurerMenuViewModel)DataContext;
			viewModel.SelectedAdventurer = adventurer;

			adventurerName.Content = adventurer.Name;
			labelGender.Content = adventurer.Gender;
			labelHairColor.Content = adventurer.HairColor;
			labelHairStyle.Content = adventurer.HairStyle;
			labelSkinColor.Content = adventurer.SkinColor;
			labelEyeColor.Content = adventurer.EyeColor;
			labelPersonality.Content = adventurer.Personality;
			labelHeight.Content = adventurer.Height;
			labelCurrentRegion.Content = adventurer.CurrentRegion?.Name ?? "None";
			labelCreatedDate.Content = adventurer.Created.ToShortDateString();

			if (adventurer.Gender == Gender.Female)
				background.ImageSource = new ImageSourceConverter().ConvertFromString("pack://application:,,,/StoryExplorer.WpfApp;component/Images/silvan-tracker.jpg") as ImageSource;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			if (goBack)
			{
				mainWindow.Show();
			}
			else
			{
				mainWindow.Close();
			}
		}

		private void back_Click(object sender, RoutedEventArgs e)
		{
			this.goBack = true;
			this.Close();
		}
	}
}
