namespace DnDClasses
{
    public class Creature : Entity
    {
        public Creature(string identity, int initiative, int health)
        {
            Identity = identity;
            Initiative = initiative;
            Health = health;
        }
    }
}
