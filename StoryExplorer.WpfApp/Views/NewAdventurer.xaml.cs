using System;
using System.Windows;
using StoryExplorer.Repository.Models;
using StoryExplorer.WpfApp.ViewModels;

namespace StoryExplorer.WpfApp.Views
{
    /// <summary>
    /// Interaction logic for NewAdventurer.xaml
    /// </summary>
    public partial class NewAdventurer : Window
	{
        private NewAdventurerViewModel viewModel;

        public NewAdventurer()
		{
			InitializeComponent();
		    viewModel = (NewAdventurerViewModel)DataContext;
        }

		public string GetNewAdventurerName()
		{
			return adventurerName.Text;
		}

		private void create_Click(object sender, RoutedEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(adventurerName.Text) ||
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
				try
				{
					var newAdventurer = new Adventurer(adventurerName.Text.Trim());
					newAdventurer.Password = adventurerPassword.Password;
					newAdventurer.Gender = (Gender)selectGender.SelectedItem;
					newAdventurer.HairColor = (HairColor)selectHairColor.SelectedItem;
					newAdventurer.HairStyle = (HairStyle)selectHairStyle.SelectedItem;
					newAdventurer.SkinColor = (SkinColor)selectSkinColor.SelectedItem;
					newAdventurer.EyeColor = (EyeColor)selectEyeColor.SelectedItem;
					newAdventurer.Personality = (Personality)selectPersonality.SelectedItem;
					newAdventurer.Height = (Height)selectHeight.SelectedItem;

				    viewModel.AddAdventurer(newAdventurer);

					// resetting name to title-cased version for retrieval by the main window
					adventurerName.Text = newAdventurer.Name;

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
