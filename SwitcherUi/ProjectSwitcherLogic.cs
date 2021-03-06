﻿using SwitcherUi.allowSwitching;
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
        bool ConfigureProject(IEnumerable<ProjectConfig> projectConfig, Project project, Project sourceProject);
        void StartAutoSwitchAndClose();
        void AddConfigMenuItems(IEnumerable<ConfigMenuOptions> configMenuOptions);
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
            return projects.FirstOrDefault(p => projectName == p.Name);
        }

        private Project _current;

        public void OnSetup() {
            _userFeedback.AddConfigMenuItems(_canSwitch.ConfigMenuOptions().Concat(_switcher.ConfigMenuOptions()));
            var projects = AllProjects().ToArray();
            _current = Selected(projects, _config.CurrentProject);
            _userFeedback.DisplayCurrentProject(_current?.DisplayName ?? "No Project Selected");
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

        private bool CheckProjectSelected(Project project, string messageTitle)
        {
            if (project != null) return true;
            _userFeedback.ErrorMessage(messageTitle, "No Project Selected");
            return false;
        }

        private bool DoConfigureProject(Project project, Project soureProject, string messageTitle)
        {
            if (!CheckProjectSelected(project, messageTitle)) return false;
            var isCurrent = project.Name == _config.CurrentProject;
            if (isCurrent && !CheckCanSwitch(PerformingAction.Config, "Config"))
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(project.Name)) _projectManager.RefreshProject(project);
            if (soureProject != null && project != soureProject) _projectManager.RefreshProject(soureProject);
            if (!_userFeedback.ConfigureProject(_switcher.ProjectConfigOptions(), project, soureProject)) return true;
            _projectManager.RefreshProject(project);
            var projs = AllProjects();
            var selected = projs.FirstOrDefault(p => p.Name == project.Name) ?? projs.FirstOrDefault(p => p.Name == soureProject?.Name);
            _userFeedback.ListProjects(projs, selected);
            if (!isCurrent
                || !_userFeedback.WarningAsk("Update Enviroment", "Current Project configuration has been changed, update enviroment?")
                || !CheckCanSwitch(PerformingAction.Switch, project.Name))
                return true;
            DoSwitch(_projectManager.Project(_config.CurrentProject));
            return true;
        }

        public bool ConfigureProject(Project project)
        {
            return DoConfigureProject(project, project, "Configure Project");
        }

        public bool CopyProject(Project sourceProject)
        {
            if (!CheckProjectSelected(sourceProject, "Copy Project")) return false;
            var project = Project.EmptyProject(sourceProject.DisplayName + " Copy of");
            return DoConfigureProject(project, sourceProject, "Copy Project");
        }

        public bool NewProject()
        {
            var project = Project.EmptyProject("New Project");
            return DoConfigureProject(project, project, "New Project");
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

        public void DeleteProject(Project project)
        {
            if (!CheckProjectSelected(project, "Delete Project")) return;
            bool isCurrent = project.Name == _config.CurrentProject;
            if (isCurrent && _userFeedback.WarningAsk("Deleted Current Project", "You have selected to Delete Current Project")) return;
            project.Deleted = true;
            _config[project] = project.Settings;
            if (isCurrent)
            {
                _userFeedback.DisplayCurrentProject("Nothing selected");
                _current = null;
            }
            _userFeedback.ListProjects(AllProjects(), _current);
        }
    }
}
