namespace CyberSecurityBot.Models
{
    public class Response
    {
        public string[] keywords { get; set; }
        public string[] replies { get; set; }
        public string[] followups { get; set; } // optional suggestions
    }
}