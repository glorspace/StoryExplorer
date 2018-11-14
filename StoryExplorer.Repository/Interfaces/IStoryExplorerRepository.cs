using System.Collections.Generic;

namespace StoryExplorer.Repository.Interfaces
{
    public interface IStoryExplorerRepository<T>
    {
        void Create(T entity);
        IEnumerable<T> ReadAll();
        T Read(string name);
        void Update(string name, T entity);
        void Delete(string name);
    }
}