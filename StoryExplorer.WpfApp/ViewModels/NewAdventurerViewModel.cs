using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryExplorer.WpfApp
{
	public class NewAdventurerViewModel
	{
		public List<Gender> GenderList { get; set; } = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
		public List<HairColor> HairColorList { get; set; } = Enum.GetValues(typeof(HairColor)).Cast<HairColor>().ToList();
		public List<HairStyle> HairStyleList { get; set; } = Enum.GetValues(typeof(HairStyle)).Cast<HairStyle>().ToList();
		public List<SkinColor> SkinColorList { get; set; } = Enum.GetValues(typeof(SkinColor)).Cast<SkinColor>().ToList();
		public List<EyeColor> EyeColorList { get; set; } = Enum.GetValues(typeof(EyeColor)).Cast<EyeColor>().ToList();
		public List<Personality> PersonalityList { get; set; } = Enum.GetValues(typeof(Personality)).Cast<Personality>().ToList();
		public List<Height> HeightList { get; set; } = Enum.GetValues(typeof(Height)).Cast<Height>().ToList();
	}
}
