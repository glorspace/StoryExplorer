using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Repository;

namespace StoryExplorer.WpfApp.Config
{
    internal class RepositoryConfig
    {
        public IAdventurerRepository AdventurerRepository { get; set; }
        public IRegionRepository RegionRepository { get; set; }
        public ISceneRepository SceneRepository { get; set; }

        public RepositoryConfig()
        {
            AdventurerRepository = new SqlAdventurerRepository();
            RegionRepository = new SqlRegionRepository();
            SceneRepository = new SceneRepository(RegionRepository);
        }
    }
}
