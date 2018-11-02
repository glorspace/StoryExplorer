using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryExplorer.Repository
{
    public static class RepositoryFactory
    {
        public static IAdventurerRepository GetAdventurerRepository()
        {
            var repoInstance = GetRepositoryInstance("AdventurerRepositoryType");
            IAdventurerRepository repo = repoInstance as IAdventurerRepository;
            return repo;
        }

        public static IRegionRepository GetRegionRepository()
        {
            var repoInstance = GetRepositoryInstance("RegionRepositoryType");
            IRegionRepository repo = repoInstance as IRegionRepository;
            return repo;
        }

        public static ISceneRepository GetSceneRepository()
        {
            return new SceneRepository(GetRegionRepository());
        }

        private static object GetRepositoryInstance(string repoTypeName)
        {
            string typeName = ConfigurationManager.AppSettings[repoTypeName];
            Type repoType = Type.GetType(typeName);
            return Activator.CreateInstance(repoType);
        }
    }
}
