# Naughty Window 🚀

## Overview
Naughty Window is an experimental Windows application designed to create persistent pop-up windows that are difficult to close. This project explores process resilience and watchdog techniques, ensuring the application remains active even if terminated.

## Features 🔥
- **Persistent Pop-ups:** Closes? No problem! The window reappears instantly.
- **Resilient Execution:** Kills one instance? Two more will spawn.
- **Auto-Restart:** The application launches itself after a system reboot.
- **Safe Mode:** A method exists to disable auto-start functionality (see below).
- **Hints System:** After multiple failed attempts to close the app, hints are gradually revealed.

## Installation 📥
1. **Download the setup file:**  
   - [Download NaughtyWindowSetup.zip](#)(https://github.com/user-attachments/files/19042644/NaughtyWindowSetup.zip)
2. **Extract the ZIP file.**
3. **Run `setup.exe` and follow the installation wizard.**
4. **The application will start running automatically.**

## Usage 🖥️
Once launched, **Naughty Window** will ensure that it remains active and reopens when closed.  
If you try to kill it via Task Manager, it will respawn with additional instances.  
If you attempt to close it multiple times, it will start giving **hints** on how to disable it.

## How to Disable Naughty Window (Safe Mode) 🛑

### ⚠️ Spoiler Alert – Try to figure it out first! ⚠️
If you keep attempting to close the window, you will receive gradual hints on how to disable it.  
If you give up, expand the section below:

<details>
  <summary>Click to reveal Safe Mode instructions</summary>
  
  **Use the built-in Safe Mode shortcut:**  
  - Press `Ctrl + Shift + X` while the Naughty Window is focused.  
  - A message will confirm that **Safe Mode** has been activated, and all instances will be closed.
  
  **Manually remove from Windows Startup:**  
  1. Open **Registry Editor** (`Win + R`, type `regedit`, and hit **Enter**).
  2. Navigate to:
     ```
     HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run
     ```
  3. Delete the entry containing `MyNaughtyWindow`.
  4. Restart your computer.
  
  **Check Task Manager (`Ctrl + Shift + Esc`) and disable any remaining instances.**
</details>

---
💡 **Hint:** The app is designed as a challenge! Keep trying to close it and pay attention to the hints that appear.  
You might need to **think outside the box** to solve it. 😈

Now you’re ready to experience **Naughty Window**! 🚀
