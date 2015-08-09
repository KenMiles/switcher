using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherCommon
{
    public class JavaJdk
    {
        public string Path { get; set; }
        public int MajorVersionPt1 { get; set; }
        public int MajorVersionPt2 { get; set; }
        public int MinorVersion { get; set; }
        public int ReleaseVersion { get; set; }
        public string Suffix { get; set; }

        public string MajorVersion
        {
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
}
