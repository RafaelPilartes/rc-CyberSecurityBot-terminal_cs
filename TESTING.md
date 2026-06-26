# Manual Test Plan — Part 2 (WPF)

Run after `dotnet build` succeeds. Launch `CyberSecurityBot.Wpf\bin\Debug\net472\CyberSecurityBot.Wpf.exe`.

## First-Launch Flow
- [ ] Delete `%AppData%\CyberSecurityBot\user_memory.json` first.
- [ ] WAV greeting plays.
- [ ] Name dialog appears. Type "Alice", click OK.
- [ ] Main window shows ASCII art header, dark background, neon green text.
- [ ] Welcome bot bubble lists all topic keys.

## Keyword Recognition (≥8)
For each, type the prompt and confirm a relevant reply:
- [ ] `password` — password tip
- [ ] `phishing` — phishing tip
- [ ] `scam` — scam tip
- [ ] `privacy` — privacy tip
- [ ] `malware` — malware tip
- [ ] `wifi` — wifi tip
- [ ] `2fa` — 2FA tip
- [ ] `safe browsing` — browsing tip
- [ ] `updates` — updates tip
- [ ] `social engineering` — social engineering tip

## Random Replies
- [ ] Ask `password` 4 times — replies vary across calls.

## Follow-ups
- [ ] Ask `password`, then `tell me more` — bot stays on password.
- [ ] Type `more` after a fresh launch (no prior topic) — bot asks what to expand.

## Memory & Recall
- [ ] Say `my name is Bob` — memory updates (verify via JSON file).
- [ ] Say `I'm interested in privacy` — `FavouriteTopic` becomes `privacy`.
- [ ] Ask several non-privacy topics — occasionally bot prepends "As someone interested in privacy…".
- [ ] Close + reopen — name dialog is skipped, welcome shows "Welcome back, Bob".

## Sentiment + Auto-tip
- [ ] `I'm worried about scams` — empathy line + scam tip in the same bubble.
- [ ] `I'm confused about 2FA` — empathy + 2FA tip.
- [ ] `I'm scared` (no topic) — empathy + fallback with topic suggestions.

## Edge Cases
- [ ] Click Send with empty input — yellow/orange system bubble warns.
- [ ] Paste >500 chars — system bubble warns.
- [ ] Type random gibberish (`asdfqwerty`) — fallback with topic list.
- [ ] Rename `Assets/greeting.wav` away — app still launches (silent).
- [ ] Rename `Assets/responses.json` away — MessageBox + clean exit.

## UI Behaviour
- [ ] Enter key sends.
- [ ] Auto-scroll keeps the latest bubble visible after 20+ messages.
- [ ] User bubbles right-aligned with grey background; bot bubbles left-aligned with dark green.
- [ ] System bubbles centred with amber text.
- [ ] Window can be resized but not below 600×500.

---

# Manual Test Plan — Part 3 (Tasks, Quiz, NLP, Activity Log)

Start **MySQL** in the XAMPP Control Panel before testing the Tasks features.

## Navigation
- [ ] Four tabs visible: Chat, Tasks, Quiz, Activity Log.
- [ ] All Part 2 chat behaviour still works in the Chat tab.

## Task Assistant (MySQL)
- [ ] On first launch with MySQL running, no error appears (database + table auto-created).
- [ ] With MySQL stopped, a warning appears but the app still opens; Chat/Quiz/Log work.
- [ ] Tasks tab: add a task with title only — appears in the list.
- [ ] Add a task with description, reminder text and a reminder date — all shown.
- [ ] Click **Done** on a task — its `Done` column updates (persisted).
- [ ] Click **Delete** on a task — it disappears (persisted).
- [ ] Close + reopen the app — tasks are still there (loaded from MySQL).
- [ ] `Add Task` button is disabled when the title is empty.

## Quiz
- [ ] Quiz tab: click **Start / Restart** — first question appears with options.
- [ ] Answering shows immediate "Correct!" / "Not quite…" + explanation.
- [ ] Score counter increases only on correct answers.
- [ ] **Next** is disabled until an answer is chosen; options disable after answering.
- [ ] After the last question, a final score + feedback message is shown.
- [ ] **Start / Restart** reshuffles — question order differs across runs.

## NLP (Chat tab)
- [ ] `remind me to update my password` — creates task "Update my password", no extra prompt, jumps to Tasks.
- [ ] `add a task to enable 2FA` — creates the task and asks about a reminder.
- [ ] `show my tasks` — lists tasks in chat and opens the Tasks tab.
- [ ] `complete task 1` — marks task #1 complete (or says it can't find it).
- [ ] `complete task` (no number) — bot asks for the task number.
- [ ] `test me` / `quiz` — starts the quiz and opens the Quiz tab.
- [ ] `what have you done` — shows the recent log.
- [ ] A normal cybersecurity question (e.g. `phishing`) still gives a tip (no false intent).

## Activity Log
- [ ] Activity Log tab updates live as you add/complete/delete tasks.
- [ ] Adding a task with a reminder logs both "Task added" and "Reminder set".
- [ ] Starting/finishing the quiz logs "Quiz started" and "Quiz finished (score X/13)".
- [ ] Sentiment and keyword interactions in chat are logged.
- [ ] NLP actions are logged ("NLP intent: …").
- [ ] Only the last 5–10 entries are shown (newest first).
