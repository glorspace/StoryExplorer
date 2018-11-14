using System;
using System.Collections.Generic;
using StoryExplorer.Repository.Interfaces;
using StoryExplorer.Repository.Models;

namespace StoryExplorer.Repository.Implementations
{
    public class MockAdventurerRepository : IAdventurerRepository
    {
        public void Create(Adventurer adventurer)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name)
        {
            throw new NotImplementedException();
        }

        public Adventurer Read(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Adventurer> ReadAll()
        {
            var adventurers = new List<Adventurer>
            {
                new Adventurer()
                {
                    Name = "Foist",
                    Password = "foist",
                    Gender = Gender.Male,
                    HairColor = HairColor.Auburn,
                    HairStyle = HairStyle.CrewCut,
                    EyeColor = EyeColor.Brown,
                    SkinColor = SkinColor.Cream,
                    Personality = Personality.Boisterous,
                    Height = Height.Average,
                    CurrentRegionName = "Kurlin",
                    CurrentPosition = new Coordinates(2, 2, 3),
                    Created = new DateTime(2011, 12, 2, 1, 23, 2)
                },
                new Adventurer()
                {
                    Name = "Garda",
                    Password = "gard",
                    Gender = Gender.Female,
                    HairColor = HairColor.Blonde,
                    HairStyle = HairStyle.Long,
                    EyeColor = EyeColor.Blue,
                    SkinColor = SkinColor.Golden,
                    Personality = Personality.Stoic,
                    Height = Height.Tall,
                    CurrentRegionName = "Quello",
                    CurrentPosition = new Coordinates(1, -2, 0),
                    Created = new DateTime(2010, 2, 1, 11, 3, 22)
                }
            };

            return adventurers;
        }

        public void Update(string name, Adventurer adventurer)
        {
            throw new NotImplementedException();
        }
    }
}
