using System.Collections.Generic;
using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.WpfApp
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
