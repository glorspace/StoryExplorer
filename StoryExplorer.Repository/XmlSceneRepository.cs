using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryExplorer.Domain;

namespace StoryExplorer.Repository
{
    public class XmlSceneRepository : ISceneRepository
    {
        private readonly XmlRegionRepository regionRepository = new XmlRegionRepository();

        public void Create(Region region, Scene scene)
        {
            region.Map.Add(scene);
            regionRepository.Update(region.Name, region);
        }

        public Scene Read(Region region, Coordinates coords)
        {
            return region.Map.Find(scene =>
                scene.Coordinates.X == coords.X &&
                scene.Coordinates.Y == coords.Y &&
                scene.Coordinates.Z == coords.Z);
        }

        public void Update(Region region, Coordinates coords, Scene scene)
        {
            var oldScene = Read(region, coords);
            region.Map.Remove(oldScene);
            region.Map.Add(scene);
            regionRepository.Update(region.Name, region);
        }

        public void Delete(Region region, Coordinates coords)
        {
            var oldScene = Read(region, coords);
            region.Map.Remove(oldScene);
            regionRepository.Update(region.Name, region);
        }
    }
}
