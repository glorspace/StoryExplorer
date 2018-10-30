using System;
using System.IO;
using StoryExplorer.Repository;

namespace StoryExplorer.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
            IAdventurerRepository adventurerRepository = new XmlAdventurerRepository();
            IRegionRepository regionRepository = new XmlRegionRepository();
            ISceneRepository sceneRepository = new XmlSceneRepository();

			var adventurer = Menus.AdventurerMenu(adventurerRepository);
			if (adventurer != null)
			{
				var region = Menus.RegionMenu(regionRepository, adventurer);
				if (region != null)
				{
					var engine = new GameEngine(adventurerRepository,
					                            regionRepository,
					                            sceneRepository,
					                            adventurer,
					                            region,
					                            RegionHelpers.PromptForSpeech());
					engine.ShowScene();
					engine.PromptForCommands();
				}
			}			

			Console.Write("Press enter to exit...");
			Console.ReadLine();
		}
	}
}
