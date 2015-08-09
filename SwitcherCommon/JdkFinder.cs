using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using log4net;
using SwitcherCommon;

namespace SwitcherCommon
{
    public class JdkFinder
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(JdkFinder));
        public const string JAVA_HOME = "JAVA_HOME";
        private readonly Dictionary<string, string> _javaPaths;

        public JdkFinder(Dictionary<string, string> javaPaths)
        {
            _javaPaths = javaPaths;
            Log.Debug("Java Paths: " +  (javaPaths == null ? "null" : string.Join("; ", _javaPaths.Select(g => $"{g.Key} = {g.Value}"))));
        }

        private int Version(string[] versionParts, int index)
        {
            if (versionParts.Length > index) return int.Parse(versionParts[index]);
            return 0;
        }

        private JavaJdk JavaJdk(string path, string suffix)
        {
            Log.Debug($"Checking '{path}' for suffix '{suffix}'");
            if (!File.Exists(Path.Combine(path, @"bin\javac.exe"))) return null;
            var version = (Path.GetFileName(path)??"").Replace("jdk", "").Split('.', '_');
            return new JavaJdk
            {
                Path = path,
                MajorVersionPt1 = Version(version, 0),
                MajorVersionPt2 = Version(version, 1),
                MinorVersion = Version(version, 2),
                ReleaseVersion = Version(version, 3),
                Suffix = suffix
            };
        }

        private IEnumerable<JavaJdk> FindJdks(string path, string suffix)
        {
            Log.Debug($"Checking '{path}' for suffix '{suffix}'");
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) return new JavaJdk[0];
            var jdkDirs = Directory.GetDirectories(path, "jdk*");
            return jdkDirs.Select(f => JavaJdk(f, suffix)).Where(j => j != null);
        }

        private string ProgramFilesPath(bool bit32)
        {
            var environmentVariable = bit32 ? "ProgramFiles(x86)" : "ProgramFiles";
            var mostLikelyPath = bit32 ? @"C:\Program Files (x86)" : @"C:\Program Files";
            var path = Environment.GetEnvironmentVariable(environmentVariable) ?? mostLikelyPath;
            // If running in 32 bit mode on 64 bit OS (such as when prefer 32 bit checked) then we only see 32 program files directory in environment variable 
            if (!bit32 && !Environment.Is64BitProcess && path.EndsWith("(x86)"))
            {
                path = path.Replace("(x86)", "").Trim();
                Log.Info("******* I think I am running in 32 bit mode instead of 64 bit! *******");
            }
            return Path.Combine(path, "java");
            //return Path.Combine(mostLikelyPath, "java");
        }

        public JavaJdk[] FindJdks()
        {
            Log.Debug("Find JDKS Called");
            var other = _javaPaths.Select(s => FindJdks(s.Key, s.Value)).SelectMany(j => j);
            return FindJdks(ProgramFilesPath(false), "").Concat(FindJdks(ProgramFilesPath(true), "32BIT")).Concat(other).ToArray();
        }

        private readonly Regex _isJavaVersion = new Regex(@"^JAVA_[0-9]+");
        public void CleanUpJavaVersionEnvironmentVariables(EnvironmentVariableTarget target, bool forceClear)
        {
            foreach (System.Collections.DictionaryEntry envVar in Environment.GetEnvironmentVariables(target))
            {
                var varName = envVar.Key.ToString();
                if (_isJavaVersion.IsMatch(varName) && (forceClear || JavaJdk(envVar.Value.ToString(), "") == null))
                {
                    Environment.SetEnvironmentVariable(varName, null, target);
                }
            }
        }
    }
}
