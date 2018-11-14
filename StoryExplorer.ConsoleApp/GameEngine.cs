using System;
using System.Linq;
using System.Speech.Synthesis;
using StoryExplorer.Repository.Interfaces;
using StoryExplorer.Repository.Models;

namespace StoryExplorer.ConsoleApp
{
	public class GameEngine
	{
		private readonly bool speechEnabled;
		private readonly SpeechSynthesizer synth = new SpeechSynthesizer();
        
	    public IAdventurerRepository AdventurerRepository { get; set; }
	    public IRegionRepository RegionRepository { get; set; }
	    public ISceneRepository SceneRepository { get; set; }
        public Adventurer Adventurer { get; set; }
		public Region Region { get; set; }

		public GameEngine (IAdventurerRepository adventurerRepository, IRegionRepository regionRepository, ISceneRepository sceneRepository, Adventurer adventurer, Region region, bool enableSpeech)
		{
		    AdventurerRepository = adventurerRepository;
		    RegionRepository = regionRepository;
		    SceneRepository = sceneRepository;
            Adventurer = adventurer;
			Region = region;
			speechEnabled = enableSpeech;

			if (Region.Name == Adventurer.CurrentRegionName)
			{
				if (SceneRepository.Read(Region, Adventurer.CurrentPosition) == null)
				{
					Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
				    AdventurerRepository.Update(Adventurer.Name, Adventurer);
				}
			}
			else
			{
				Adventurer.CurrentRegionName = Region.Name;
				Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
			    AdventurerRepository.Update(Adventurer.Name, Adventurer);
            }

			if (Region.OwnerName == null)
			{
				new RegionHelpers(regionRepository).OptionallyAssumeOwnership(Region, Adventurer);
			}
		}

		internal void PromptForCommands()
		{
			while (true)
			{
				Console.Write($"|{Adventurer.Name}:{Region.Name}:[{Adventurer.CurrentPosition.X},{Adventurer.CurrentPosition.Y},{Adventurer.CurrentPosition.Z}] > ");
				var command = Console.ReadLine();

				switch (command?.ToLower())
				{
					case "p":
					case "profile":
						AdventurerHelpers.ShowAdventurerProfile(Adventurer);
						break;
					case "i":
					case "inv":
					case "inventory":
						ShowInventory();
						break;
					case "n":
					case "north":
						AttemptMove(Direction.North);
						break;
					case "e":
					case "east":
						AttemptMove(Direction.East);
						break;
					case "s":
					case "south":
						AttemptMove(Direction.South);
						break;
					case "w":
					case "west":
						AttemptMove(Direction.West);
						break;
					case "u":
					case "up":
						AttemptMove(Direction.Up);
						break;
					case "d":
					case "down":
						AttemptMove(Direction.Down);
						break;
					case "edit":
						if (Region.Mode == RegionMode.Author)
						{
							EditScene();
						}
						else
						{
							Console.WriteLine();
							Console.WriteLine("ERROR: Invalid command.");
						}
						break;
					case "h":
					case "help":
						ShowHelp();
						break;
					case "q":
					case "quit":
					case "x":
					case "exit":
					case "logoff":
					case "logout":
						Console.WriteLine();
						Console.WriteLine("Saving current position in this story region...");
						AdventurerRepository.Update(Adventurer.Name, Adventurer);
						Console.WriteLine();
						Console.WriteLine("Leaving the story world. Goodbye.");
						return;
					default:
						Console.WriteLine();
						Console.WriteLine("ERROR: Invalid command.");
						break;
				}
			}
		}

		private void ShowHelp()
		{
			Console.WriteLine("Directional Commands:");
			Console.WriteLine("   n (or north)");
			Console.WriteLine("   e (or east)");
			Console.WriteLine("   s (or south)");
			Console.WriteLine("   w (or west)");
			Console.WriteLine("   u (or up)");
			Console.WriteLine("   d (or down)");
			Console.WriteLine("Other Commands:");
			Console.WriteLine("   p (or profile)");
			Console.WriteLine("   i (or inv, inventory)");
			Console.WriteLine("   edit (only available if authorized to edit scenes)");
			Console.WriteLine("   q (or quit, x, exit, logoff, logout)");
		}

		private void EditScene()
		{
			var scene = SceneRepository.Read(Region, Adventurer.CurrentPosition);
			
			if (Menus.Confirm("Would you like to edit the scene title?"))
			{
				Console.WriteLine();
				Console.Write("Enter a new title for this scene: ");
				scene.Title = Console.ReadLine();
				RegionRepository.Update(Region.Name, Region);
			}
			
			if (Menus.Confirm("Would you like to edit the scene description?"))
			{
				Console.WriteLine();
				Console.Write("Enter a new description for this scene: ");
				scene.Description = Console.ReadLine();
			    RegionRepository.Update(Region.Name, Region);
            }

			ShowScene();
		}

		private void AttemptMove(Direction direction)
		{
			if (SceneRepository.Read(Region, Adventurer.Peek(direction)) != null)
			{
				Adventurer.Move(direction);
				ShowScene();
			}
			else if (Region.Mode == RegionMode.Explorer)
			{
				Console.WriteLine();
				Console.WriteLine("Try as you might, you are unable to move in that direction.");
			}
			else if (RegionHelpers.ChooseToCreateNewScene())
			{
				SceneRepository.Create(Region, RegionHelpers.CreateNewScene(Adventurer.Peek(direction)));
				Adventurer.Move(direction);
                AdventurerRepository.Update(Adventurer.Name, Adventurer);
				ShowScene();
			}
			else
			{
				ShowScene(false);
			}
		}

		private void ShowScene(bool enableSpeech)
		{
			var scene = Region.Map.Find(x =>
		        x.Coordinates.X == Adventurer.CurrentPosition.X &&
		        x.Coordinates.Y == Adventurer.CurrentPosition.Y &&
		        x.Coordinates.Z == Adventurer.CurrentPosition.Z);

			Console.WriteLine();
			Console.WriteLine($"[ {scene.Title} ]");
			Console.WriteLine($"{scene.Description}");
			Console.Write("[ ");
			SceneRepository.GetAllowableMoves(Region, Adventurer).ToList().ForEach(x => Console.Write(x.ToString() + " "));
			Console.WriteLine("]");

			if (enableSpeech)
			{
				synth.Speak(scene.Title);
				synth.Speak(scene.Description);
			}
		}

		internal void ShowScene()
		{
			ShowScene(speechEnabled);
		}

		private static void ShowInventory()
		{
			Console.WriteLine();
			Console.WriteLine("Feature not yet implemented. Try again later!");
			Console.WriteLine();
		}
	}
}
