using System.Diagnostics;
using DnDClasses.Database;

namespace DnDClasses
{
    public class Encounter
    {
        public int EncounterId { get; }
        public List<Entity> Entities { get; private set; } = new();
        private DbEncounters dbEncounters = new();
        private DbCreature dbCreature = new();

        public Encounter(int userId, List<Player> players)
        {
            EncounterId = dbEncounters.GetEncounterID(userId);
            Entities = dbCreature.GetAllCreaturesByEncounterID(EncounterId);
            Entities.AddRange(players);
            SortEntities();
        }

        public void SortEntities()
        {
            Entities = Entities.OrderByDescending(entity => entity.Initiative).ToList();
        }

        public void GenerateCreatures(int maxAmount, List<Player> players)
        {
            dbCreature.RemoveAllCreaturesByEncounterID(EncounterId);
            Entities.Clear();
            Entities.AddRange(players);

            Random random = new Random();
            int amount = random.Next(1, maxAmount + 1);

            for (int i = 0; i < amount; i++)
            {
                int InititativeRnd = random.Next(0, 22);
                int healthRnd = random.Next(7, 14);
                Creature creature = new("Goblin", InititativeRnd, healthRnd);
                Entities.Add(creature);
            }
            dbCreature.AddCreatures(Entities, EncounterId);
            SortEntities();
        }
    }
}
