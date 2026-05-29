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
   git clone https://github.com/RafaelPilartes/rc-CyberSecurityBot-terminal_cs
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

---

## Part 2 — WPF GUI

Part 2 wraps the chatbot in a WPF desktop application with a hacker-terminal theme (neon green on dark background). The Part 1 console app remains intact under `CyberSecurityBot/` and is preserved by the `v1.0` git tag.

### Solution Structure

```
CyberSecurityBot.sln
├── CyberSecurityBot/          # Part 1 console (.exe) — intact
├── CyberSecurityBot.Core/     # UI-independent chatbot logic (.dll)
└── CyberSecurityBot.Wpf/      # WPF App (.exe)
```

### Build & Run

```powershell
dotnet build .\CyberSecurityBot.sln -c Debug
.\CyberSecurityBot.Wpf\bin\Debug\net472\CyberSecurityBot.Wpf.exe
```

### Features

- 10 cybersecurity topics with multi-reply random selection
- Follow-up handling (`tell me more`, `another tip`, `more`)
- Persistent memory (name + favourite topic) saved to `%AppData%\CyberSecurityBot\user_memory.json`
- Sentiment detection — empathetic line + relevant tip in a single message
- Input validation (empty / >500 chars) and friendly error handling
- WAV greeting on startup, ASCII art header
- `Action<BotMessage>` delegate between Core and UI (engine → ViewModel)

### Manual Test Plan

See [`TESTING.md`](TESTING.md).

---

## 📚 References

The following resources were consulted during the development of Parts 1 and 2. References are presented in IEEE style and numbered in order of citation.

[1] Microsoft, "Windows Presentation Foundation (WPF) overview," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/. [Accessed: 19-May-2026].

[2] Microsoft, "Data binding overview (WPF .NET)," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/data-binding-overview. [Accessed: 20-May-2026].

[3] Microsoft, "ObservableCollection<T> Class," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1. [Accessed: 20-May-2026].

[4] Microsoft, "INotifyPropertyChanged Interface," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged. [Accessed: 21-May-2026].

[5] Microsoft, "ICommand Interface," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.icommand. [Accessed: 21-May-2026].

[6] Microsoft, "IValueConverter Interface," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.ivalueconverter. [Accessed: 22-May-2026].

[7] Microsoft, "Model-View-ViewModel (MVVM)," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm. [Accessed: 19-May-2026].

[8] Microsoft, "SoundPlayer Class," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/api/system.media.soundplayer. [Accessed: 23-May-2026].

[9] Microsoft, "Delegates (C# Programming Guide)," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/. [Accessed: 24-May-2026].

[10] Microsoft, "Collections (C# and .NET)," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/standard/generics/collections. [Accessed: 22-May-2026].

[11] Microsoft, "Regular expressions in .NET," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions. [Accessed: 23-May-2026].

[12] Microsoft, ".NET project SDK overview," *Microsoft Learn*, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview. [Accessed: 25-May-2026].

[13] J. Smith, "WPF Apps With The Model-View-ViewModel Design Pattern," *MSDN Magazine*, Feb. 2009. [Online]. Available: https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern. [Accessed: 20-May-2026].

[14] Newtonsoft, "Json.NET Documentation," *Newtonsoft*, 2023. [Online]. Available: https://www.newtonsoft.com/json/help/html/Introduction.htm. [Accessed: 21-May-2026].

[15] IAmTimCorey, "WPF Tutorial: Build a Real World Application from Scratch," *YouTube*, 2020. [Online]. Available: https://www.youtube.com/watch?v=Vjldip84CXQ. [Accessed: 24-May-2026].

[16] IAmTimCorey, "Intro to WPF: Learn the Basics and Best Practices," *YouTube*, 2021. [Online]. Available: https://www.youtube.com/watch?v=gSfMNjWNoX0. [Accessed: 25-May-2026].

[17] Programming with Mosh, "C# Tutorial for Beginners," *YouTube*, 2019. [Online]. Available: https://www.youtube.com/watch?v=gfkTfcpWqAY. [Accessed: 19-May-2026].

[18] OpenAI, *ChatGPT (GPT-4)*, large language model. [Online]. Available: https://chat.openai.com/. [Accessed: 26-May-2026].

[19] National Cyber Security Centre, "Cyber Aware: Tips to stay secure online," *NCSC*, 2023. [Online]. Available: https://www.ncsc.gov.uk/cyberaware/home. [Accessed: 27-May-2026].

[20] National Cybersecurity Alliance, "Online Safety and Privacy," *StaySafeOnline*, 2023. [Online]. Available: https://staysafeonline.org/. [Accessed: 27-May-2026].

[21] T. Hunt, "Have I Been Pwned: Check if your email has been compromised in a data breach," 2023. [Online]. Available: https://haveibeenpwned.com/. [Accessed: 28-May-2026].

[22] OWASP Foundation, "OWASP Top 10," *OWASP*, 2021. [Online]. Available: https://owasp.org/Top10/. [Accessed: 28-May-2026].

---

🧑‍💻 **Author:** ST10465622 — Rafael Pilartes
