using System;
using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.ConsoleApp
{
	class Menus
	{
		internal static Adventurer AdventurerMenu(IAdventurerRepository adventurerRepository)
		{
            var helpers = new AdventurerHelpers(adventurerRepository);

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
					adventurer = helpers.CreateAdventurer();
					break;
				case '2':
					Console.WriteLine();
					adventurer = helpers.LoadSavedAdventurer();
					break;
				case 'q':
				case 'Q':
					Console.WriteLine();
					return null;
				default:
					Console.WriteLine();
					Console.Write("Invalid selection. Press enter to continue...");
					Console.ReadLine();
					return AdventurerMenu(adventurerRepository);
			}

			if (adventurer == null)
			{
				return AdventurerMenu(adventurerRepository);
			}
			Console.WriteLine();
			Console.WriteLine($"{adventurer.Name} is ready for adventure!");
			return AdventurerLoadedMenu(adventurerRepository, adventurer);
		}

		private static Adventurer AdventurerLoadedMenu(IAdventurerRepository adventurerRepository, Adventurer adventurer)
		{
		    var helpers = new AdventurerHelpers(adventurerRepository);

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
					return AdventurerLoadedMenu(adventurerRepository, adventurer);
				case '2':
					helpers.CreatePassword(adventurer);
					return AdventurerLoadedMenu(adventurerRepository, adventurer);
				case '3':
					if (Menus.Confirm("Are you sure you want to delete this adventurer?"))
					{
					    adventurerRepository.Delete(adventurer.Name);
						return AdventurerMenu(adventurerRepository);
					}
					return AdventurerLoadedMenu(adventurerRepository, adventurer);
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
					return AdventurerLoadedMenu(adventurerRepository, adventurer);
			}
		}

		internal static Region RegionMenu(IRegionRepository regionRepository, Adventurer adventurer)
		{
            var helpers = new RegionHelpers(regionRepository);

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
					region = helpers.CreateRegion(adventurer);
					break;
				case '2':
					region = helpers.LoadSavedRegion(adventurer);
					break;
				case 'q':
				case 'Q':
					return null;
				default:
					Console.WriteLine();
					Console.Write("Invalid selection. Press enter to continue...");
					Console.ReadLine();
					return RegionMenu(regionRepository, adventurer);
			}

			if (adventurer.Name == region.OwnerName)
			{
				return RegionOwnerMenu(regionRepository, region);
			}
			else
			{
				return RegionLoadedMenu(region, adventurer);
			}
		}

		private static Region RegionOwnerMenu(IRegionRepository regionRepository, Region region)
		{
            var helpers = new RegionHelpers(regionRepository);

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
					return RegionOwnerMenu(regionRepository, region);
				case '2':
					helpers.EditRegionDescription(region);
					return RegionOwnerMenu(regionRepository, region);
				case '3':
					RegionHelpers.ShowDesignatedAuthors(region);
					return RegionOwnerMenu(regionRepository, region);
				case '4':
					helpers.AddDesignatedAuthor(region);
					return RegionOwnerMenu(regionRepository, region);
				case '5':
					helpers.RemoveDesignatedAuthor(region);
					return RegionOwnerMenu(regionRepository, region);
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
					return RegionOwnerMenu(regionRepository, region);
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
