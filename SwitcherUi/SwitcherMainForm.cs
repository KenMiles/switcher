using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using SwitcherUi.config;
using SwitcherUi.allowSwitching;
using SwitcherUi.switching;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ServiceProcess;
using System.IO;
using SwitcherUi.switching.cfg;

namespace SwitcherUi
{
    public partial class SwitcherMainForm : Form, IUserFeedback
    {
        public void DisplayCurrentProject(string project)
        {
            Text = "Current Project " + project;
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
            cbProject.DisplayMember = "Name";
            cbProject.SelectedIndex = projects.ToList().IndexOf(select);
        }

        private Color CanSwitchStatusColor(EnumCanSwitch status) {
            switch (status)
            {
                case EnumCanSwitch.csNo: return Color.Red;
                case EnumCanSwitch.csAfterWarning: return Color.Orange;
            }
            return DefaultBackColor;
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
            _config = new ConfigurationImpl(new CommandArguments(Environment.GetCommandLineArgs()));
            _logic = new ProjectSwitcherLogic(_config, this);
        }



        private void button4_Click(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController("MySQL56");
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(20000);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                txtIssuesDisplay.Text = "Started";
            }
            catch (Exception ex)
            {
                txtIssuesDisplay.Text = ex.StackTrace;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            switch (this.WindowState) {
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
            }
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
            _logic.ReadCurrentProject();
            _logic.CheckCanSwitch();

        }

        private void timerCheckStatus_Tick(object sender, EventArgs e)
        {
            _logic.CheckCanSwitch();
            _showedShowSwitchedLogCount--;

        }

        private void buttonSwitch_Click(object sender, EventArgs e)
        {
            var result = _logic.SwitchTo((Project)cbProject.SelectedItem);
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

        private void avoidProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logic.DoConfig(() => new ProcessesForm(_config));
        }

        private void txtIssuesDisplay_TextChanged(object sender, EventArgs e)
        {

        }

        private void substDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logic.DoConfig(() => new FrmSubstDriveCfg(_config));
        }

        private void javaHomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logic.DoConfig(() => new FrmJavaHomeCfg(_config));
        }

        public bool ConfigureProject(Project project)
        {
            var frm = new ProjectConfigForm(_config, project);
            return frm.ShowDialog() == DialogResult.OK;
        }


        private void btnConfigureProject_Click(object sender, EventArgs e)
        {
            _logic.ConfigureProject((Project)cbProject.SelectedItem);

        }
    }


}