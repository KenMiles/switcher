using SwitcherUi.allowSwitching;
using SwitcherUi.config;
using SwitcherUi.switching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwitcherCommon;
using IConfiguration = SwitcherUi.config.IConfiguration;
using SwitcherUi.ServiceReference1;

namespace SwitcherUi
{
    enum PerformingAction { Switch, Config}
    public delegate Form CreateConfigForm();
    interface IUserFeedback {
        bool WarningAsk(string subject, string message);
        void ErrorMessage(string subject, string message);
        void DisplaySwitchLog(IEnumerable<string> logEntry);
        void DisplayCurrentProject(string project);
        void ListProjects(Project[] projects, Project select);
        void ShowCanSwitchStatus(EnumCanSwitch status, string[] messages);
        bool ConfigureProject(Project project);
        void StartAutoSwitchAndClose();
    }


    class ProjectSwitcherLogic
    {
        private readonly IConfiguration _config;
        private readonly ProjectManager _projectManager;
        private readonly CanSwitch _canSwitch;
        private readonly Switcher _switcher;
        private readonly IUserFeedback _userFeedback;
        private readonly CommandArguments _args;

        internal ProjectSwitcherLogic(IConfiguration config, ProjectManager projectManager, Switcher switcher, CanSwitch canSwitch, IUserFeedback userFeedback, CommandArguments args)
        {
            _config = config;
            _projectManager = projectManager;
            _canSwitch = canSwitch;
            _switcher = switcher;
            _userFeedback = userFeedback;
            _args = args;
        }
        public ProjectSwitcherLogic(IConfiguration config, IUserFeedback userFeedback, CommandArguments args) : 
            this(config, new ProjectManager(config), new Switcher(config), new CanSwitch(config), userFeedback, args)
        {
        }

        public Project[] AllProjects() {
            return _projectManager.Projects();
        }

        private IEnumerable<string> Messages(string description, string[] messages)
        {
            if (messages == null || messages.Length == 0) return new string[0];
            return new[] { "", description }.Concat(messages);
        }

        private string CanSwitchStatusMessage(EnumCanSwitch canSwitch) {
            switch (canSwitch) {
                case EnumCanSwitch.csYes: return "Can Switch";
                case EnumCanSwitch.csAfterWarning: return "Please Review issues before Switching";
                case EnumCanSwitch.csNo:
                default: return "Issues Blocking Switching";
            }
        }

        public void CheckCanSwitch() {
            var status = _canSwitch.CheckCanSwitch();
            var messages = new[] { CanSwitchStatusMessage(status.AllowSwitch) }
              .Concat(Messages("Blocking Issues:", status.Blocking))
              .Concat(Messages("Warning Issues:", status.Warning))
              .ToArray();
            _userFeedback.ShowCanSwitchStatus(status.AllowSwitch, messages);
        }

        private void FailSwitch(PerformingAction action, string projectName, string reason, params string[] extraInfo) {
            var descFmt = action == PerformingAction.Switch ? "Switching Blocked to '{0}' because {1}" : "Configuration Blocked because {1}";
            var description = string.Format(descFmt, projectName, reason);
            var log = new List<string>() { description };
            log.AddRange(extraInfo);
            _userFeedback.DisplaySwitchLog(log);
        }

        private bool CheckCanSwitch(PerformingAction action, string projectName) {
            var canSwitch = _canSwitch.CheckCanSwitch();
            switch (canSwitch.AllowSwitch)
            {
                case EnumCanSwitch.csNo:
                    _userFeedback.ErrorMessage("Blocking Processes Running", string.Join("\r\n", canSwitch.Blocking));
                    FailSwitch(action, projectName, "Blocking Processes Running", canSwitch.Blocking);
                    return false;
                case EnumCanSwitch.csAfterWarning:
                    var title = action == PerformingAction.Switch ? "Switch With Applications Running" : "Configuration With Applications Running";
                    if (!_userFeedback.WarningAsk(title, string.Join("\r\n", canSwitch.Warning)))
                    {
                        FailSwitch(action, projectName, "User Checked Warning Applications", canSwitch.Warning);
                        return false;
                    }
                    break;
            }
            return true;
        }

        private bool DoSwitch(Project project)
        {
            _projectManager.RefreshProject(project);
            var result = _switcher.SwitchTo(project);
            if (result.Switched != Switched.Yes)
            {
                _userFeedback.ErrorMessage(result.MessageTitle(), string.Join("\r\n", result.IssueMessages));
            }
            _userFeedback.DisplaySwitchLog(result.DisplayMessages);
            if (result.Switched != Switched.No)
            {
                _config.CurrentProject = project.Name;
                _userFeedback.DisplayCurrentProject(project.Name);
            }
            return result.Switched != Switched.No;
        }

        public bool SwitchTo(Project project) {
            if (project == null)
            {
                FailSwitch(PerformingAction.Switch, "Unknown Project", "no project provided");
                return false;
            }
            if (!CheckCanSwitch(PerformingAction.Switch, project.Name))
            {
                return false;
            }
            return DoSwitch(project);
        }

        private Project Selected(Project[] projects, string projectName) {
            if (projects == null || projects.Length == 0 || string.IsNullOrWhiteSpace(projectName)) return null;
            return projects.First(p => projectName == p.Name);
        }

        private Project _current;

        public void ReadCurrentProject() {
            var current = _config.CurrentProject;
            _userFeedback.DisplayCurrentProject(current);
            var projects = AllProjects();
            _current = Selected(projects, current);
            _userFeedback.ListProjects(projects, _current);
            if (_current != null && _args.ArgumentExists("switch")) _userFeedback.StartAutoSwitchAndClose();
        }

        public bool SwitchToCurrent()
        {
            return SwitchTo(_current);
        }

        public void DoConfig(CreateConfigForm createConfigForm) {
            if (!CheckCanSwitch(PerformingAction.Config, "Config"))
            {
                return;
            }
            var result = _switcher.MakeReadyForConfig();
            var frm = createConfigForm();
            frm.ShowDialog();
            DoSwitch(_projectManager.Project(_config.CurrentProject));
        }

        public void ConfigureProject(Project project)
        {
            if (project == null)
            {
                _userFeedback.ErrorMessage("Configure Project", "No Project Selelected");
                return;
            }
            if (!CheckCanSwitch(PerformingAction.Config, "Config"))
            {
                return;
            }
            _projectManager.RefreshProject(project);
            if (!_userFeedback.ConfigureProject(project)
                || project.Name != _config.CurrentProject
                || !_userFeedback.WarningAsk("Update Enviroment", "Current Project configuration has been changed, update enviroment?")
                || !CheckCanSwitch(PerformingAction.Switch, project.Name)) return;
            DoSwitch(_projectManager.Project(_config.CurrentProject));
        }

        public void ShutLocalDatabases()
        {
            var cfg = new DatabaseControllerConfig(_config);
            var databases = cfg.Databases;
            if (databases.Length == 0)
            {
                _userFeedback.ErrorMessage("Closing Databases", "No Databases' configured");
                return;
            }
            var svc = new SwitchingServiceClient();
            var closed = databases.Select(s => svc.StopDatabase(s)).SelectMany(s => s).ToArray();
            _userFeedback.DisplaySwitchLog(closed);

        }

    }
}
