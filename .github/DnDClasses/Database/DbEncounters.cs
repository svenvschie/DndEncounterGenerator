using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace DnDClasses.Database
{
    public class DbEncounters
    {
        private string connectionString = "Data Source=DESKTOP-1PTNL5R;Initial Catalog=EncounterGenerator;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public int GetEncounterID(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT EncounterID FROM Encounters WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int encounterID = reader.GetInt32(0);
                        return encounterID;
                    }
                    return -1; // Indicates encounter not found for the user
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return -1; // Indicates an error occurred
                }
            }
        }

    }
}
