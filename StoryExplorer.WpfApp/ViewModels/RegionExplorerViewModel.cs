
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using StoryExplorer.Domain;
using StoryExplorer.Repository;
using StoryExplorer.WpfApp.Config;

namespace StoryExplorer.WpfApp
{
	public class RegionExplorerViewModel
	{
	    private readonly IAdventurerRepository adventurerRepository;
	    private readonly IRegionRepository regionRepository;
	    private readonly ISceneRepository sceneRepository;

        public RegionExplorerViewModel()
	    {
	        adventurerRepository = new RepositoryConfig().AdventurerRepository;
	        regionRepository = new RepositoryConfig().RegionRepository;
	        sceneRepository = new RepositoryConfig().SceneRepository;
	    }

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
                    nonAuthors = adventurerRepository.ReadAll().Select(x => x.Name).Except(Region.DesignatedAuthors).ToList();
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
            
            adventurerRepository.Update(Adventurer.Name, Adventurer);
		}

		public void SetRegionDescription(string description)
		{
			Region.Description = description;
		    regionRepository.Update(Region.Name, Region);
        }

		public void AddDesignatedAuthor(string name)
		{
			Region.DesignatedAuthors.Add(name);
		    regionRepository.Update(Region.Name, Region);
        }

		public void RemoveDesignatedAuthor(string name)
		{
			Region.DesignatedAuthors.Remove(name);
		    regionRepository.Update(Region.Name, Region);
        }

		public void SetCurrentSceneTitle(string title)
		{
			CurrentScene.Title = title;
		    regionRepository.Update(Region.Name, Region);
        }

		public void SetCurrentSceneDescription(string description)
		{
			CurrentScene.Description = description;
		    regionRepository.Update(Region.Name, Region);
        }

		public void RefreshCurrentScene()
		{
		    CurrentScene = sceneRepository.Read(Region, Adventurer.CurrentPosition);
		    CurrentScene.AllowableMoves = sceneRepository.GetAllowableMoves(Region, Adventurer).ToList();
		}

		public bool AttemptMove(Direction direction)
		{
			if (sceneRepository.Read(Region, Adventurer.CurrentPosition) != null)
			{
				Adventurer.Move(direction);
			    adventurerRepository.Update(Adventurer.Name, Adventurer);
                return true;
			}

			return false;
		}

		public void CreateNewScene(Direction direction)
		{
			var scene = new Scene
			{
				Coordinates = Adventurer.Peek(direction),
				Title = String.Empty,
				Description = String.Empty
			};
			CurrentScene = scene;
		}

		public void SaveNewScene()
		{
			Region.Map.Add(CurrentScene);
		    regionRepository.Update(Region.Name, Region);
			Adventurer.CurrentPosition = new Coordinates(CurrentScene.Coordinates.X, CurrentScene.Coordinates.Y, CurrentScene.Coordinates.Z);
		    adventurerRepository.Update(Adventurer.Name, Adventurer);
        }
	}
}
