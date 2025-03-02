//using Microsoft.Win32;

//namespace MyNaughtyWindow
//{
//    internal static class Program
//    {
//        /// <summary>
//        ///  The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        private static void AddToStartup()
//        {
//            string appName = "MyNaughtyWindow"; // שם הערך ברג'יסטרי
//            string exePath = Application.ExecutablePath; // הנתיב המלא לאפליקציה

//            try
//            {
//                // פתח את המפתח ברג'יסטרי תחת HKEY_CURRENT_USER
//                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

//                // בדוק אם המפתח כבר קיים
//                object existingValue = regKey.GetValue(appName);

//                if (existingValue == null || existingValue.ToString() != exePath)
//                {
//                    // הוסף את האפליקציה ל-Startup
//                    regKey.SetValue(appName, exePath);
//                    MessageBox.Show("Application successfully added to startup!", "Startup", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Failed to add to startup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        static void Main()
//        {
//            AddToStartup();
//           /// MessageBox.Show("Naughty Window is running after startup!", "Startup", MessageBoxButtons.OK, MessageBoxIcon.Information);

//            // To customize application configuration such as set high DPI settings or default font,
//            // see https://aka.ms/applicationconfiguration.
//            ApplicationConfiguration.Initialize();
//            Application.Run(new NW());
//        }
//    }
//}
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MyNaughtyWindow
{
    internal static class Program
    {
        private static readonly string ShutdownFlagPath = Path.Combine(Path.GetTempPath(), "shutdown.flag");
        private static readonly string ExePath = Application.ExecutablePath;

        [STAThread]
        static void Main(string[] args)
        {
            // אם התוכנית הופעלה כ-Watchdog
            if (args.Length > 0 && args[0] == "watchdog")
            {
                RunWatchdog(args[1]); // args[1] זה ה-PID של התהליך הראשי
            }
            else
            {
                // ניקוי קובץ ה-shutdown בעת הפעלה רגילה (לא אם זה Watchdog)
                if (File.Exists(ShutdownFlagPath))
                {
                    File.WriteAllText(ShutdownFlagPath, "");
                }

                StartWatchdog();
                ApplicationConfiguration.Initialize();
                Application.Run(new NW());
            }
        }

        // הפעלת Watchdog למניעת סגירת התהליך
        private static void StartWatchdog()
        {
            Process currentProcess = Process.GetCurrentProcess();
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = ExePath,
                Arguments = $"watchdog {currentProcess.Id}",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process.Start(psi);
        }

        // Watchdog שמוסיף 2 מופעים אם נמחק תהליך אחד
        private static void RunWatchdog(string targetProcessId)
        {
            int monitoredProcessId = int.Parse(targetProcessId);

            while (true)
            {
                try
                {
                    if (File.Exists(ShutdownFlagPath) && File.ReadAllText(ShutdownFlagPath).Trim() == "shutdown")
                    {
                        Environment.Exit(0);
                    }

                    Process monitoredProcess = Process.GetProcessById(monitoredProcessId);
                }
                catch
                {
                    // אם התהליך נמחק → נוסיף **שני מופעים חדשים**
                    for (int i = 0; i < 2; i++)
                    {
                        Process.Start(ExePath);
                        Thread.Sleep(500); // השהיה קטנה למניעת הפעלה כפולה
                    }

                    Environment.Exit(0); // ה-Watchdog הנוכחי מסיים את עצמו
                }

                Thread.Sleep(1000); // בדיקה כל שנייה
            }
        }
    }
}

