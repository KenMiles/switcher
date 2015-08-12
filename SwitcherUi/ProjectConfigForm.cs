using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly Dictionary<string, string> _projectNames;
        private readonly List<string> _projectDisplayNames;

        public ProjectConfigForm(IConfiguration config, Project project, Project sourceProject = null)
        {
            InitializeComponent();
            _config = config;
            _project = project;
            var source = sourceProject ?? project;
            _projectConfigItems = GetAll(this).Where(IsProjectConfigControl).Cast<IProjectItem>().ToList();
            _projectConfigItems.ForEach(pi => pi.DisplayCurrent(_config[source], _config, source));
            edtDisplayName.Text = project.DisplayName;
            _projectNames = new ProjectManager(config).ProjectNames();
            _projectDisplayNames = _projectNames.Where(d => !project.Name.Equals(d.Key, StringComparison.InvariantCultureIgnoreCase))
                    .Select(d => d.Value.Trim().ToLower()).ToList();
            Text = "Configuration for Project '" + project.DisplayName + "'";
        }

        private ProjectConfigForm(IConfiguration config) : this(config, new ProjectManager(config).CurrentProject())
        {
        }

        public ProjectConfigForm(): this(new ConfigurationImpl())
        {
        }
         
        private bool CanSave(bool check, string message)
        {
            if (!check) MessageBox.Show(message, "Save Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return check;
        }


        private bool DisplayNameOk()
        {
            return !_projectDisplayNames.Contains(edtDisplayName.Text.ToLower().Trim());
        }

        private bool CheckCanSave()
        {
            return CanSave(DisplayNameOk(), $"Display Name '{edtDisplayName.Text}' conflicts with another Project");
        }


        private string ProjectName()
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var nameRoot = rgx.Replace(edtDisplayName.Text, "").ToLower().Trim();
            if (string.IsNullOrWhiteSpace(nameRoot)) nameRoot = "Project";
            var name = nameRoot;
            var idx = 0;
            while (_projectNames.ContainsKey(name) || _projectDisplayNames.Contains(name))
            {
                name = $"{nameRoot}_{idx++}";
            }
            return name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckCanSave()) return;
            if (string.IsNullOrWhiteSpace(_project.Name)) _project.Name = ProjectName();
            var projectSettings = _config[_project];
            _projectConfigItems.ForEach(pi => pi.SaveConfig(projectSettings, _config, _project));
            projectSettings.Values[Project.DisplayNameSettingName] = edtDisplayName.Text.Trim();
            _config[_project] = projectSettings;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
