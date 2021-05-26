using Microsoft.Win32;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Forms;

namespace CWIcon
{
    public partial class IconCreator : Form
    {
        private NotifyIcon trayIcon;
        private System.Timers.Timer timer;

        public IconCreator()
        {
            InitializeComponent();

            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Update", Update),
                new MenuItem("Exit", Exit),
            }),
                Visible = true
            };

            trayIcon.Click += TrayIcon_Click;

            updateIcon();

            setupTimer();

            setupSystemEventListeners();
        }

        private void setupSystemEventListeners()
        {
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            updateIcon();
        }

        private void setupTimer()
        {
            timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Interval = 1000 * 60 * 60; // check every hour
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            timer.Start();
        }

        private int tick = 0;
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            updateIcon();
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEvent = (MouseEventArgs)e;

            if (mouseEvent.Button == MouseButtons.Left)
            {
                CalendarForm calendar = new CalendarForm();

                calendar.Show();
                calendar.Activate();
            }

        }

        public void Exit(object sender, EventArgs e)
        {
            this.Close();
            trayIcon.Visible = false;
            timer.Enabled = false;
            timer.Dispose();
            timer = null;

            SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;

            System.Windows.Forms.Application.Exit();
        }

        private void Update(object sender, EventArgs e)
        {
            updateIcon();
        }

        private void updateIcon()
        {
            string cw;

            cw = getCW().ToString();

            trayIcon.Text = "Calendar week:" + cw;

            updateIconText(cw);
        }

        private void updateIconText(string text)
        {
            Font fontToUse = new Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brushToUse = new SolidBrush(Color.White);
            Bitmap bitmapText = new Bitmap((int)SystemParameters.SmallIconWidth, (int)SystemParameters.SmallIconHeight);
            Graphics g = System.Drawing.Graphics.FromImage(bitmapText);

            IntPtr hIcon;

            g.Clear(Color.Transparent);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawString(text, fontToUse, brushToUse, 0, 0);

            hIcon = (bitmapText.GetHicon());
            trayIcon.Icon = Icon.FromHandle(hIcon);
        }

        private int getCW()
        {
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            return myCal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
