using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryExplorer.Domain;

namespace StoryExplorer.Repository
{
    public class SceneRepository : ISceneRepository
    {
        private readonly IRegionRepository regionRepository;

        public SceneRepository(IRegionRepository regionRepo)
        {
            regionRepository = regionRepo;
        }

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

        /// <summary>
        /// Discovers all directions in which a defined Scene could be found if an Adventurer was to move
        /// that way and returns them in a list.
        /// </summary>
        /// <param name="adventurer">The Adventurer instance for which potential moves could be made.</param>
        /// <returns>A list of directions in which an Adventurer could move from the specified Scene.</returns>
        public IEnumerable<Direction> GetAllowableMoves(Region region, Adventurer adventurer)
        {
            var allowablesMoves = new List<Direction>();

            if (Read(region, adventurer.Peek(Direction.North)) != null)
            {
                allowablesMoves.Add(Direction.North);
            }
            if (Read(region, adventurer.Peek(Direction.East)) != null)
            {
                allowablesMoves.Add(Direction.East);
            }
            if (Read(region, adventurer.Peek(Direction.South)) != null)
            {
                allowablesMoves.Add(Direction.South);
            }
            if (Read(region, adventurer.Peek(Direction.West)) != null)
            {
                allowablesMoves.Add(Direction.West);
            }
            if (Read(region, adventurer.Peek(Direction.Up)) != null)
            {
                allowablesMoves.Add(Direction.Up);
            }
            if (Read(region, adventurer.Peek(Direction.Down)) != null)
            {
                allowablesMoves.Add(Direction.Down);
            }

            return allowablesMoves;
        }
    }
}
