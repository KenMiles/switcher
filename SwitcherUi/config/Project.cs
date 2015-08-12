using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherCommon;

namespace SwitcherUi.config
{
    public class Project
    {
        public const string DisplayNameSettingName = "display-name";

        public string Name { get; set; }
        public string DisplayName { get { return Settings?.Value(DisplayNameSettingName, Name) ?? Name; } set { Settings.Values[DisplayNameSettingName] = value; } }
        public Settings Settings { get; set; }

        public static Project EmptyProject(string displayName = "")
        {
            var result = new Project {Name = "", Settings = new Settings(Settings.SettingsDictionary())};
            result.DisplayName = displayName;
            return result;
        }
    }
}
