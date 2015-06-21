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

        public string EnvironmentVariable
        {
            get
            {
                string suffix = string.IsNullOrWhiteSpace(Suffix) ? "" : "_" + Suffix.Trim();
                string fmt = MajorVersionPt2 <= 4 ? "JAVA_{0}.{1}{2}" : "JAVA_{1}{2}";
                return string.Format(fmt, MajorVersionPt1, MajorVersionPt2, suffix);
            }
        }
    }

    class JavaHome : AbstractSwitcher
    {
        private readonly Settings _javaPaths;
        public JavaHome(Configuration config) : base(config, "JAVA_HOME")
        {
            _javaPaths = config[this, "paths"];
        }

        private int Version(string[] versionParts, int index) {
            if (versionParts.Length > index) return Int32.Parse(versionParts[index]);
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

        private IEnumerable<JavaJdk> Jdks(string path, string suffix) {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) return new JavaJdk[0];
            var jdkDirs = Directory.GetDirectories(path, "jdk*");
            return jdkDirs.Select(f => JavaJdk(f, suffix)).Where(j => j != null);
        }

        private string ProgramFilesPath(bool bit32) {
            var environmentVariable = bit32 ? "ProgramFiles(x86)" : "ProgramFiles";
            return Path.Combine(Environment.GetEnvironmentVariable(environmentVariable), "java");
        }

        private JavaJdk[] Jdks() {
            var other = _javaPaths.Values.Select(s => Jdks(s.Key, s.Value)).SelectMany(j => j);
            return Jdks(ProgramFilesPath(false), "").Concat(Jdks(ProgramFilesPath(true), "32BIT")).Concat(other).ToArray();
        }

        private Dictionary<string, JavaJdk> FindLatest() {
            var jdks = Jdks().GroupBy(j => j.EnvironmentVariable).Select(g => g.OrderByDescending(j => j.MinorVersion).ThenBy(j => j.ReleaseVersion).Take(1));
            return jdks.SelectMany(j => j).ToDictionary(k => k.EnvironmentVariable, v => v, StringComparer.OrdinalIgnoreCase);
        }


        private void SetVersionEnvironmentVariable(Dictionary<string, JavaJdk> latest)
        {
            foreach (var k in latest) {
                EnvironmentVariables.SetEnvironmentVariable(k.Key, k.Value.Path, EnvironmentVariableTarget.Machine);
            }
        }

        private readonly Regex IsJavaVersion = new Regex(@"^JAVA_[0-9]+");
        private void CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget target, bool forceClear) {
            var lines = new List<string>();
            foreach (System.Collections.DictionaryEntry envVar in Environment.GetEnvironmentVariables(target))
            {
                var varName = envVar.Key.ToString();
                if (IsJavaVersion.IsMatch(varName) && (forceClear || JavaJdk(envVar.Value.ToString(), "") == null)) {
                    Environment.SetEnvironmentVariable(varName, null, target);
                }
            }
        }

        private void CleanUpJavaVersionEnvironmentVariables()
        {
            CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget.User, true);
            CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget.Machine, false);
        }

        public override SwitchResult SwitchTo(Project project)
        {
            var jdks = FindLatest();
            SetVersionEnvironmentVariable(jdks);
            CleanUpJavaVersionEnvironmentVariables();
            var javaVersion = project.Settings["java"];
            if (string.IsNullOrWhiteSpace(javaVersion))
            {
                return new SwitchResult
                {
                    SourceName = Name,
                    Success = true,
                    Message = "No Java Version Defined for Project"
                };
            }
            if (!jdks.ContainsKey(javaVersion))
            {
                return new SwitchResult
                {
                    SourceName = Name,
                    Success = false,
                    Message = String.Format("Unable to switch to '{0}' as can only find {1}", javaVersion, string.Join(",", jdks.Keys))
                };
            }
            EnvironmentVariables.SetEnvironmentVariable("JAVA_HOME", jdks[javaVersion].Path, EnvironmentVariableTarget.Machine);
            return new SwitchResult {
                SourceName = Name,
                Success = true,
                Message = String.Format("Switched JAVA_HOME to '{0}'", javaVersion)
            };
        }
    }
}
