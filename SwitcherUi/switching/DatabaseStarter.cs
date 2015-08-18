using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitcherUi.config;
using SwitcherUi.switching.cfg;
using SwitcherUi.ServiceReference1;

namespace SwitcherUi.switching
{
    class DatabaseStarter: AbstractSwitcher
    {
        public DatabaseStarter(IConfiguration config) : base(config, "Database Starter")
        {
        }


        internal const string DatabasesSettingName = "Databases";

        public override SwitchResult SwitchTo(Project project)
        {
            var db = project.Settings.ContainsKey(DatabasesSettingName) ? project.Settings[DatabasesSettingName] : "";
            if (string.IsNullOrWhiteSpace(db)) return new SwitchResult { SourceName = Name, Success = true, Message = "No Databases Specified" };

            try
            {
                var svc = new SwitchingServiceClient();
                var results = db.Split(';').Select(s => svc.StartDatabase(s)).SelectMany(s => s).ToArray();
                var failed = results.Where(s => s.StartsWith("ERROR:")).ToArray();
                if (failed.Length == 0) return Result(true, $"Databases '{db}' Services Running");
                var issues = string.Join("; ", failed);
                return Result(false, $"Issues starting databases '{db}' - {issues}");
            }
            catch (Exception e)
            {
                return Result(false, $"Issues starting databases '{db}' - {e.Message}");
            }

        }

        public override ConfigMenuOptions ConfigureAction()
        {
            return new ConfigMenuOptions
            {
                MenuText = "Database Start Config",
                CreateConfigForm = c => new FrmDatabaseServices(c)
            };
        }

        public override ProjectConfig ProjectConfig()
        {
            return new ProjectConfig {CreateControl = () => new ProjectDatabaseStarter(), SectionCaption = "Database Starter"};
        }
    }
}
