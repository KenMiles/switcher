﻿using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitcherUi.config;
using SwitcherUi.allowSwitching;
using System.Linq;
using System.Collections.Generic;
using SwitcherCommon;
using SwitcherUi.allowSwitching.cfg;
using SwitcherUi.switching.cfg;
using ConfigurationImpl = SwitcherUi.config.ConfigurationImpl;
using IConfiguration = SwitcherUi.config.IConfiguration;
using SwitcherUi.switching;

namespace SwitcherUi
{
    public partial class SwitcherMainForm : Form, IUserFeedback
    {
        public void DisplayCurrentProject(string project)
        {
            Text = "Current Project: " + project;
        }

        private readonly string[] _seperator = new[] { "", "----------", "" };
        public void DisplaySwitchLog(IEnumerable<string> logEntry)
        {
            var log = new List<string>() { DateTime.Now.ToShortTimeString(), "" };
            log.AddRange(logEntry);
            if (switchLog.Lines.Length > 1)
            {
                log.AddRange(_seperator);
                log.AddRange(switchLog.Lines);
            }
            switchLog.Lines = log.ToArray();
            tabControlLogs.SelectedTab = pageSwitchLog;
            _showedShowSwitchedLogCount = 20;
        }

        public void ErrorMessage(string subject, string message)
        {
            MessageBox.Show(message, subject, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool WarningAsk(string subject, string message)
        {
            return MessageBox.Show(message, subject, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void ListProjects(Project[] projects, Project select) {
            cbProject.DataSource = _logic.AllProjects();
            cbProject.DisplayMember = "DisplayName";
            cbProject.SelectedIndex = projects.ToList().IndexOf(select);
        }

        private Color CanSwitchStatusColor(EnumCanSwitch status) {
            switch (status)
            {
                case EnumCanSwitch.csNo: return Color.Red;
                case EnumCanSwitch.csAfterWarning: return Color.Orange;
            }
            return _switchAndShut > 0 ? Color.Green : DefaultBackColor;
        }

        private int _showedShowSwitchedLogCount = 0;
        public void ShowCanSwitchStatus(EnumCanSwitch status, string[] messages)
        {
            BackColor = CanSwitchStatusColor(status);
            //txtIssuesDisplay.BackColor = BackColor;
            txtIssuesDisplay.Lines = messages;
            if (_showedShowSwitchedLogCount <= 0 && status != EnumCanSwitch.csYes)
            {
                tabControlLogs.SelectedTab = pageIssues;
            }
        }

 
        private readonly IConfiguration _config; 
        private readonly ProjectSwitcherLogic _logic;
        public SwitcherMainForm()
        {
            InitializeComponent();
            var args = new CommandArguments(Environment.GetCommandLineArgs());
            _config = new ConfigurationImpl(args);
            _logic = new ProjectSwitcherLogic(_config, this, args);
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            /*switch (this.WindowState) {
                case FormWindowState.Minimized:
                    trayIcon.BalloonTipTitle = "Switcher";
                    trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                    trayIcon.Visible = true;
                    trayIcon.ShowBalloonTip(500);
                    this.Hide();
                    break;
                default:
                    trayIcon.Visible = false;
                    break;
            }*/
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            _logic.OnSetup();
            _logic.CheckCanSwitch();
        }

        private void timerCheckStatus_Tick(object sender, EventArgs e)
        {
            _logic.CheckCanSwitch();
            _showedShowSwitchedLogCount--;

        }

        private void buttonSwitch_Click(object sender, EventArgs e)
        {
            _logic.SwitchTo((Project)cbProject.SelectedItem);
        }

        private void tabControlLogs_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabControlLogs.SelectedTab == pageSwitchLog)
            {
                _showedShowSwitchedLogCount = 20;
            }
        }

        private void tabControlLogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlLogs.SelectedTab == pageSwitchLog)
            {
                _showedShowSwitchedLogCount = 20;
            }
        }


        public bool ConfigureProject(IEnumerable<ProjectConfig> projectConfig, Project project, Project sourceProject)
        {
            var frm = new ProjectConfigForm(_config, projectConfig, project, sourceProject);
            return frm.ShowDialog() == DialogResult.OK;
        }

        public void StartAutoSwitchAndClose()
        {
            _switchAndShut = 1;
            timerSwitch.Enabled = true;
        }


        private void btnConfigureProject_Click(object sender, EventArgs e)
        {
            _logic.ConfigureProject((Project)cbProject.SelectedItem);

        }

        private int _switchAndShut = 0;
        private void timerSwitch_Tick(object sender, EventArgs e)
        {
            if (_switchAndShut++ < 6 ) return;
            timerSwitch.Enabled = false;
            if (_logic.SwitchToCurrent()) Close();
        }


        private void shutLocalDatabasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logic.ShutLocalDatabases();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            _logic.CopyProject((Project)cbProject.SelectedItem);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _logic.NewProject();
        }

        public void AddConfigMenuItems(IEnumerable<ConfigMenuOptions> configMenuOptions)
        {
            var menuEntries = configMenuOptions.Where(c => c?.CreateConfigForm != null).Select(c =>
            {
                var menuEntry = new ToolStripMenuItem(c.MenuText);
                // watch this - two nested lambdas 
                menuEntry.Click += (sender, e) => _logic.DoConfig(() => c.CreateConfigForm(_config));
                return (ToolStripItem)menuEntry;
            });
            settingsToolStripMenuItem.DropDownItems.AddRange(menuEntries.ToArray());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _logic.DeleteProject((Project)cbProject.SelectedItem);
        }
    }


}