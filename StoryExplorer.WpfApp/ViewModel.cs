using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace StoryExplorer.WpfApp
{
	public class ViewModel
	{
		public List<Adventurer> AllSavedAdventurers => Adventurer.GetAllSavedAdventurers();
		public Adventurer SelectedAdventurer { get; set; }
	}
}
