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
	}
}
