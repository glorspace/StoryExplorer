using System.Collections.Generic;
using StoryExplorer.Domain;

namespace StoryExplorer.Repository
{
    public interface IRegionRepository
    {
        void Create(Region region);
        IEnumerable<Region> ReadAll();
        Region Read(string name);
        void Update(string name, Region region);
        void Delete(string name);
    }
}
