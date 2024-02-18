using Microsoft.Win32;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace CWIcon
{
    public partial class ApplicationCore : Form
    {
        private NotifyIcon trayIcon;
        private Timer focusTimer;
        private Timer activityTimer;
        private Timer mouseTimer;
        private Point previousMousePosition;
        private AlarmForm alarmMessage;
        private ActivityReminderForm activityReminder;
        private ActivityReminderBeastForm activityReminderBeast;
        private int postponeNums;
        private int currentdDir = +1;
        private ManagedMouse managedMouse = new ManagedMouse();

        private enum MouseState
        {
            Init,
            Wait,
            Toggle
        }
        private MouseState mouseState = MouseState.Init;

        private enum TimerState
        {
            None,
            Focus,
            Break,
        }
        private TimerState focusTimerState;

        private enum ActivityState
        {
            Stopped,
            Windup,
            Reminder,
        }
        private ActivityState activityState;

        public ApplicationCore()
        {
            InitializeComponent();

            initTrayIcon();

            if(Properties.Settings.Default.inactivityTimerAutoRun == true)
            {
                setupActivityTimer(Properties.Settings.Default.inactivityTimeMins, ActivityState.Windup);
            }
            else
            {
                stopActivityTimer();
            }
            
            setupSystemEventListeners();
        }

        private void initTrayIcon()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Start Focus", StartFocus),
                new MenuItem("Start Break", StartBreak),
                new MenuItem("Cancel Timer", CancelTimer ),
                new MenuItem("-"),
                new MenuItem("Activity Reminder On/Off", toggleActivityTimerState),
                new MenuItem("-"),
                new MenuItem("Mouse Wiggle", toggleMouseWiggle),
                new MenuItem("-"),
                new MenuItem("No Format Clipboard", removeFormatClipboard),
                new MenuItem("PathFormat ClipBoard", formatClipboard),
                new MenuItem("-"),
                new MenuItem ("Settings", openSettings),
                new MenuItem ("About CWIcon", openAboutForm),
                new MenuItem("-"),
                new MenuItem("Exit", Exit),
            }),
                Visible = true
            };

            // tick Activity reminder (quick and dirty solution)
            if(Properties.Settings.Default.inactivityTimerAutoRun == true)
            {
                trayIcon.ContextMenu.MenuItems[4].Checked = true;
            }

            if(Properties.Settings.Default.mouseWiggleLastState == true)
            {
                setupMouseTimer();
                trayIcon.ContextMenu.MenuItems[6].Checked = true;
            }

            trayIcon.Click += TrayIcon_Click;

            updateIcon();
        }

        private void formatClipboard(object o, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string cbTxt = Clipboard.GetText();
                cbTxt = cbTxt.Trim();
                cbTxt = string.Join("_", cbTxt.Split(Path.GetInvalidFileNameChars()));
                cbTxt = cbTxt.Replace(" ", "_");
                Clipboard.SetText(cbTxt);
            }
        }

        private void removeFormatClipboard(object o, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Html) || Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                string cbTxt = Clipboard.GetText();

                Clipboard.SetText(cbTxt, TextDataFormat.Text);
                // Clipboard.SetData(DataFormats.Text, cbTxt);
            }
        }

        private void openSettings(object o, EventArgs e)
        {
            SettingForm sf = new SettingForm();
            sf.ShowDialog();
        }

        private void openAboutForm(object sender, EventArgs e)
        {
            UpdateForm updateWindow = new UpdateForm();

            updateWindow.ShowDialog();
        }

        private void toggleMouseWiggle(object sender, EventArgs e)
        {
            if(mouseState != MouseState.Init)
            {
                stopMouseTimer();
                trayIcon.ContextMenu.MenuItems[6].Checked = false;
                Properties.Settings.Default.mouseWiggleLastState = false;
            }
            else
            {
                setupMouseTimer();
                trayIcon.ContextMenu.MenuItems[6].Checked = true;
                Properties.Settings.Default.mouseWiggleLastState = true;
            }
        }

        private void setupMouseTimer()
        {
            mouseState = MouseState.Wait;

            mouseTimer = new Timer();
            mouseTimer.Interval = Properties.Settings.Default.mouseWaitTimePeriodSec * 1000;
            mouseTimer.Tick += MoveMouse;
            mouseTimer.Enabled = true;
            mouseTimer.Start();
        }

        private void stopMouseTimer()
        {
            mouseState = MouseState.Init;

            mouseTimer.Stop();
            mouseTimer= null;
        }

        private void setupActivityTimer(int minutes, ActivityState state)
        {
            activityState = state;

            // dispose previos timer if present
            if (activityTimer != null)
            {
                activityTimer.Enabled = false;
                activityTimer.Stop();
                activityTimer.Dispose();
                activityTimer = null;
            }

            // init new timer
            activityTimer = new Timer();
            activityTimer.Interval = minutes * 60 * 1000;
            activityTimer.Tick += ActiviyTimerElapsed;
            activityTimer.Enabled = true;
            activityTimer.Start();

            updateIcon();
        }

        private void stopActivityTimer()
        {
            activityState = ActivityState.Stopped;
            if(activityTimer != null)
            {
                activityTimer.Enabled = false;
                activityTimer.Stop();
                activityTimer.Dispose();
                activityTimer = null;
            }

            updateIcon();
        }

        private void toggleActivityTimerState(object sender, EventArgs e)
        {
            if(activityState == ActivityState.Stopped)
            {
                setupActivityTimer(Properties.Settings.Default.inactivityTimeMins, ActivityState.Windup);
                this.trayIcon.ContextMenu.MenuItems[4].Checked = true;
            } 
            else
            {
                if(activityReminder != null)
                {
                    activityReminder.Close();
                }
                stopActivityTimer();
                this.trayIcon.ContextMenu.MenuItems[4].Checked = false;
            }

        }

        private void MoveMouse(object sender, EventArgs e)
        {
            switch(mouseState)
            {
                case MouseState.Wait:
                    if(previousMousePosition != null && previousMousePosition == managedMouse.Position)
                    {
                        mouseTimer.Stop();
                        int movementIndex = currentdDir * 5;
                        currentdDir = currentdDir * -1;

                        managedMouse.MoveMouse(movementIndex, movementIndex);

                        mouseState = MouseState.Toggle;

                        // set timer
                        mouseTimer.Interval = Properties.Settings.Default.mouseWiggleTimeMs;
                        mouseTimer.Start();
                    }
                    break;

                case MouseState.Toggle:
                    {
                        mouseTimer.Stop();
                        int movementIndex = currentdDir * 5;
                        currentdDir = currentdDir * -1;

                        this.Cursor = new Cursor(Cursor.Current.Handle);

                        managedMouse.MoveMouse(movementIndex, movementIndex);

                        mouseState = MouseState.Wait;

                        // set timer
                        mouseTimer.Interval = Properties.Settings.Default.mouseWaitTimePeriodSec * 1000;
                        mouseTimer.Start();
                    }
                    break;
                default:
                    break;
            }
            //previousMousePosition = Cursor.Position;
            previousMousePosition = managedMouse.Position;
        }

        private void ActiviyTimerElapsed(object sender, EventArgs e)
        {
            switch(activityState)
            {
                case ActivityState.Windup:
                case ActivityState.Reminder:
                    // show a reminder alert

                    // beast mode
                    if (Properties.Settings.Default.beastMode == true & postponeNums > Properties.Settings.Default.beastModeThreshold)
                    {
                        openBeastReminder();
                    }
                    else
                    {
                        // normal mode
                        activityReminder = new ActivityReminderForm(Properties.Resources.strActivityReminderNote);

                        activityReminder.OnFinishEvent += activityConfirmed;
                        activityReminder.OnCancelEvent += activityPostponed;

                        activityReminder.Show();

                        activityState = ActivityState.Stopped;
                    }

                    break;
                case ActivityState.Stopped:
                default:
                    break;
            }

            if (activityTimer != null)
            {
                activityTimer.Enabled = false;
                activityTimer.Stop();
                activityTimer.Dispose();
                activityTimer = null;
            }

            updateIcon();
        }

        private void activityConfirmed(object sender, EventArgs e)
        {
            postponeNums = 0;
            setupActivityTimer(Properties.Settings.Default.inactivityTimeMins, ActivityState.Windup);
        }

        private void openBeastReminder()
        {
            activityReminderBeast = new ActivityReminderBeastForm();
            activityReminderBeast.ShowDialog();

            if (activityReminderBeast.DialogResult == DialogResult.OK)
            {
                setupActivityTimer(Properties.Settings.Default.inactivityTimeMins, ActivityState.Windup);
                postponeNums = 0;
            }
            else
            {
                setupActivityTimer(Properties.Settings.Default.inactivityReminderTimerMins, ActivityState.Reminder);
            }
        }

        private void activityPostponed(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.beastMode == true)
            {
                postponeNums++;

                if(postponeNums >= Properties.Settings.Default.beastModeThreshold)
                {
                    ((ActivityReminderForm)sender).Close();
                    openBeastReminder();
                }
                else
                {
                    setupActivityTimer(Properties.Settings.Default.inactivityReminderTimerMins, ActivityState.Reminder);
                }
            }
            else
            {
                setupActivityTimer(Properties.Settings.Default.inactivityReminderTimerMins, ActivityState.Reminder);
            }
        }

        private void showAlarm(string msg)
        {
            alarmMessage = new AlarmForm( msg);
            alarmMessage.OnCancelEvent += CancelTimer;
            
            alarmMessage.Show();
        }

        private void CancelTimer(object sender, EventArgs e)
        {
            if (focusTimerState != TimerState.None)
            {
                focusTimerState = TimerState.None;

                if(focusTimer != null)
                {
                    // dispose timer
                    focusTimer.Enabled = false;
                    focusTimer.Stop();
                    focusTimer.Dispose();
                    focusTimer = null;

                    updateIcon();
                }
            }
        }   

        private void StartBreak(object sender, EventArgs e)
        {
            focusTimerState = TimerState.Break;

            if (focusTimer != null)
            {
                focusTimer.Stop();
            }
            // init a timer
            focusTimer = new Timer();
            focusTimer.Interval = 5 * 60 * 1000; // 5 minutes
            focusTimer.Tick += FocusTimer_Elapsed; ;
            focusTimer.Enabled = true;

            focusTimer.Start();

            updateIcon();
        }

        private void StartFocus(object sender, EventArgs e)
        {
            focusTimerState = TimerState.Focus;

            if (focusTimer != null)
            {
                focusTimer.Stop();
            }
            // init a timer
            focusTimer = new Timer();
            focusTimer.Interval =  25 * 60 * 1000; // 25 minutes
            // focusTimer.Interval = 25 * 1000; // 25 mils
            focusTimer.Tick += FocusTimer_Elapsed;
            focusTimer.Enabled = true;
            
            focusTimer.Start();

            updateIcon();
        }

        private void FocusTimer_Elapsed(object sender, EventArgs e)
        {
            switch(focusTimerState)
            {
                case TimerState.Focus:
                    showAlarm("Time to have a break.");

                    focusTimerState = TimerState.Break;

                    focusTimer.Interval = 5 * 60 * 1000; // 5 minutes break
                    focusTimer.Enabled = true;
                    focusTimer.Start();

                    break;

                case TimerState.Break:
                    showAlarm("Time to focus.");

                    focusTimerState = TimerState.Focus;

                    focusTimer.Interval = 25 * 60 * 1000; // 25 minutes focus
                    focusTimer.Enabled = true;
                    focusTimer.Start();
                    break;

                case TimerState.None:
                default:
                    break;
            }

            updateIcon();
        }

        private void setupSystemEventListeners()
        {
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    if (mouseState != MouseState.Init)
                    {
                        stopMouseTimer();
                        Properties.Settings.Default.mouseWiggleLastState = true;
                    }
                    break;
                default:
                    if (Properties.Settings.Default.mouseWiggleLastState == true)
                    {
                        setupMouseTimer();
                        trayIcon.ContextMenu.MenuItems[6].Checked = true;
                    }
                    updateIcon();
                    break;
            }
        }

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
            CancelTimer(sender, e);
            this.Close();
            trayIcon.Visible = false;

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

            Pen penToUse = new Pen(brushToUse);
            Rectangle iconFrame = new Rectangle(0, 0,(int) SystemParameters.SmallIconWidth-1, (int) SystemParameters.SmallIconHeight-1);

            IntPtr hIcon;
            
            switch(focusTimerState)
            {
                case TimerState.Break:
                    g.Clear(Color.DarkOliveGreen);
                    break;
                case TimerState.Focus:
                    g.Clear(Color.DarkRed);
                    break;
                case TimerState.None:
                default:
                    g.Clear(Color.Transparent);
                    break;
            }

            if(activityState != ActivityState.Stopped)
            {
                g.DrawRectangle(penToUse, iconFrame);
            }

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
