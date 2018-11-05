using System.Collections.Generic;
using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.WpfApp
{
	public class RegionEntryViewModel
	{
        private readonly IRegionRepository regionRepository = RepositoryFactory.Get<IRegionRepository>();
		public IEnumerable<Region> AllSavedRegions { get; set; }
		public Adventurer Adventurer { get; set; }

		public RegionEntryViewModel()
		{
		    RefreshRegionList();
		}

	    public void RefreshRegionList()
	    {
	        AllSavedRegions = regionRepository.ReadAll();
	    }
	}
}
