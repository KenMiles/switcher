using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherUi.config;

namespace SwitcherUi.switching.cfg
{
    public interface IProjectItem
    {
        void DisplayCurrent(Settings projectSettings, IConfiguration config, Project project);
        void SaveConfig(Settings projectSettings, IConfiguration config, Project project);
    }
}
