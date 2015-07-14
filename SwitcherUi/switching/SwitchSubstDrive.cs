using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitcherUi.config;
using System.Runtime.InteropServices;
using System.IO;

namespace SwitcherUi.switching
{
    class SwitchSubstDrive : AbstractSwitcher
    {
        internal const string SwitcherName = "Substitute Drive";
        public SwitchSubstDrive(Configuration config): base(config, SwitcherName)
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

        private string Drive { get { return settings.Value(DriveLetterSettingName, "z").ToUpper() + ":";  } }
        private string RootPath { get { return settings.Value(RootPathSettingName, "c:\temp"); } }
        private bool Enabled { get { return settings.ContainsKey(DriveLetterSettingName); } }

        internal bool RemoveDrive() {
            if (!Directory.Exists(Drive + "\\")) return true;
            return DefineDosDevice(DDD_REMOVE_DEFINITION, Drive, null);
        }

        public override SwitchResult SwitchTo(Project project)
        {
            if (!Enabled) return new SwitchResult { SourceName = Name, Success = true, Message = "Disabled" };
            if (RemoveDrive()) {
               return Result(false, "Unable to remove drive " + Drive);
            }
            var path = Path.Combine(RootPath, project.Settings.Value("folder", project.Name));
            var exists = Directory.Exists(path);
            var created = false;
            try
            {
                created = !exists && (Directory.CreateDirectory(path) != null);
            }
            catch (Exception e) {
                return Result(false, "Creating path '" + path + "' threw excetion " + e.ToString());
            }
            var defined = DefineDosDevice(0, Drive, path) ;
            return Result(defined, "{0} Drive '{1}' to Path '{2}' ", defined ? "Set" : "Unable to set", Drive, path);
        }

        override public SwitchResult MakeReadyForConfig()
        {
            var removed = RemoveDrive();
            return Result(removed, string.Format(removed ? "Drive {0} removed/not susbt": "Unable to remove Drive {0}", Drive) );
        }
    }
}
