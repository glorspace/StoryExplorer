using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.DataModel;
using System;

namespace StoryExplorer.Tests
{
	[TestClass()]
	public class RegionTests
	{
		[TestMethod()]
		[ExpectedException(typeof(MissingMemberException))]
		public void NewExceptionThrow()
		{
			// Arrange
			var name = string.Empty;
			var ownerName = "NameUnimportant";

			// Act
			var region = new Region(name, ownerName);
		}

		[TestMethod()]
		public void NewExceptionMessage()
		{
			// Arrange
			var name = string.Empty;
			var ownerName = "NameUnimportant";
			var expected = "You must assign a name for the Region before calling the New() method.";

			// Act
			try
			{
				var region = new Region(name, ownerName);
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
			Region region = Region.Load(name);
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
				Region region = Region.Load(name);
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
			var region = new Region() { Name = String.Empty };

			// Act
			region.Save();
		}

		[TestMethod()]
		public void SaveExceptionMessage()
		{
			// Arrange
			var region = new Region() { Name = String.Empty };
			var expected = "You must assign a name for the Region before calling the Save() method.";

			// Act
			try
			{
				region.Save();
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
			var region = new Region() { Name = String.Empty };

			// Act
			region.Delete();
		}

		[TestMethod()]
		public void DeleteExceptionMessage()
		{
			// Arrange
			var region = new Region() { Name = String.Empty };
			var expected = "You must assign a name for the Region before calling the Delete() method.";

			// Act
			try
			{
				region.Delete();
			}
			catch (MissingMemberException exc)
			{
				// Assert
				Assert.AreEqual(expected, exc.Message);
			}
		}

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