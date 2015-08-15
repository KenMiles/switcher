using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SwitcherCommon;
using IConfiguration = SwitcherUi.config.IConfiguration;

namespace SwitcherUi.allowSwitching.cfg
{
    public partial class ProcessesForm : Form
    {
        private readonly IConfiguration _config;
        internal ProcessesForm(IConfiguration config)
        {
            _config = config;
            InitializeComponent();
        }

        private void PopulateListView(ListView lv, Settings processes) {
           lv.Items.AddRange(processes.Values.Select(p => new ListViewItem(new[] { p.Key, p.Value })).ToArray());
        }

        private void ProcessesForm_Load(object sender, EventArgs e)
        {
            PopulateListView(lvBlocking, _config.BlockingProcesses);
            PopulateListView(lvWarning, _config.WarningProcesses);
        }

        private void RemoveSelected(ListView lv) {
            foreach (ListViewItem item in lv.SelectedItems)
            {
                lv.Items.Remove(item);
            }
        }

        private void btnRemoveBlocking_Click(object sender, EventArgs e)
        {
            RemoveSelected(lvBlocking);
        }

        private void btnRemoveWarning_Click(object sender, EventArgs e)
        {
            RemoveSelected(lvWarning);
        }

        private ListViewItem FileDetails(string fileName) {
            var exeName = Path.GetFileNameWithoutExtension(fileName);
            var info = FileVersionInfo.GetVersionInfo(fileName);
            var descEmpty = info == null || string.IsNullOrWhiteSpace(info.FileDescription);
            var desc = descEmpty ?  exeName: info.FileDescription;
            return new ListViewItem(new[] { exeName, desc});
        }

        private void Add(ListView lv, string typeDesc)
        {
            openFileDialog1.Title = "Select Executables for " + typeDesc + " when running";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            lv.Items.AddRange(openFileDialog1.FileNames.Select(FileDetails).ToArray());
        }


        private void btnAddBlocking_Click(object sender, EventArgs e)
        {
            Add(lvBlocking, "Blocking");
        }

        private void btnAddWarning_Click(object sender, EventArgs e)
        {
            Add(lvWarning, "Warning");
        }

        private void MoveSelected(ListView lvSource, ListView lvDestination)
        {
            foreach (ListViewItem item in lvSource.SelectedItems)
            {
                lvDestination.Items.Add((ListViewItem)item.Clone());
                lvSource.Items.Remove(item);
            }
        }

        private void btnMoveWarning_Click(object sender, EventArgs e)
        {
            MoveSelected(lvBlocking, lvWarning);
        }

        private void btnMoveBlocking_Click(object sender, EventArgs e)
        {
            MoveSelected(lvWarning, lvBlocking);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private Settings ValuesFrom(ListView lv) {
            var values = Settings.SettingsDictionary();
            foreach (ListViewItem item in lv.Items) {
                if (!values.ContainsKey(item.Text)) {
                    var desc = item.SubItems.Count > 1 ? item.SubItems[1].Text : item.Text;
                    values.Add(item.Text, desc);
                }
            }
            return new Settings(values);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _config.BlockingProcesses = ValuesFrom(lvBlocking);
            _config.WarningProcesses = ValuesFrom(lvWarning);
            DialogResult = DialogResult.OK;
        }
    }
}
