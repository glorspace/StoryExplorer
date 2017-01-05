using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace StoryExplorer.DataModel
{
	public class Region : PersistableObject
	{
		private static string storageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StoryExplorer\\Regions\\";
		
		public string Name { get; set; }
		public string Description { get; set; }		
		public string OwnerName { get; set; }
		public List<string> DesignatedAuthors { get; set; } = new List<string>();
		public List<Scene> Map { get; set; } = new List<Scene>();
		public DateTime Created { get; set; }

		private Adventurer owner;
		[XmlIgnoreAttribute]
		public Adventurer Owner
		{
			get
			{
				if (owner == null && !String.IsNullOrEmpty(OwnerName))
				{
					owner = Adventurer.Load(OwnerName);
				}
				return owner;
			}
			set
			{
				owner = value;
			}
		}
		[XmlIgnoreAttribute]
		public RegionMode Mode { get; set; }

		public Region() { }

		public Region(string name, Adventurer creator)
		{
			Name = name;
			OwnerName = creator.Name;
			Created = DateTime.Now;
			New(name);
			Owner = Adventurer.Load(OwnerName);
		}

		public void New(string name)
		{
			VerifyDirectory(storageFolder);
			string fileName = storageFolder + name + ".xml";
			try
			{
				New<Region>(fileName);
			}
			catch (IOException)
			{
				throw new IOException($"A region already exists with name '{name}'.");
			}
		}

		public static Region Load(string name)
		{
			VerifyDirectory(storageFolder);
			string fileName = storageFolder + name + ".xml";
			try
			{
				return Load<Region>(fileName);
			}
			catch (FileNotFoundException)
			{
				throw new FileNotFoundException($"No saved region found with the name '{name}'.");
			}
		}

		public void Save()
		{
			VerifyDirectory(storageFolder);
			string fileName = storageFolder + Name + ".xml";
			Save<Region>(fileName);
		}

		public void Delete()
		{
			string fileName = storageFolder + Name + ".xml";
			File.Delete(fileName);
		}

		public static List<string> GetNames()
		{
			return DirectoryListing(storageFolder).ConvertAll(x => x.Substring(0, x.IndexOf(".xml", StringComparison.Ordinal)));
		}

		public Scene GetScene(Coordinates newPosition)
		{
			return Map.Find(scene =>
				scene.Coordinates.X == newPosition.X && 
				scene.Coordinates.Y == newPosition.Y && 
				scene.Coordinates.Z == newPosition.Z);
		}

		public void AddScene(Scene scene)
		{
			Map.Add(scene);
			Save();
		}

		public List<Direction> GetAllowableMoves(Scene scene)
		{
			var allowablesMoves = new List<Direction>();

			if (GetScene(scene.Coordinates.Peek(Direction.North)) != null)
			{
				allowablesMoves.Add(Direction.North);
			}
			if (GetScene(scene.Coordinates.Peek(Direction.East)) != null)
			{
				allowablesMoves.Add(Direction.East);
			}
			if (GetScene(scene.Coordinates.Peek(Direction.South)) != null)
			{
				allowablesMoves.Add(Direction.South);
			}
			if (GetScene(scene.Coordinates.Peek(Direction.West)) != null)
			{
				allowablesMoves.Add(Direction.West);
			}
			if (GetScene(scene.Coordinates.Peek(Direction.Up)) != null)
			{
				allowablesMoves.Add(Direction.Up);
			}
			if (GetScene(scene.Coordinates.Peek(Direction.Down)) != null)
			{
				allowablesMoves.Add(Direction.Down);
			}

			return allowablesMoves;
		}
	}
}
