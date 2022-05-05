using System;
using System.Deployment.Application;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;


namespace CWIcon
{
    public partial class SettingForm : Form
    {
        private EventHandler onCancelEvent;
        public EventHandler OnCancelEvent { get => onCancelEvent; set => onCancelEvent = value; }

        public SettingForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;

            int x = Screen.GetWorkingArea(this).Right - Size.Width;
            int y = Screen.GetWorkingArea(this).Bottom - Size.Height;

            this.Location = new Point(x, y);

            cbStatup.Checked = Properties.Settings.Default.runAtStartup;
            cbActivityReminderOn.Checked = Properties.Settings.Default.inactivityTimerAutoRun;

            inActivityTimer.Value = Properties.Settings.Default.inactivityTimeMins;
            inReminderTime.Value = Properties.Settings.Default.inactivityReminderTimerMins;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            saveTimeValues();

            Properties.Settings.Default.Save();
            writeRegistry();
            this.Close();
        }

        private void saveTimeValues()
        {
            Properties.Settings.Default.inactivityTimeMins = (int)inActivityTimer.Value;
            Properties.Settings.Default.inactivityReminderTimerMins = (int)inReminderTime.Value;
        }

        private void cbStartupChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.runAtStartup = cbStatup.Checked;
        }

        private void writeRegistry()
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey regKeyLocation = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (cbStatup.Checked == true)
            {
                // string appPath = Application.ExecutablePath; // no
                // string appPath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;
                string appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                

                regKeyLocation.SetValue(Properties.Resources.strAppName, appPath);

            }
            else
            {
                if (regKeyLocation.GetValue(Properties.Resources.strAppName) != null)
                {
                    regKeyLocation.DeleteValue(Properties.Resources.strAppName);
                }
            }
        }

        private void cbActivityTimerChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.inactivityTimerAutoRun = cbActivityReminderOn.Checked;
        }

    }
}
