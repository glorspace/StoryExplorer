using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace StoryExplorer.DataModel
{
	/// <summary>
	/// Entity class to respresent a story region that an adventurer can explore.
	/// </summary>
	public class Region : PersistableObject
	{
		private static readonly string StorageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\StoryExplorer\\Regions\\";
		private Adventurer owner;

		public Region() { }

		public Region(string name, Adventurer creator) : this()
		{
			Name = name;
			OwnerName = creator.Name;
			Created = DateTime.Now;
			New();
			Owner = Adventurer.Load(OwnerName);
		}

		public override string Name { get; set; }
		public string Description { get; set; }		
		public string OwnerName { get; set; }
		public List<string> DesignatedAuthors { get; set; } = new List<string>();
		public List<Scene> Map { get; set; } = new List<Scene>();
		public DateTime Created { get; set; }		
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

		/// <summary>
		/// Creates a new XML file to persist the data for a newly-created Region instance.
		/// </summary>
		/// 
		public void New() => New<Region>(StorageFolder);

		/// <summary>
		/// Loads a Region from a persisted XML file identified by name.
		/// </summary>
		/// <param name="name">The name of the Region to load.</param>
		/// <returns>A populated Region instance that corresponds to the provided name.</returns>
		public static Region Load(string name) => Load<Region>(name, StorageFolder);

		/// <summary>
		/// Commits data in the Region instance to file.
		/// </summary>
		public void Save() => Save<Region>(StorageFolder);

		/// <summary>
		/// Deletes the persisted data file for this Region.
		/// </summary>
		public void Delete() => Delete<Adventurer>(StorageFolder);

		/// <summary>
		/// Provides a list of all saved Regions on the local system.
		/// </summary>
		/// <returns>A list of all available persisted XML files for Regions.</returns>
		public static List<string> GetNames() => GetNames(StorageFolder);

		/// <summary>
		/// Provides a list of Region instances for all saved Regions on the local system.
		/// </summary>
		/// <returns>A list of Region instances.</returns>
		public static List<Region> GetAllSavedRegions() => GetAll<Region>(StorageFolder);

		/// <summary>
		/// Retrieves the Scene located at the specified position.
		/// </summary>
		/// <param name="position">The location for the Scene in a 3-dimensional grid.</param>
		/// <returns>The Scene instance at the specified location.</returns>
		public Scene GetScene(Coordinates position)
		{
			return Map.Find(scene =>
				scene.Coordinates.X == position.X && 
				scene.Coordinates.Y == position.Y && 
				scene.Coordinates.Z == position.Z);
		}

		/// <summary>
		/// Adds the specified Scene instance to the Map list for the Region.
		/// </summary>
		/// <param name="scene">The Scene instance to add.</param>
		public void AddScene(Scene scene)
		{
			Map.Add(scene);
			Save();
		}

		/// <summary>
		/// Discovers all directions in which a defined Scene could be found if an Adventurer was to move
		/// that way and returns them in a list.
		/// </summary>
		/// <param name="scene">The Scene instance from which potential moves could be made.</param>
		/// <returns>A list of directions in which an Adventurer could move from the specified Scene.</returns>
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
