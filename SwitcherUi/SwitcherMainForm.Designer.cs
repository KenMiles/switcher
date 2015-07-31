namespace SwitcherUi
{
    partial class SwitcherMainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SwitcherMainForm));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlLogs = new System.Windows.Forms.TabControl();
            this.pageIssues = new System.Windows.Forms.TabPage();
            this.txtIssuesDisplay = new System.Windows.Forms.TextBox();
            this.pageSwitchLog = new System.Windows.Forms.TabPage();
            this.switchLog = new System.Windows.Forms.TextBox();
            this.timerCheckStatus = new System.Windows.Forms.Timer(this.components);
            this.cbProject = new System.Windows.Forms.ComboBox();
            this.buttonSwitch = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avoidProcessesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.substDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.javaHomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConfigureProject = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControlLogs.SuspendLayout();
            this.pageIssues.SuspendLayout();
            this.pageSwitchLog.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "notifyIcon1";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 26);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // tabControlLogs
            // 
            this.tabControlLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlLogs.Controls.Add(this.pageIssues);
            this.tabControlLogs.Controls.Add(this.pageSwitchLog);
            this.tabControlLogs.Location = new System.Drawing.Point(9, 81);
            this.tabControlLogs.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlLogs.Name = "tabControlLogs";
            this.tabControlLogs.SelectedIndex = 0;
            this.tabControlLogs.Size = new System.Drawing.Size(838, 409);
            this.tabControlLogs.TabIndex = 6;
            this.tabControlLogs.SelectedIndexChanged += new System.EventHandler(this.tabControlLogs_SelectedIndexChanged);
            this.tabControlLogs.TabIndexChanged += new System.EventHandler(this.tabControlLogs_TabIndexChanged);
            // 
            // pageIssues
            // 
            this.pageIssues.Controls.Add(this.txtIssuesDisplay);
            this.pageIssues.Location = new System.Drawing.Point(4, 22);
            this.pageIssues.Margin = new System.Windows.Forms.Padding(2);
            this.pageIssues.Name = "pageIssues";
            this.pageIssues.Padding = new System.Windows.Forms.Padding(2);
            this.pageIssues.Size = new System.Drawing.Size(830, 383);
            this.pageIssues.TabIndex = 0;
            this.pageIssues.Text = "Issues";
            this.pageIssues.UseVisualStyleBackColor = true;
            // 
            // txtIssuesDisplay
            // 
            this.txtIssuesDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIssuesDisplay.Location = new System.Drawing.Point(4, 5);
            this.txtIssuesDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.txtIssuesDisplay.Multiline = true;
            this.txtIssuesDisplay.Name = "txtIssuesDisplay";
            this.txtIssuesDisplay.ReadOnly = true;
            this.txtIssuesDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIssuesDisplay.Size = new System.Drawing.Size(824, 376);
            this.txtIssuesDisplay.TabIndex = 1;
            this.txtIssuesDisplay.TextChanged += new System.EventHandler(this.txtIssuesDisplay_TextChanged);
            // 
            // pageSwitchLog
            // 
            this.pageSwitchLog.Controls.Add(this.switchLog);
            this.pageSwitchLog.Location = new System.Drawing.Point(4, 22);
            this.pageSwitchLog.Margin = new System.Windows.Forms.Padding(2);
            this.pageSwitchLog.Name = "pageSwitchLog";
            this.pageSwitchLog.Padding = new System.Windows.Forms.Padding(2);
            this.pageSwitchLog.Size = new System.Drawing.Size(830, 383);
            this.pageSwitchLog.TabIndex = 1;
            this.pageSwitchLog.Text = "Switch Log";
            this.pageSwitchLog.UseVisualStyleBackColor = true;
            // 
            // switchLog
            // 
            this.switchLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.switchLog.Location = new System.Drawing.Point(4, 5);
            this.switchLog.Margin = new System.Windows.Forms.Padding(2);
            this.switchLog.Multiline = true;
            this.switchLog.Name = "switchLog";
            this.switchLog.ReadOnly = true;
            this.switchLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.switchLog.Size = new System.Drawing.Size(824, 392);
            this.switchLog.TabIndex = 2;
            // 
            // timerCheckStatus
            // 
            this.timerCheckStatus.Enabled = true;
            this.timerCheckStatus.Interval = 1000;
            this.timerCheckStatus.Tick += new System.EventHandler(this.timerCheckStatus_Tick);
            // 
            // cbProject
            // 
            this.cbProject.FormattingEnabled = true;
            this.cbProject.Location = new System.Drawing.Point(45, 40);
            this.cbProject.Margin = new System.Windows.Forms.Padding(2);
            this.cbProject.Name = "cbProject";
            this.cbProject.Size = new System.Drawing.Size(233, 21);
            this.cbProject.TabIndex = 9;
            // 
            // buttonSwitch
            // 
            this.buttonSwitch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSwitch.Location = new System.Drawing.Point(288, 40);
            this.buttonSwitch.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSwitch.Name = "buttonSwitch";
            this.buttonSwitch.Size = new System.Drawing.Size(56, 21);
            this.buttonSwitch.TabIndex = 8;
            this.buttonSwitch.Text = "Switch";
            this.buttonSwitch.UseVisualStyleBackColor = false;
            this.buttonSwitch.Click += new System.EventHandler(this.buttonSwitch_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(856, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.avoidProcessesToolStripMenuItem,
            this.substDriveToolStripMenuItem,
            this.javaHomeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // avoidProcessesToolStripMenuItem
            // 
            this.avoidProcessesToolStripMenuItem.Name = "avoidProcessesToolStripMenuItem";
            this.avoidProcessesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.avoidProcessesToolStripMenuItem.Text = "Avoid Processes";
            this.avoidProcessesToolStripMenuItem.Click += new System.EventHandler(this.avoidProcessesToolStripMenuItem_Click);
            // 
            // substDriveToolStripMenuItem
            // 
            this.substDriveToolStripMenuItem.Name = "substDriveToolStripMenuItem";
            this.substDriveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.substDriveToolStripMenuItem.Text = "Subst Drive";
            this.substDriveToolStripMenuItem.Click += new System.EventHandler(this.substDriveToolStripMenuItem_Click);
            // 
            // javaHomeToolStripMenuItem
            // 
            this.javaHomeToolStripMenuItem.Name = "javaHomeToolStripMenuItem";
            this.javaHomeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.javaHomeToolStripMenuItem.Text = "Java Home";
            this.javaHomeToolStripMenuItem.Click += new System.EventHandler(this.javaHomeToolStripMenuItem_Click);
            // 
            // btnConfigureProject
            // 
            this.btnConfigureProject.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnConfigureProject.Location = new System.Drawing.Point(359, 40);
            this.btnConfigureProject.Margin = new System.Windows.Forms.Padding(2);
            this.btnConfigureProject.Name = "btnConfigureProject";
            this.btnConfigureProject.Size = new System.Drawing.Size(67, 21);
            this.btnConfigureProject.TabIndex = 11;
            this.btnConfigureProject.Text = "Configure";
            this.btnConfigureProject.UseVisualStyleBackColor = false;
            this.btnConfigureProject.Click += new System.EventHandler(this.btnConfigureProject_Click);
            // 
            // SwitcherMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 496);
            this.Controls.Add(this.btnConfigureProject);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.cbProject);
            this.Controls.Add(this.buttonSwitch);
            this.Controls.Add(this.tabControlLogs);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SwitcherMainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControlLogs.ResumeLayout(false);
            this.pageIssues.ResumeLayout(false);
            this.pageIssues.PerformLayout();
            this.pageSwitchLog.ResumeLayout(false);
            this.pageSwitchLog.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlLogs;
        private System.Windows.Forms.TabPage pageIssues;
        private System.Windows.Forms.TabPage pageSwitchLog;
        private System.Windows.Forms.TextBox txtIssuesDisplay;
        private System.Windows.Forms.TextBox switchLog;
        private System.Windows.Forms.Timer timerCheckStatus;
        private System.Windows.Forms.ComboBox cbProject;
        private System.Windows.Forms.Button buttonSwitch;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem avoidProcessesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem substDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem javaHomeToolStripMenuItem;
        private System.Windows.Forms.Button btnConfigureProject;
    }
}

