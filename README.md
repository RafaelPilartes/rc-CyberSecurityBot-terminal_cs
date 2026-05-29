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

## 📚 References & Research

The following resources were consulted during the development of Parts 1 and 2.

### Microsoft Official Documentation

- WPF overview and getting started — https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/
- Data binding in WPF — https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/data-binding-overview
- `ObservableCollection<T>` class — https://learn.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1
- `INotifyPropertyChanged` interface — https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged
- `ICommand` and the command pattern in WPF — https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.icommand
- `IValueConverter` interface — https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.ivalueconverter
- The MVVM pattern — https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm
- `System.Media.SoundPlayer` (WAV playback) — https://learn.microsoft.com/en-us/dotnet/api/system.media.soundplayer
- C# delegates (`Action`, `Func`) — https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
- C# generic collections — https://learn.microsoft.com/en-us/dotnet/standard/generics/collections
- Regular expressions in .NET — https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions
- SDK-style project format — https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview

### YouTube Tutorials

- IAmTimCorey — _WPF Tutorial: Build a Real World Application from Scratch_ — https://www.youtube.com/watch?v=Vjldip84CXQ
- IAmTimCorey — _Intro to WPF: Learn the Basics and Best Practices_ — https://www.youtube.com/watch?v=gSfMNjWNoX0
- Brian Lagunas — _MVVM Made Simple in WPF_ — https://www.youtube.com/@BrianLagunas
- Programming with Mosh — _C# Tutorial for Beginners_ — https://www.youtube.com/watch?v=gfkTfcpWqAY

### Articles and Blog Posts

- Josh Smith — _WPF Apps With The Model-View-ViewModel Design Pattern_ (MSDN Magazine, the classic MVVM article) — https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern
- Newtonsoft.Json documentation — https://www.newtonsoft.com/json/help/html/Introduction.htm
- _How to handle exceptions globally in WPF_ — https://wpf-tutorial.com/wpf-application/handling-exceptions/
- _Difference between Action<T> and Func<T>_ — https://stackoverflow.com/questions/4317479/func-vs-action-vs-predicate

### AI Assistance

- **ChatGPT (GPT-4)** — used as a brainstorming partner for the MVVM composition root, for explaining the difference between `ShutdownMode.OnLastWindowClose` and `OnExplicitShutdown` in WPF, and for sanity-checking regex patterns used in `MemoryStore.CaptureFacts`.

### Cybersecurity Content (for the response set)

- NCSC UK — _Top Tips for Staying Secure Online_ — https://www.ncsc.gov.uk/cyberaware/home
- StaySafeOnline.org — phishing and scam awareness — https://staysafeonline.org/
- Have I Been Pwned — password breach awareness — https://haveibeenpwned.com/
- OWASP Top 10 — https://owasp.org/Top10/
- Mozilla — _Privacy Not Included_ — https://foundation.mozilla.org/en/privacynotincluded/

---

🧑‍💻 **Author:** ST10465622 — Rafael Pilartes
