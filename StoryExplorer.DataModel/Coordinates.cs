using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryExplorer.DataModel
{
	public class Coordinates
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }

		public Coordinates() { }

		public Coordinates(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

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
					throw new Exception("Unexpected direction provided.");
			}
		}

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
			}
		}

		public override bool Equals(object obj)
		{
			var testObject = obj as Coordinates;

			if (testObject == null)
			{
				return false;
			}

			return (X == testObject.X) && (Y == testObject.Y) && (Z == testObject.Z);
		}

		public override string ToString() => $"X: {X}, Y: {Y}, Z: {Z}";
	}	
}
