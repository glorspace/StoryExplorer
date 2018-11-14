using StoryExplorer.Repository.Implementations;
using StoryExplorer.Repository.Interfaces;

namespace DataMigrationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IAdventurerRepository sourceAdventurerRepository = new XmlAdventurerRepository();
            IRegionRepository sourceRegionRepository = new XmlRegionRepository();
            IAdventurerRepository destinationAdventurerRepository = new SqlAdventurerRepository();
            IRegionRepository destinationRegionRepository = new SqlRegionRepository();

            MigrateAdventurers(sourceAdventurerRepository, destinationAdventurerRepository);
            MigrateRegions(sourceRegionRepository, destinationRegionRepository);
        }

        private static void MigrateAdventurers(IAdventurerRepository sourceAdventurerRepository, IAdventurerRepository destinationAdventurerRepository)
        {
            var adventurers = sourceAdventurerRepository.ReadAll();

            foreach (var adventurer in adventurers)
            {
                destinationAdventurerRepository.Create(adventurer);
            }
        }

        private static void MigrateRegions(IRegionRepository sourceRegionRepository, IRegionRepository destinationRegionRepository)
        {
            var regions = sourceRegionRepository.ReadAll();

            foreach (var region in regions)
            {
                destinationRegionRepository.Create(region);
            }
        }
    }
}
