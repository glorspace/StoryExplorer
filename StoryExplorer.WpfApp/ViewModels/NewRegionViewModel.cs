using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.Repository;
using StoryExplorer.WpfApp.Config;

namespace StoryExplorer.WpfApp
{
	public class NewRegionViewModel
	{
        private readonly IRegionRepository regionRepository = new RepositoryConfig().RegionRepository;

		public string AdventurerName { get; set; }
		public string RegionName { get; set; }

	    public void SaveRegion(Region region)
	    {
	        regionRepository.Create(region);
        }
	}
}
