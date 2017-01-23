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
					newAdventurer.Save();

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
