using System;
using System.IO;
using System.Speech.Synthesis;
using StoryExplorer.DataModel;

namespace StoryExplorer.ConsoleApp
{
	public class GameEngine
	{
		private readonly bool speechEnabled;
		private readonly SpeechSynthesizer synth = new SpeechSynthesizer();
		public Adventurer Adventurer { get; set; }
		public Region Region { get; set; }

		public GameEngine (Adventurer adventurer, Region region, bool enableSpeech)
		{
			Adventurer = adventurer;
			Region = region;
			speechEnabled = enableSpeech;

			if (Region.Name == Adventurer.CurrentRegion?.Name)
			{
				if (Region.GetScene(Adventurer.CurrentPosition) == null)
				{
					Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
					Adventurer.Save();
				}
			}
			else
			{
				Adventurer.CurrentRegionName = Region.Name;
				Adventurer.CurrentPosition = new Coordinates(0, 0, 0);
				Adventurer.Save();
			}

			if (Region.Owner?.Name == null)
			{
				RegionHelpers.OptionallyAssumeOwnership(Region, Adventurer);
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
						Adventurer.Save();
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
			var scene = Region.GetScene(Adventurer.CurrentPosition);
			
			if (Menus.Confirm("Would you like to edit the scene title?"))
			{
				Console.WriteLine();
				Console.Write("Enter a new title for this scene: ");
				scene.Title = Console.ReadLine();
				Region.Save();
			}
			
			if (Menus.Confirm("Would you like to edit the scene description?"))
			{
				Console.WriteLine();
				Console.Write("Enter a new description for this scene: ");
				scene.Description = Console.ReadLine();
				Region.Save();
			}

			ShowScene();
		}

		private void AttemptMove(Direction direction)
		{
			if (Region.GetScene(Adventurer.CurrentPosition.Peek(direction)) != null)
			{
				Adventurer.CurrentPosition.Move(direction);
				ShowScene();
			}
			else if (Region.Mode == RegionMode.Explorer)
			{
				Console.WriteLine();
				Console.WriteLine("Try as you might, you are unable to move in that direction.");
			}
			else if (RegionHelpers.ChooseToCreateNewScene())
			{
				Region.AddScene(RegionHelpers.CreateNewScene(Adventurer.CurrentPosition.Peek(direction)));
				Adventurer.CurrentPosition.Move(direction);
				ShowScene();
			}
			else
			{
				ShowScene(false);
			}
		}

		private void ShowScene(bool enableSpeech)
		{
			var scene = Region.GetScene(Adventurer.CurrentPosition);
			Console.WriteLine();
			Console.WriteLine($"[ {scene.Title} ]");
			Console.WriteLine($"{scene.Description}");
			Console.Write("[ ");
			Region.GetAllowableMoves(scene).ForEach(x => Console.Write(x.ToString() + " "));
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
