using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace StoryExplorer.DataModel
{
	/// <summary>
	/// Entity class to represent a character that can be played or navigated through a story region.
	/// </summary>
	public class Adventurer : PersistableObject
	{
		private static readonly string StorageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StoryExplorer\\Adventurers\\";
		private Region currentRegion;


		public Adventurer() { }

		public Adventurer(string name) : this()
		{
			Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
			Created = DateTime.Now;
			New();
		}

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

		/// <summary>
		/// Creates a new XML file to persist the data for a newly-created Adventurer instance.
		/// </summary>
		/// 
		private void New()
		{
			VerifyDirectory(StorageFolder);
			string fileName = StorageFolder + Name + ".xml";
			try
			{
				New<Adventurer>(fileName);
			}
			catch (IOException)
			{
				throw new IOException($"An adventurer already exists with name '{Name}'.");
			}
		}

		/// <summary>
		/// Loads an Adventurer from a persisted XML file identified by name.
		/// </summary>
		/// <param name="name">The name of the Adventurer to load.</param>
		/// <returns>A populated Adventurer instance that corresponds to the provided name.</returns>
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

		/// <summary>
		/// Commits data in the Adventurer instance to file.
		/// </summary>
		public void Save()
		{
			VerifyDirectory(StorageFolder);
			string fileName = StorageFolder + Name + ".xml";
			Save<Adventurer>(fileName);
		}

		/// <summary>
		/// Deletes the persisted data file for this Adventurer.
		/// </summary>
		public void Delete()
		{
			string fileName = StorageFolder + Name + ".xml";
			File.Delete(fileName);
		}

		/// <summary>
		/// Provides a list of the names of all saved Adventurers on the local system.
		/// </summary>
		/// <returns>A list of all available persisted XML files for Adventurers.</returns>
		public static List<string> GetNames()
		{
			return DirectoryListing(StorageFolder).ConvertAll(x => x.Substring(0, x.IndexOf(".xml", StringComparison.Ordinal)));
		}

		/// <summary>
		/// Provides a list of Adventurer instances for all saved Adventurers on the local system.
		/// </summary>
		/// <returns>A list of Adventurer instances.</returns>
		public static List<Adventurer> GetAllSavedAdventurers()
		{
			var names = GetNames();
			var adventurers = new List<Adventurer>();
			foreach (string name in names)
			{
				var adventurer = Load(name);
				adventurers.Add(adventurer);
			}

			return adventurers;
		}

		/// <summary>
		/// Custom implementation to show a meaningful string representation of the Adventurer instance.
		/// </summary>
		/// <returns>String representation of the Adventurer instance.</returns>
		public override string ToString() => $"Name: {Name}";
	}
}
