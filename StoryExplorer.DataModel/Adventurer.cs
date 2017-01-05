using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace StoryExplorer.DataModel
{
	public class Adventurer : PersistableObject
	{
		private static readonly string StorageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StoryExplorer\\Adventurers\\";

		public string Name { get; set; }
		public string Password { get; set; }
		public Gender Gender { get; set; }
		public HairColor HairColor { get; set; }
		public HairStyle HairStyle { get; set; }
		public SkinColor SkinColor { get; set; }
		public EyeColor EyeColor { get; set; }
		public Personality Personality { get; set; }
		public Height Height { get; set; }
		public DateTime Created { get; set; }		
		public string CurrentRegionName { get; set; }
		public Coordinates CurrentPosition { get; set; }

		private Region currentRegion;
		[XmlIgnoreAttribute]
		public Region CurrentRegion
		{
			get
			{
				if (currentRegion == null && !String.IsNullOrEmpty(CurrentRegionName))
				{
					try
					{
						currentRegion = Region.Load(CurrentRegionName);
					}
					catch
					{
						// eat exception
					}
				}
				return currentRegion;
			}
			set
			{
				currentRegion = value;
			}
		}

		public Adventurer()	{}

		public Adventurer(string name)
		{
			Name = name;
			Created = DateTime.Now;
			New(name);
		}

		public void New(string name)
		{
			VerifyDirectory(StorageFolder);
			string fileName = StorageFolder + name + ".xml";
			try
			{
				New<Adventurer>(fileName);
			}
			catch (IOException)
			{
				throw new IOException($"An adventurer already exists with name '{name}'.");
			}
		}

		public static Adventurer Load(string name)
		{
			VerifyDirectory(StorageFolder);
			string fileName = StorageFolder + name + ".xml";
			try
			{
				return Load<Adventurer>(fileName);
			}
			catch (FileNotFoundException)
			{
				throw new FileNotFoundException($"No saved adventurer found with the name '{name}'.");
			}
		}

		public void Save()
		{
			VerifyDirectory(StorageFolder);
			string fileName = StorageFolder + Name + ".xml";
			Save<Adventurer>(fileName);
		}

		public void Delete()
		{
			string fileName = StorageFolder + Name + ".xml";
			File.Delete(fileName);
		}

		public static List<string> GetNames()
		{
			return DirectoryListing(StorageFolder).ConvertAll(x => x.Substring(0, x.IndexOf(".xml", StringComparison.Ordinal)));
		}
	}
}
