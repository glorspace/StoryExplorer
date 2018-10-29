using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;

namespace StoryExplorer.Tests
{
	[TestClass()]
	public class CoordinatesTests
	{
		[TestMethod]
		public void VerifyImplicitDefaultConstructor()
		{
			// Arrange
			var adventurer = new Adventurer();

			// Assert
			Assert.AreEqual(0, adventurer.CurrentPosition.X);
			Assert.AreEqual(0, adventurer.CurrentPosition.Y);
			Assert.AreEqual(0, adventurer.CurrentPosition.Z);
	    }

	    [TestMethod]
	    public void VerifyParameterizedConstructor()
	    {
	        // Arrange
	        var coords = new Coordinates(12, 3, -9);

	        // Assert
	        Assert.AreEqual(12, coords.X);
	        Assert.AreEqual(3, coords.Y);
	        Assert.AreEqual(-9, coords.Z);
	    }

        [TestMethod]
		public void EqualsTest()
		{
			// Arrange
			var expectedCoords = new Coordinates(25, 12, 3);

			// Act
			var actualCoords = new Coordinates(25, 12, 3);

			// Assert
			Assert.AreEqual(expectedCoords, actualCoords);
		}

		[TestMethod]
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