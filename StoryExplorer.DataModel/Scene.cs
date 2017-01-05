using System.Collections.Generic;

namespace StoryExplorer.DataModel
{
	public class Scene
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public Coordinates Coordinates { get; set; }
		public List<Direction> AllowableMoves { get; set; } = new List<Direction>();

		public override string ToString() => $"[{Coordinates.X}, {Coordinates.Y}, {Coordinates.Z}]: {Title}";
	}
}
