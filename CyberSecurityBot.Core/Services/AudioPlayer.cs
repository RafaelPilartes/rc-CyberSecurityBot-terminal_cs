using System;
using System.IO;
using System.Media;

namespace CyberSecurityBot.Core.Services
{
    public class AudioPlayer
    {
        public Action<string> OnError { get; set; }

        public void PlayWav(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return;
                using (var player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Audio error: {ex.Message}");
            }
        }
    }
}
