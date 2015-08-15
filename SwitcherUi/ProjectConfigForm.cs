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
using SwitcherUi.switching;
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

        /*            
            //tabControl1.SuspendLayout();
            //tab.SuspendLayout();
            //Controls.Add(tab);

            tabControl1.TabPages.Add(tab);
            var test = new ProjectJavaSettings
            {
                Location = new Point(0, 0),
                Size = new Size(747, 262),
            };
            tab.Controls.Add(test);
*/

        private readonly IConfiguration _config;
        private readonly Project _project;
        private readonly List<IProjectItem> _projectConfigItems;
        private readonly Dictionary<string, string> _projectNames;
        private readonly List<string> _projectDisplayNames;

        private const int TabHeight = 262;
        private const int TabWidth = 747;
        private const int TabPadding = 3;

        private int NextTabIndex = 1;

        private TabPage TabPage(ProjectConfig cfg)
        {
            if (cfg?.CreateControl == null) return null;
            var tab = new TabPage
            {
                Location = new Point(0, 0),
                Padding = new Padding(TabPadding),
                Size = new Size(TabWidth, TabHeight),
                TabIndex = NextTabIndex++,
                Text = cfg.SectionCaption,
                UseVisualStyleBackColor = true
            };
            var control = cfg.CreateControl();
            control.Location = new Point(0, 0);
            control.Size = new Size(TabWidth, TabHeight);
            tab.Controls.Add(control);
            return tab;
        }

        public ProjectConfigForm(IConfiguration config, IEnumerable<ProjectConfig> projectConfig, Project project, Project sourceProject = null)
        {
            InitializeComponent();
            if (projectConfig == null) throw new ArgumentNullException(nameof(projectConfig));
            tabControlConfigurations.TabPages.AddRange(projectConfig.Select(TabPage).Where(t => t != null).ToArray());
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

        private ProjectConfigForm(IConfiguration config) : this(config, new Switcher(config).ProjectConfigOptions(),   new ProjectManager(config).CurrentProject())
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
