
namespace CWIcon
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.cbStatup = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbActivityTimer = new System.Windows.Forms.Label();
            this.inReminderTime = new System.Windows.Forms.NumericUpDown();
            this.inActivityTimer = new System.Windows.Forms.NumericUpDown();
            this.cbBeastMode = new System.Windows.Forms.CheckBox();
            this.cbActivityReminderOn = new System.Windows.Forms.CheckBox();
            this.inMouseWait = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inReminderTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inActivityTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inMouseWait)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(92)))));
            this.panel1.Controls.Add(this.lbTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 36);
            this.panel1.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbTitle.Location = new System.Drawing.Point(11, 4);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(86, 25);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Settings";
            // 
            // btCancel
            // 
            this.btCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(152)))), ((int)(((byte)(137)))));
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Font = new System.Drawing.Font("Lucida Fax", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btCancel.Location = new System.Drawing.Point(65, 152);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 33);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Close";
            this.btCancel.UseVisualStyleBackColor = false;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // cbStatup
            // 
            this.cbStatup.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbStatup.Location = new System.Drawing.Point(16, 17);
            this.cbStatup.Name = "cbStatup";
            this.cbStatup.Size = new System.Drawing.Size(212, 23);
            this.cbStatup.TabIndex = 2;
            this.cbStatup.Text = "Start App with PC (beta)";
            this.cbStatup.UseVisualStyleBackColor = true;
            this.cbStatup.CheckedChanged += new System.EventHandler(this.cbStartupChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lbActivityTimer);
            this.panel2.Controls.Add(this.inMouseWait);
            this.panel2.Controls.Add(this.inReminderTime);
            this.panel2.Controls.Add(this.inActivityTimer);
            this.panel2.Controls.Add(this.cbBeastMode);
            this.panel2.Controls.Add(this.cbActivityReminderOn);
            this.panel2.Controls.Add(this.cbStatup);
            this.panel2.Controls.Add(this.btCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(241, 197);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Postpone activity timer (mins)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbActivityTimer
            // 
            this.lbActivityTimer.AutoSize = true;
            this.lbActivityTimer.Location = new System.Drawing.Point(13, 77);
            this.lbActivityTimer.Name = "lbActivityTimer";
            this.lbActivityTimer.Size = new System.Drawing.Size(128, 13);
            this.lbActivityTimer.TabIndex = 4;
            this.lbActivityTimer.Text = "Activity timer period (mins)";
            // 
            // inReminderTime
            // 
            this.inReminderTime.Location = new System.Drawing.Point(176, 101);
            this.inReminderTime.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.inReminderTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.inReminderTime.Name = "inReminderTime";
            this.inReminderTime.Size = new System.Drawing.Size(52, 20);
            this.inReminderTime.TabIndex = 3;
            this.inReminderTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // inActivityTimer
            // 
            this.inActivityTimer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inActivityTimer.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.inActivityTimer.Location = new System.Drawing.Point(176, 75);
            this.inActivityTimer.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.inActivityTimer.Name = "inActivityTimer";
            this.inActivityTimer.Size = new System.Drawing.Size(52, 20);
            this.inActivityTimer.TabIndex = 3;
            this.inActivityTimer.Value = new decimal(new int[] {
            55,
            0,
            0,
            0});
            // 
            // cbBeastMode
            // 
            this.cbBeastMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbBeastMode.AutoSize = true;
            this.cbBeastMode.Location = new System.Drawing.Point(146, 46);
            this.cbBeastMode.Name = "cbBeastMode";
            this.cbBeastMode.Size = new System.Drawing.Size(82, 23);
            this.cbBeastMode.TabIndex = 2;
            this.cbBeastMode.Text = "Samuel Mode";
            this.cbBeastMode.UseVisualStyleBackColor = true;
            this.cbBeastMode.CheckedChanged += new System.EventHandler(this.cbBeatModeChanged);
            // 
            // cbActivityReminderOn
            // 
            this.cbActivityReminderOn.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbActivityReminderOn.AutoSize = true;
            this.cbActivityReminderOn.Location = new System.Drawing.Point(16, 46);
            this.cbActivityReminderOn.Name = "cbActivityReminderOn";
            this.cbActivityReminderOn.Size = new System.Drawing.Size(124, 23);
            this.cbActivityReminderOn.TabIndex = 2;
            this.cbActivityReminderOn.Text = "Auto Activity Reminder";
            this.cbActivityReminderOn.UseVisualStyleBackColor = true;
            this.cbActivityReminderOn.CheckedChanged += new System.EventHandler(this.cbActivityTimerChanged);
            // 
            // inMouseWait
            // 
            this.inMouseWait.Location = new System.Drawing.Point(176, 127);
            this.inMouseWait.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.inMouseWait.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.inMouseWait.Name = "inMouseWait";
            this.inMouseWait.Size = new System.Drawing.Size(52, 20);
            this.inMouseWait.TabIndex = 3;
            this.inMouseWait.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mouse wait time (sec)";
            this.label1.Click += new System.EventHandler(this.label2_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(241, 233);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.Text = "CWIcon";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inReminderTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inActivityTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inMouseWait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.CheckBox cbStatup;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbActivityReminderOn;
        private System.Windows.Forms.NumericUpDown inReminderTime;
        private System.Windows.Forms.NumericUpDown inActivityTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbActivityTimer;
        private System.Windows.Forms.CheckBox cbBeastMode;
        private System.Windows.Forms.NumericUpDown inMouseWait;
        private System.Windows.Forms.Label label1;
    }
}