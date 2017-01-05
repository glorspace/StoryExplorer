using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;

namespace StoryExplorer.Tests
{
	[TestClass()]
	public class SceneTests
	{
		[TestMethod()]
		public void ToStringTest()
		{
			// Arrange
			var scene = new Scene
			{
				Title = "Test Scene Title",
				Coordinates = new Coordinates
				{
					X = 12,
					Y = 22,
					Z = 8
				},
			};
			var expected = "[12, 22, 8]: Test Scene Title";

			// Act
			var actual = scene.ToString();

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}