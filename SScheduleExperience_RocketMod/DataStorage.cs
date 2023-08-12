using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SScheduleExperience_RocketMod
{
    public class DataStorage : IDisposable
    {
        public MySqlConnection Connection { get; private set; }

        public DataStorage()
        {
            string connectionString = $"server={PluginMain.Config.mysql_ip};port={PluginMain.Config.mysql_port};user={PluginMain.Config.mysql_username};password={PluginMain.Config.mysql_password};database={PluginMain.Config.mysql_database}";
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            Connection = connection;
        }

        public Dictionary<string, List<string>> Query(string d_steamID)
        {
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            string query = $"SELECT * FROM {PluginMain.Config.mysql_table} WHERE SteamID=\"{d_steamID}\"";
            using (var command = new MySqlCommand(query, Connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string steamID = reader.GetString(0);
                    string playerName = reader.GetString(1);
                    string experience = reader.GetString(2);

                    data[steamID] = new List<string> { playerName, experience };
                }
            }

            if (data.Count == 0)
            {
                return null;
            }

            return data;
        }

        public Dictionary<string, List<string>> QueryAll()
        {
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            string query = $"SELECT * FROM {PluginMain.Config.mysql_table}";
            using (var command = new MySqlCommand(query, Connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string steamID = reader.GetString(0);
                    string playerName = reader.GetString(1);
                    string experience = reader.GetString(2);

                    data[steamID] = new List<string> { playerName, experience };
                }
            }

            return data;
        }

        public void Insert(string steamID, string playerName, string exp)
        {
            string insertQuery = $"INSERT INTO {PluginMain.Config.mysql_table} (SteamID, PlayerName, Experience) VALUES (@val1, @val2, @val3)";
            using (var command = new MySqlCommand(insertQuery, Connection))
            {
                command.Parameters.AddWithValue("@val1", steamID);
                command.Parameters.AddWithValue("@val2", playerName);
                command.Parameters.AddWithValue("@val3", exp);

                command.ExecuteNonQuery();
            }
        }

        public void Update(string steamID, string playerName, string exp)
        {
            string updateQuery = $"UPDATE {PluginMain.Config.mysql_table} SET PlayerName=@val2, Experience=@val3 WHERE SteamID=@val1";
            using (var command = new MySqlCommand(updateQuery, Connection))
            {
                command.Parameters.AddWithValue("@val1", steamID);
                command.Parameters.AddWithValue("@val2", playerName);
                command .Parameters.AddWithValue("@val3", exp);

                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            Connection?.Dispose();
            Connection = null;
        }
    }
}
