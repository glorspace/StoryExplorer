using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace StoryExplorer.WpfApp
{
	public class RegionExplorerViewModel
	{
		public Adventurer Adventurer { get; set; }
		public Region Region { get; set; }
		public Scene CurrentScene { get; set; }
		public RegionMode Mode { get; set; }

		public List<string> NonAuthors
		{
			get
			{
				var nonAuthors = new List<string>();

				if (Region != null)
				{
					nonAuthors = Adventurer.GetAllSavedAdventurers().Select(x => x.Name).Except(Region.DesignatedAuthors).ToList();
					nonAuthors.Remove(Adventurer.Name);
				}

				return nonAuthors;
			}
		}

		public List<string> DesignatedAuthors
		{
			get
			{
				var authors = new List<string>();

				if (Region != null)
				{
					authors = Region.DesignatedAuthors.ToList();
				}

				return authors;
			}
		}
	}
}
