using System.Collections.Generic;
using StoryExplorer.Domain;

namespace StoryExplorer.Repository
{
    public interface IAdventurerRepository
    {
        void Create(Adventurer adventurer);
        IEnumerable<Adventurer> ReadAll();
        Adventurer Read(string name);
        void Update(string name, Adventurer adventurer);
        void Delete(string name);
    }
}
