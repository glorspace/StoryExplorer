using System.Collections.Generic;
using StoryExplorer.Repository;
using StoryExplorer.Repository.Interfaces;
using StoryExplorer.Repository.Models;

namespace StoryExplorer.WpfApp.ViewModels
{
	public class MainWindowViewModel
	{
	    private readonly IAdventurerRepository adventurerRepository = RepositoryFactory.Get<IAdventurerRepository>();
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
