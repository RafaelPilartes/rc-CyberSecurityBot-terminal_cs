using CyberSecurityBot.Services;

namespace CyberSecurityBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var chatbotService = new ChatbotService();
            chatbotService.Start();
        }
    }
}