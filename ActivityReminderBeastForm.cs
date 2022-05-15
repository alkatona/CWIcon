using System;
using System.Drawing;
using System.Windows.Forms;

namespace CWIcon
{
    public partial class ActivityReminderBeastForm : Form
    {
        private EventHandler onCancelEvent;
        private EventHandler onFinishEvent;
        public EventHandler OnCancelEvent { get => onCancelEvent; set => onCancelEvent = value; }
        public EventHandler OnFinishEvent { get => onFinishEvent; set => onFinishEvent = value; }
        public DialogResult resultTemp;

        private Timer fadeTimer;

        public ActivityReminderBeastForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;

            int x = Screen.GetWorkingArea(this).Right - Size.Width;
            int y = Screen.GetWorkingArea(this).Bottom - Size.Height;

            this.Location = new Point(x, y);

            this.lbMessage.Text = Properties.Resources.msgBeastMessage;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            resultTemp = DialogResult.OK;
            pbMain.Image = Properties.Resources.beast_ok;

            // continue timer
            EventHandler eventHandler = onFinishEvent;
            if (eventHandler != null)
            {
                eventHandler(this, null);
            }
            this.CloseMe();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            resultTemp = DialogResult.Cancel;
            pbMain.Image = Properties.Resources.beast_nok;

            EventHandler eventHandler = onCancelEvent;
            if(eventHandler != null)
            {
                eventHandler(this, null);
            }
            this.CloseMe();
        }


        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.CloseMe();
        }

        private void AlarmForm_Load(object sender, EventArgs e)
        {
            Opacity = 0;
            fadeTimer = new Timer();
            fadeTimer.Interval = 10;
            fadeTimer.Tick += FadeInTimer_Tick;

            fadeTimer.Start();
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            if(Opacity >= 1)
            {
                fadeTimer.Stop();
                fadeTimer.Dispose();
            }
            else
            {
                Opacity += 0.05;
            }
        }

        private void CloseMe()
        {
            Opacity = 1;
            fadeTimer = new Timer();
            fadeTimer.Interval = 10;
            fadeTimer.Tick += FadeOutTimer_Tick;

            fadeTimer.Start();
        }

        private void FadeOutTimer_Tick(object sender, EventArgs e)
        {
            if (Opacity <= 0)
            {
                fadeTimer.Stop();
                fadeTimer.Dispose();

                DialogResult = resultTemp;
                
                this.Close();
            }
            else
            {
                Opacity -= 0.01;
            }
        }
    }
}
