using MySql.Data.MySqlClient;

namespace CyberSecurityBot.Core.Services.Data
{
    /// <summary>
    /// Ensures the database and the `tasks` table exist. This is infrastructure
    /// setup (run once at startup); the task CRUD logic lives in
    /// <see cref="TaskRepository"/>.
    /// </summary>
    public class DatabaseInitializer
    {
        private readonly DatabaseConfig _config;

        public DatabaseInitializer(DatabaseConfig config)
        {
            _config = config;
        }

        public void EnsureCreated()
        {
            CreateDatabaseIfMissing();
            CreateTasksTableIfMissing();
        }

        private void CreateDatabaseIfMissing()
        {
            using (var connection = new MySqlConnection(_config.BuildConnectionString(includeDatabase: false)))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText =
                        $"CREATE DATABASE IF NOT EXISTS `{_config.Database}` " +
                        "CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CreateTasksTableIfMissing()
        {
            using (var connection = new MySqlConnection(_config.BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS tasks (
    id            INT AUTO_INCREMENT PRIMARY KEY,
    title         VARCHAR(255) NOT NULL,
    description   TEXT NULL,
    reminder      VARCHAR(255) NULL,
    reminder_date DATETIME NULL,
    is_complete   TINYINT(1) NOT NULL DEFAULT 0,
    created_at    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
