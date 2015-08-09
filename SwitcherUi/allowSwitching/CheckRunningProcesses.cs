using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherCommon;
using IConfiguration = SwitcherUi.config.IConfiguration;

namespace SwitcherUi.allowSwitching
{
    class CheckRunningProcesses : ICanSwitch
    {
        private readonly IConfiguration _config;
        public CheckRunningProcesses(IConfiguration config) {
            _config = config;
        }

        private EnumCanSwitch CanSwitchMode(int noBlocking, int noWarning) {
            return noBlocking > 0 ? EnumCanSwitch.csNo : (noWarning > 0 ? EnumCanSwitch.csAfterWarning : EnumCanSwitch.csYes);
        }

        private string ProcessDescription(Process process, Settings processDescriptions) {
            if (!string.IsNullOrWhiteSpace(process.MainWindowTitle)) {
                return string.Format("{0} - {1}", process.ProcessName, process.MainWindowTitle);
            }
            var processName = process.ProcessName.ToLower();
            if (processDescriptions.ContainsKey(processName)) {
                return string.Format("{0} - {1}", process.ProcessName, processDescriptions[processName]);
            }
            return process.ProcessName;
        }

        private string[] Messages(string description, Process[] processes, Settings processDescriptions) 
        {
            if (processes == null || processes.Length == 0) return new string[0];
            return new string[] { description, "" }.Concat(processes.Select(p => ProcessDescription(p, processDescriptions))).ToArray();
        }

        public CanSwitchResult CanSwitch()
        {
            var blocking = _config.BlockingProcesses;
            var warning = _config.WarningProcesses;
            Process[] processlist = Process.GetProcesses();
            var blockingProcesses = processlist.Where(p => blocking.ContainsKey(p.ProcessName.ToLower())).ToArray();
            var warningProcesses = processlist.Where(p => warning.ContainsKey(p.ProcessName.ToLower())).ToArray();
            return new CanSwitchResult {
                AllowSwitch = CanSwitchMode(blockingProcesses.Length, warningProcesses.Length),
                Blocking = Messages("The Following Critcial Processes are running:", blockingProcesses, blocking),
                Warning = Messages("The Following Processes are running:", warningProcesses, warning)
            };

        }
    }
}
