using SwitcherUi.allowSwitching;
using SwitcherUi.config;
using SwitcherUi.switching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi
{
    interface IUserFeedback {
        bool WarningAsk(string subject, string message);
        void ErrorMessage(string subject, string message);
        void DisplaySwitchLog(IEnumerable<string> logEntry);
        void DisplayCurrentProject(string project);
        void ListProjects(Project[] projects, Project select);
        void ShowCanSwitchStatus(EnumCanSwitch status, string[] messages);
    }


    class ProjectSwitcherLogic
    {
        private readonly Configuration _config;
        private readonly ProjectManager _projectManager;
        private readonly CanSwitch _canSwitch;
        private readonly Switcher _switcher;
        private readonly IUserFeedback _userFeedback;

        internal ProjectSwitcherLogic(Configuration config, ProjectManager projectManager, Switcher switcher, CanSwitch canSwitch, IUserFeedback userFeedback)
        {
            _config = config;
            _projectManager = projectManager;
            _canSwitch = canSwitch;
            _switcher = switcher;
            _userFeedback = userFeedback;
        }
        public ProjectSwitcherLogic(Configuration config, IUserFeedback userFeedback) : 
            this(config, new ProjectManager(config), new Switcher(config), new CanSwitch(config), userFeedback)
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
                case EnumCanSwitch.csAfterWarning: return "Please Review issues before switching";
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

        private void FailSwitch(string projectName, string reason, params string[] extraInfo) {
            var description = string.Format("Switching Blocked to '{0}' because {1}", projectName, reason);
            var log = new List<string>() { description };
            log.AddRange(extraInfo);
            _userFeedback.DisplaySwitchLog(log);
        }

        public bool SwitchTo(Project project) {
            if (project == null)
            {
                FailSwitch("Unknown Project", "no project provided");
                return false;
            }
            var canSwitch = _canSwitch.CheckCanSwitch();
            switch (canSwitch.AllowSwitch) {
                case EnumCanSwitch.csNo:
                    _userFeedback.ErrorMessage("Blocking Processes Running", string.Join("\r\n", canSwitch.Blocking));
                    FailSwitch(project.Name, "Check Switch", canSwitch.Blocking);
                    return false;
                case EnumCanSwitch.csAfterWarning:
                    if (!_userFeedback.WarningAsk("Switch With Applications Running", string.Join("\r\n", canSwitch.Warning))){
                        FailSwitch(project.Name, "User Checked Warning Applications", canSwitch.Warning);
                        return false;
                    }
                    break;
            }
            var result = _switcher.SwitchTo(project);
            if (result.Switched != Switched.Yes) {
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

        private Project Selected(Project[] projects, string projectName) {
            if (projects == null || projects.Length == 0 || string.IsNullOrWhiteSpace(projectName)) return null;
            return projects.Where(p => projectName == p.Name).First();
        }

        public void ReadCurrentProject() {
            var current = _config.CurrentProject;
            _userFeedback.DisplayCurrentProject(current);
            var projects = AllProjects();
            var selected = Selected(projects, current);
            _userFeedback.ListProjects(projects, selected);
        }

    }
}
