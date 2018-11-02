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
	    public string RegionDescription { get; set; }
	    public string SceneTitle { get; set; }
	    public string SceneDescription { get; set; }

	    public void CreateRegion()
	    {
	        var region = new Region(RegionName, AdventurerName);
	        region.Description = RegionDescription;
	        var scene = new Scene()
	        {
	            Title = SceneTitle,
	            Description = SceneDescription,
	            Coordinates = new Coordinates(0, 0, 0)
	        };
	        region.Map.Add(scene);

	        regionRepository.Create(region);
        }
	}
}
