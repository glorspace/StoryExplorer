using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace StoryExplorer.WpfApp
{
	public class MainWindowViewModel
	{
		public List<Adventurer> AllSavedAdventurers => Adventurer.GetAllSavedAdventurers();
		public Adventurer SelectedAdventurer { get; set; }
		public Visibility NewAdventurerElementsVisibility { get; set; } = Visibility.Visible;
		public Visibility LoadAdventurerElementsVisibility { get; set; } = Visibility.Collapsed;
	}
}
