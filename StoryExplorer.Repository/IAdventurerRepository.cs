using System.Collections.Generic;
using StoryExplorer.Domain;

namespace StoryExplorer.Repository
{
    public interface IAdventurerRepository
    {
        void Delete(string name);
        void Create(Adventurer adventurer);
        Adventurer Read(string name);
        void Update(string name, Adventurer adventurer);
        IEnumerable<Adventurer> ReadAll();
    }
}
