using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryExplorer.WpfApp
{
	public class RegionExplorerViewModel
	{
		public Adventurer Adventurer { get; set; }
		public Region Region { get; set; }
		public Scene CurrentScene { get; set; }
		public RegionMode Mode { get; set; }

		public RegionExplorerViewModel()
		{

		}

		public RegionExplorerViewModel(Adventurer adventurer, Region region)
		{
			Adventurer = adventurer;
			Region = region;

			Adventurer.CurrentRegion = region;
			Adventurer.CurrentRegionName = region.Name;

			if (Adventurer.CurrentPosition == null)
			{
				Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			}

			CurrentScene = Region.GetScene(Adventurer.CurrentPosition);
		}
	}
}
