using System;
using System.Collections.Generic;
using StoryExplorer.Repository.Interfaces;
using StoryExplorer.Repository.Models;

namespace StoryExplorer.Repository.Implementations
{
    public class MockRegionRepository : IRegionRepository
    {
        public void Create(Region region)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name)
        {
            throw new NotImplementedException();
        }

        public Region Read(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Region> ReadAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Name = "Quello",
                    Description = "This is the land of Quello",
                    OwnerName = "Garda",
                    Created = new DateTime(2012, 12, 1, 1, 12, 11)
                },
                new Region
                {
                    Name = "Kurlin",
                    Description = "This is the land of Kurlin",
                    OwnerName = "Foist",
                    Created = new DateTime(2011, 12, 2, 1, 35, 42)
                },
                new Region
                {
                    Name = "Jirnit",
                    Description = "This is the land of Jirnit",
                    OwnerName = "Garda",
                    Created = new DateTime(2012, 12, 1, 1, 22, 11)
                }
            };

            return regions;
        }

        public void Update(string name, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
