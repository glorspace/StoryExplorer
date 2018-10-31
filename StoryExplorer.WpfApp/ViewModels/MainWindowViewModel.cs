using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using StoryExplorer.Domain;
using StoryExplorer.Repository;
using StoryExplorer.WpfApp.Config;

namespace StoryExplorer.WpfApp
{
	public class MainWindowViewModel
	{
	    private readonly IAdventurerRepository adventurerRepository = new RepositoryConfig().AdventurerRepository;
        public IEnumerable<Adventurer> AllSavedAdventurers { get; set; }

		public MainWindowViewModel()
		{
		    RefreshAdventurerList();
		}

	    public void RefreshAdventurerList()
	    {
	        AllSavedAdventurers = adventurerRepository.ReadAll();
        }
	}
}
