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

		/// <summary>
		/// Peeks in the direction specified without moving. Returns the coordinates of the grid location one
		/// unit in the specified direction from the current coordinates.
		/// </summary>
		/// <param name="direction">The direction in which to peek.</param>
		/// <returns>Coordinates one unit away in the specified direction.</returns>
		public Coordinates Peek(Direction direction)
		{
			switch (direction)
			{
				case Direction.North:
					return new Coordinates(X, Y + 1, Z);
				case Direction.East:
					return new Coordinates(X + 1, Y, Z);
				case Direction.South:
					return new Coordinates(X, Y - 1, Z);
				case Direction.West:
					return new Coordinates(X - 1, Y, Z);
				case Direction.Up:
					return new Coordinates(X, Y, Z + 1);
				case Direction.Down:
					return new Coordinates(X, Y, Z - 1);
				default:
					throw new ArgumentException("Unexpected direction provided to Peek().");
			}
		}

		/// <summary>
		/// Performs a move of one unit in the direction specified of the current Coordinates instance.
		/// Unlike Peek(), this method changes the instance to a new value.
		/// </summary>
		/// <param name="direction">The direction in which to move.</param>
		public void Move(Direction direction)
		{
			switch (direction)
			{
				case Direction.North:
					Y += 1;
					break;
				case Direction.East:
					X += 1;
					break;
				case Direction.South:
					Y -= 1;
					break;
				case Direction.West:
					X -= 1;
					break;
				case Direction.Up:
					Z += 1;
					break;
				case Direction.Down:
					Z -= 1;
					break;
				default:
					throw new ArgumentException("Unexpected direction provided to Move().");
			}
		}

		public override string ToString() => $"X: {X}, Y: {Y}, Z: {Z}";
	}	
}
