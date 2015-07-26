using SwitcherUi.switching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.config
{
    public interface IConfiguration
    {
        Settings BlockingProcesses { get; set; }
        Settings WarningProcesses { get; set; }

        Settings GetSwitcherCfg(string switcherName);
        void SetSwitcherCfg(string switcherName, Settings value);
        Settings GetSwitcherCfg(string switcherName, string section);
        void SetSwitcherCfg(string switcherName, string section, Settings value);

        Settings this[ISwitch switcher] { get; set; }
        Settings this[ISwitch switcher, string section] { get; set; }

        Settings this[Project project] { get; set; }
        Settings this[string projectName] { get; set; }
        Settings this[Project project, string section] { get; set; }
        Settings this[string projectName, string section] { get; set; }

        string[] ProjectNames { get; }
        string CurrentProject { get; set; }

    }
}
