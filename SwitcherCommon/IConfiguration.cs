using System.Collections.Generic;

namespace SwitcherCommon
{
    public interface IConfiguration
    {
        Settings BlockingProcesses { get; set; }
        Settings WarningProcesses { get; set; }

        Settings GetSwitcherCfg(string switcherName);
        void SetSwitcherCfg(string switcherName, Settings value);
        Settings GetSwitcherCfg(string switcherName, string section);
        void SetSwitcherCfg(string switcherName, string section, Settings value);

        Settings this[string projectName] { get; set; }
        Settings this[string projectName, string section] { get; set; }

        string[] ProjectNames { get; }
        string CurrentProject { get; set; }

        Dictionary<string, string> IgnoreServices();

    }
}
