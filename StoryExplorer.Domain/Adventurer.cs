using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryExplorer.Domain
{
    public class Adventurer
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public HairColor HairColor { get; set; }
        public HairStyle HairStyle { get; set; }
        public SkinColor SkinColor { get; set; }
        public EyeColor EyeColor { get; set; }
        public Personality Personality { get; set; }
        public Height Height { get; set; }
        public DateTime Created { get; set; }
        public string CurrentRegionName { get; set; }
        public Coordinates CurrentPosition { get; set; }

        public Adventurer() { }

        public Adventurer(string name) : this()
        {
            Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            Created = DateTime.Now;
        }

        /// <summary>
        /// Custom implementation to show a meaningful string representation of the Adventurer instance.
        /// </summary>
        /// <returns>String representation of the Adventurer instance.</returns>
        public override string ToString() => Name;
    }
}
