﻿using System.Collections.Generic;
using StoryExplorer.Domain;

namespace StoryExplorer.Repository
{
    public interface ISceneRepository
    {
        void Create(Region region, Scene scene);
        Scene Read(Region region, Coordinates coords);
        void Update(Region region, Coordinates coords, Scene scene);
        void Delete(Region region, Coordinates coords);
        IEnumerable<Direction> GetAllowableMoves(Region region, Adventurer adventurer);
    }
}
