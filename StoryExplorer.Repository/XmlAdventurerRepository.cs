using System;
using System.Collections.Generic;
using StoryExplorer.Domain;

namespace StoryExplorer.Repository
{
    public class XmlAdventurerRepository : IAdventurerRepository
    {
        private static readonly string StorageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StoryExplorer\\Adventurers\\";

        public void Create(Adventurer adventurer)
        {
            XmlFileSystemClient.Create(adventurer.Name, adventurer, StorageFolder);
        }

        public IEnumerable<Adventurer> ReadAll()
        {
            return XmlFileSystemClient.GetAll<Adventurer>(StorageFolder);
        }

        public Adventurer Read(string name)
        {
            return XmlFileSystemClient.Load<Adventurer>(name, StorageFolder);
        }

        public void Update(string name, Adventurer adventurer)
        {
            XmlFileSystemClient.Save(name, adventurer, StorageFolder);
        }

        public void Delete(string name)
        {
            XmlFileSystemClient.Delete(name, StorageFolder);
        }
    }
}
