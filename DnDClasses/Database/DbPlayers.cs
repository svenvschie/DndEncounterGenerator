using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace DnDClasses.Database
{
    public class DbPlayers
    {
        private string connectionString = "Data Source=DESKTOP-1PTNL5R;Initial Catalog=EncounterGenerator;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public bool CreatePlayer(int userId, Player player)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Players (UserID, Name, Initiative, Health) VALUES (@UserID, @Name, @Initiative, @Health)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@Name", player.Identity);
                command.Parameters.AddWithValue("@Initiative", player.Initiative);
                command.Parameters.AddWithValue("@Health", player.Health);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public List<Player> GetPlayersByUserID(int userID)
        {
            List<Player> players = new List<Player>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Initiative, Health FROM Players WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        int initiative = reader.GetInt32(1);
                        int health = reader.GetInt32(2);

                        Player player = new Player
                        {
                            Identity = name,
                            Initiative = initiative,
                            Health = health
                        };
                        players.Add(player);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return players;
        }


    }
}
