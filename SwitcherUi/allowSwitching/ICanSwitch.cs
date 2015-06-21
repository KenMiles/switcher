using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.allowSwitching
{
    public enum EnumCanSwitch {csYes, csAfterWarning, csNo };
    public struct CanSwitchResult {
        public EnumCanSwitch AllowSwitch { get; set; }
        public string[] Blocking { get; set; }
        public string[] Warning { get; set; }
    }

    interface ICanSwitch
    {
        CanSwitchResult CanSwitch();
    }
}
