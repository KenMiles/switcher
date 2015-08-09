using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitcherCommon;
using SwitcherUi.config;
using IConfiguration = SwitcherUi.config.IConfiguration;

namespace SwitcherUi.switching.cfg
{
    public partial class ProjectDatabaseStarter : UserControl, IProjectItem
    {
        public ProjectDatabaseStarter()
        {
            InitializeComponent();
        }

        private void ProjectDatabaseStarter_Load(object sender, EventArgs e)
        {

        }

        public void DisplayCurrent(Settings projectSettings, IConfiguration config, Project project)
        {
            var cfg = new DatabaseControllerConfig(config);
            cbDatabases.Items.AddRange(cfg.Databases.Cast<object>().ToArray());
            var db = project.Settings.ContainsKey(DatabaseStarter.DatabasesSettingName) ? project.Settings[DatabaseStarter.DatabasesSettingName] : "";
            chkEnabled.Checked = !string.IsNullOrWhiteSpace(db);
            cbDatabases.SelectedIndex = cbDatabases.Items.IndexOf(db??"");
            chkEnabled_CheckedChanged(null, null);
        }

        public void SaveConfig(Settings projectSettings, IConfiguration config, Project project)
        {
            projectSettings[DatabaseStarter.DatabasesSettingName] = chkEnabled.Checked ? cbDatabases.Text : "";
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            cbDatabases.Enabled = chkEnabled.Checked;
        }
    }
}
