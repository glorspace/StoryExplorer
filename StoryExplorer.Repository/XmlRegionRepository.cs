using System;
using System.Collections.Generic;
using StoryExplorer.Domain;

namespace StoryExplorer.Repository
{
    public class XmlRegionRepository : IRegionRepository
    {
        private static readonly string StorageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StoryExplorer\\Regions\\";

        public void Create(Region region)
        {
            XmlFileSystemClient.Create(region.Name, region, StorageFolder);
        }

        public IEnumerable<Region> ReadAll()
        {
            return XmlFileSystemClient.GetAll<Region>(StorageFolder);
        }

        public Region Read(string name)
        {
            return XmlFileSystemClient.Load<Region>(name, StorageFolder);
        }

        public void Update(string name, Region region)
        {
            XmlFileSystemClient.Save(name, region, StorageFolder);
        }

        public void Delete(string name)
        {
            XmlFileSystemClient.Delete(name, StorageFolder);
        }
    }
}
