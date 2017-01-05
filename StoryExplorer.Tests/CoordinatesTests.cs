using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryExplorer.DataModel.Tests
{
	[TestClass()]
	public class CoordinatesTests
	{
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
			var expected = "[12, 22, 8]";

			// Act
			var actual = coordinates.ToString();

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}