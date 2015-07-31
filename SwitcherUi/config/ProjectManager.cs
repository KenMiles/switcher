using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.config
{
    class ProjectManager
    {
        private IConfiguration _config;
        public ProjectManager(IConfiguration config) {
            _config = config;
        }

        public Project Project(string name) {
            return new Project {
                Name = name,
                Settings = _config[name]
            };
        }

        public void RefreshProject(Project project)
        {
            if (project == null) return;
            project.Settings = _config[project.Name];
        }

        public Project[] Projects()
        {
            return _config.ProjectNames.Select(Project).OrderBy(p => p.Name.ToUpper()).ToArray();
        }
    }
}
