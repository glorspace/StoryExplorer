//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoryExplorer.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Adventurer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adventurer()
        {
            this.Regions = new HashSet<Region>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int GenderId { get; set; }
        public int HairColorId { get; set; }
        public int HairStyleId { get; set; }
        public int SkinColorId { get; set; }
        public int EyeColorId { get; set; }
        public int PersonalityId { get; set; }
        public int HeightId { get; set; }
        public Nullable<int> CurrentRegionId { get; set; }
        public Nullable<int> CurrentPositionX { get; set; }
        public Nullable<int> CurrentPositionY { get; set; }
        public Nullable<int> CurrentPositionZ { get; set; }
        public System.DateTime Created { get; set; }
    
        public virtual EyeColor EyeColor { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual HairColor HairColor { get; set; }
        public virtual HairStyle HairStyle { get; set; }
        public virtual Height Height { get; set; }
        public virtual Personality Personality { get; set; }
        public virtual Region Region { get; set; }
        public virtual SkinColor SkinColor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Region> Regions { get; set; }
    }
}