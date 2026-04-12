using System;

namespace CyberSecurityBot.Services
{
    public class UiService
    {
        public static void ShowHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════════╗
║                                                                           ║
║       ██████╗██╗   ██╗███████╗████████╗██████╗  █████╗ ███████╗████████╗  ║
║      ██╔════╝╚██╗ ██╔╝██╔════╝╚══██╔══╝██╔══██╗██╔══██╗██╔════╝╚══██╔══╝  ║
║      ██║      ╚████╔╝ █████╗     ██║   ██████╔╝███████║█████╗     ██║     ║
║      ██║       ╚██╔╝  ██╔══╝     ██║   ██╔══██╗██╔══██║██╔══╝     ██║     ║
║      ╚██████╗   ██║   ███████╗   ██║   ██║  ██║██║  ██║███████╗   ██║     ║
║       ╚═════╝   ╚═╝   ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝   ╚═╝     ║
║                                                                           ║
╚═══════════════════════════════════════════════════════════════════════════╝
");
            Console.ResetColor();
        }

        public static void PrintColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}