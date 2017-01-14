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