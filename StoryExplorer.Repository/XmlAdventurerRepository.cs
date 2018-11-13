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
            XmlFileSystemService.Create(adventurer.Name, adventurer, StorageFolder);
        }

        public IEnumerable<Adventurer> ReadAll()
        {
            return XmlFileSystemService.GetAll<Adventurer>(StorageFolder);
        }

        public Adventurer Read(string name)
        {
            return XmlFileSystemService.Load<Adventurer>(name, StorageFolder);
        }

        public void Update(string name, Adventurer adventurer)
        {
            XmlFileSystemService.Save(name, adventurer, StorageFolder);
        }

        public void Delete(string name)
        {
            XmlFileSystemService.Delete(name, StorageFolder);
        }
    }
}
