using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.WpfApp
{
	public class AdventurerMenuViewModel
	{
        private readonly IAdventurerRepository adventurerRepository = new XmlAdventurerRepository();
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
