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
			var adventurer = new Adventurer();
			adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(0, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
		}

		[TestMethod]
		public void MoveNorthByReinstantiating()
		{
			var adventurer = new Adventurer();
			adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			adventurer.CurrentPosition = adventurer.CurrentPosition.Peek(Direction.North);
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(1, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
		}

		[TestMethod]
		public void MoveNorthWithoutReinstantiating()
		{
			var adventurer = new Adventurer();
			adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			adventurer.CurrentPosition.Move(Direction.North);
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(1, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
		}
	}
}
