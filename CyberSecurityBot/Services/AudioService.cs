using System.Media;
using System;

namespace CyberSecurityBot.Services
{
    public class AudioService
    {
        public static void PlayGreetingSound()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");

                using (SoundPlayer player = new SoundPlayer(path))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Audio error: {ex.Message}");
            }
        }
    }
}