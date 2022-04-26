﻿using Microsoft.Win32;
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
        private Timer focusTimer;
        private Timer activityTimer;
        private AlarmForm alarmMessage;
        private ActivityReminderForm activityReminder;

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


        public IconCreator()
        {
            InitializeComponent();

            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Start Focus", StartFocus),
                new MenuItem("Start Break", StartBreak),
                new MenuItem("Cancel Timer", CancelTimer ),
                new MenuItem("-"),
                new MenuItem("ON/OFF Activity Timer", toggleActivityTimerState),
                new MenuItem("-"),
                new MenuItem("Exit", Exit),
            }),
                Visible = true
            };

            trayIcon.Click += TrayIcon_Click;

            updateIcon();

            // ON by default
            setupActivityTimer(1, ActivityState.Windup);
            
            setupSystemEventListeners();
        }

        private void setupActivityTimer(int minutes, ActivityState state)
        {
            activityState = state;
            // timer init
            activityTimer = new Timer();
            activityTimer.Interval = minutes * 60 * 1000; // 58 minutes
            activityTimer.Tick += ActiviyTimerElapsed;
            activityTimer.Enabled = true;

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
                setupActivityTimer(65, ActivityState.Windup);
            } 
            else
            {
                stopActivityTimer();
            }

        }



        private void ActiviyTimerElapsed(object sender, EventArgs e)
        {
            switch(activityState)
            {
                case ActivityState.Windup:
                case ActivityState.Reminder:
                    // show a reminder alert
                    activityReminder = new ActivityReminderForm(Properties.Resources.strActivityReminderNote);
                    activityReminder.OnCancelEvent += activityConfirmed;
                    activityReminder.OnFinishEvent += activityPostponed;

                    activityReminder.Show();
                    break;

                case ActivityState.Stopped:
                default:
                    // dispose timer:
                    if(activityTimer != null)
                    {
                        activityTimer.Enabled=false;
                        activityTimer.Stop();
                        activityTimer.Dispose();
                        activityTimer = null;
                    }
                    break;
            }
        }

        private void activityConfirmed(object sender, EventArgs e)
        {
            setupActivityTimer(58, ActivityState.Windup);
        }

        private void activityPostponed(object sender, EventArgs e)
        {
            setupActivityTimer(10, ActivityState.Reminder);
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
            updateIcon();
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
