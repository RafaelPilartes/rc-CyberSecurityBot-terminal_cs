using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace CyberSecurityBot.Core.Services.Data
{
    /// <summary>
    /// Holds the MySQL/MariaDB connection settings, loaded from an external
    /// JSON file so the connection string is configurable and never hardcoded
    /// in the middle of the code.
    /// </summary>
    public class DatabaseConfig
    {
        public string Server { get; set; } = "localhost";
        public uint Port { get; set; } = 3306;
        public string User { get; set; } = "root";
        public string Password { get; set; } = "";
        public string Database { get; set; } = "cybersecuritybot";

        /// <summary>
        /// Loads the configuration from a JSON file. Falls back to the defaults
        /// (XAMPP/MariaDB on localhost, root, no password) if the file is missing
        /// or unreadable.
        /// </summary>
        public static DatabaseConfig Load(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var loaded = JsonConvert.DeserializeObject<DatabaseConfig>(json);
                    if (loaded != null) return loaded;
                }
            }
            catch
            {
                // fall through to defaults
            }
            return new DatabaseConfig();
        }

        /// <summary>
        /// Builds the connection string. When <paramref name="includeDatabase"/>
        /// is false the connection points at the server only — used to create the
        /// database itself before it exists.
        /// </summary>
        public string BuildConnectionString(bool includeDatabase = true)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = Server,
                Port = Port,
                UserID = User,
                Password = Password
            };
            if (includeDatabase) builder.Database = Database;
            return builder.ConnectionString;
        }
    }
}
