using System;
using System.Globalization;
using System.IO;
using System.Linq;
using StoryExplorer.Domain;
using StoryExplorer.Repository;

namespace StoryExplorer.ConsoleApp
{
	class RegionHelpers
	{
	    private readonly IRegionRepository regionRepository;

	    public RegionHelpers(IRegionRepository repository)
	    {
	        regionRepository = repository;
	    }

        internal static void ChooseRegionMode(Region region)
		{
			if (Menus.Confirm("Would you like to enable author mode for this session?"))
			{
				region.Mode = RegionMode.Author;
				Console.WriteLine("Enabling Author Mode. You will be able to add and edit scenes for this region as you explore.");
			}
			else
			{
				region.Mode = RegionMode.Explorer;
				Console.WriteLine("Setting Explorer Mode. You will experience the story region as an intrepid explorer.");
			}
		}

		internal void EditRegionDescription(Region region)
		{
			ShowRegionProfile(region);
			Console.WriteLine();
			Console.Write("Enter a new description for this region: ");
			var newDescription = Console.ReadLine();
			if (!String.IsNullOrEmpty(newDescription))
			{
				region.Description = newDescription;
				regionRepository.Update(region.Name, region);
			}
			else
			{
				Console.Write("No text entered. Region description was not changed. Press enter to continue...");
				Console.ReadLine();
			}
			ShowRegionProfile(region);
		}

		internal static bool ShowDesignatedAuthors(Region region)
		{
			Console.WriteLine();
			Console.WriteLine("Designated authors for this region:");
			Console.WriteLine("===================================");
			if (region.DesignatedAuthors.Count > 0)
			{
				foreach (var name in region.DesignatedAuthors)
				{
					Console.WriteLine($"   {name}");
				}
				return true;
			}
			else
			{
				Console.WriteLine("   No designated authors yet defined.");
				return false;
			}
		}

		internal void AddDesignatedAuthor(Region region)
		{
			ShowDesignatedAuthors(region);
			Console.Write("Enter the name of an adventurer that you want to allow to author scenes in this region: ");
			var author = Console.ReadLine();
			if (!String.IsNullOrEmpty(author))
			{
				author = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author);
				if (region.DesignatedAuthors.Find(x => x == author) == null && author != region.OwnerName)
				{
					region.DesignatedAuthors.Add(author);
				    regionRepository.Update(region.Name, region);
                }
			}
			else
			{
				Console.Write("No name entered. Press enter to continue...");
				Console.ReadLine();
			}
		}

		internal void RemoveDesignatedAuthor(Region region)
		{
			if (ShowDesignatedAuthors(region))
			{
				Console.Write("Enter the name of the adventurer you wish to remove author rights for in this region: ");
				var author = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
				if (region.DesignatedAuthors.Find(x => x == author) != null)
				{
					region.DesignatedAuthors.Remove(author);
				    regionRepository.Update(region.Name, region);
                }
				else
				{
					Console.WriteLine("The name you entered is not among those designated to author scenes in this region.");
				}
			}
			else
			{
				Console.WriteLine("There are no designated authors for this region.");
			}
		}

		internal Region CreateRegion(Adventurer creator)
		{
			var name = String.Empty;
			while (String.IsNullOrEmpty(name))
			{
				Console.WriteLine();
				Console.Write("Enter a name for this new region: ");
				name = Console.ReadLine();
			}

			Region region = new Region(name, creator.Name);

		    try
		    {
		        regionRepository.Create(region);
		    }
		    catch (IOException)
		    {
		        Console.WriteLine("A saved region by that name already exists. You'll need to pick a different name.");
		        return CreateRegion(creator);
		    }
            
			region = new Region(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name), creator.Name);
			Console.Write("Describe this new region in a few sentences: ");
			region.Description = Console.ReadLine();
			regionRepository.Update(region.Name, region);
            Console.Write("Your new region needs an opening scene. Press enter to continue...");
			Console.ReadLine();
			region.Map.Add(CreateNewScene(new Coordinates(0, 0, 0)));
			regionRepository.Update(region.Name, region);

			return region;
		}

		internal Region LoadSavedRegion(Adventurer creator)
		{
			var names = regionRepository.ReadAll().ToList().Select(x => x.Name);

			Console.WriteLine();
			Console.WriteLine("Regions that have already been created:");
			Console.WriteLine("=======================================");
			foreach (var name in names)
			{
				Console.WriteLine($"   {name}");
			}

			try
			{
				Console.Write("Enter the name of the region: ");
			    var name = String.Empty;
			    do
			    {
			        name = Console.ReadLine();
			    } while (String.IsNullOrWhiteSpace(name));

                return regionRepository.Read(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name));
			}
			catch (FileNotFoundException exc)
			{
				Console.WriteLine($"{exc.Message} You can try again with a different name if you would like.");
				Console.Write("Press enter to continue...");
				Console.ReadLine();
				return Menus.RegionMenu(regionRepository, creator);
			}
		}

		internal static void ShowRegionProfile(Region region)
		{
			if (region != null)
			{
				Console.WriteLine();
				Console.WriteLine("Region Profile:");
				Console.WriteLine("===============");
				Console.WriteLine($"Name: {region.Name}");
				Console.WriteLine($"Description: {region.Description}");
				Console.WriteLine($"Number of Scenes: {region.Map.Count}");
				Console.WriteLine($"Created: {region.Created}");
			}
			else
			{
				Console.WriteLine("ERROR: Specified adventurer not found in memory.");
			}
		}

		internal static bool ChooseToCreateNewScene()
		{
			Console.WriteLine();
			Console.Write("This scene has not yet been written. Would you like to create it now? [y/n]: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case 'y':
				case 'Y':
					return true;
				case 'n':
				case 'N':
					return false;
				default:
					Console.Write("Invalid selection. Press enter to continue...");
					Console.ReadLine();
					return ChooseToCreateNewScene();
			}
		}

		internal static Scene CreateNewScene(Coordinates coordinates)
		{
		    var scene = new Scene { Coordinates = coordinates };

		    Console.WriteLine();
			Console.Write("Enter a title for this scene: ");
			scene.Title = Console.ReadLine();
			Console.WriteLine();
			Console.Write("Describe this scene in a few sentences: ");
			scene.Description = Console.ReadLine();
			return scene;
		}

		internal void OptionallyAssumeOwnership(Region region, Adventurer adventurer)
		{
			if (Menus.Confirm("This region does not appear to have an owner. Would you like to assume ownership of this region?"))
			{
				region.OwnerName = adventurer.Name;
			    regionRepository.Update(region.Name, region);
			}
		}

		internal static bool PromptForSpeech()
		{
			if (Menus.Confirm("Would you like the program to read the story to you as you explore the region?"))
			{
				return true;
			}

			return false;
		}
	}
}
