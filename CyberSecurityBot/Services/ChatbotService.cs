using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using CyberSecurityBot.Models;
using CyberSecurityBot.Utilities;
using CyberSecurityBot.Constants;

namespace CyberSecurityBot.Services
{
    public class ChatbotService
    {
        private List<Response> _responses;
        private User _currentUser;
        private readonly Random _rng = new Random();

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

                ProcessUserQuery(userInput);
            }
        }

        private void InitializeUser()
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            _currentUser = new User { Name = !string.IsNullOrWhiteSpace(name) ? name : "Guest" };

            UiService.PrintColoredMessage($"\nWelcome, {_currentUser.Name}! Let's learn about cybersecurity.", ConsoleColor.Green);

            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("💡 QUICK TIPS:");
            Console.WriteLine("- You can ask about: Passwords, Phishing, or Safe Browsing.");
            Console.WriteLine("- Type 'exit' at any time to close the program.");
            Console.WriteLine("-----------------------------------------------------------");

            System.Threading.Thread.Sleep(700);
        }

        private void LoadResponses()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "responses.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find responses.json file.", filePath);
            }

            string jsonContent = File.ReadAllText(filePath);
            var responsesFile = JsonConvert.DeserializeObject<ResponsesFile>(jsonContent);
            _responses = responsesFile?.responses ?? new List<Response>();
        }

        private void ProcessUserQuery(string query)
        {
            // Build list of candidate responses with score
            var candidates = new List<(Response resp, int score)>();

            foreach (var resp in _responses)
            {
                int score = InputValidator.CountKeywordMatches(query, resp.keywords);
                if (score > 0)
                {
                    candidates.Add((resp, score));
                }
            }

            if (candidates.Count == 0)
            {
                UiService.PrintColoredMessage(BotResponses.InvalidInputResponse, ConsoleColor.Red);
                return;
            }

            // Choose highest score(s)
            int maxScore = candidates.Max(c => c.score);
            var topCandidates = candidates.Where(c => c.score == maxScore).Select(c => c.resp).ToList();

            // If tie, optionally prefer candidate with more keywords matched overall, else random
            Response chosen = topCandidates.Count == 1 ? topCandidates[0] : topCandidates[_rng.Next(topCandidates.Count)];

            // Pick a random reply from chosen.replies
            string reply = chosen.replies != null && chosen.replies.Length > 0
                ? chosen.replies[_rng.Next(chosen.replies.Length)]
                : BotResponses.InvalidInputResponse;

            UiService.PrintColoredMessage(reply, ConsoleColor.Cyan);

            // Optionally print a follow-up suggestion
            if (chosen.followups != null && chosen.followups.Length > 0)
            {
                string follow = chosen.followups[_rng.Next(chosen.followups.Length)];
                UiService.PrintColoredMessage("\nYou might also ask: " + follow, ConsoleColor.DarkGray);
            }
        }

        private void EndSession()
        {
            UiService.PrintColoredMessage(BotResponses.FarewellMessage, ConsoleColor.Magenta);
        }
    }
}