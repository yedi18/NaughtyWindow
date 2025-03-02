# Naughty Window 🚀

**Naughty Window** is an experimental Windows application designed to create persistent pop-up windows that are difficult to close. This project explores process resilience and watchdog techniques.

## 🔥 Features
- Displays pop-ups that reappear when closed
- Launches two new instances if terminated forcefully
- Auto-restarts after system reboot
- **Safe Mode**: Allows stopping auto-start functionality

## 📥 Installation
Download the setup file from:  
[📎 NaughtyWindowSetup.zip](https://github.com/USERNAME/NaughtyWindow/releases/latest/download/NaughtyWindowSetup.zip)

### Steps:
1. **Download and run the setup file**.
2. The program will automatically start running in the background.
3. To disable auto-start, remove the registry entry (see instructions below).

## 🛑 **How to Disable Naughty Window (Safe Mode)**
If the application keeps running and you need to stop it:
1. **Open "Registry Editor"** (`regedit` via Start Menu).
2. Navigate to: HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run
3. Delete the entry containing `MyNaughtyWindow`.

## 🛠️ Technologies Used
- **C# (.NET 8.0)**
- **Windows Forms**
- **Process Monitoring & Registry Manipulation**

## 👨‍💻 Development & Contributions
This project is for educational purposes only.  
Feel free to fork this repository and submit a **Pull Request** if you'd like to contribute.

📬 **Project Maintainer**: [yedid18](https://github.com/yedid18)
