using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace DnDClasses.Database
{
    public class DbCreature
    {
        private string connectionString =
            "Data Source=DESKTOP-1PTNL5R;Initial Catalog=EncounterGenerator;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public void AddCreatures(List<Entity> entities, int encounterID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query =
                    "INSERT INTO Creatures (EncounterID, Type, Initiative, Health) VALUES (@EncounterID, @Type, @Initiative, @Health)";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    foreach (Entity entity in entities)
                    {
                        if (entity is Creature)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@EncounterID", encounterID);
                            command.Parameters.AddWithValue("@Type", entity.Identity);
                            command.Parameters.AddWithValue("@Initiative", entity.Initiative);
                            command.Parameters.AddWithValue("@Health", entity.Health);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public void RemoveAllCreaturesByEncounterID(int encounterID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Creatures WHERE EncounterID = @EncounterID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EncounterID", encounterID);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public List<Entity> GetAllCreaturesByEncounterID(int encounterID)
        {
            List<Entity> creatures = new List<Entity>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Type, Initiative, Health FROM Creatures WHERE EncounterID = @EncounterID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EncounterID", encounterID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string type = reader.GetString(0);
                        int initiative = reader.GetInt32(1);
                        int health = reader.GetInt32(2);

                        Creature creature = new Creature(type, initiative, health);
                        creatures.Add(creature);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return creatures;
        }
    }
}

