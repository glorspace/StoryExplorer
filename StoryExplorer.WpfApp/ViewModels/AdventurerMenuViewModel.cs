using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.Repository;
using StoryExplorer.WpfApp.Config;

namespace StoryExplorer.WpfApp
{
	public class AdventurerMenuViewModel
	{
        private readonly IAdventurerRepository adventurerRepository = new RepositoryConfig().AdventurerRepository;
		public Adventurer SelectedAdventurer { get; set; }

	    public void DeleteSelectedAdventurer()
	    {
            adventurerRepository.Delete(SelectedAdventurer.Name);
	    }

	    public void SaveSelectedAdventurer()
	    {
	        adventurerRepository.Update(SelectedAdventurer.Name, SelectedAdventurer);

        }
	}
}
