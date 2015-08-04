using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitcherUi.config;

namespace SwitcherUi.switching.cfg
{
    public partial class ProjectSubsDriveSettings : UserControl, IProjectItem
    {
        public ProjectSubsDriveSettings()
        {
            InitializeComponent();
        }

        private string RootPath { get; set; }
        private string ErrorCreatingRootPath { get; set; }

        private bool CreateRootDir()
        {
            try
            {
                Directory.CreateDirectory(RootPath);
                return Directory.Exists(RootPath);
            }
            catch (Exception e)
            {
                ErrorCreatingRootPath = e.Message;
                return false;
            }
        }

        private bool RootPathExists
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ErrorCreatingRootPath))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(RootPath))
                {
                    ErrorCreatingRootPath = "Root Path Undefined";
                    return false;
                }
                return Directory.Exists(RootPath) || CreateRootDir();
            }
        }



        public void DisplayCurrent(Settings projectSettings, IConfiguration config, Project project)
        {
            var sub = new SwitchSubstDrive(config);
            RootPath = sub.RootPath;
            var folder = sub.Folder(project);
            var message = RootPathExists ? $"In folder '{RootPath}'" : $"Configuration root folder '{RootPath}' has issues '{ErrorCreatingRootPath}'";
            lblInFolder.Text = message;
            lblPointDrive.Text = $"Point Drive '{sub.Drive}' at folder:";
            var idx = 0;
            cbFolders.Items.Add(SwitchSubstDrive.NoSubDriveRequired);
            var dirs = RootPathExists ? Directory.GetDirectories(RootPath).Select(Path.GetFileName).ToList() : new List<string>();
            if (folder.DriveRequired)
            {
                cbFolders.Items.Add(folder.Path);
                dirs.RemoveAll(f => f.Equals(folder.Path, StringComparison.InvariantCultureIgnoreCase));
                idx++;
            }
            cbFolders.Items.Add(sub.DefaultProjectFolder(project));
            cbFolders.Items.AddRange(dirs.Cast<object>().ToArray());
            cbFolders.SelectedIndex = idx;
        }

        public void SaveConfig(Settings projectSettings, IConfiguration config, Project project)
        {
            projectSettings[SwitchSubstDrive.ProjectSubstDirSettingName] = cbFolders.Text;
    }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = RootPath;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
            var path = Path.GetFullPath(folderBrowserDialog1.SelectedPath);
            var rootPath = Path.GetFullPath(RootPath).ToLower() + Path.DirectorySeparatorChar;
            if (!path.ToLower().StartsWith(rootPath))
            {
                MessageBox.Show($"Folder selected must be under folder \"{RootPath}\" - selected \"{path}\"");
                return;
            }
            var toShow = path.Substring(rootPath.Length);
            var idx = cbFolders.Items.IndexOf(toShow);
            if (idx >= 0) cbFolders.Items.RemoveAt(idx);
            cbFolders.Items.Insert(1, toShow);
            cbFolders.SelectedIndex = 1;
        }
    }
}
