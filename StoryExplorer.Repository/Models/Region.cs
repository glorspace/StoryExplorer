using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StoryExplorer.Repository.Models
{
    public class Region
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerName { get; set; }
        public List<string> DesignatedAuthors { get; set; } = new List<string>();
        public List<Scene> Map { get; set; } = new List<Scene>();
        public DateTime Created { get; set; }
        [XmlIgnore]
        public RegionMode Mode { get; set; }

        public Region() { }

        public Region(string name, string creatorName) : this()
        {
            Name = name;
            OwnerName = creatorName;
            Created = DateTime.Now;
        }

        /// <summary>
        /// Custom implementation to show a meaningful string representation of the Region instance.
        /// </summary>
        /// <returns>String representation of the Region instance.</returns>
        public override string ToString() => Name;
    }
}
