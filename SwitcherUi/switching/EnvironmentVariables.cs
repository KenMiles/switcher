using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherUi.config;
using SwitcherUi.switching.cfg;

namespace SwitcherUi.switching
{
    class EnvironmentVariables : AbstractSwitcher
    {
        public EnvironmentVariables(IConfiguration config) : base(config, "Environment Variables") {
        }

        public static void SetEnvironmentVariable(string name, string value, EnvironmentVariableTarget target)
        {
            if (Environment.GetEnvironmentVariable(name, target) == value) return;
            Environment.SetEnvironmentVariable(name, value, target);
        }

        private const string NoEnvironmentVariables = "No Environment Variables defined for project";
        private const string SetEnvironmentVariables = "Set Environment Variables: ";
        public const string ProjectConfigSectionName = "Environment Variables";
        public override SwitchResult SwitchTo(Project project)
        {
            var settings = Config[project, ProjectConfigSectionName];
            foreach (var value in settings.Values) {
                SetEnvironmentVariable(value.Key, value.Value, EnvironmentVariableTarget.Machine);
            }
            var names = settings.Values.Select(v => v.Key).ToArray();
            return Result(true, names.Length == 0 ?  NoEnvironmentVariables : SetEnvironmentVariables + string.Join(",", names));
        }

        public override ConfigMenuOptions ConfigureAction()
        {
            return new ConfigMenuOptions
            {
                MenuText = "Environment Variables Config (TBI)",
                CreateConfigForm = null //TODO Config Frm If Required (should filter out creating menus) - c => new FrmJavaHomeCfg(c)
            };
        }

        public override ProjectConfig ProjectConfig()
        {
            return new ProjectConfig {CreateControl  = ()=> new ProjectEnvVariablesSettings(), SectionCaption = "Environment Variables" };
        }
    }
}
