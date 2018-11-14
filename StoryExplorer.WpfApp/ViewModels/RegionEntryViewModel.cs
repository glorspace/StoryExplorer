using System.Collections.Generic;
using StoryExplorer.Repository;
using StoryExplorer.Repository.Interfaces;
using StoryExplorer.Repository.Models;

namespace StoryExplorer.WpfApp.ViewModels
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
