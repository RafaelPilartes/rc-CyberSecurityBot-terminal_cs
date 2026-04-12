using CyberSecurityBot.Models;
using CyberSecurityBot.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            while (true)
            {
                Console.Write($"\n{_currentUser.Name}, ask me something: ");
                string userInput = Console.ReadLine()?.Trim();

                if (InputValidator.IsNullOrEmpty(userInput))
                {
                    continue;
                }

                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    EndSession();
                    break;
                }

                ProcessUserQuery(userInput.ToLower());
            }
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

        private void ProcessUserQuery(string query)
        {
            foreach (var responseObj in _responses)
            {
                string[] keywords = ((List<object>)responseObj.keywords).ToArray().Select(x => x.ToString()).ToArray();
                if (InputValidator.ContainsKeyword(query, keywords))
                {
                    UiService.PrintColoredMessage(responseObj.reply.ToString(), ConsoleColor.Cyan);
                    return;
                }
            }
        }

        private void EndSession()
        {
        }
    }
}