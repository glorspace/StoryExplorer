using StoryExplorer.Domain;
using StoryExplorer.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace StoryExplorer.ConsoleApp
{
	class AdventurerHelpers
	{
	    private readonly IAdventurerRepository adventurerRepository;
        public AdventurerHelpers(IAdventurerRepository repository)
        {
            adventurerRepository = repository;
        }

		public Adventurer CreateAdventurer()
		{
			string name = String.Empty;
			while (String.IsNullOrEmpty(name))
			{
				Console.Write("Enter a name for your adventurer: ");
				name = Console.ReadLine();
			}

			Adventurer adventurer = new Adventurer(name);

			try
			{
				adventurerRepository.Create(adventurer);
			}
			catch (IOException)
			{
				Console.WriteLine();
				Console.WriteLine("A saved adventurer by that name already exists. You'll need to pick a different name.");
				Console.WriteLine();
				return null;
			}

			CreatePassword(adventurer);
			ChooseGender(adventurer);
			ChooseHairColor(adventurer);
			ChooseHairStyle(adventurer);
			ChooseSkinColor(adventurer);
			ChooseEyeColor(adventurer);
			ChoosePersonality(adventurer);
			ChooseHeight(adventurer);

			return adventurer;
		}

		public Adventurer LoadSavedAdventurer()
		{
		    var names = adventurerRepository.ReadAll().ToList().Select(x => x.Name);

			Console.WriteLine("Adventurers that have already been created:");
			Console.WriteLine("===========================================");
			foreach (var name in names)
			{
				Console.WriteLine($"   {name}");
			}

			try
			{
			    var name = String.Empty;
			    Adventurer adventurer = null;
			    do
			    {
			        do
			        {
			            Console.Write("Enter the name of your adventurer: ");
                        name = Console.ReadLine();
			        } while (String.IsNullOrWhiteSpace(name));

			        adventurer = adventurerRepository.Read(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name));
                    if (adventurer == null)
                        Console.WriteLine("ERROR: Invalid character name. Please try again.");
                } while (adventurer == null);

				
				if (String.IsNullOrEmpty(adventurer.Password))
				{
					NoPasswordFound(adventurer);
				}
				else
				{
					Console.Write("Enter password: ");
					var password = ReadPassword('*');
					while (adventurer.Password != password)
					{
						Console.Write("Incorrect password. Please try again: ");
						password = ReadPassword('*');
					}
				}
				return adventurer;
			}
			catch (FileNotFoundException exc)
			{
				Console.WriteLine();
				Console.WriteLine($"{exc.Message} You can try again with a different name if you would like.");
				Console.Write("Press enter to continue...");
				Console.ReadLine();
				Console.WriteLine();
				return null;
			}
		}

		public void NoPasswordFound(Adventurer adventurer)
		{
			if (Menus.Confirm("No password has been set for this adventurer. Would you like to set one now?"))
			{
				CreatePassword(adventurer);
			}
		}

		public string ReadPassword(char mask)
		{
			var password = String.Empty;
			var key = Console.ReadKey(true);

			while (key.Key != ConsoleKey.Enter)
			{
				if (key.Key == ConsoleKey.Backspace)
				{
					if (!String.IsNullOrEmpty(password))
					{
						password = password.Substring(0, password.Length - 1);
						Console.Write("\b");
						Console.Write(" ");
						Console.Write("\b");
					}
				}
				else if (Char.IsLetterOrDigit(key.KeyChar) || Char.IsSymbol(key.KeyChar) || Char.IsPunctuation(key.KeyChar))
				{
					System.Console.Write(mask);
					password += key.KeyChar;
				}
				key = Console.ReadKey(true);
			}

			System.Console.WriteLine();

			return password;
		}

		public static void ShowAdventurerProfile(Adventurer adventurer)
		{
			if (adventurer != null)
			{
				Console.WriteLine();
				Console.WriteLine("Adventurer Profile:");
				Console.WriteLine("===================");
				Console.WriteLine($"Name: {adventurer.Name}");
				Console.WriteLine($"Gender: {adventurer.Gender}");
				Console.WriteLine($"HairColor: {adventurer.HairColor}");
				Console.WriteLine($"HairStyle: {adventurer.HairStyle}");
				Console.WriteLine($"SkinColor: {adventurer.SkinColor}");
				Console.WriteLine($"EyeColor: {adventurer.EyeColor}");
				Console.WriteLine($"Personality: {adventurer.Personality}");
				Console.WriteLine($"Height: {adventurer.Height}");
				Console.WriteLine($"Created: {adventurer.Created}");
			}
			else
			{
				Console.WriteLine("ERROR: Specified adventurer not found in memory.");
			}
		}

		public void CreatePassword(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.Write("Enter a password that will be required to access this adventurer: ");
			adventurer.Password = ReadPassword('*');
			adventurerRepository.Update(adventurer.Name, adventurer);
		}

		public void ChooseGender(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("Choose Gender:");
			Console.WriteLine("================");
			Console.WriteLine("1) Male");
			Console.WriteLine("2) Female");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					adventurer.Gender = Gender.Male;
					break;
				case '2':
					adventurer.Gender = Gender.Female;
					break;
				default:
					Console.WriteLine("Invalid selection. Press enter to continue...");
					ChooseGender(adventurer);
					break;
			}
		    adventurerRepository.Update(adventurer.Name, adventurer);
        }

		public void ChooseHairColor(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("Choose Hair Color:");
			Console.WriteLine("================");
			Console.WriteLine("1) Blonde");
			Console.WriteLine("2) Brunette");
			Console.WriteLine("3) Auburn");
			Console.WriteLine("4) Ebony");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					adventurer.HairColor = HairColor.Blonde;
					break;
				case '2':
					adventurer.HairColor = HairColor.Brunette;
					break;
				case '3':
					adventurer.HairColor = HairColor.Auburn;
					break;
				case '4':
					adventurer.HairColor = HairColor.Ebony;
					break;
				default:
					Console.WriteLine("Invalid selection. Press enter to continue...");
					ChooseHairColor(adventurer);
					break;
			}
		    adventurerRepository.Update(adventurer.Name, adventurer);
        }

		public void ChooseHairStyle(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("Choose Hair Style:");
			Console.WriteLine("================");
			Console.WriteLine("1) Cropped");
			Console.WriteLine("2) Pigtails");
			Console.WriteLine("3) Ponytail");
			Console.WriteLine("4) Page Boy");
			Console.WriteLine("5) Bun");
			Console.WriteLine("6) Pixie");
			Console.WriteLine("7) Pixie (with bangs)");
			Console.WriteLine("8) Long");
			Console.WriteLine("9) Crew Cut");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					adventurer.HairStyle = HairStyle.Cropped;
					break;
				case '2':
					adventurer.HairStyle = HairStyle.Pigtails;
					break;
				case '3':
					adventurer.HairStyle = HairStyle.Ponytail;
					break;
				case '4':
					adventurer.HairStyle = HairStyle.PageBoy;
					break;
				case '5':
					adventurer.HairStyle = HairStyle.Bun;
					break;
				case '6':
					adventurer.HairStyle = HairStyle.Pixie;
					break;
				case '7':
					adventurer.HairStyle = HairStyle.PixieWithBangs;
					break;
				case '8':
					adventurer.HairStyle = HairStyle.Long;
					break;
				case '9':
					adventurer.HairStyle = HairStyle.CrewCut;
					break;
				default:
					Console.WriteLine("Invalid selection. Press enter to continue...");
					ChooseHairStyle(adventurer);
					break;
			}
		    adventurerRepository.Update(adventurer.Name, adventurer);
        }

		public void ChooseSkinColor(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("Choose Skin Color:");
			Console.WriteLine("================");
			Console.WriteLine("1) Cream");
			Console.WriteLine("2) Olive");
			Console.WriteLine("3) Golden");
			Console.WriteLine("4) Chocolate");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					adventurer.SkinColor = SkinColor.Cream;
					break;
				case '2':
					adventurer.SkinColor = SkinColor.Olive;
					break;
				case '3':
					adventurer.SkinColor = SkinColor.Golden;
					break;
				case '4':
					adventurer.SkinColor = SkinColor.Chocolate;
					break;
				default:
					Console.WriteLine("Invalid selection. Press enter to continue...");
					ChooseSkinColor(adventurer);
					break;
			}
		    adventurerRepository.Update(adventurer.Name, adventurer);
        }

		public void ChooseEyeColor(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("Choose Eye Color:");
			Console.WriteLine("================");
			Console.WriteLine("1) Blue");
			Console.WriteLine("2) Green");
			Console.WriteLine("3) Grey");
			Console.WriteLine("4) Brown");
			Console.WriteLine("5) Hazel");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					adventurer.EyeColor = EyeColor.Blue;
					break;
				case '2':
					adventurer.EyeColor = EyeColor.Green;
					break;
				case '3':
					adventurer.EyeColor = EyeColor.Grey;
					break;
				case '4':
					adventurer.EyeColor = EyeColor.Brown;
					break;
				case '5':
					adventurer.EyeColor = EyeColor.Hazel;
					break;
				default:
					Console.WriteLine("Invalid selection. Press enter to continue...");
					ChooseEyeColor(adventurer);
					break;
			}
		    adventurerRepository.Update(adventurer.Name, adventurer);
        }

		public void ChoosePersonality(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("Choose Personality:");
			Console.WriteLine("================");
			Console.WriteLine("1) Stoic");
			Console.WriteLine("2) Mischievous");
			Console.WriteLine("3) Boisterous");
			Console.WriteLine("4) Melancholic");
			Console.WriteLine("5) Whimsical");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					adventurer.Personality = Personality.Stoic;
					break;
				case '2':
					adventurer.Personality = Personality.Mischievous;
					break;
				case '3':
					adventurer.Personality = Personality.Boisterous;
					break;
				case '4':
					adventurer.Personality = Personality.Melancholic;
					break;
				case '5':
					adventurer.Personality = Personality.Whimsical;
					break;
				default:
					Console.WriteLine("Invalid selection. Press enter to continue...");
					ChoosePersonality(adventurer);
					break;
			}
		    adventurerRepository.Update(adventurer.Name, adventurer);
        }

		public void ChooseHeight(Adventurer adventurer)
		{
			Console.WriteLine();
			Console.WriteLine("Choose Height:");
			Console.WriteLine("================");
			Console.WriteLine("1) Short");
			Console.WriteLine("2) Average");
			Console.WriteLine("3) Tall");
			Console.Write("Enter selection: ");
			var key = Console.ReadKey();
			Console.WriteLine();
			switch (key.KeyChar)
			{
				case '1':
					adventurer.Height = Height.Short;
					break;
				case '2':
					adventurer.Height = Height.Average;
					break;
				case '3':
					adventurer.Height = Height.Tall;
					break;
				default:
					Console.WriteLine("Invalid selection. Press enter to continue...");
					ChooseHeight(adventurer);
					break;
			}
		    adventurerRepository.Update(adventurer.Name, adventurer);
        }
	}
}
