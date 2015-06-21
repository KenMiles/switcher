using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.config
{
    class ProjectManager
    {
        private Configuration _config;
        public ProjectManager(Configuration config) {
            _config = config;
        }

        public Project Project(string name) {
            return new Project {
                Name = name,
                Settings = _config[name]
            };
        }

        public Project[] Projects()
        {
            return _config.ProjectNames.Select(Project).OrderBy(p => p.Name.ToUpper()).ToArray();
        }
    }
}
