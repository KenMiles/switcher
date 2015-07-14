using SwitcherUi.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.switching
{
    struct SwitchResult {
        public string SourceName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    interface ISwitch
    {
        string Name { get; }
        SwitchResult SwitchTo(Project project);
        SwitchResult MakeReadyForConfig();
    }
}
