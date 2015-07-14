using SwitcherUi.config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwitcherUi.switching.cfg
{
    //TODO make cleaner seperation of data and display
    public partial class FrmJavaHomeCfg : Form
    {
        delegate ListViewItem ListItem();
        private string _lastValue;
        private string _lastRoot;
        private ListViewItem _changing;
        private readonly Configuration _config;
        internal FrmJavaHomeCfg(Configuration config)
        {
            InitializeComponent();
            SetButtonStates();
            _config = config;
            var paths = _config.GetSwitcherCfg(JavaHome.JAVA_HOME, JavaHome.JAVA_PATHS).Values.ToList();
            paths.ForEach(v => setValues(NewListItem, v.Key, v.Value));
        }

        private void SetButtonStates() {
            btnUpdate.Enabled = _changing != null;
            btnRemove.Enabled = Selected() != null;
        }

        private void SetLastValues()
        {
            _lastValue = edtJavaSuffix.Text;
            _lastRoot = edtPath.Text;
        }

        private void SetValues(string suffix, string rootPath) {
            edtJavaSuffix.Text = suffix;
            edtPath.Text = rootPath;
            SetLastValues();
        }

        private void ClearValues()
        {
            SetValues("", "");
        }

        private bool Check(bool isError, string message, Control selectControl) {
            if (!isError) return true;
            MessageBox.Show(message, "Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (selectControl != null) selectControl.Select();
            return false;
        }

        private bool Check(TextBox textBox, string descritpion)
        {
            return Check(string.IsNullOrEmpty(textBox.Text), string.Format("Expecting value in {0}", descritpion), textBox);
        }

        private bool Check()
        {
            return Check(edtJavaSuffix, "Java Suffix") && Check(edtPath, "Root Path");
        }

        private void setValues(ListItem listItem, string path, string suffix) {
            var li = listItem();
            li.SubItems.Clear();
            li.Text = path;
            li.SubItems.Add(suffix);
        }

        private bool CheckAlreadyExists(bool adding)
        {
            var path = edtPath.Text.ToLower();
            foreach (ListViewItem item in lvJavaSearchLocations.Items)
            {
                if (item.Text.ToLower() == path && (adding || item != _changing)) return true;
            }
            return false;
        }
        private void SetValue(ListItem listItem, bool clear, bool adding)
        {
            if (!Check()) return;
            if (CheckAlreadyExists(adding)) {
                var message = string.Format("There is already an entry for path {0}", edtPath.Text);
                MessageBox.Show(message, "Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            setValues(listItem, edtPath.Text, edtJavaSuffix.Text);
            if (clear) ClearValues();
            SetLastValues();
            SetButtonStates();
        }

        private ListViewItem NewListItem() {
            var li = new ListViewItem();
            lvJavaSearchLocations.Items.Add(li);
            return li;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetValue(NewListItem, true, true);
        }

        private bool AbandonChanges() {
            if (string.IsNullOrWhiteSpace(edtJavaSuffix.Text) && string.IsNullOrWhiteSpace(edtPath.Text)) return true;
            var changed =  edtJavaSuffix.Text != _lastValue || edtPath.Text != _lastRoot;
            return !changed || MessageBox.Show("Abandon Changes", "Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private ListViewItem Selected() {
            var selected = lvJavaSearchLocations.SelectedItems;
            return selected != null && selected.Count == 1 ? selected[0] : null;
        }

        private void lvJavaSearchLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_changing != Selected() && !AbandonChanges())
            {
                SetButtonStates();
                return;
            }
            _changing = Selected();
            var changingValue = _changing != null;
            btnUpdate.Enabled = changingValue;
            if (changingValue)
            {
                SetValues(_changing.SubItems[1].Text, _changing.Text);
            }
            else {
                ClearValues();
            }
            SetButtonStates();
        }

        private void DoUpdateOnSelected(Action action) {
            if (_changing == null)
            {
                MessageBox.Show("Something is wrong as _changing is Null :s");
                return;
            }
            action();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DoUpdateOnSelected( () => SetValue(() => _changing, _changing != Selected(), false));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DoUpdateOnSelected(() => {
                _changing.Remove();
            });
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var initialDir = edtPath.Text;
            if (!string.IsNullOrWhiteSpace(initialDir) && Directory.Exists(initialDir)) {
                folderBrowserDialog1.SelectedPath = initialDir;
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                edtPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private Settings GetValues() {
            var result = Settings.SettingsDictionary();
            foreach (ListViewItem item in lvJavaSearchLocations.Items) {
                if (result.ContainsKey(item.Text)) throw new Exception(string.Format("Multiple entries for path'{0}'", item.Text));
                result.Add(item.Text, item.SubItems[1].Text);
            }
            return new Settings(result);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _config.SetSwitcherCfg(JavaHome.JAVA_HOME, JavaHome.JAVA_PATHS, GetValues());
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
