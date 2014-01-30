using CustomTimers.View;

namespace CustomTimers
{
    partial class TimerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimerForm));
            this.nicoMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTimer = new System.Windows.Forms.TabControl();
            this.tabpCountdown = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabpStopWatch = new System.Windows.Forms.TabPage();
            this.cbxIsTopMost = new System.Windows.Forms.CheckBox();
            this.ucCountdown = new CustomTimers.View.CountdownUserControl();
            this.ucStopWatch = new CustomTimers.View.StopWatchUserControl();
            this.menuStrip1.SuspendLayout();
            this.tabTimer.SuspendLayout();
            this.tabpCountdown.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabpStopWatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // nicoMain
            // 
            this.nicoMain.Icon = ((System.Drawing.Icon)(resources.GetObject("nicoMain.Icon")));
            this.nicoMain.Text = "notifyIcon1";
            this.nicoMain.Visible = true;
            this.nicoMain.DoubleClick += new System.EventHandler(this.NicoMainDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(497, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.resetStateToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // resetStateToolStripMenuItem
            // 
            this.resetStateToolStripMenuItem.Name = "resetStateToolStripMenuItem";
            this.resetStateToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.resetStateToolStripMenuItem.Text = "Remove State";
            this.resetStateToolStripMenuItem.Click += new System.EventHandler(this.resetStateToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // tabTimer
            // 
            this.tabTimer.Controls.Add(this.tabpCountdown);
            this.tabTimer.Controls.Add(this.tabpStopWatch);
            this.tabTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTimer.Location = new System.Drawing.Point(0, 24);
            this.tabTimer.Name = "tabTimer";
            this.tabTimer.SelectedIndex = 0;
            this.tabTimer.Size = new System.Drawing.Size(497, 322);
            this.tabTimer.TabIndex = 2;
            // 
            // tabpCountdown
            // 
            this.tabpCountdown.Controls.Add(this.ucCountdown);
            this.tabpCountdown.Location = new System.Drawing.Point(4, 22);
            this.tabpCountdown.Margin = new System.Windows.Forms.Padding(0);
            this.tabpCountdown.Name = "tabpCountdown";
            this.tabpCountdown.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tabpCountdown.Size = new System.Drawing.Size(489, 296);
            this.tabpCountdown.TabIndex = 0;
            this.tabpCountdown.Text = "Countdown";
            this.tabpCountdown.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(497, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // tabpStopWatch
            // 
            this.tabpStopWatch.Controls.Add(this.ucStopWatch);
            this.tabpStopWatch.Location = new System.Drawing.Point(4, 22);
            this.tabpStopWatch.Margin = new System.Windows.Forms.Padding(0);
            this.tabpStopWatch.Name = "tabpStopWatch";
            this.tabpStopWatch.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tabpStopWatch.Size = new System.Drawing.Size(489, 318);
            this.tabpStopWatch.TabIndex = 1;
            this.tabpStopWatch.Text = "Stop Watch";
            this.tabpStopWatch.UseVisualStyleBackColor = true;
            // 
            // cbxIsTopMost
            // 
            this.cbxIsTopMost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxIsTopMost.AutoSize = true;
            this.cbxIsTopMost.BackColor = System.Drawing.SystemColors.Window;
            this.cbxIsTopMost.Checked = true;
            this.cbxIsTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsTopMost.Location = new System.Drawing.Point(426, 7);
            this.cbxIsTopMost.Name = "cbxIsTopMost";
            this.cbxIsTopMost.Size = new System.Drawing.Size(71, 17);
            this.cbxIsTopMost.TabIndex = 0;
            this.cbxIsTopMost.Text = "Top Most";
            this.cbxIsTopMost.UseVisualStyleBackColor = false;
            this.cbxIsTopMost.CheckedChanged += new System.EventHandler(this.CbxIsTopMostCheckedChanged);
            // 
            // ucCountdown
            // 
            this.ucCountdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCountdown.Location = new System.Drawing.Point(0, 10);
            this.ucCountdown.Margin = new System.Windows.Forms.Padding(0);
            this.ucCountdown.Name = "ucCountdown";
            this.ucCountdown.Size = new System.Drawing.Size(489, 286);
            this.ucCountdown.StatusReporter = null;
            this.ucCountdown.TabIndex = 0;
            // 
            // ucStopWatch
            // 
            this.ucStopWatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStopWatch.Location = new System.Drawing.Point(0, 10);
            this.ucStopWatch.Margin = new System.Windows.Forms.Padding(0);
            this.ucStopWatch.Name = "ucStopWatch";
            this.ucStopWatch.Size = new System.Drawing.Size(489, 308);
            this.ucStopWatch.TabIndex = 0;
            // 
            // TimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 368);
            this.Controls.Add(this.cbxIsTopMost);
            this.Controls.Add(this.tabTimer);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TimerForm";
            this.Text = "Timer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TimerFormFormClosed);
            this.Load += new System.EventHandler(this.TimerFormLoad);
            this.Resize += new System.EventHandler(this.TimerFormResize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabTimer.ResumeLayout(false);
            this.tabpCountdown.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabpStopWatch.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon nicoMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl tabTimer;
        private System.Windows.Forms.TabPage tabpStopWatch;
        private System.Windows.Forms.TabPage tabpCountdown;
        private CountdownUserControl ucCountdown;
        private StopWatchUserControl ucStopWatch;
        private System.Windows.Forms.CheckBox cbxIsTopMost;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetStateToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

