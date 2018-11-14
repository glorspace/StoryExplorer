using System;
using System.Globalization;

namespace StoryExplorer.Repository.Models
{
    public class Adventurer
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public HairColor HairColor { get; set; }
        public HairStyle HairStyle { get; set; }
        public SkinColor SkinColor { get; set; }
        public EyeColor EyeColor { get; set; }
        public Personality Personality { get; set; }
        public Height Height { get; set; }
        public DateTime Created { get; set; }
        public string CurrentRegionName { get; set; }
        public Coordinates CurrentPosition { get; set; }

        public Adventurer() { }

        public Adventurer(string name) : this()
        {
            Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            Created = DateTime.Now;
        }

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
                    return new Coordinates(CurrentPosition.X, CurrentPosition.Y + 1, CurrentPosition.Z);
                case Direction.East:
                    return new Coordinates(CurrentPosition.X + 1, CurrentPosition.Y, CurrentPosition.Z);
                case Direction.South:
                    return new Coordinates(CurrentPosition.X, CurrentPosition.Y - 1, CurrentPosition.Z);
                case Direction.West:
                    return new Coordinates(CurrentPosition.X - 1, CurrentPosition.Y, CurrentPosition.Z);
                case Direction.Up:
                    return new Coordinates(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.Z + 1);
                case Direction.Down:
                    return new Coordinates(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.Z - 1);
                default:
                    throw new ArgumentException("Unexpected direction provided to Peek().");
            }
        }

        /// <summary>
        /// Performs a move of one unit in the direction specified, changing the CurrentPosition value.
        /// Unlike Peek(), this method changes the instance to a new value.
        /// </summary>
        /// <param name="direction">The direction in which to move.</param>
        public void Move(Direction direction)
        {
            CurrentPosition = Peek(direction);
        }

        /// <summary>
        /// Custom implementation to show a meaningful string representation of the Adventurer instance.
        /// </summary>
        /// <returns>String representation of the Adventurer instance.</returns>
        public override string ToString() => Name;
    }
}
