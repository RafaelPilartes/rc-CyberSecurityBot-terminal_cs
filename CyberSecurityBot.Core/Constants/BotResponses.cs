namespace CyberSecurityBot.Core.Constants
{
    public static class BotResponses
    {
        public const string GreetingPrompt = "Hello! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.";
        public const string FallbackPrefix = "I'm not sure I understand. You can ask me about: ";
        public const string FollowupWithoutContext = "Sure — what topic would you like to know more about?";
        public const string EngineError = "Sorry, I had trouble processing that. Try again.";
        public const string FarewellMessage = "Stay secure and have a great day!";
        public const string EmptyInputNotice = "Please type something.";
        public const string TooLongInputNotice = "Message too long (max 500 characters).";
    }
}
