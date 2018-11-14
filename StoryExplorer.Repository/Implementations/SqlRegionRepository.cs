using System.Collections.Generic;
using System.Linq;
using StoryExplorer.EFModel;
using StoryExplorer.Repository.Interfaces;
using StoryExplorer.Repository.Models;
using Region = StoryExplorer.Repository.Models.Region;
using Scene = StoryExplorer.Repository.Models.Scene;

namespace StoryExplorer.Repository.Implementations
{
    public class SqlRegionRepository : IRegionRepository
    {
        public void Create(Region region)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var newRegion = new EFModel.Region
                {
                    Name = region.Name,
                    Description = region.Description,
                    OwnerId = dbContext.Adventurers.FirstOrDefault(adventurer => adventurer.Name == region.OwnerName)?.Id ?? 0,
                    Created = region.Created,
                };
                region.Map.ForEach(scene => newRegion.Scenes.Add(new EFModel.Scene
                {
                    Title = scene.Title,
                    Description = scene.Description,
                    X = scene.Coordinates.X,
                    Y = scene.Coordinates.Y,
                    Z = scene.Coordinates.Z
                }));
                dbContext.Regions.Add(newRegion);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Region> ReadAll()
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var regions = new List<Region>();
                dbContext.Regions.ToList().ForEach(region =>
                    regions.Add(ConvertEntityToDomainObject(region, dbContext)));
                return regions;
            }
        }

        public Region Read(string name)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var entity = dbContext.Regions.FirstOrDefault(x => x.Name == name);
                Region domainObject = null;
                if (entity != null)
                    domainObject = ConvertEntityToDomainObject(entity, dbContext);

                return domainObject;
            }
        }

        public void Update(string name, Region region)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var dbRegion = dbContext.Regions.FirstOrDefault(x => x.Name == name);
                dbRegion.Name = region.Name;
                dbRegion.Description = region.Description;
                dbRegion.OwnerId = dbContext.Adventurers.FirstOrDefault(adventurer => adventurer.Name == region.OwnerName)?.Id ?? 0;
                dbRegion.Created = region.Created;
                UpdateDesignatedAuthors(dbRegion, region, dbContext);
                UpdateScenes(dbRegion, region);

                dbContext.SaveChanges();
            }
        }

        public void Delete(string name)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var region = dbContext.Regions.FirstOrDefault(x => x.Name == name);
                dbContext.Regions.Remove(region);
                dbContext.SaveChanges();
            }

        }

        private Region ConvertEntityToDomainObject(EFModel.Region region, StoryExplorerEntities dbContext)
        {
            return new Region
            {
                Name = region.Name,
                Description = region.Description,
                OwnerName = dbContext.Adventurers.Find(region.OwnerId).Name,
                Created = region.Created,
                DesignatedAuthors = region.Adventurers1.Select(adventurer => adventurer.Name).ToList(),
                Map = region.Scenes.Select(scene => new Scene
                {
                    Title = scene.Title,
                    Description = scene.Description,
                    Coordinates = new Coordinates(scene.X, scene.Y, scene.Z)
                }).ToList()
            };
        }

        private void UpdateDesignatedAuthors(EFModel.Region dbRegion, Region region, StoryExplorerEntities dbContext)
        {
            foreach (var authorName in region.DesignatedAuthors)
            {
                var existingAuthor = dbRegion.Adventurers1.FirstOrDefault(existing => existing.Name == authorName);

                if (existingAuthor == null)
                {
                    var addedAuthor = dbContext.Adventurers.FirstOrDefault(adventurer => adventurer.Name == authorName);
                    if (addedAuthor != null) dbRegion.Adventurers1.Add(addedAuthor);
                }
            }

            var deletedAuthors = dbRegion.Adventurers1.Where(adventurer =>
                region.DesignatedAuthors.All(authorName => authorName != adventurer.Name)).ToList();
            deletedAuthors.ForEach(author => dbRegion.Adventurers1.Remove(author));
        }

        private void UpdateScenes(EFModel.Region dbRegion, Region region)
        {
            foreach (var scene in region.Map)
            {
                var existingScene = dbRegion.Scenes.FirstOrDefault(existing =>
                    existing.X == scene.Coordinates.X &&
                    existing.Y == scene.Coordinates.Y &&
                    existing.Z == scene.Coordinates.Z);

                if (existingScene != null)
                {
                    existingScene.Title = scene.Title;
                    existingScene.Description = scene.Description;
                }
                else
                {
                    var addedScene = new EFModel.Scene()
                    {
                        Title = scene.Title,
                        Description = scene.Description,
                        X = scene.Coordinates.X,
                        Y = scene.Coordinates.Y,
                        Z = scene.Coordinates.Z
                    };
                    dbRegion.Scenes.Add(addedScene);
                }
            }

            RemoveDeletedScenes(dbRegion, region);
        }

        private void RemoveDeletedScenes(EFModel.Region dbRegion, Region region)
        {
            var deletedScenes = dbRegion.Scenes.Where(scene => SceneNotFound(scene, region.Map)).ToList();
            deletedScenes.ForEach(scene => dbRegion.Scenes.Remove(scene));
        }

        private bool SceneNotFound(EFModel.Scene scene, List<Scene> map)
        {
            return !map.Any(s =>
                s.Coordinates.X == scene.X &&
                s.Coordinates.Y == scene.Y &&
                s.Coordinates.Z == scene.Z);
        }
    }
}
