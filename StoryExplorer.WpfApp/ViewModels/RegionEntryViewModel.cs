using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryExplorer.WpfApp
{
	public class RegionEntryViewModel
	{
		public IEnumerable<Region> AllSavedRegions { get; set; }
		public Adventurer Adventurer { get; set; }

		public RegionEntryViewModel()
		{
			AllSavedRegions = Region.GetAllSavedRegions();
		}
	}
}
