using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;

namespace StoryExplorer.Tests
{
	[TestClass()]
	public class CoordinatesTests
	{
		[TestMethod]
		public void VerifyConstructor()
		{
			// Arrange
			var adventurer = new Adventurer { CurrentPosition = new Coordinates(0, 0, 0) };

			// Assert
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(0, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
		}

		[TestMethod]
		public void MoveNorthByReinstantiating()
		{
			// Arrange
			var adventurer = new Adventurer { CurrentPosition = new Coordinates(0, 0, 0) };

			// Act
			adventurer.CurrentPosition = adventurer.CurrentPosition.Peek(Direction.North);

			// Assert
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(1, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
		}

		[TestMethod]
		public void MoveNorthWithoutReinstantiating()
		{
			// Arrange
			var adventurer = new Adventurer { CurrentPosition = new Coordinates(0, 0, 0) };

			// Act
			adventurer.CurrentPosition.Move(Direction.North);

			// Assert
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(1, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
		}

		[TestMethod()]
		public void ToStringTest()
		{
			// Arrange
			var coordinates = new Coordinates
			{
				X = 12,
				Y = 22,
				Z = 8
			};
			var expected = "X: 12, Y: 22, Z: 8";

			// Act
			var actual = coordinates.ToString();

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}