using SwitcherUi.switching.cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitcherCommon;
using SwitcherUi.switching;

namespace SwitcherUi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var cfg = new CommandArguments(Environment.GetCommandLineArgs());
            if (cfg.ArgumentExists("test-cfg"))
            {
                Application.Run(new ProjectConfigForm());
            }
            else
            {
                Application.Run(new SwitcherMainForm());
            }
        }
    }
}
