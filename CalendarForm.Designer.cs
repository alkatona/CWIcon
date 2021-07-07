
namespace CWIcon
{
    partial class CalendarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.btToday = new System.Windows.Forms.Button();
            this.tbCopy = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.BackColor = System.Drawing.SystemColors.Window;
            this.monthCalendar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthCalendar1.Location = new System.Drawing.Point(16, 12);
            this.monthCalendar1.MaxSelectionCount = 32;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowTodayCircle = false;
            this.monthCalendar1.ShowWeekNumbers = true;
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.monthCalendar1_KeyDown);
            // 
            // btToday
            // 
            this.btToday.BackColor = System.Drawing.Color.White;
            this.btToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btToday.Font = new System.Drawing.Font("Microsoft New Tai Lue", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btToday.Location = new System.Drawing.Point(94, 182);
            this.btToday.Name = "btToday";
            this.btToday.Size = new System.Drawing.Size(64, 23);
            this.btToday.TabIndex = 1;
            this.btToday.Text = "Today";
            this.btToday.UseVisualStyleBackColor = false;
            this.btToday.Click += new System.EventHandler(this.btToday_Click);
            // 
            // tbCopy
            // 
            this.tbCopy.BackColor = System.Drawing.Color.White;
            this.tbCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbCopy.Font = new System.Drawing.Font("Microsoft New Tai Lue", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCopy.Location = new System.Drawing.Point(16, 182);
            this.tbCopy.Name = "tbCopy";
            this.tbCopy.Size = new System.Drawing.Size(73, 23);
            this.tbCopy.TabIndex = 1;
            this.tbCopy.Text = "Copy date";
            this.tbCopy.UseVisualStyleBackColor = false;
            this.tbCopy.Click += new System.EventHandler(this.tbCopy_Click);
            // 
            // btClose
            // 
            this.btClose.BackColor = System.Drawing.Color.White;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Font = new System.Drawing.Font("Microsoft New Tai Lue", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClose.Location = new System.Drawing.Point(164, 182);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(52, 23);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(230, 216);
            this.Controls.Add(this.tbCopy);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btToday);
            this.Controls.Add(this.monthCalendar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CalendarForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CalendarForm";
            this.Deactivate += new System.EventHandler(this.CalendarForm_MouseCaptureChanged);
            this.Load += new System.EventHandler(this.CalendarForm_Load);
            this.MouseCaptureChanged += new System.EventHandler(this.CalendarForm_MouseCaptureChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Button btToday;
        private System.Windows.Forms.Button tbCopy;
        private System.Windows.Forms.Button btClose;
    }
}