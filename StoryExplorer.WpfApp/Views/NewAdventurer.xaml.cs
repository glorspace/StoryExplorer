using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
	/// Interaction logic for NewAdventurer.xaml
	/// </summary>
	public partial class NewAdventurer : Window
	{
		public NewAdventurer()
		{
			InitializeComponent();
		}

		public Adventurer GetNewAdventurer()
		{
			var viewModel = (NewAdventurerViewModel)DataContext;
			return viewModel.NewAdventurer;
		}

		private void create_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (NewAdventurerViewModel)DataContext;
			if (String.IsNullOrWhiteSpace(viewModel.NewAdventurer.Name) ||
				selectGender.SelectedItem == null ||
				selectHairColor.SelectedItem == null ||
				selectHairStyle.SelectedItem == null ||
				selectSkinColor.SelectedItem == null ||
				selectEyeColor.SelectedItem == null ||
				selectPersonality.SelectedItem == null)
			{
				validationMessage.Text = "Choose a name and all of your adventurer's characteristics before trying to create.";
				validationMessage.Visibility = Visibility.Visible;
			}
			else
			{
				viewModel.NewAdventurer.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(viewModel.NewAdventurer.Name);
				viewModel.NewAdventurer.Password = adventurerPassword.Password;
				viewModel.NewAdventurer.Gender = (Gender)selectGender.SelectedItem;
				viewModel.NewAdventurer.HairColor = (HairColor)selectHairColor.SelectedItem;
				viewModel.NewAdventurer.HairStyle = (HairStyle)selectHairStyle.SelectedItem;
				viewModel.NewAdventurer.SkinColor = (SkinColor)selectSkinColor.SelectedItem;
				viewModel.NewAdventurer.EyeColor = (EyeColor)selectEyeColor.SelectedItem;
				viewModel.NewAdventurer.Personality = (Personality)selectPersonality.SelectedItem;

				try
				{
					viewModel.NewAdventurer.New();
					DialogResult = true;
				}
				catch (Exception exc)
				{
					validationMessage.Text = exc.Message;
					validationMessage.Visibility = Visibility.Visible;
				}
			}
		}
	}
}
