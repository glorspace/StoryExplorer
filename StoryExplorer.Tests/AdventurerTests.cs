using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;
using System;

namespace StoryExplorer.Tests
{
	[TestClass()]
	public class AdventurerTests
	{
		[TestMethod()]
		[ExpectedException(typeof(MissingMemberException))]
		public void NewExceptionThrow()
		{
			// Arrange
			var name = string.Empty;

			// Act
			var adventurer = new Adventurer(name);
		}

		[TestMethod()]
		public void NewExceptionMessage()
		{
			// Arrange
			var name = string.Empty;
			var expected = "You must assign a name for the Adventurer before calling the New() method.";

			// Act
			try
			{
				var adventurer = new Adventurer(name);
			}
			catch (MissingMemberException exc)
			{
				// Assert
				Assert.AreEqual(expected, exc.Message);
			}
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void LoadExceptionThrow()
		{
			// Arrange
			var name = string.Empty;

			// Act
			Adventurer region = Adventurer.Load(name);
		}

		[TestMethod()]
		public void LoadExceptionMessage()
		{
			// Arrange
			var name = string.Empty;
			var expected = "Value cannot be null.\r\nParameter name: name";

			// Act
			try
			{
				Adventurer region = Adventurer.Load(name);
			}
			catch (ArgumentNullException exc)
			{
				// Assert
				Assert.AreEqual(expected, exc.Message);
			}
		}

		[TestMethod()]
		[ExpectedException(typeof(MissingMemberException))]
		public void SaveExceptionThrow()
		{
			// Arrange
			var adventurer = new Adventurer() { Name = String.Empty };

			// Act
			adventurer.Save();
		}

		[TestMethod()]
		public void SaveExceptionMessage()
		{
			// Arrange
			var adventurer = new Adventurer() { Name = String.Empty };
			var expected = "You must assign a name for the Adventurer before calling the Save() method.";

			// Act
			try
			{
				adventurer.Save();
			}
			catch (MissingMemberException exc)
			{
				// Assert
				Assert.AreEqual(expected, exc.Message);
			}
		}

		[TestMethod()]
		[ExpectedException(typeof(MissingMemberException))]
		public void DeleteExceptionThrow()
		{
			// Arrange
			var adventurer = new Adventurer() { Name = String.Empty };

			// Act
			adventurer.Delete();
		}

		[TestMethod()]
		public void DeleteExceptionMessage()
		{
			// Arrange
			var adventurer = new Adventurer() { Name = String.Empty };
			var expected = "You must assign a name for the Adventurer before calling the Delete() method.";

			// Act
			try
			{
				adventurer.Delete();
			}
			catch (MissingMemberException exc)
			{
				// Assert
				Assert.AreEqual(expected, exc.Message);
			}
        }

        [TestMethod]
        public void PeekNorth()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            var coords = adventurer.Peek(Direction.North);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
            Assert.AreEqual(0, coords.X);
            Assert.AreEqual(1, coords.Y);
            Assert.AreEqual(0, coords.Z);
        }

        [TestMethod]
        public void PeekSouth()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            var coords = adventurer.Peek(Direction.South);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
            Assert.AreEqual(0, coords.X);
            Assert.AreEqual(-1, coords.Y);
            Assert.AreEqual(0, coords.Z);
        }

        [TestMethod]
        public void PeekEast()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            var coords = adventurer.Peek(Direction.East);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
            Assert.AreEqual(1, coords.X);
            Assert.AreEqual(0, coords.Y);
            Assert.AreEqual(0, coords.Z);
        }

        [TestMethod]
        public void PeekWest()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            var coords = adventurer.Peek(Direction.West);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
            Assert.AreEqual(-1, coords.X);
            Assert.AreEqual(0, coords.Y);
            Assert.AreEqual(0, coords.Z);
        }

        [TestMethod]
        public void PeekUp()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            var coords = adventurer.Peek(Direction.Up);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
            Assert.AreEqual(0, coords.X);
            Assert.AreEqual(0, coords.Y);
            Assert.AreEqual(1, coords.Z);
        }

        [TestMethod]
        public void PeekDown()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            var coords = adventurer.Peek(Direction.Down);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
            Assert.AreEqual(0, coords.X);
            Assert.AreEqual(0, coords.Y);
            Assert.AreEqual(-1, coords.Z);
        }

        [TestMethod]
        public void MoveNorth()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            adventurer.Move(Direction.North);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(1, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
        }

        [TestMethod]
        public void MoveSouth()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            adventurer.Move(Direction.South);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(-1, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
        }

        [TestMethod]
        public void MoveEast()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            adventurer.Move(Direction.East);

            // Assert
            Assert.AreEqual(1, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
        }

        [TestMethod]
        public void MoveWest()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            adventurer.Move(Direction.West);

            // Assert
            Assert.AreEqual(-1, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(0, adventurer.CurrentPosition.Z);
        }

        [TestMethod]
        public void MoveUp()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            adventurer.Move(Direction.Up);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(1, adventurer.CurrentPosition.Z);
        }

        [TestMethod]
        public void MoveDown()
        {
            // Arrange
            var adventurer = new Adventurer();

            // Act
            adventurer.Move(Direction.Down);

            // Assert
            Assert.AreEqual(0, adventurer.CurrentPosition.X);
            Assert.AreEqual(0, adventurer.CurrentPosition.Y);
            Assert.AreEqual(-1, adventurer.CurrentPosition.Z);
        }

        [TestMethod()]
		public void ToStringTest()
		{
			// Arrange
			var adventurer = new Adventurer { Name = "Test Adventurer Name" };
			var expected = "Name: Test Adventurer Name";

			// Act
			var actual = adventurer.ToString();

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}