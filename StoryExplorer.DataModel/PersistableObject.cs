using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace StoryExplorer.DataModel
{
	/// <summary>
	/// Base class that provides common code for persisting entity data to XML files.
	/// </summary>
	public class PersistableObject
	{
		/// <summary>
		/// Deserializes XML data to populate an entity class instance.
		/// </summary>
		/// <typeparam name="T">The entity class type.</typeparam>
		/// <param name="fileName">The XML file to deserialize.</param>
		/// <returns>A populated entity class instance.</returns>
		public static T Load<T>(string fileName) where T : PersistableObject, new()
		{
			T result;

			using (FileStream stream = File.OpenRead(fileName))
			{
				result = new XmlSerializer(typeof(T)).Deserialize(stream) as T;
			}

			return result;
		}

		/// <summary>
		/// Creates a new XML data file with the specified filename.
		/// </summary>
		/// <typeparam name="T">The entity class type.</typeparam>
		/// <param name="fileName">The XML file to create.</param>
		public void New<T>(string fileName) where T : PersistableObject
		{
			using (FileStream stream = new FileStream(fileName, FileMode.CreateNew))
			{
				new XmlSerializer(typeof(T)).Serialize(stream, this);
			}
		}

		/// <summary>
		/// Persists data from the entity class instance to the specified XML file.
		/// </summary>
		/// <typeparam name="T">The entity class type.</typeparam>
		/// <param name="fileName">The XML file to persist data to.</param>
		public void Save<T>(string fileName) where T : PersistableObject
		{
			using (FileStream stream = new FileStream(fileName, FileMode.Truncate))
			{
				new XmlSerializer(typeof(T)).Serialize(stream, this);
			}
		}

		/// <summary>
		/// A helper method that checks for the existence of the specified directory and creates it if it doesn't.
		/// </summary>
		/// <param name="folderPath">The directory to verify.</param>
		public static void VerifyDirectory(string folderPath)
		{
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}
		}
		
		/// <summary>
		/// Reads the specified directory and returns the names of the files in a list.
		/// </summary>
		/// <param name="folderPath">The directory to read.</param>
		/// <returns>A list of files in the directory.</returns>
		public static List<string> DirectoryListing(string folderPath)
		{
			VerifyDirectory(folderPath);
			return Directory.EnumerateFiles(folderPath).ToList().ConvertAll(x => x.Substring(folderPath.Length));
		}
	}
}
