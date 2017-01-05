using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryExplorer.DataModel
{
	public class Scene
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public Coordinates Coordinates { get; set; }
		List<Direction> AllowableMoves { get; set; }
	}
}
