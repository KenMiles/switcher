using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace SwitcherCommon
{
    public class WindowsServiceDescription
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public static WindowsServiceDescription[] Load(IConfiguration config)
        {
            var exclude = config.IgnoreServices();
            var services = ServiceController.GetServices();
            return services.Where(s => !exclude.ContainsKey(s.ServiceName)).Select(serviceController => new WindowsServiceDescription
            {
                DisplayName = serviceController.DisplayName,
                Name = serviceController.ServiceName
            }).ToArray();
        }
    }
}
