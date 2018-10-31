using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.WpfApp
{
	public class NewRegionViewModel
	{
        private readonly IRegionRepository regionRepository = new XmlRegionRepository();

		public string AdventurerName { get; set; }
		public string RegionName { get; set; }

	    public void SaveRegion(Region region)
	    {
	        regionRepository.Update(region.Name, region);
        }
	}
}
