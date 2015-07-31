using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherUi.config;
using System.Text.RegularExpressions;

namespace SwitcherUi.switching
{
    class JavaJdk
    {
        public string Path { get; set; }
        public int MajorVersionPt1 { get; set; }
        public int MajorVersionPt2 { get; set; }
        public int MinorVersion { get; set; }
        public int ReleaseVersion { get; set; }
        public string Suffix { get; set; }

        public string MajorVersion {
            get
            {
                string fmt = MajorVersionPt2 <= 4 ? "{0}.{1}" : "{1}";
                return string.Format(fmt, MajorVersionPt1, MajorVersionPt2);
            }
            }

        public string EnvironmentVariable
        {
            get
            {
                string suffix = string.IsNullOrWhiteSpace(Suffix) ? "" : "_" + Suffix.Trim();
                return $"JAVA_{MajorVersion}{suffix}";
            }
        }
    }

    class JavaHome : AbstractSwitcher
    {
        private readonly Settings _javaPaths;
        internal const string JAVA_HOME = "JAVA_HOME";
        internal const string JAVA_PATHS = "paths";
        public JavaHome(IConfiguration config) : base(config, JAVA_HOME)
        {
            _javaPaths = config[this, JAVA_PATHS];
        }

        private int Version(string[] versionParts, int index) {
            if (versionParts.Length > index) return int.Parse(versionParts[index]);
            return 0;
        }

        private JavaJdk JavaJdk(string path, string suffix) {
            if (!File.Exists(Path.Combine(path, @"bin\javac.exe"))) return null;
            var version = Path.GetFileName(path).Replace("jdk", "").Split('.', '_');
            return new JavaJdk {
                Path = path,
                MajorVersionPt1 = Version(version, 0),
                MajorVersionPt2 = Version(version, 1),
                MinorVersion = Version(version, 2),
                ReleaseVersion = Version(version, 3),
                Suffix = suffix
            };
        }

        private IEnumerable<JavaJdk> FindJdks(string path, string suffix) {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) return new JavaJdk[0];
            var jdkDirs = Directory.GetDirectories(path, "jdk*");
            return jdkDirs.Select(f => JavaJdk(f, suffix)).Where(j => j != null);
        }

        private string ProgramFilesPath(bool bit32) {
            var environmentVariable = bit32 ? "ProgramFiles(x86)" : "ProgramFiles";
            return Path.Combine(Environment.GetEnvironmentVariable(environmentVariable), "java");
        }

        private JavaJdk[] FindJdks() {
            var other = _javaPaths.Values.Select(s => FindJdks(s.Key, s.Value)).SelectMany(j => j);
            return FindJdks(ProgramFilesPath(false), "").Concat(FindJdks(ProgramFilesPath(true), "32BIT")).Concat(other).ToArray();
        }

        private JavaJdk[] _jdks;
        public JavaJdk[] Jdks { get { return _jdks = _jdks ?? FindJdks(); } }

        private Dictionary<string, JavaJdk> BuildLatestJdkList() {
            var jdks = Jdks.GroupBy(j => j.EnvironmentVariable).Select(g => g.OrderByDescending(j => j.MinorVersion).ThenBy(j => j.ReleaseVersion).Take(1));
            return jdks.SelectMany(j => j).ToDictionary(k => k.EnvironmentVariable, v => v, StringComparer.OrdinalIgnoreCase);
        }

        private Dictionary<string, JavaJdk> _latestJdk;
        public Dictionary<string, JavaJdk> LatestJdk { get { return _latestJdk = _latestJdk ?? BuildLatestJdkList();} }


        private void SetVersionEnvironmentVariable()
        {
            foreach (var k in LatestJdk) {
                EnvironmentVariables.SetEnvironmentVariable(k.Key, k.Value.Path, EnvironmentVariableTarget.Machine);
            }
        }

        private readonly Regex _isJavaVersion = new Regex(@"^JAVA_[0-9]+");
        private void CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget target, bool forceClear) {
            var lines = new List<string>();
            foreach (System.Collections.DictionaryEntry envVar in Environment.GetEnvironmentVariables(target))
            {
                var varName = envVar.Key.ToString();
                if (_isJavaVersion.IsMatch(varName) && (forceClear || JavaJdk(envVar.Value.ToString(), "") == null)) {
                    Environment.SetEnvironmentVariable(varName, null, target);
                }
            }
        }

        private void CleanUpJavaVersionEnvironmentVariables()
        {
            CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget.User, true);
            CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget.Machine, false);
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

        public override SwitchResult SwitchTo(Project project)
        {
            SetVersionEnvironmentVariable();
            CleanUpJavaVersionEnvironmentVariables();
            var javaVersions = project.Settings.ArrayValue("JAVA");
            if (javaVersions == null || javaVersions.Length ==0)
            {
                return new SwitchResult
                {
                    SourceName = Name,
                    Success = true,
                    Message = "No Java Version Defined for Project"
                };
            }
            var desiredFound = FindBestMatch(javaVersions);
            if (desiredFound.Found)
            {
                EnvironmentVariables.SetEnvironmentVariable(JAVA_HOME, LatestJdk[desiredFound.JavaVersion].Path, EnvironmentVariableTarget.Machine);
            }
            return new SwitchResult
                {
                    SourceName = Name,
                    Success = desiredFound.Found,
                    Message = desiredFound.Message()
                };
        }
    }
}
