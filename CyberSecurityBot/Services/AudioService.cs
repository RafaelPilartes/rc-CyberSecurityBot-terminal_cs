using System.Media;

namespace CyberSecurityBot.Services
{
    public class AudioService
    {
        public static void PlayGreetingSound()
        {
            try
            {
                string path = @"Assets/greeting.wav";
                SoundPlayer player = new SoundPlayer(path);
                player.PlaySync();
            }
            catch
            {
                System.Console.WriteLine("[!] Failed to play greeting sound.");
            }
        }
    }
}