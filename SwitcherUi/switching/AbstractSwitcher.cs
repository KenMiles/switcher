using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherUi.config;

namespace SwitcherUi.switching
{
    abstract class AbstractSwitcher : ISwitch
    {
        protected readonly Configuration config;
        protected readonly Settings settings;
        private readonly string _name;

        public string Name { get { return _name; } }

        protected SwitchResult Result(bool success, string message) {
            return new SwitchResult {
                SourceName = Name,
                Success = success,
                Message = message
            };
        }
        protected SwitchResult Result(bool success, string messageFmt, params object[] args)
        {
            return Result(success, string.Format(messageFmt, args));
        }

        protected AbstractSwitcher(Configuration config, string name) {
            _name = name;
            this.config = config;
            settings = config[this];
        }

        abstract public SwitchResult SwitchTo(Project project);
    }
}
