using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherCommon;
using SwitcherUi.config;
using IConfiguration = SwitcherUi.config.IConfiguration;

namespace SwitcherUi.switching
{
    abstract class AbstractSwitcher : ISwitch
    {
        protected readonly IConfiguration Config;
        protected readonly Settings Settings;

        public string Name { get; }

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

        protected AbstractSwitcher(IConfiguration config, string name) {
            Name = name;
            this.Config = config;
            Settings = config[this];
        }

        abstract public SwitchResult SwitchTo(Project project);

        virtual public SwitchResult MakeReadyForConfig()
        {
            return Result(true, "Not Required");
        }
    }
}
