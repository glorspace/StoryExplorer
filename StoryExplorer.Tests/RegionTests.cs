using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryExplorer.Tests
{
	[TestClass()]
	public class RegionTests
	{
		[TestMethod()]
		public void ToStringTest()
		{
			// Arrange
			var region = new Region { Name = "Test Region Name" };
			var expected = "Name: Test Region Name";

			// Act
			var actual = region.ToString();

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}