using System;
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
		public virtual string Name { get; set; }

		/// <summary>
		/// Creates a new XML data file for the entity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="folderPath"></param>
		protected void New<T>(string folderPath) where T : PersistableObject
		{
			if (String.IsNullOrWhiteSpace(Name))
			{
				throw new MissingMemberException($"You must assign a name for the {typeof(T).Name} before calling the New() method.");
			}

			VerifyDirectory(folderPath);
			string fileName = folderPath + Name + ".xml";
			try
			{
				using (FileStream stream = new FileStream(fileName, FileMode.CreateNew))
				{
					new XmlSerializer(typeof(T)).Serialize(stream, this);
				}
			}
			catch (IOException)
			{
				throw new IOException($"A saved {typeof(T).Name} already exists with name '{Name}'.");
			}
		}

		/// <summary>
		/// Deserializes XML data to populate an entity class instance based on a specified filename.
		/// </summary>
		/// <typeparam name="T">The entity class type.</typeparam>
		/// <param name="fileName">The XML file to deserialize.</param>
		/// <returns>A populated entity class instance.</returns>
		protected static T Load<T>(string fileName) where T : PersistableObject, new()
		{
			T result;

			using (FileStream stream = File.OpenRead(fileName))
			{
				result = new XmlSerializer(typeof(T)).Deserialize(stream) as T;
			}

			return result;
		}

		/// <summary>
		/// Populates an entity class instance based on a specified name and folder path.
		/// </summary>
		/// <typeparam name="T">The entity class type.</typeparam>
		/// <param name="name">The name for the desired entity.</param>
		/// <param name="folderPath">The folder where instances of this entity type are stored.</param>
		/// <returns>A populated entity class instance.</returns>
		protected static T Load<T>(string name, string folderPath) where T : PersistableObject, new()
		{
			if (String.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException(nameof(name));
			}

			VerifyDirectory(folderPath);
			string fileName = folderPath + name + ".xml";
			try
			{
				return Load<T>(fileName);
			}
			catch (FileNotFoundException)
			{
				throw new FileNotFoundException($"No saved {typeof(T).Name} found with the name '{name}'.");
			}
		}

		/// <summary>
		/// Persists data from the entity class instance to the specified XML file.
		/// </summary>
		/// <typeparam name="T">The entity class type.</typeparam>
		/// <param name="folderPath">The XML file to persist data to.</param>
		protected void Save<T>(string folderPath) where T : PersistableObject
		{
			if(String.IsNullOrWhiteSpace(Name))
			{
				throw new MissingMemberException($"You must assign a name for the {typeof(T).Name} before calling the Save() method.");
			}

			VerifyDirectory(folderPath);
			string fileName = folderPath + Name + ".xml";

			using (FileStream stream = new FileStream(fileName, FileMode.Truncate))
			{
				new XmlSerializer(typeof(T)).Serialize(stream, this);
			}
		}

		/// <summary>
		/// Deletes the persisted data file for this entity class instance.
		/// </summary>
		protected void Delete<T>(string folderPath)
		{
			if (String.IsNullOrWhiteSpace(Name))
			{
				throw new MissingMemberException($"You must assign a name for the {typeof(T).Name} before calling the Delete() method.");
			}

			string fileName = folderPath + Name + ".xml";
			File.Delete(fileName);
		}

		/// <summary>
		/// A helper method that checks for the existence of the specified directory and creates it if it doesn't.
		/// </summary>
		/// <param name="folderPath">The directory to verify.</param>
		protected static void VerifyDirectory(string folderPath)
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
		protected static IEnumerable<string> DirectoryListing(string folderPath)
		{
			VerifyDirectory(folderPath);
			return Directory.EnumerateFiles(folderPath).Select(x => x.Substring(folderPath.Length));
		}

		/// <summary>
		/// Provides a list of the names of all saved entities on the local system.
		/// </summary>
		/// <returns>A list of all available persisted XML files for the specified entity type.</returns>
		protected static IEnumerable<string> GetNames(string folderPath) => DirectoryListing(folderPath).Select(x => x.Substring(0, x.IndexOf(".xml", StringComparison.Ordinal)));

		/// <summary>
		/// Provides a list of Region instances for all saved Regions on the local system.
		/// </summary>
		/// <returns>A list of Region instances.</returns>
		protected static IEnumerable<T> GetAll<T>(string folderPath) where T : PersistableObject, new() => Directory.EnumerateFiles(folderPath)?.Select(Load<T>).ToList();

		/// <summary>
		/// Custom implementation to show a meaningful string representation of the instance.
		/// </summary>
		/// <returns>String representation of the instance based on specified name.</returns>
		public override string ToString() => $"Name: {Name}";
	}
}
