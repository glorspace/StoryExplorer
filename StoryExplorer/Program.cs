using System;
using System.IO;

namespace StoryExplorer.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
			var adventurer = Menus.AdventurerMenu();
			if (adventurer != null)
			{
				var region = Menus.RegionMenu(adventurer);
				if (region != null)
				{
					var engine = new GameEngine(adventurer, region, RegionHelpers.PromptForSpeech());
					engine.ShowScene();
					engine.PromptForCommands();
				}
			}			

			Console.Write("Press enter to exit...");
			Console.ReadLine();
		}
	}
}
