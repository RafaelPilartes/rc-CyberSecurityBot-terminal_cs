namespace CyberSecurityBot.Core.Models
{
    public class Response
    {
        public string topicKey { get; set; }
        public string[] keywords { get; set; }
        public string[] replies { get; set; }
        public string[] followups { get; set; }
    }
}
