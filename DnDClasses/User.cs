using DnDClasses.Database;

namespace DnDClasses
{
    public class User
    {
        public int UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public Encounter Encounter { get; }
        public List<Player> Players { get; } = new();
        private DbPlayers dbPlayers = new();
        public Player Player;

        public User(int userId, string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
            UserId = userId;
            Players = dbPlayers.GetPlayersByUserID(UserId);
            Encounter = new(UserId, Players);
        }

        public bool AddPlayer(Player player)
        {
            int maxPlayerAmount = 6;

            if (Players.Count != maxPlayerAmount)
            {
                Players.Add(player);
                Encounter.Entities.Add(player);
                Encounter.SortEntities();
                dbPlayers.CreatePlayer(UserId, player);
                return true;
            }
            return false;
        }

        public void AddEncounter(int maxAmount)
        {
            Encounter.GenerateCreatures(maxAmount, Players);
        }
    }
}