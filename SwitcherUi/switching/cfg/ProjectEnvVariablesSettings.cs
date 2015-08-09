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
    public partial class ProjectEnvVariablesSettings : UserControl, IProjectItem
    {
        public ProjectEnvVariablesSettings()
        {
            InitializeComponent();
        }

        private string VarName(string name)
        {
            if (name == null) return "";
            return name.ToUpper().Trim();
        }

        private ListViewItem CurrentMatch()
        {
            var name = VarName(edtName.Text);
            return ListViewItemsByName.ContainsKey(name) ? ListViewItemsByName[name] : null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = VarName(edtName.Text);
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Please Provide A Variable Name");
                return;
            }
            var currentMatch = CurrentMatch();
            if (currentMatch != null)
            {
                currentMatch.SubItems[1].Text = edtValue.Text;
            }
            else
            {
                lvEnvVars.Items.Add(EnvVarLisViewItem(name, edtValue.Text));
                ClearListViewItemsByNameCache();
            }
            btnClear_Click(sender, e);
        }

        private Dictionary<string, ListViewItem> BuildListViewItemsByName()
        {
            var interm = from ListViewItem lvItem in lvEnvVars.Items select lvItem;
            return interm.ToDictionary(d => d.Text, d => d, StringComparer.OrdinalIgnoreCase);
        }

        private Dictionary<string, ListViewItem> _listViewItemsByName = null;
        private Dictionary<string, ListViewItem> ListViewItemsByName { get
        {
            return _listViewItemsByName = _listViewItemsByName ?? BuildListViewItemsByName();
        } }

        private void ClearListViewItemsByNameCache()
        {
            _listViewItemsByName = null;
        }

        private Dictionary<string, string> DesiredVariables()
        {
            var interm = from ListViewItem lvItem in lvEnvVars.Items select new {name = lvItem.Text, value = lvItem.SubItems[1].Text};
            return interm.ToDictionary(d => d.name, d => d.value, StringComparer.OrdinalIgnoreCase);
        }

        private ListViewItem EnvVarLisViewItem(string name, string value)
        {
            var result = new ListViewItem { Text = VarName(name) };
            result.SubItems.Add(value);
            return result;
        }

        public void DisplayCurrent(Settings projectSettings, IConfiguration config, Project project)
        {
            var variables = config[project, EnvironmentVariables.ProjectConfigSectionName];
            lvEnvVars.Items.AddRange(variables.Values.Select(d => EnvVarLisViewItem(d.Key, d.Value)).ToArray());
        }

        public void SaveConfig(Settings projectSettings, IConfiguration config, Project project)
        {
            config[project, EnvironmentVariables.ProjectConfigSectionName] = new Settings(DesiredVariables());
        }

        private void edtName_TextChanged(object sender, EventArgs e)
        {
            var current = CurrentMatch();
            btnAdd.Text = current == null ? "Add" : "Update";
            if (current != null) current.Selected = true;
        }

        private bool DataChanged()
        {
            var current = CurrentMatch();
            return current == null && !string.IsNullOrWhiteSpace(edtValue.Text) ||
                   current != null && current.SubItems[1].Text != edtValue.Text;
        }

        private void lvEnvVars_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = lvEnvVars.SelectedIndices.Count > 0;
            if (!btnRemove.Enabled || DataChanged()) return;
            var sel = lvEnvVars.SelectedItems[0];
            edtName.Text = sel.Text;
            edtValue.Text = sel.SubItems[1].Text;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvEnvVars.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Nothing selected to remove");
                return;
            }
            ClearListViewItemsByNameCache();
            var sel = lvEnvVars.SelectedItems[0];
            edtName.Text = sel.SubItems[0].Text;
            edtValue.Text = sel.SubItems[1].Text;
            lvEnvVars.Items.Remove(sel);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            edtValue.Text = "";
            edtName.Text = "";
        }
    }
}
