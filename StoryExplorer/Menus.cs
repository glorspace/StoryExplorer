using StoryExplorer.DataModel;
using System;

namespace StoryExplorer.ConsoleApp
{
	class Menus
	{
		internal static Adventurer AdventurerMenu()
		{
			Adventurer adventurer = null;
			Console.WriteLine();
			Console.WriteLine("Adventurer Menu:");
			Console.WriteLine("================");
			Console.WriteLine("1) Create a new adventurer");
			Console.WriteLine("2) Load a saved adventurer");
			Console.WriteLine("Q) Quit menu");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					Console.WriteLine();
					adventurer = AdventurerHelpers.CreateAdventurer();
					break;
				case '2':
					Console.WriteLine();
					adventurer = AdventurerHelpers.LoadSavedAdventurer();
					break;
				case 'q':
				case 'Q':
					Console.WriteLine();
					return null;
				default:
					Console.WriteLine();
					Console.Write("Invalid selection. Press enter to continue...");
					Console.ReadLine();
					return AdventurerMenu();
			}

			if (adventurer == null)
			{
				return AdventurerMenu();
			}
			Console.WriteLine();
			Console.WriteLine($"{adventurer.Name} is ready for adventure!");
			return AdventurerLoadedMenu(adventurer);
		}

		private static Adventurer AdventurerLoadedMenu(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("What do you want to do next?");
			Console.WriteLine("============================");
			Console.WriteLine("1) Show adventurer profile");
			Console.WriteLine("2) Change password");
			Console.WriteLine("3) Delete this adventurer");
			Console.WriteLine("4) Choose your story region");
			Console.WriteLine("Q) Quit menu");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					AdventurerHelpers.ShowAdventurerProfile(adventurer);
					return AdventurerLoadedMenu(adventurer);
				case '2':
					AdventurerHelpers.CreatePassword(adventurer);
					return AdventurerLoadedMenu(adventurer);
				case '3':
					if (Menus.Confirm("Are you sure you want to delete this adventurer?"))
					{
						adventurer.Delete();
						return AdventurerMenu();
					}
					return AdventurerLoadedMenu(adventurer);
				case '4':
					return adventurer;
				case 'q':
				case 'Q':
					return null;
				default:
					Console.WriteLine();
					Console.Write("Invalid selection. Press enter to continue...");
					Console.ReadLine();
					Console.WriteLine();
					return AdventurerLoadedMenu(adventurer);
			}
		}

		internal static Region RegionMenu(Adventurer adventurer)
		{
			Region region = null;
			Console.WriteLine();
			Console.WriteLine("Region Menu:");
			Console.WriteLine("============");
			Console.WriteLine("1) Create a new region");
			Console.WriteLine("2) Load a saved region");
			Console.WriteLine("Q) Quit menu");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					region = RegionHelpers.CreateRegion(adventurer);
					break;
				case '2':
					region = RegionHelpers.LoadSavedRegion(adventurer);
					break;
				case 'q':
				case 'Q':
					return null;
				default:
					Console.WriteLine();
					Console.Write("Invalid selection. Press enter to continue...");
					Console.ReadLine();
					return RegionMenu(adventurer);
			}

			if (adventurer.Name == region.OwnerName)
			{
				return RegionOwnerMenu(region);
			}
			else
			{
				return RegionLoadedMenu(region, adventurer);
			}
		}

		private static Region RegionOwnerMenu(Region region)
		{
			Console.WriteLine();
			Console.WriteLine("What do you want to do next?");
			Console.WriteLine("============================");
			Console.WriteLine("1) Show region profile");
			Console.WriteLine("2) Edit region description");
			Console.WriteLine("3) Show designated authors for this region");
			Console.WriteLine("4) Add a designated author");
			Console.WriteLine("5) Remove a designated author");
			Console.WriteLine("6) Enter the story region");
			Console.WriteLine("Q) Quit menu");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					RegionHelpers.ShowRegionProfile(region);
					return RegionOwnerMenu(region);
				case '2':
					RegionHelpers.EditRegionDescription(region);
					return RegionOwnerMenu(region);
				case '3':
					RegionHelpers.ShowDesignatedAuthors(region);
					return RegionOwnerMenu(region);
				case '4':
					RegionHelpers.AddDesignatedAuthor(region);
					return RegionOwnerMenu(region);
				case '5':
					RegionHelpers.RemoveDesignatedAuthor(region);
					return RegionOwnerMenu(region);
				case '6':
					RegionHelpers.ChooseRegionMode(region);
					break;
				case 'q':
				case 'Q':
					return null;
				default:
					Console.WriteLine();
					Console.Write("Invalid selection. Press enter to continue...");
					Console.ReadLine();
					Console.WriteLine();
					return RegionOwnerMenu(region);
			}

			return region;
		}

		private static Region RegionLoadedMenu(Region region, Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("What do you want to do next?");
			Console.WriteLine("============================");
			Console.WriteLine("1) Show region profile");
			Console.WriteLine("2) Enter the story region");
			Console.WriteLine("Q) Quit menu");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					RegionHelpers.ShowRegionProfile(region);
					return RegionLoadedMenu(region, adventurer);
				case '2':
					if (region.DesignatedAuthors.Contains(adventurer.Name))
					{
						RegionHelpers.ChooseRegionMode(region);
					}
					else
					{
						region.Mode = RegionMode.Explorer;
					}

					break;
				case 'q':
				case 'Q':
					return null;
				default:
					Console.WriteLine();
					Console.Write("Invalid selection. Press enter to continue...");
					Console.ReadLine();
					Console.WriteLine();
					return RegionLoadedMenu(region, adventurer);
			}

			return region;
		}

		internal static bool Confirm(string question)
		{
			Console.WriteLine();
			Console.Write(question + " [y/n]: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case 'y':
				case 'Y':
					return true;
				default:
					return false;
			}
		}
	}
}
