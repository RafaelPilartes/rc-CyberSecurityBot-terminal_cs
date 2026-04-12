using CyberSecurityBot.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace CyberSecurityBot.Services
{
    public class ChatbotService
    {
        private readonly List<dynamic> _responses;
        private User _currentUser;

        public ChatbotService()
        {
            LoadResponses();
        }

        public void Start()
        {
            AudioService.PlayGreetingSound();
            UiService.ShowHeader();

            InitializeUser();
        }

        private void InitializeUser()
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            _currentUser = new User { Name = !string.IsNullOrWhiteSpace(name) ? name : "Guest" };

            UiService.PrintColoredMessage($"\nWelcome {_currentUser.Name}! Let's learn about cybersecurity.\n", ConsoleColor.Green);
        }

        private void LoadResponses()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "responses.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find responses.json file.", filePath);
            }

            string jsonContent = File.ReadAllText(filePath);
        }
    }
}