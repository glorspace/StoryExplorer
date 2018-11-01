using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.EFModel;
using Adventurer = StoryExplorer.Domain.Adventurer;
using Region = StoryExplorer.EFModel.Region;

namespace StoryExplorer.Repository
{
    public class SqlRegionRepository : IRegionRepository
    {
        public void Create(Domain.Region region)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var newRegion = new EFModel.Region
                {
                    Name = region.Name,
                    Description = region.Description,
                    OwnerId = dbContext.Adventurers.FirstOrDefault(adventurer => adventurer.Name == region.OwnerName)?.Id ?? 0,
                    Created = region.Created
                };
                dbContext.Regions.Add(newRegion);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Domain.Region> ReadAll()
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var regions = dbContext.Regions.Select(region => ConvertEntityToDomainObject(region, dbContext));
                return regions;
            }
        }

        public Domain.Region Read(string name)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var result = dbContext.Regions.Where(x => x.Name == name)
                    .Select(region => ConvertEntityToDomainObject(region, dbContext))
                    .FirstOrDefault();
                return result;
            }
        }

        public void Update(string name, Domain.Region region)
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

        private Domain.Region ConvertEntityToDomainObject(EFModel.Region region, StoryExplorerEntities dbContext)
        {
            return new Domain.Region
            {
                Name = region.Name,
                Description = region.Description,
                OwnerName = dbContext.Adventurers.Find(region.OwnerId).Name,
                Created = region.Created,
                DesignatedAuthors = region.Adventurers1.Select(adventurer => adventurer.Name).ToList(),
                Map = region.Scenes.Select(scene => new Domain.Scene
                {
                    Title = scene.Title,
                    Description = scene.Description,
                    Coordinates = new Coordinates(scene.X, scene.Y, scene.Z)
                }).ToList()
            };
        }

        private void UpdateDesignatedAuthors(EFModel.Region dbRegion, Domain.Region region, StoryExplorerEntities dbContext)
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

        private void UpdateScenes(Region dbRegion, Domain.Region region)
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

        private void RemoveDeletedScenes(EFModel.Region dbRegion, Domain.Region region)
        {
            var deletedScenes = dbRegion.Scenes.Where(scene => SceneNotFound(scene, region.Map)).ToList();
            deletedScenes.ForEach(scene => dbRegion.Scenes.Remove(scene));
        }

        private bool SceneNotFound(EFModel.Scene scene, List<Domain.Scene> map)
        {
            return !map.Any(s =>
                s.Coordinates.X == scene.X &&
                s.Coordinates.Y == scene.Y &&
                s.Coordinates.Z == scene.Z);
        }
    }
}
