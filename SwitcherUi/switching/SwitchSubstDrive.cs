using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherUi.config;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;

namespace SwitcherUi.switching
{
    internal class SubsDriveInfo
    {
        public bool DriveRequired { get; set; }
        public string Path { get; set; }
        public bool DefaultValue { get; set; }
    }

    class SwitchSubstDrive : AbstractSwitcher
    {
        internal const string SwitcherName = "Substitute Drive";
        public SwitchSubstDrive(IConfiguration config): base(config, SwitcherName)
        {
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool DefineDosDevice(
            int dwFlags,
            string lpDeviceName,
            string lpTargetPath
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetDriveType(
            string lpRootPathName
            );

        private const int DDD_RAW_TARGET_PATH = 0x00000001;
        private const int DDD_REMOVE_DEFINITION = 0x00000002;
        private const int DDD_EXACT_MATCH_ON_REMOVE = 0x00000004;

        private const int DRIVE_UNKNOWN = 0;
        private const int DRIVE_NO_ROOT_DIR = 1;
        private const int DRIVE_FIXED = 3;

        internal const string DriveLetterSettingName = "drive-letter";
        internal const string RootPathSettingName = "root-path";

        public string Drive => Settings.Value(DriveLetterSettingName, "z").ToUpper() + ":";
        public string RootPath => Settings.Value(RootPathSettingName, @"c:\temp");
        public bool Enabled => Settings.ContainsKey(DriveLetterSettingName);

        internal bool RemoveDrive() {
            if (!Directory.Exists(Drive + "\\")) return true;
            return DefineDosDevice(DDD_REMOVE_DEFINITION, Drive, null);
        }

        public const string NoSubDriveRequired = "- No Subst Drive Required -";
        public const string ProjectSubstDirSettingName = "Subst Drive Folder";

        private string StripNonAlpahNum(string name)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -_]");
            var result = rgx.Replace(name, "");
            return string.IsNullOrWhiteSpace(result) ? "z-project" : result;
        }

        public string DefaultProjectFolder(Project project)
        {
            return StripNonAlpahNum(project.Name);
        }

        public SubsDriveInfo Folder(Project project)
        {
            var hasValue = project.Settings.ContainsKey(ProjectSubstDirSettingName);
            var setting = hasValue ? project.Settings[ProjectSubstDirSettingName] : DefaultProjectFolder(project);
            return new SubsDriveInfo
            {
                Path = setting,
                DefaultValue = !hasValue,
                DriveRequired = !(string.IsNullOrWhiteSpace(setting) || setting == NoSubDriveRequired)
            };
        }

        public override SwitchResult SwitchTo(Project project)
        {
            if (!Enabled) return new SwitchResult { SourceName = Name, Success = true, Message = "Disabled" };
            var folder = Folder(project);
            if (!folder.DriveRequired) return new SwitchResult { SourceName = Name, Success = true, Message = "Drive Not Required" };
            if (!RemoveDrive()) {
               return Result(false, "Unable to remove drive " + Drive);
            }
            var path = Path.Combine(RootPath, folder.Path);
            var exists = Directory.Exists(path);
            try
            {
                if (!exists && !Directory.CreateDirectory(path).Exists)
                {
                    return Result(false, $"Unable to creating missing path '{path}'");
                }
            }
            catch (Exception e) {
                return Result(false, "Creating path '" + path + "' threw excetion " + e.ToString());
            }
            var defined = DefineDosDevice(0, Drive, path) ;
            return Result(defined, "{0} Drive '{1}' to Path '{2}' {3}", defined ? "Set" : "Unable to set", Drive, path, exists ? "" : "(created as path didn't exist)");
        }

        override public SwitchResult MakeReadyForConfig()
        {
            var removed = RemoveDrive();
            return Result(removed, string.Format(removed ? "Drive {0} removed/not susbt": "Unable to remove Drive {0}", Drive) );
        }
    }
}
