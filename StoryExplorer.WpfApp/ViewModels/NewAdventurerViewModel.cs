using System;
using System.Collections.Generic;
using System.Linq;
using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.WpfApp
{
	public class NewAdventurerViewModel
	{
	    private readonly IAdventurerRepository adventurerRepository = RepositoryFactory.Get<IAdventurerRepository>();

        public IEnumerable<Gender> GenderList { get; set; } = Enum.GetValues(typeof(Gender)).Cast<Gender>();
		public IEnumerable<HairColor> HairColorList { get; set; } = Enum.GetValues(typeof(HairColor)).Cast<HairColor>();
		public IEnumerable<HairStyle> HairStyleList { get; set; } = Enum.GetValues(typeof(HairStyle)).Cast<HairStyle>();
		public IEnumerable<SkinColor> SkinColorList { get; set; } = Enum.GetValues(typeof(SkinColor)).Cast<SkinColor>();
		public IEnumerable<EyeColor> EyeColorList { get; set; } = Enum.GetValues(typeof(EyeColor)).Cast<EyeColor>();
		public IEnumerable<Personality> PersonalityList { get; set; } = Enum.GetValues(typeof(Personality)).Cast<Personality>();
		public IEnumerable<Height> HeightList { get; set; } = Enum.GetValues(typeof (Height)).Cast<Height>();

	    public void AddAdventurer(Adventurer newAdventurer)
	    {
	        adventurerRepository.Create(newAdventurer);
        }
	}
}
