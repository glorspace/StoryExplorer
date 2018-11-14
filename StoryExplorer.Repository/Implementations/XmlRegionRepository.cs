using System;
using System.Collections.Generic;
using StoryExplorer.Repository.Interfaces;
using StoryExplorer.Repository.Models;
using StoryExplorer.Repository.Services;

namespace StoryExplorer.Repository.Implementations
{
    public class XmlRegionRepository : IRegionRepository
    {
        private static readonly string StorageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StoryExplorer\\Regions\\";

        public void Create(Region region)
        {
            XmlFileSystemService.Create(region.Name, region, StorageFolder);
        }

        public IEnumerable<Region> ReadAll()
        {
            return XmlFileSystemService.GetAll<Region>(StorageFolder);
        }

        public Region Read(string name)
        {
            return XmlFileSystemService.Load<Region>(name, StorageFolder);
        }

        public void Update(string name, Region region)
        {
            XmlFileSystemService.Save(name, region, StorageFolder);
        }

        public void Delete(string name)
        {
            XmlFileSystemService.Delete(name, StorageFolder);
        }
    }
}
