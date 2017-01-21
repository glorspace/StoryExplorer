using StoryExplorer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace StoryExplorer.WpfApp
{
	public class RegionExplorerViewModel
	{
		public Adventurer Adventurer { get; set; }
		public Region Region { get; set; }
		public Scene CurrentScene { get; set; }
		public RegionMode Mode { get; set; }

		public List<string> NonAuthors
		{
			get
			{
				var nonAuthors = new List<string>();

				if (Region != null)
				{
					nonAuthors = Adventurer.GetAllSavedAdventurers().Select(x => x.Name).Except(Region.DesignatedAuthors).ToList();
					nonAuthors.Remove(Adventurer.Name);
				}

				return nonAuthors;
			}
		}

		public List<string> DesignatedAuthors
		{
			get
			{
				var authors = new List<string>();

				if (Region != null)
				{
					authors = Region.DesignatedAuthors.ToList();
				}

				return authors;
			}
		}

		public void InitializeAdventurer()
		{
			if (Adventurer.CurrentRegionName != Region.Name)
			{
				Adventurer.CurrentRegionName = Region.Name;
				Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			}

			Adventurer.CurrentRegion = Region;

			if (Adventurer.CurrentPosition == null)
			{
				Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			}

			Adventurer.Save();
		}

		public void SetRegionDescription(string description)
		{
			Region.Description = description;
			Region.Save();
		}

		public void AddDesignatedAuthor(string name)
		{
			Region.DesignatedAuthors.Add(name);
			Region.Save();
		}

		public void RemoveDesignatedAuthor(string name)
		{
			Region.DesignatedAuthors.Remove(name);
			Region.Save();
		}

		public void SetCurrentSceneTitle(string title)
		{
			CurrentScene.Title = title;
			Region.Save();
		}

		public void SetCurrentSceneDescription(string description)
		{
			CurrentScene.Description = description;
			Region.Save();
		}

		public void RefreshCurrentScene()
		{
			CurrentScene = Region.GetScene(Adventurer.CurrentPosition);
			CurrentScene.AllowableMoves = Region.GetAllowableMoves(CurrentScene);
		}

		public bool AttemptMove(Direction direction)
		{
			if (Region.GetScene(Adventurer.CurrentPosition.Peek(direction)) != null)
			{
				Adventurer.CurrentPosition.Move(direction);
				Adventurer.Save();
				return true;
			}

			return false;
		}

		public void CreateNewScene(Direction direction)
		{
			var scene = new Scene
			{
				Coordinates = Adventurer.CurrentPosition.Peek(direction),
				Title = String.Empty,
				Description = String.Empty
			};
			CurrentScene = scene;
			//Region.Map.Add(scene);
			//Adventurer.CurrentPosition.Move(direction);
		}

		public void SaveNewScene()
		{
			Region.Map.Add(CurrentScene);
			Region.Save();
			Adventurer.CurrentPosition = new Coordinates(CurrentScene.Coordinates.X, CurrentScene.Coordinates.Y, CurrentScene.Coordinates.Z);
			Adventurer.Save();
		}
	}
}
