using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using log4net;

namespace SwitcherLibrary
{
    delegate string ServiceControlDelegate(ServiceController service);
    class ServiceControl
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServiceControl));
        private readonly IEnumerable<string> _serviceNames;
        private readonly int _timeOutSeconds;
        public ServiceControl(IEnumerable<string> serviceNames, int timeOutSeconds = 30)
        {
            _serviceNames = serviceNames.ToList();
            _timeOutSeconds = Math.Max(10, timeOutSeconds);
        }

        private string ControlService(string serviceName, ServiceControlDelegate control, ServiceControllerStatus desiredStatus)
        {
            var timeout = _timeOutSeconds*1000;
            try
            {
                using (var service = new ServiceController(serviceName))
                {
                    if (service.Status == desiredStatus)
                    {
                        return $"{serviceName} is already " + Enum.GetName(typeof(ServiceControllerStatus), desiredStatus);
                    }
                    var result = $"{serviceName} " + control(service);
                    service.WaitForStatus(desiredStatus, TimeSpan.FromMilliseconds(timeout));
                    return result;
                }
            }
            catch (Exception e)
            {
                var desc = Enum.GetName(typeof (ServiceControllerStatus), desiredStatus);
                Log.Error($"Changing {serviceName} to {desc}  (timeout = {timeout})", e);
                return $"ERROR: Changing {serviceName} to {desc} threw {e.Message}";
            }
        }

        private string[] ControlServices(string[] serviceNames, ServiceControlDelegate control, ServiceControllerStatus desiredStatus)
        {
            return serviceNames.Select(s => ControlService(s, control, desiredStatus)).ToArray();
        }

        public string[] Start()
        {
            return ControlServices(_serviceNames.ToArray(), service =>
            {
                service.Start();
                return "Started";
            }, ServiceControllerStatus.Running);
        }

        public string[] Stop()
        {
            return ControlServices(_serviceNames.Reverse().ToArray(), service =>
            {
                service.Stop();
                return "Stopped";
            }, ServiceControllerStatus.Stopped);
        }
    }
}
