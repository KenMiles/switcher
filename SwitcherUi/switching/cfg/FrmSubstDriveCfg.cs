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
    public partial class FrmSubstDriveCfg : Form
    {
        private readonly Configuration _config;
        internal FrmSubstDriveCfg(Configuration config)
        {
            _config = config;
            InitializeComponent();
        }

        private void FrmSubstDriveCfg_Load(object sender, EventArgs e)
        {
            var settings = _config.GetSwitcherCfg(SwitchSubstDrive.SwitcherName);
            var current = settings.Value(SwitchSubstDrive.DriveLetterSettingName, "").ToUpper();
            var drives = DriveInfo.GetDrives().Select(d => d.Name.Substring(0, 1)).Except(new[] { current });
            cbDriveLetter.Items.AddRange(Enumerable.Range('A', 26).Select(x => new string((char)x, 1)).Except(drives).ToArray());
            cbDriveLetter.SelectedIndex = cbDriveLetter.Items.IndexOf(current);
            txtRootPath.Text = settings.Value(SwitchSubstDrive.RootPathSettingName, "");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var settings = _config.GetSwitcherCfg(SwitchSubstDrive.SwitcherName);
            var current = settings.Value(SwitchSubstDrive.DriveLetterSettingName, "").ToUpper();
            var values = settings.Values;
            values[SwitchSubstDrive.DriveLetterSettingName] = cbDriveLetter.Text;
            values[SwitchSubstDrive.RootPathSettingName] = txtRootPath.Text;
            if (current != cbDriveLetter.Text)
            {
                var switcher = new SwitchSubstDrive(_config);
                if (!switcher.RemoveDrive()) MessageBox.Show("Unable to remove susbt drive '" + current + ":'");
            }
            _config.SetSwitcherCfg(SwitchSubstDrive.SwitcherName, new Settings(values));
            DialogResult = DialogResult.OK;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtRootPath.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                txtRootPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
