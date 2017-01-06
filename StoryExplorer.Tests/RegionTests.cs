using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;

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