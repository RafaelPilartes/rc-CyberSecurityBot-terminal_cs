# CyberSecurityBot - Part 1

A modular C# Console Application designed to raise awareness about cybersecurity through an interactive chatbot. This project is the first stage of the Programming 2A Assessment, focusing on clean code, string manipulation, and basic natural language processing (NLP) using keyword matching.

## 🤖 About the Project

The **CyberSecurityBot** is a "semi-intelligent" assistant that provides users with tips on digital safety. It features a personalized interaction system, multimedia elements, and a scoring-based engine to understand user queries.

## Youtube link

https://youtu.be/Fhvue0KN16U

### Key Features

- **Voice Greeting:** Plays a recorded `.wav` file upon launch.
- **ASCII Art:** Displays a professional cybersecurity-themed header.
- **Intelligent Matching:** Uses a scoring system to detect multiple keywords in a single sentence.
- **Dynamic Content:** Loads responses, keywords, and follow-up suggestions from an external `responses.json` file.
- **Input Validation:** Handles empty or unrecognized strings gracefully.
- **Enhanced UI:** Uses colored text, dividers, and structured headers for better readability.

---

## 📁 Project Structure

The project follows a modular architecture to ensure scalability for future GUI implementations:

````text
CyberSecurityBot/
├── Assets/                 # Multimedia files (greeting.wav)
├── Constants/              # Static strings and system messages
│   └── BotResponses.cs
├── Data/                   # Knowledge base
│   └── responses.json      # Intents, keywords, and replies
├── Models/                 # Data structures
│   ├── User.cs
│   ├── Response.cs
│   └── ResponsesFile.cs
├── Services/               # Core business logic
│   ├── AudioService.cs     # Executes audio playback
│   ├── ChatbotService.cs   # Main conversation engine
│   └── UiService.cs        # Console formatting and ASCII display
├── Utilities/              # Helper classes
│   └── InputValidator.cs   # String tokenization and scoring
└── Program.cs              # Application entry point

---

## 🛠️ Technical Implementation

### **Response Engine**
Instead of a simple `switch` statement, the bot tokenizes the user's input and scores it against the intents defined in `responses.json`. The intent with the highest keyword match count is selected, providing a more natural conversational feel.

### **String Manipulation**
The application uses normalization, regex, and tokenization to process user input, ensuring the bot can understand words regardless of punctuation or casing.

### **Auto-Properties**
The `User` model utilizes C# automatic properties to store and retrieve interaction data efficiently.

---

## 🚀 Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) (NuGet Package)

### Running the App
1. Clone the repository:
   ```bash
   git clone https://github.com/[your-username]/CyberSecurityBot.git
````

2. Navigate to the project directory:

   ```bash
   cd CyberSecurityBot
   ```

3. Restore packages and run:
   ```bash
   dotnet run
   ```

📝 Usage Example

1. **Launch**: The app displays ASCII art and plays a voice welcome.
2. **Greeting**: Enter your name when prompted.
3. **Query**: Ask something like "How can I stay safe from phishing?".
4. **Learn**: The bot provides a tip and a follow-up suggestion.
5. **Exit**: Type exit to close the session.

🧑‍💻 Author

ST10465622 - Rafael Pilartes
