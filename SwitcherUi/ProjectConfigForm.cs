using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitcherUi.config;
using SwitcherUi.switching.cfg;

namespace SwitcherUi
{
    public partial class ProjectConfigForm : Form
    {
        private static readonly Type ProjectItemType = typeof(IProjectItem);
        private static bool IsProjectConfigControl(Control control)
        {
            return control != null && ProjectItemType.IsInstanceOfType(control);
        }

        public IEnumerable<Control> GetAll(Control control)
        {
            var controls = control.Controls.Cast<Control>().ToList();
            return controls.SelectMany(GetAll).Concat(controls).Where(IsProjectConfigControl);
        }

        private readonly IConfiguration _config;
        private readonly Project _project;
        private readonly List<IProjectItem> _projectConfigItems;

        public ProjectConfigForm(IConfiguration config, Project project)
        {
            InitializeComponent();
            _config = config;
            _project = project;
            _projectConfigItems = GetAll(this).Where(IsProjectConfigControl).Cast<IProjectItem>().ToList();
            _projectConfigItems.ForEach(pi => pi.DisplayCurrent(_config[project], _config, _project));
            Text = "Configuration for Project '" + project.Name + "'";
        }

        private ProjectConfigForm(IConfiguration config):this(config, new ProjectManager(config).Project(config.CurrentProject))
        {
        }

        public ProjectConfigForm(): this(new ConfigurationImpl())
        {
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            var projectSettings = _config[_project];
            _projectConfigItems.ForEach(pi => pi.SaveConfig(projectSettings, _config, _project));
            _config[_project] = projectSettings;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
