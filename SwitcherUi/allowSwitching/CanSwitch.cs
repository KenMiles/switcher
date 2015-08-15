using SwitcherUi.config;
using SwitcherUi.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.allowSwitching
{
    class CanSwitch
    {
        private readonly static Type[] _constructorParamTypes = new[] { typeof(IConfiguration) };

        private readonly ICanSwitch[] _canSwitches;
        internal CanSwitch(ICanSwitch[] canSwitches)
        {
            _canSwitches = canSwitches;
        }

        public CanSwitch(IConfiguration config): this(LoadImplementers.Load<ICanSwitch>(config)){
        }

        internal EnumCanSwitch Highest(CanSwitchResult[] results) {
            foreach (var result in new[] { EnumCanSwitch.csNo, EnumCanSwitch.csAfterWarning }) {
                if (results.Any(r => r.AllowSwitch == result)) return result;
            }
            return EnumCanSwitch.csYes;
        }

        public CanSwitchResult CheckCanSwitch() {
            var results = _canSwitches.Select(p => p.CanSwitch()).ToArray();
            return new CanSwitchResult
            {
                AllowSwitch = Highest(results),
                Warning = results.SelectMany(r => r.Warning).ToArray(),
                Blocking = results.SelectMany(r => r.Blocking).ToArray()
            };
        }

        public IEnumerable<ConfigMenuOptions> ConfigMenuOptions()
        {
            return _canSwitches.Select(c => c.ConfigureAction());
        }

    }
}
