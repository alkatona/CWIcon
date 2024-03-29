﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CWIcon
{
    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {
            InitializeComponent();
        }

        private void CalendarForm_Load(object sender, EventArgs e)
        {
            int x = Screen.GetWorkingArea(this).Right  - Size.Width;
            int y = Screen.GetWorkingArea(this).Bottom - Size.Height;

            this.Location = new Point(x, y);
        }

        private void CalendarForm_MouseCaptureChanged(object sender, EventArgs e)
        {
            this.Close();

            /*
            if (!this.Capture)
            {
                if (!this.RectangleToScreen(this.DisplayRectangle).Contains(Cursor.Position))
                {
                    this.Close();
                }
                else
                {
                    this.Capture = true;
                }
            }
            */

            //base.OnCaptureChanged(e);
            // base.OnMouseCaptureChanged(e);
        }

        private void CalendarForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            Console.WriteLine(e.Start.ToString() + " " + e.End.ToString());
        }

        private void tbCopy_Click(object sender, EventArgs e)
        {
            // using iso 8601 yyyy-mm-dd format
            
            string selectedDate = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");

            if (monthCalendar1.SelectionStart.DayOfYear != monthCalendar1.SelectionEnd.DayOfYear)
            {
                selectedDate = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd") + " - " + monthCalendar1.SelectionRange.End.ToString("yyyy-MM-dd");
            }

            Clipboard.SetText(selectedDate);
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btToday_Click(object sender, EventArgs e)
        {
            monthCalendar1.SetSelectionRange(DateTime.Now, DateTime.Now);
        }
    }
}
