using SwitcherUi.switching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherCommon;

namespace SwitcherUi.config
{
    public interface IConfiguration: SwitcherCommon.IConfiguration
    {

        Settings this[ISwitch switcher] { get; set; }
        Settings this[ISwitch switcher, string section] { get; set; }

        Settings this[Project project] { get; set; }
        Settings this[Project project, string section] { get; set; }

    }
}
