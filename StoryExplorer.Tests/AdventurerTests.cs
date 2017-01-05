using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryExplorer.Tests
{
	[TestClass()]
	public class AdventurerTests
	{
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