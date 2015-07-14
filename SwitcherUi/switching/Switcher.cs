using SwitcherUi.config;
using SwitcherUi.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.switching
{
    public enum Switched { Yes, Partial, No };
    class SwitchedResult {
        private readonly SwitchResult[] _results;
        private readonly Project _project;
        public SwitchedResult(Project project, SwitchResult[] results) {
            _results = results;
            _project = project;
        }

        public Switched Switched { get {
                if (!_results.Any(r => !r.Success)) return Switched.Yes;
                if (!_results.Any(r => r.Success)) return Switched.No;
                return Switched.Partial;
            }
        }

        private IEnumerable<string> Messages(string description, string[] messages)
        {
            if (messages == null || messages.Length == 0) return new string[0];
            return new[] { "", description }.Concat(messages);
        }

        private string ProjectName { get { return _project == null ? "No Project" : _project.Name; } }
        private string SuccessStatus
        {
            get
            {
                switch (Switched)
                {
                    case Switched.Yes: return "Switched";
                    case Switched.Partial: return "Partially Switched";
                    default: return "Unable to Switch";
                };
            }
        }

        public string MessageTitle() {
            return SuccessStatus + "  to project " + ProjectName;
        }
        public string[] DisplayMessages {
            get {
                return new[] { MessageTitle() }
                .Concat(Messages("Issues Switching", IssueMessages))
                .Concat(Messages("Switched OK", SuccessMessages))
                .Concat(new[] { "" })
                .ToArray();
            }
        }

        public string[] IssueMessages
        {
            get
            {
                return _results.Where(r => !r.Success).Select(r => string.Format("{0} - {1}", r.SourceName, r.Message)).ToArray();
            }
        }

        public string[] SuccessMessages
        {
            get
            {
                return _results.Where(r => r.Success).Select(r => string.Format("{0} - {1}", r.SourceName, r.Message)).ToArray();
            }
        }

    }
    class Switcher
    {
        private readonly ISwitch[] _switchers;
        internal Switcher(ISwitch[] switcher)
        {
            _switchers = switcher;
        }

        public Switcher(Configuration config): this(LoadImplementers.Load<ISwitch>(config)) {
        }

        public SwitchedResult SwitchTo(Project project) {
            return new SwitchedResult(project, _switchers.Select(s => s.SwitchTo(project)).ToArray());
        }

        public SwitchedResult MakeReadyForConfig()
        {
            return new SwitchedResult(null, _switchers.Select(s => s.MakeReadyForConfig()).ToArray());
        }

    }
}
