using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherCommon
{
    public class SetEnviromentVariableHelper
    {
        public static void SetEnvironmentVariable(string name, string value, EnvironmentVariableTarget target)
        {
            if (Environment.GetEnvironmentVariable(name, target) == value) return;
            Environment.SetEnvironmentVariable(name, value, target);
        }
    }
}
