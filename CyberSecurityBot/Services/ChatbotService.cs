using CyberSecurityBot.Constants;
using CyberSecurityBot.Models;
using CyberSecurityBot.Services;
using CyberSecurityBot.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CyberSecurityBot.Services
{
    public class ChatbotService
    {
        private List<dynamic> _responses;
        private User _currentUser;

        public ChatbotService()
        {
            LoadResponses();
        }

        public void Start()
        {
            UiService.ShowHeader();
            
            AudioService.PlayGreetingSound();

            InitializeUser();

            while (true)
            {
                Console.Write($"\n{_currentUser.Name}, ask me something: ");
                string userInput = Console.ReadLine()?.Trim();

                if (InputValidator.IsNullOrEmpty(userInput))
                {
                    UiService.PrintColoredMessage(BotResponses.InvalidInputResponse, ConsoleColor.Yellow);
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

            // Mensagem de boas-vindas personalizada
            UiService.PrintColoredMessage($"\nWelcome, {_currentUser.Name}! Let's learn about cybersecurity.", ConsoleColor.Green);

            // Instruções de uso e comandos
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("💡 QUICK TIPS:");
            Console.WriteLine("- You can ask about: Passwords, Phishing, or Safe Browsing.");
            Console.WriteLine("- Typing 'exit' will close the program at any time.");
            Console.WriteLine("-----------------------------------------------------------");

            // Pequeno delay opcional para o usuário ler as instruções
            System.Threading.Thread.Sleep(1000);
        }

        private void LoadResponses()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "responses.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find responses.json file.", filePath);
            }

            string jsonContent = File.ReadAllText(filePath);
            dynamic jsonObj = JsonConvert.DeserializeObject(jsonContent);
            _responses = jsonObj.responses.ToObject<List<dynamic>>();
        }

        private void ProcessUserQuery(string query)
        {
            foreach (var responseObj in _responses)
            {
                string[] keywords = responseObj.keywords.ToObject<string[]>();
                if (InputValidator.ContainsKeyword(query, keywords))
                {
                    UiService.PrintColoredMessage(responseObj.reply.ToString(), ConsoleColor.Cyan);
                    return;
                }
            }

            UiService.PrintColoredMessage(BotResponses.InvalidInputResponse, ConsoleColor.Red);
        }

        private void EndSession()
        {
            UiService.PrintColoredMessage(BotResponses.FarewellMessage, ConsoleColor.Magenta);
        }
    }
}