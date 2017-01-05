using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace StoryExplorer.DataModel
{
	public class PersistableObject
	{
		public static T Load<T>(string fileName) where T : PersistableObject, new()
		{
			T result;

			using (FileStream stream = File.OpenRead(fileName))
			{
				result = new XmlSerializer(typeof(T)).Deserialize(stream) as T;
			}

			return result;
		}

		public void New<T>(string fileName) where T : PersistableObject
		{
			using (FileStream stream = new FileStream(fileName, FileMode.CreateNew))
			{
				new XmlSerializer(typeof(T)).Serialize(stream, this);
			}
		}

		public void Save<T>(string fileName) where T : PersistableObject
		{
			using (FileStream stream = new FileStream(fileName, FileMode.Truncate))
			{
				new XmlSerializer(typeof(T)).Serialize(stream, this);
			}
		}

		public static void VerifyDirectory(string folderPath)
		{
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}
		}

		public static List<string> DirectoryListing(string folderPath)
		{
			VerifyDirectory(folderPath);
			return Directory.EnumerateFiles(folderPath).ToList().ConvertAll(x => x.Substring(folderPath.Length));
		}
	}
}
