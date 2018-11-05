using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace StoryExplorer.Repository
{
    public static class RepositoryFactory
    {
        public static T Get<T>() where T : class
        {
            object instance;
            instance = typeof(T) != typeof(ISceneRepository)
                ? GetRepositoryInstance(typeof(T).Name)
                : new SceneRepository(Get<IRegionRepository>());

            var repository = instance as T;
            return repository;
        }

        private static object GetRepositoryInstance(string repoTypeName)
        {
            string typeName = ConfigurationManager.AppSettings[repoTypeName];
            Type repoType = Type.GetType(typeName);
            return Activator.CreateInstance(repoType);
        }
    }
}
