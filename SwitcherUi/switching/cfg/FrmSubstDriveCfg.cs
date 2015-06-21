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
            var drives = DriveInfo.GetDrives();
            //switchLog.Lines = drives.Select(d => d.Name.Substring(0, 1)).ToArray();

        }
    }
}
