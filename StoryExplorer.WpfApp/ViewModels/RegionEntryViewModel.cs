using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.WpfApp
{
	public class RegionEntryViewModel
	{
        private readonly IRegionRepository regionRepository = new XmlRegionRepository();
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
