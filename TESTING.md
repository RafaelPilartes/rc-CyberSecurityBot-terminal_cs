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
