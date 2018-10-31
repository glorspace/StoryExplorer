using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.Repository;
using StoryExplorer.WpfApp.Config;

namespace StoryExplorer.WpfApp
{
	public class RegionEntryViewModel
	{
        private readonly IRegionRepository regionRepository = new RepositoryConfig().RegionRepository;
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
