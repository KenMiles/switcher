using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherUi.config;
using System.Text.RegularExpressions;
using SwitcherCommon;
using SwitcherUi.switching.cfg;
using SwitcherUi.ServiceReference1;

namespace SwitcherUi.switching
{

    class JavaHome : AbstractSwitcher
    {
        internal const string JAVA_PATHS = "paths";
        private readonly JdkFinder _jdkFinder;
        public JavaHome(config.IConfiguration config) : base(config, JdkFinder.JAVA_HOME)
        {
            _jdkFinder = new JdkFinder(config[this, JAVA_PATHS].Values);
        }



        private JavaJdk[] _jdks;
        public JavaJdk[] Jdks { get { return _jdks = _jdks ?? _jdkFinder.FindJdks(); } }

        private Dictionary<string, JavaJdk> BuildLatestJdkList() {
            var jdks = Jdks.GroupBy(j => j.EnvironmentVariable).Select(g => g.OrderByDescending(j => j.MinorVersion).ThenBy(j => j.ReleaseVersion).Take(1));
            return jdks.SelectMany(j => j).ToDictionary(k => k.EnvironmentVariable, v => v, StringComparer.OrdinalIgnoreCase);
        }

        private Dictionary<string, JavaJdk> _latestJdk;
        public Dictionary<string, JavaJdk> LatestJdk { get { return _latestJdk = _latestJdk ?? BuildLatestJdkList();} }




        private void CleanUpJavaVersionEnvironmentVariables()
        {
            //_jdkFinder.CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget.User, true);
        }

        class DesiredJavaVersionSearchResult
        {
            public string JavaVersion { get; set; }
            public List<string> TriedVersions = new List<string>();
            public bool Found => !string.IsNullOrWhiteSpace((JavaVersion));
            public string Message() {
                var notFound = TriedVersions.Count == 0 ? "" : "Unable to find JDKS for " + string.Join(", ", TriedVersions);
                if (Found) return $"Switched JAVA_HOME to '{JavaVersion}' {notFound}".Trim();
                return notFound;
            }
        }

        private DesiredJavaVersionSearchResult FindBestMatch(string[] javaVersions)
        {
            var checkedList = new  List<string>();
            foreach (var javaVersion in javaVersions)
            {
                if (LatestJdk.ContainsKey((javaVersion)))
                {
                    return new DesiredJavaVersionSearchResult {JavaVersion = javaVersion, TriedVersions = checkedList};
                }
                checkedList.Add(javaVersion);
            }
            return new DesiredJavaVersionSearchResult { JavaVersion = null, TriedVersions = checkedList };
        }

        private void SetUserVariableToTriggerRefresh(string value)
        {
            // might as well make it userful for bug finding
            SetEnviromentVariableHelper.SetEnvironmentVariable(JdkFinder.JAVA_HOME + "_SWITCHER_LAST", value, EnvironmentVariableTarget.User);
        }

        public override SwitchResult SwitchTo(Project project)
        {
            CleanUpJavaVersionEnvironmentVariables();
            var javaVersions = project.Settings.ArrayValue("JAVA");
            var svc = new SwitchingServiceClient();
            if (javaVersions == null || javaVersions.Length ==0)
            {
                svc.SetJavaEnvVars();
                SetUserVariableToTriggerRefresh("Project Doesn't care");
                return new SwitchResult
                {
                    SourceName = Name,
                    Success = true,
                    Message = "No Java Version Defined for Project"
                };
            }
            var desiredFound = FindBestMatch(javaVersions);
            string result = "";
            if (desiredFound.Found)
            {
                result = svc.SetJavaHome(desiredFound.JavaVersion);
            }
            SetUserVariableToTriggerRefresh(desiredFound.JavaVersion);
            var versionSet = result == null || !result.StartsWith("ERROR");
            return new SwitchResult
                {
                    SourceName = Name,
                    Success = desiredFound.Found && versionSet,
                    Message = versionSet ? desiredFound.Message() : desiredFound.Message() + " - " + result
            };
        }

        public override ConfigMenuOptions ConfigureAction()
        {
            return new ConfigMenuOptions
            {
                MenuText = "Java Home",
                CreateConfigForm = c => new FrmJavaHomeCfg(c)
            };
        }

        public override ProjectConfig ProjectConfig()
        {
            return new ProjectConfig { CreateControl = () => new ProjectJavaSettings(), SectionCaption = "Java Home" };
        }
    }
}
