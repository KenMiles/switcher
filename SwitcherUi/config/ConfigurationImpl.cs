using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherCommon;
using SwitcherUi.switching;

namespace SwitcherUi.config
{
    delegate Dictionary<string, string> DefaultDictionay();
    delegate void Update(IniData ini);
    class ConfigurationImpl : SwitcherCommon.ConfigurationImpl, IConfiguration
    {

        public ConfigurationImpl(CommandArguments args):base(args)
        {
        }

        public ConfigurationImpl() : this(new CommandArguments(Environment.GetCommandLineArgs()))
        {
        }

        public Settings this[Project project]
        {
            get
            {
                return this[project.Name];
            }

            set
            {
                this[project.Name] = value;
            }
        }

        public Settings this[Project project, string section]
        {
            get
            {
                return this[project.Name, section];
            }

            set
            {
                this[project.Name, section] = value;
            }
        }


        public Settings this[ISwitch switcher]
        {
            get
            {
                return GetSwitcherCfg(switcher.Name);
            }

            set
            {
                SetSwitcherCfg(switcher.Name, value);
            }
        }

        public Settings this[ISwitch switcher, string section]
        {
            get
            {
                return GetSwitcherCfg(switcher.Name, section);
            }

            set
            {
                SetSwitcherCfg(switcher.Name, section, value);
            }
        }

    }
}
