using System;
using System.Collections.Generic;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Core.Services.Data
{
    /// <summary>
    /// Data-access layer for cybersecurity tasks stored in MySQL.
    ///
    /// STRUCTURE ONLY — the CRUD bodies are implemented in a later Part 3 step.
    /// The method signatures are fixed here so the ViewModels can be wired up.
    /// </summary>
    public class TaskRepository
    {
        private readonly DatabaseConfig _config;

        public TaskRepository(DatabaseConfig config)
        {
            _config = config;
        }

        /// <summary>Returns all tasks, newest first.</summary>
        public List<CyberTask> GetAll()
        {
            throw new NotImplementedException("Part 3 Task 1 — implemented in a later step.");
        }

        /// <summary>Inserts a task and returns its new id.</summary>
        public int Add(CyberTask task)
        {
            throw new NotImplementedException("Part 3 Task 1 — implemented in a later step.");
        }

        /// <summary>Marks the task with the given id as complete.</summary>
        public void MarkComplete(int id)
        {
            throw new NotImplementedException("Part 3 Task 1 — implemented in a later step.");
        }

        /// <summary>Deletes the task with the given id.</summary>
        public void Delete(int id)
        {
            throw new NotImplementedException("Part 3 Task 1 — implemented in a later step.");
        }
    }
}
