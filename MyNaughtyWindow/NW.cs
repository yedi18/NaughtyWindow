using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MyNaughtyWindow
{
    public partial class NW : Form
    {
        private System.Windows.Forms.Timer colorTimer = new System.Windows.Forms.Timer();
        private Random rand = new Random();
        private string[] messages = new string[]
        {
            "You can't close me! 😈",
            "Nice try! 😅",
            "Still here! 👻",
            "You'll never get rid of me! 🤡",
            "Muahaha! 💀"
        };
        private int closeAttempts = 0;
        private bool safeMode = false;
        private bool isShowingInitialMessage = false;
        private string attemptFilePath = Path.Combine(Path.GetTempPath(), "naughty_attempts.txt");
        private static string initialMessageFilePath = Path.Combine(Path.GetTempPath(), "naughty_initial_message_shown.txt");
        private string randomFlagPath = Path.Combine(Path.GetTempPath(), "random_flag.txt");
        private string shutdownFlagPath = Path.Combine(Path.GetTempPath(), "shutdown.flag");



        private static bool initialMessageShown = false;
        public NW()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(NW_FormClosing);
            this.Icon = new Icon("NaughtyIcon.ico");
            InitializeNaughtyWindow();
            AddToStartup();
            StartColorTimer();


            if (File.Exists(randomFlagPath))
            {
                string flagContent = File.ReadAllText(randomFlagPath).Trim();
                if (flagContent == "1")
                {
                    MoveWindowRandomly();
                }
                else
                {
                    ShowInitialMessage(); // הצג הודעה רק במופע הראשי
                }
            }
            else
            {
                ShowInitialMessage(); // אם הקובץ לא קיים
            }


        }

        private void InitializeNaughtyWindow()
        {
            this.Text = "Naughty Window 😈";
            this.Size = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblMessage.Location = new Point((this.ClientSize.Width - lblMessage.Width) / 2, 40);

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.Size = new Size(100, 40);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 2;
            btnClose.BackColor = Color.White;
            btnClose.ForeColor = Color.Black;
            btnClose.Location = new Point((this.ClientSize.Width - btnClose.Width) / 2, 150);
            btnClose.Click += BtnClose_Click;
            this.Controls.Add(btnClose);
        }

        private void ShowInitialMessage()
        {
            // בדיקה אם ההודעה כבר הוצגה (ע"י קובץ זמני)
            if (!File.Exists(initialMessageFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(initialMessageFilePath)))
            {
                isShowingInitialMessage = true;

                // הצגת ההודעה הראשונית
                MessageBox.Show("Hey there! Don't try to close me, or I might cause some trouble 😉\n" +
                        "               You won’t succeed in closing me!\n" +
                        "           I’ll come back after a system restart😈",
                    "Naughty Window | Made By Yedidya Shauli", MessageBoxButtons.OK, MessageBoxIcon.Information);

                isShowingInitialMessage = false;

                // יצירת קובץ זמני שמציין שההודעה הוצגה
                File.WriteAllText(initialMessageFilePath, "shown");
            }
        }


        private void BtnClose_Click(object sender, EventArgs e)
        {
            int closeAttempts = GetCloseAttempts();
            closeAttempts++;
            SetCloseAttempts(closeAttempts);

            // צור קובץ זמני לסימון מופעים רנדומליים
            // הודעת ניסיון
            MessageBox.Show($"Attempt number {closeAttempts}! Can't catch me! 🎉", "Naughty Window", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // פתיחת שני מופעים חדשים
            for (int i = 0; i < 2; i++)
            {
                File.WriteAllText(randomFlagPath, "1");
                Process.Start(Application.ExecutablePath);
                System.Threading.Thread.Sleep(100);

            }
            if (closeAttempts == 2)
            {
                MessageBox.Show("You won’t succeed in closing me! I’ll come back after a system restart. 😈", "You Can't Win!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (closeAttempts < 5) { 
            
                MessageBox.Show("Don't play games with me! Think harder! ⚠️", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (closeAttempts == 5)
            {
                var result = MessageBox.Show("Tried 5 times already! Want me to stop?", "Safe Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ShowHint(closeAttempts);
                }
            }
            if (closeAttempts > 5)
            {
                ShowHint(closeAttempts);
            }
            // הזזת החלון למקום רנדומלי לפני הסגירה
            MoveWindowRandomly();

            // סגירת החלון הנוכחי
            this.Close();
        }


        private void ShowHint(int closeAttempts)
        {
            if (closeAttempts < 5)
            {
                // אין רמזים לפני 5 ניסיונות
                return;
            }
            else if (closeAttempts >= 5 && closeAttempts < 8)
            {
                MessageBox.Show("Think hard about the hint! 🧠", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("Hint: Think about cutting things by moving aside ✂️", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (closeAttempts >= 8 && closeAttempts < 10)
            {
                MessageBox.Show("Here's another hint: Think about keyboard shortcuts for cutting using moving aside ⌨️✂️", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (closeAttempts >= 10 && closeAttempts < 15)
            {
                MessageBox.Show("You're so close! 🤯\nThink about a powerful keyboard shortcut...\nOne that lets you *cut* something out quickly while moving it aside.", "Almost There!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (closeAttempts >= 15)
            {
                MessageBox.Show("Still stuck? 😵\nHere's a big hint:\nPress these together → CTRL + SHIFT + ❌", "Final Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void MoveWindowRandomly()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            Random rand = new Random();

            int newX = rand.Next(0, screenWidth - this.Width);
            int newY = rand.Next(0, screenHeight - this.Height);

            this.StartPosition = FormStartPosition.Manual; // נדרש כדי לא לאפשר CenterScreen
            this.Location = new Point(newX, newY);
        }

        private int GetCloseAttempts()
        {
            if (File.Exists(attemptFilePath))
            {
                var content = File.ReadAllText(attemptFilePath);
                if (int.TryParse(content, out int attempts))
                {
                    return attempts;
                }
            }

            // אם הקובץ לא קיים או לא תקין, התחל מ-0
            return 0;
        }

        private void SetCloseAttempts(int attempts)
        {
            File.WriteAllText(attemptFilePath, attempts.ToString());
        }

        private void StartColorTimer()
        {
            colorTimer.Interval = 500;
            colorTimer.Tick += ChangeColors;
            colorTimer.Start();
        }

        private void ChangeColors(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            lblMessage.ForeColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            lblMessage.Text = messages[rand.Next(messages.Length)];
        }

        private void AddToStartup()
        {
            string appName = "NaughtyWindow";
            string exePath = Application.ExecutablePath;

            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            regKey.SetValue(appName, exePath);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.X))
            {
                EnableSafeMode();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void EnableSafeMode()
        {
            safeMode = true;

            MessageBox.Show("Safe Mode Enabled! Closing all windows.", "Safe Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // צור קובץ דגל לסגירה וכתוב בו "shutdown"
            try
            {
                File.WriteAllText(shutdownFlagPath, "shutdown");
                Thread.Sleep(100); // השהיה קצרה כדי לוודא שה-Watchdog מזהה את הקובץ

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create shutdown flag: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // הסר את האפליקציה מה-Startup ברג'יסטרי
            try
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                regKey.DeleteValue("MyNaughtyWindow", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to remove from startup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // סגור את כל המופעים של MyNaughtyWindow
            var processes = Process.GetProcessesByName("MyNaughtyWindow");
            foreach (var process in processes)
            {
                try
                {
                    // איפוס קבצים
                    File.WriteAllText(attemptFilePath, "0");
                    File.WriteAllText(initialMessageFilePath, "");
                    File.WriteAllText(randomFlagPath, "");

                    // סגור את התהליך
                    process.Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error closing process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // רוקן את תוכן קובץ הדגל (השאר אותו קיים אבל ריק)
            

            // סגור גם את התהליך הנוכחי
            Environment.Exit(0);
        }


        private void NW_FormClosing(object sender, FormClosingEventArgs e)
        {
            // מניעת סגירת החלון לחלוטין
            e.Cancel = true; // מבטל את הסגירה
            MessageBox.Show("You can't close me! 😈", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // מאזין לסגירת התהליך
            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.EnableRaisingEvents = true;
            currentProcess.Exited += CurrentProcess_Exited;
        }

        private void CurrentProcess_Exited(object sender, EventArgs e)
        {
            // כאשר התהליך נהרג, יופעלו מופעים חדשים
            for (int i = 0; i < 2; i++)
            {
                Process.Start(Application.ExecutablePath);
            }
        }


    }

}
