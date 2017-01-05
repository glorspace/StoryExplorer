using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.ConsoleApp;
using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryExplorer.Tests
{
	[TestClass]
	public class CoordinateTests
	{
		[TestMethod]
		public void VerifyConstructor()
		{
			// Arrange
			var adventurer = new Adventurer {CurrentPosition = new Coordinates(0, 0, 0)};

			// Assert
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(0, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
		}

		[TestMethod]
		public void MoveNorthByReinstantiating()
		{
			// Arrange
			var adventurer = new Adventurer {CurrentPosition = new Coordinates(0, 0, 0)};

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
			var adventurer = new Adventurer {CurrentPosition = new Coordinates(0, 0, 0)};

			// Act
			adventurer.CurrentPosition.Move(Direction.North);

			// Assert
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(1, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
		}
	}
}
