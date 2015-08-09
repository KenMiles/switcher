using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SwitcherCommon;

namespace SwitcherLibrary
{
    class JavaHomeSetter
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(JavaHomeSetter));
        private readonly JdkFinder _jdkFinder;

        public JavaHomeSetter(IConfiguration config)
        {
            _jdkFinder = new JdkFinder(config?.GetSwitcherCfg(JdkFinder.JAVA_HOME, "paths")?.Values ?? new Dictionary<string, string>());
        }

        private JavaJdk[] _jdks;
        public JavaJdk[] Jdks { get { return _jdks = _jdks ?? _jdkFinder.FindJdks(); } }

        private Dictionary<string, JavaJdk> BuildLatestJdkList()
        {
            var jdks = Jdks.GroupBy(j => j.EnvironmentVariable).Select(g => g.OrderByDescending(j => j.MinorVersion).ThenBy(j => j.ReleaseVersion).Take(1));
            return jdks.SelectMany(j => j).ToDictionary(k => k.EnvironmentVariable, v => v, StringComparer.OrdinalIgnoreCase);
        }

        private Dictionary<string, JavaJdk> _latestJdk;
        public Dictionary<string, JavaJdk> LatestJdk { get { return _latestJdk = _latestJdk ?? BuildLatestJdkList(); } }


        private void SetVersionEnvironmentVariable()
        {
            foreach (var k in LatestJdk)
            {
                SetEnviromentVariableHelper.SetEnvironmentVariable(k.Key, k.Value.Path, EnvironmentVariableTarget.Machine);
            }
        }


        private void CleanUpJavaVersionEnvironmentVariables()
        {
            _jdkFinder.CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget.Machine, false);
        }

        public void SetupJavaEnvironmentalVariables()
        {
            SetVersionEnvironmentVariable();
            CleanUpJavaVersionEnvironmentVariables();
        }

        public string SetJavaHome(string javaVersion)
        {
            Log.Info("Switching to javaVersion");
            SetupJavaEnvironmentalVariables();
            Log.Info("All Available: " + string.Join(", ", Jdks.Select(j => j.EnvironmentVariable)));
            Log.Info("Latest Avaialable: " + string.Join(", ", LatestJdk.Keys));
            if (!LatestJdk.ContainsKey(javaVersion)) return $"ERROR: Service Doesn't know of Java Version: '{javaVersion}'";
            SetEnviromentVariableHelper.SetEnvironmentVariable(JdkFinder.JAVA_HOME, LatestJdk[javaVersion].Path, EnvironmentVariableTarget.Machine);
            return $"Java HOME set to: '{javaVersion}' ({LatestJdk[javaVersion].Path})";
        }

    }
}
