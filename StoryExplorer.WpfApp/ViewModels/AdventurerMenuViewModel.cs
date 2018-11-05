using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.WpfApp
{
	public class AdventurerMenuViewModel
	{
        private readonly IAdventurerRepository adventurerRepository = RepositoryFactory.Get<IAdventurerRepository>();
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
