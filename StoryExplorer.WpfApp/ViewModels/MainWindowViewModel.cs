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
		public IEnumerable<Adventurer> AllSavedAdventurers { get; set; }

		public MainWindowViewModel()
		{
			AllSavedAdventurers = Adventurer.GetAllSavedAdventurers();
		}
	}
}
