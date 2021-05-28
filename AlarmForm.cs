using System;
using System.Drawing;
using System.Windows.Forms;

namespace CWIcon
{
    public partial class AlarmForm : Form
    {
        private EventHandler onCancelEvent;
        public EventHandler OnCancelEvent { get => onCancelEvent; set => onCancelEvent = value; }

        // System.Timers.Timer timer;
        private Timer timer;

        public AlarmForm(string message)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;

            int x = Screen.GetWorkingArea(this).Right - Size.Width;
            int y = Screen.GetWorkingArea(this).Bottom - Size.Height;

            this.Location = new Point(x, y);

            lbMessage.Text = message;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            disposeTimer();
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            EventHandler eventHandler = onCancelEvent;
            if(eventHandler != null)
            {
                eventHandler(this, null);
            }
            disposeTimer();
            this.Close();
        }

        private void AlarmForm_Show(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 10 * 1000; // 10 sec
            timer.Enabled = true;
            timer.Start();
        }

        private void disposeTimer()
        {
            if(timer != null)
            {
                timer.Stop();
                timer.Enabled = false;
                timer.Dispose();
               
                timer = null;
                
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            disposeTimer();
            this.Close();
        }

        private void AlarmForm_Load(object sender, EventArgs e)
        {
            
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            disposeTimer();
            this.Close();
        }
    }
}
