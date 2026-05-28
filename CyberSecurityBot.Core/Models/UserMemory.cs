using System.Collections.Generic;

namespace CyberSecurityBot.Core.Models
{
    public class UserMemory
    {
        public string Name { get; set; }
        public string FavouriteTopic { get; set; }
        public List<string> DiscussedTopics { get; set; } = new List<string>();
    }
}
