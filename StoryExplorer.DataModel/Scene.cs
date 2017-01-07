using System.Collections.Generic;

namespace StoryExplorer.DataModel
{
	/// <summary>
	/// Represents a scene of a story region.
	/// </summary>
	public class Scene
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public Coordinates Coordinates { get; set; }
		public List<Direction> AllowableMoves { get; set; } = new List<Direction>();

		/// <summary>
		/// Custom implementation to show a meaningful string representation of the Scene instance.
		/// </summary>
		/// <returns>String representation of the Scene instance.</returns>
		public override string ToString() => $"[{Coordinates.X}, {Coordinates.Y}, {Coordinates.Z}]: {Title}";
	}
}
