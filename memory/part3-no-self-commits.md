---
name: part3-no-self-commits
description: During Part 3 work, never run git commit; stop and give the user the commit message instead
metadata:
  type: feedback
---

For all Part 3 work on CyberSecurityBot, never create commits myself. When reaching a logical commit point, stop, tell the user, and provide the exact commit message to use. The user commits manually via GitHub Desktop.

**Why:** The user wants full manual control over the commit history in GitHub Desktop.

**How to apply:** Do code/file changes freely, but at each logical checkpoint pause and output a ready-to-paste commit message (no Co-Authored-By trailer, consistent with existing history). Do not run `git commit`/`git push`. See [[part3-plan]] if present.
