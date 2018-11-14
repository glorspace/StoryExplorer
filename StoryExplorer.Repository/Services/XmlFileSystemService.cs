using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StoryExplorer.Repository.Services
{
    /// <summary>
	/// Provides common code for persisting entity data to XML files.
	/// </summary>
	public abstract class XmlFileSystemService
    {
        /// <summary>
        /// Creates a new XML data file for the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="entity"></param>
        /// <param name="folderPath"></param>
        public static void Create<T>(string name, T entity, string folderPath)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            VerifyDirectory(folderPath);
            string fileName = folderPath + name + ".xml";
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.CreateNew))
                {
                    new XmlSerializer(typeof(T)).Serialize(stream, entity);
                }
            }
            catch (IOException)
            {
                throw new IOException($"A saved {typeof(T).Name} already exists with name '{name}'.");
            }
        }

        /// <summary>
        /// Deserializes XML data to populate an entity class instance based on a specified filename.
        /// </summary>
        /// <typeparam name="T">The entity class type.</typeparam>
        /// <param name="fileName">The XML file to deserialize.</param>
        /// <returns>A populated entity class instance.</returns>
        private static T Load<T>(string fileName) where T : class, new()
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
        public static T Load<T>(string name, string folderPath) where T : class, new()
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
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="entity"></param>
        /// <param name="folderPath"></param>
        public static void Save<T>(string name, T entity, string folderPath)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            VerifyDirectory(folderPath);
            string fileName = folderPath + name + ".xml";

            using (FileStream stream = new FileStream(fileName, FileMode.Truncate))
            {
                new XmlSerializer(typeof(T)).Serialize(stream, entity);
            }
        }

        /// <summary>
        /// Deletes the persisted data file for this entity class instance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folderPath"></param>
        public static void Delete(string name, string folderPath)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            
            string fileName = folderPath + name + ".xml";
            File.Delete(fileName);
        }

        /// <summary>
        /// Provides a list of Region instances for all saved Regions on the local system.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folderPath"></param>
        /// <returns>A list of Region instances.</returns>
        public static IEnumerable<T> GetAll<T>(string folderPath) where T : class, new()
        {
            VerifyDirectory(folderPath);
            return Directory.EnumerateFiles(folderPath)?.Select(Load<T>).ToList();
        }

        /// <summary>
        /// Provides a list of the names of all saved entities on the local system.
        /// </summary>
        /// <returns>A list of all available persisted XML files for the specified entity type.</returns>
        public static IEnumerable<string> GetNames(string folderPath) => DirectoryListing(folderPath).Select(x => x.Substring(0, x.IndexOf(".xml", StringComparison.Ordinal)));

        /// <summary>
        /// A helper method that checks for the existence of the specified directory and creates it if it doesn't.
        /// </summary>
        /// <param name="folderPath">The directory to verify.</param>
        private static void VerifyDirectory(string folderPath)
        {
            if (String.IsNullOrWhiteSpace(folderPath)) throw new ArgumentNullException(nameof(folderPath));

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
        private static IEnumerable<string> DirectoryListing(string folderPath)
        {
            VerifyDirectory(folderPath);
            return Directory.EnumerateFiles(folderPath).Select(x => x.Substring(folderPath.Length));
        }
    }
}
