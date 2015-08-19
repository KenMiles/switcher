using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitcherCommon;
using SwitcherUi.config;

namespace SwitcherUi.switching.cfg
{
    public partial class ProjectJavaSettings : UserControl, IProjectItem
    {
        public ProjectJavaSettings()
        {
            InitializeComponent();
        }

        private Dictionary<string, JavaJdk> _availableJdks;
        private string _projectPreferredJdkVersion;
        private string[] _projectPreferredJdks;
        private string _lastSelected;

        private string CurrentlySelected()
        {
            if (cbPreferredJavaVersion.SelectedIndex >= 0) return cbPreferredJavaVersion.Text;
            if (!string.IsNullOrWhiteSpace(_projectPreferredJdkVersion)) return _projectPreferredJdkVersion;
            if (cbPreferredJavaVersion.Items.Count > 0) return cbPreferredJavaVersion.Items[0].ToString();
            return "8";
        }

        private object[] AvailableJdksToAdd()
        {
                var curent = CurrentlySelected();
                var sameVersion = _availableJdks.Values.Where(j => j.MajorVersion == curent);
                return sameVersion.Select(j => (object) j.EnvironmentVariable).Distinct().Except(DesiredJavaVersions()).ToArray();
        }

        private void SetUpAvailableToAddCombo()
        {
            var previous = cbAvailableJdkToAdd.Text;
            cbAvailableJdkToAdd.Items.Clear();
            var avail = AvailableJdksToAdd();
            cbAvailableJdkToAdd.Items.AddRange(avail);
            cbAvailableJdkToAdd.SelectedIndex = avail.Length == 1? 0 : cbAvailableJdkToAdd.Items.IndexOf(previous);
            cbAvailableJdkToAdd.Enabled = avail.Length > 0;
            btnAdd.Enabled = avail.Length > 0;
        }

        private object[] JavaVersion {
            get
            {
                var jdkByVersion =
                    _availableJdks.Values.GroupBy(j => new {majorP1 = j.MajorVersionPt1, majorP2 = j.MajorVersionPt2})
                        .OrderByDescending(jg => jg.Key.majorP1)
                        .ThenByDescending(jg => jg.Key.majorP2);
                return jdkByVersion.Select(jg => (object)jg.First().MajorVersion).ToArray();
            }
        }

        private string[] DesiredJavaVersions()
        {
            return (from ListViewItem lvItem in lvDesiredJavaVersion.Items select (lvItem.Text)).ToArray();
        }

        private ListViewItem JdkListItem(string javaVersion)
        {
            var available = _availableJdks.ContainsKey(javaVersion);
            var result = new ListViewItem {Text = javaVersion};
            result.SubItems.Add(available ? "Yes" : "No");
            result.SubItems.Add(available ? _availableJdks[javaVersion].Path : "");
            return result;
        }

        public void DisplayCurrent(Settings projectSettings, config.IConfiguration config, Project project)
        {
            var switcher = new JavaHome(config);
            _availableJdks = switcher.LatestJdk;
            var newestVersion = _availableJdks.Values.OrderByDescending(j => j.MajorVersionPt2).FirstOrDefault();
            _projectPreferredJdks = projectSettings.ArrayValue("JAVA");
            _projectPreferredJdkVersion = projectSettings.Value("JAVA Version", newestVersion == null ? "8" :newestVersion.MajorVersion);
            lvDesiredJavaVersion.Items.AddRange(_projectPreferredJdks.Select(JdkListItem).ToArray());
            cbPreferredJavaVersion.Items.AddRange(JavaVersion);
            cbPreferredJavaVersion.SelectedIndex = cbPreferredJavaVersion.Items.IndexOf((_projectPreferredJdkVersion));
            SetUpAvailableToAddCombo();
            _lastSelected = _projectPreferredJdkVersion;
        }

        public void SaveConfig(Settings projectSettings, config.IConfiguration config, Project project)
        {
            projectSettings.SetArrayValue("JAVA", DesiredJavaVersions());
            projectSettings["JAVA Version"] = CurrentlySelected();
        }

        private bool WarnIfNothingSelected()
        {
            var result = lvDesiredJavaVersion.SelectedItems.Count == 0;
            if (result) MessageBox.Show("No JDK is selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return result;
        }

        private void DoActionOnListView(Action action)
        {
            if (WarnIfNothingSelected()) return;
            try
            {
                action();
            }
            finally
            {
                SetUpAvailableToAddCombo();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DoActionOnListView(() => lvDesiredJavaVersion.SelectedItems[0].Remove());
        }

        enum ChangePriority { Increase, Decrease}
        private void ChangePositionOnListView(ChangePriority changePriority)
        {
            DoActionOnListView(() =>
            {
                var selected = lvDesiredJavaVersion.SelectedItems[0];
                var currentIndex = selected.Index;
                var goingUp = changePriority == ChangePriority.Increase;
                if (goingUp && currentIndex == 0 || changePriority == ChangePriority.Decrease && currentIndex == lvDesiredJavaVersion.Items.Count - 1)
                {
                    var alreadyItem = goingUp ? "First" : "Last";
                    MessageBox.Show($"Already {alreadyItem} Item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                lvDesiredJavaVersion.Items.Remove(selected);
                lvDesiredJavaVersion.Items.Insert(currentIndex + (goingUp ? - 1 : 1), selected);
                selected.Selected = true;
            });
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            ChangePositionOnListView(ChangePriority.Increase);

        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            ChangePositionOnListView(ChangePriority.Decrease);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbAvailableJdkToAdd.SelectedIndex < 0)
            {
                MessageBox.Show("No JDK is selected To Add", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lvDesiredJavaVersion.Items.Add(JdkListItem(cbAvailableJdkToAdd.Text));
            SetUpAvailableToAddCombo();
        }

        private void cbPreferredJavaVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lastSelected == null) return;
            Regex reg = new Regex("(?<=JAVA_)" + _lastSelected + "(?=_|$)");
            var newValues = DesiredJavaVersions().Select(j => reg.Replace(j, CurrentlySelected()));
            var selected = lvDesiredJavaVersion.SelectedItems.Count > 0 ? lvDesiredJavaVersion.SelectedItems[0].Index : -1;
            lvDesiredJavaVersion.Items.Clear();
            lvDesiredJavaVersion.Items.AddRange(newValues.Select(JdkListItem).ToArray());
            _lastSelected = CurrentlySelected();
            if (selected >= 0) lvDesiredJavaVersion.Items[selected].Selected = true;
            SetUpAvailableToAddCombo();
        }
    }
}
