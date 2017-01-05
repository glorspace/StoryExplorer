using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryExplorer.DataModel
{
	public enum Direction
	{
		North,
		East,
		South,
		West,
		Up,
		Down
	}

	public enum Gender
	{
		Male,
		Female
	}
	public enum HairColor
	{
		Blonde,
		Brunette,
		Auburn,
		Ebony
	}

	public enum HairStyle
	{
		Cropped,
		Pigtails,
		Ponytail,
		PageBoy,
		Bun,
		Pixie,
		PixieWithBangs,
		Long,
		CrewCut
	}

	public enum SkinColor
	{
		Cream,
		Olive,
		Golden,
		Chocolate
	}

	public enum EyeColor
	{
		Blue,
		Green,
		Grey,
		Brown,
		Hazel
	}

	public enum Personality
	{
		Stoic,
		Mischievous,
		Boisterous,
		Melancholic,
		Whimsical
	}

	public enum Height
	{
		Short,
		Average,
		Tall
	}

	public enum RegionMode
	{
		Explorer,
		Author
	}
}
