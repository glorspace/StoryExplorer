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
		public void EqualsTest()
		{
			// Arrange
			var expectedCoords = new Coordinates(25, 12, 3);

			// Act
			var actualCoords = new Coordinates(25, 12, 3);

			// Assert
			Assert.AreEqual(expectedCoords, actualCoords);
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

		[TestMethod()]
		public void PeekNorthTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(0, 1, 0);

			// Act
			var actualCoords = startingCoords.Peek(Direction.North);

			// Assert

			Assert.AreEqual(expectedCoords, actualCoords);
		}

		[TestMethod()]
		public void MoveNorthTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(0, 1, 0);

			// Act
			startingCoords.Move(Direction.North);

			// Assert

			Assert.AreEqual(expectedCoords, startingCoords);
		}

		[TestMethod()]
		public void PeekEastTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(1, 0, 0);

			// Act
			var actualCoords = startingCoords.Peek(Direction.East);

			// Assert

			Assert.AreEqual(expectedCoords, actualCoords);
		}

		[TestMethod()]
		public void MoveEastTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(1, 0, 0);

			// Act
			startingCoords.Move(Direction.East);

			// Assert

			Assert.AreEqual(expectedCoords, startingCoords);
		}

		[TestMethod()]
		public void PeekSouthTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(0, -1, 0);

			// Act
			var actualCoords = startingCoords.Peek(Direction.South);

			// Assert

			Assert.AreEqual(expectedCoords, actualCoords);
		}

		[TestMethod()]
		public void MoveSouthTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(0, -1, 0);

			// Act
			startingCoords.Move(Direction.South);

			// Assert

			Assert.AreEqual(expectedCoords, startingCoords);
		}

		[TestMethod()]
		public void PeekWestTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(-1, 0, 0);

			// Act
			var actualCoords = startingCoords.Peek(Direction.West);

			// Assert

			Assert.AreEqual(expectedCoords, actualCoords);
		}

		[TestMethod()]
		public void MoveWestTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(-1, 0, 0);

			// Act
			startingCoords.Move(Direction.West);

			// Assert

			Assert.AreEqual(expectedCoords, startingCoords);
		}

		[TestMethod()]
		public void PeekUpTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(0, 0, 1);

			// Act
			var actualCoords = startingCoords.Peek(Direction.Up);

			// Assert

			Assert.AreEqual(expectedCoords, actualCoords);
		}

		[TestMethod()]
		public void MoveUpTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(0, 0, 1);

			// Act
			startingCoords.Move(Direction.Up);

			// Assert

			Assert.AreEqual(expectedCoords, startingCoords);
		}

		[TestMethod()]
		public void PeekDownTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(0, 0, -1);

			// Act
			var actualCoords = startingCoords.Peek(Direction.Down);

			// Assert

			Assert.AreEqual(expectedCoords, actualCoords);
		}

		[TestMethod()]
		public void MoveDownTest()
		{
			// Arrange
			var startingCoords = new Coordinates(0, 0, 0);
			var expectedCoords = new Coordinates(0, 0, -1);

			// Act
			startingCoords.Move(Direction.Down);

			// Assert

			Assert.AreEqual(expectedCoords, startingCoords);
		}
	}
}