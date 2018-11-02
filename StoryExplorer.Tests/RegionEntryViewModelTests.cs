using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryExplorer.WpfApp;

namespace StoryExplorer.Tests
{
    [TestClass]
    public class RegionEntryViewModelTests
    {
        [TestMethod]
        public void AllSavedRegions_RefreshRegionList_IsPopulated()
        {
            // Arrange
            var viewModel = new RegionEntryViewModel();

            // Act
            viewModel.RefreshRegionList();

            // Assert
            Assert.IsNotNull(viewModel.AllSavedRegions);
            Assert.AreEqual(3, viewModel.AllSavedRegions.ToList().Count);
        }
    }
}
