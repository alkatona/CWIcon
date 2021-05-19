using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Threading;

namespace CWIcon
{
    public class App : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private System.Timers.Timer timer;
        private Int32 tick = 0;


        public App()
        {
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

            updateTime();
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
            // TODO: singleton variant?

            MouseEventArgs mouseEvent = (MouseEventArgs) e;

            if(mouseEvent.Button == MouseButtons.Left)
            {
                CalendarForm calendar = new CalendarForm();

                calendar.Show();
                calendar.Activate();
            }

        }

        private void setTimer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 2000;
            timer.Elapsed += OnTimerTriggerred;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private int getCW()
        {
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            return myCal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private static void OnTimerTriggerred(Object source, System.Timers.ElapsedEventArgs e)
        {
            // something invoke solution needed here
            // Dispatcher.Invoke(delegate { updateTime(); }, DispatcherPriority.Normal);
        }

        private void Update(object sender, EventArgs e)
        {
            updateTime();
        }

        private void updateTime()
        {
            string cw;

            cw = getCW().ToString();

            trayIcon.Text = "Calendar week:" + cw;

            updateIconTect(cw);
        }

        private void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            // timer.Stop();
            // timer.Dispose();

            System.Windows.Forms.Application.Exit();
        }

        private void updateIconTect(string inputStr)
        {
            Font fontToUse = new Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brushToUse = new SolidBrush(Color.White);
            Bitmap bitmapText = new Bitmap((int)SystemParameters.SmallIconWidth, (int)SystemParameters.SmallIconHeight);
            Graphics g = System.Drawing.Graphics.FromImage(bitmapText);

            IntPtr hIcon;

            g.Clear(Color.Transparent);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawString(inputStr, fontToUse, brushToUse, 0,0);

            hIcon = (bitmapText.GetHicon());
            trayIcon.Icon = System.Drawing.Icon.FromHandle(hIcon);

        }

    }
}
