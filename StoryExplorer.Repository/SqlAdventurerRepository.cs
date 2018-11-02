using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.EFModel;

namespace StoryExplorer.Repository
{
    public class SqlAdventurerRepository : IAdventurerRepository
    {
        public void Create(Domain.Adventurer adventurer)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var newAdventurer = new EFModel.Adventurer
                {
                    Name = adventurer.Name,
                    Password = adventurer.Password,
                    Gender = dbContext.Genders.FirstOrDefault(gender => gender.Name == adventurer.Gender.ToString()),
                    HairColor = dbContext.HairColors.FirstOrDefault(hairColor => hairColor.Name == adventurer.HairColor.ToString()),
                    HairStyle = dbContext.HairStyles.FirstOrDefault(hairStyle => hairStyle.Name == adventurer.HairStyle.ToString()),
                    SkinColor = dbContext.SkinColors.FirstOrDefault(skinColor => skinColor.Name == adventurer.SkinColor.ToString()),
                    EyeColor = dbContext.EyeColors.FirstOrDefault(eyeColor => eyeColor.Name == adventurer.EyeColor.ToString()),
                    Personality = dbContext.Personalities.FirstOrDefault(personality => personality.Name == adventurer.Personality.ToString()),
                    Height = dbContext.Heights.FirstOrDefault(height => height.Name == adventurer.Height.ToString()),
                    Created = adventurer.Created
                };
                dbContext.Adventurers.Add(newAdventurer);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Domain.Adventurer> ReadAll()
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var adventurers = new List<Domain.Adventurer>();
                dbContext.Adventurers.ToList().ForEach(adventurer =>
                    adventurers.Add(ConvertEntityToDomainObject(adventurer, dbContext)));
                return adventurers;
            }
        }

        public Domain.Adventurer Read(string name)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var entity = dbContext.Adventurers.FirstOrDefault(x => x.Name == name);
                Domain.Adventurer domainObject = null;
                if (entity != null)
                    domainObject = ConvertEntityToDomainObject(entity, dbContext);

                return domainObject;
            }
        }

        public void Update(string name, Domain.Adventurer adventurer)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var dbAdventurer = dbContext.Adventurers.FirstOrDefault(x => x.Name == name);
                dbAdventurer.Name = adventurer.Name;
                dbAdventurer.Password = adventurer.Password;
                dbAdventurer.Gender = dbContext.Genders.FirstOrDefault(gender => gender.Name == adventurer.Gender.ToString());
                dbAdventurer.HairColor = dbContext.HairColors.FirstOrDefault(hairColor => hairColor.Name == adventurer.HairColor.ToString());
                dbAdventurer.HairStyle = dbContext.HairStyles.FirstOrDefault(hairStyle => hairStyle.Name == adventurer.HairStyle.ToString());
                dbAdventurer.SkinColor = dbContext.SkinColors.FirstOrDefault(skinColor => skinColor.Name == adventurer.SkinColor.ToString());
                dbAdventurer.EyeColor = dbContext.EyeColors.FirstOrDefault(eyeColor => eyeColor.Name == adventurer.EyeColor.ToString());
                dbAdventurer.Personality = dbContext.Personalities.FirstOrDefault(personality => personality.Name == adventurer.Personality.ToString());
                dbAdventurer.Height = dbContext.Heights.FirstOrDefault(height => height.Name == adventurer.Height.ToString());
                dbAdventurer.Created = adventurer.Created;
                dbAdventurer.CurrentRegionId = dbContext.Regions.FirstOrDefault(region => region.Name == adventurer.Name)?.Id;
                dbAdventurer.CurrentPositionX = adventurer.CurrentPosition.X;
                dbAdventurer.CurrentPositionY = adventurer.CurrentPosition.Y;
                dbAdventurer.CurrentPositionZ = adventurer.CurrentPosition.Z;
                dbContext.SaveChanges();
            }
        }

        public void Delete(string name)
        {
            using (var dbContext = new StoryExplorerEntities())
            {
                var adventurer = dbContext.Adventurers.FirstOrDefault(x => x.Name == name);
                dbContext.Adventurers.Remove(adventurer);
                dbContext.SaveChanges();
            }
        }

        private Domain.Adventurer ConvertEntityToDomainObject(EFModel.Adventurer adventurer, StoryExplorerEntities dbContext)
        {
            return new Domain.Adventurer
            {
                Name = adventurer.Name,
                Password = adventurer.Password,
                Gender = (Domain.Gender)Enum.Parse(typeof(Domain.Gender), adventurer.Gender.Name),
                HairColor = (Domain.HairColor)Enum.Parse(typeof(Domain.HairColor), adventurer.HairColor.Name),
                HairStyle = (Domain.HairStyle)Enum.Parse(typeof(Domain.HairStyle), adventurer.HairStyle.Name),
                SkinColor = (Domain.SkinColor)Enum.Parse(typeof(Domain.SkinColor), adventurer.SkinColor.Name),
                EyeColor = (Domain.EyeColor)Enum.Parse(typeof(Domain.EyeColor), adventurer.EyeColor.Name),
                Personality = (Domain.Personality)Enum.Parse(typeof(Domain.Personality), adventurer.Personality.Name),
                Height = (Domain.Height)Enum.Parse(typeof(Domain.Height), adventurer.Height.Name),
                Created = adventurer.Created,
                CurrentRegionName = dbContext.Regions.Find(adventurer.CurrentRegionId)?.Name,
                CurrentPosition = new Coordinates(
                    adventurer.CurrentPositionX ?? 0,
                    adventurer.CurrentPositionY ?? 0,
                    adventurer.CurrentPositionZ ?? 0)
            };
        }
    }
}
