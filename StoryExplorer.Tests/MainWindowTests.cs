using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.WpfApp;

namespace StoryExplorer.Tests
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void AllSavedAdventurers_RefreshAdventurerList_IsPopulated()
        {
            // Arrange
            var viewModel = new MainWindowViewModel();

            // Act
            viewModel.RefreshAdventurerList();

            // Assert
            Assert.IsNotNull(viewModel.AllSavedAdventurers);
            Assert.AreEqual(2, viewModel.AllSavedAdventurers.ToList().Count);
        }
    }
}
