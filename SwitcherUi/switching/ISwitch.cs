using SwitcherUi.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwitcherUi.switching
{
    public struct SwitchResult {
        public string SourceName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public delegate UserControl CreateProjectConfigControl();

    public class ProjectConfig
    {
        public string SectionCaption { get; set; }
        public CreateProjectConfigControl CreateControl { get; set; }
    }

    public interface ISwitch
    {
        string Name { get; }
        SwitchResult SwitchTo(Project project);
        SwitchResult MakeReadyForConfig();
        ConfigMenuOptions ConfigureAction();
        ProjectConfig ProjectConfig();

    }

}
