using System.Collections.Generic;
using CyberSecurityBot.Core.Models;
using MySql.Data.MySqlClient;

namespace CyberSecurityBot.Core.Services.Data
{
    /// <summary>
    /// Data-access layer for cybersecurity tasks stored in MySQL/MariaDB.
    /// All queries are parameterised to avoid SQL injection.
    /// </summary>
    public class TaskRepository
    {
        private readonly DatabaseConfig _config;

        public TaskRepository(DatabaseConfig config)
        {
            _config = config;
        }

        private MySqlConnection OpenConnection()
        {
            var connection = new MySqlConnection(_config.BuildConnectionString());
            connection.Open();
            return connection;
        }

        /// <summary>Returns all tasks, newest first.</summary>
        public List<CyberTask> GetAll()
        {
            var tasks = new List<CyberTask>();
            using (var connection = OpenConnection())
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText =
                    "SELECT id, title, description, reminder, reminder_date, is_complete, created_at " +
                    "FROM tasks ORDER BY created_at DESC, id DESC;";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new CyberTask
                        {
                            Id = reader.GetInt32("id"),
                            Title = reader.GetString("title"),
                            Description = reader.IsDBNull(reader.GetOrdinal("description"))
                                ? null : reader.GetString("description"),
                            Reminder = reader.IsDBNull(reader.GetOrdinal("reminder"))
                                ? null : reader.GetString("reminder"),
                            ReminderDate = reader.IsDBNull(reader.GetOrdinal("reminder_date"))
                                ? (System.DateTime?)null : reader.GetDateTime("reminder_date"),
                            IsComplete = reader.GetBoolean("is_complete"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return tasks;
        }

        /// <summary>Inserts a task and returns its new id.</summary>
        public int Add(CyberTask task)
        {
            using (var connection = OpenConnection())
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO tasks (title, description, reminder, reminder_date, is_complete) " +
                    "VALUES (@title, @description, @reminder, @reminderDate, @isComplete);";
                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@description", (object)task.Description ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@reminder", (object)task.Reminder ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@reminderDate", (object)task.ReminderDate ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@isComplete", task.IsComplete);
                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
        }

        /// <summary>Marks the task with the given id as complete.</summary>
        public void MarkComplete(int id)
        {
            using (var connection = OpenConnection())
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE tasks SET is_complete = 1 WHERE id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Deletes the task with the given id.</summary>
        public void Delete(int id)
        {
            using (var connection = OpenConnection())
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM tasks WHERE id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
