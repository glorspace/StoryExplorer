using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryExplorer.DataModel
{
	/// <summary>
	/// Represents a point in 3-dimensional space in a story region grid.
	/// </summary>
	public struct Coordinates
	{
		public Coordinates(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }
        
		public override string ToString() => $"X: {X}, Y: {Y}, Z: {Z}";
	}
}
