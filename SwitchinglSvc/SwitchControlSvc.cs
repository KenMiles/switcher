using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using SwitcherCommon;
using SwitcherLibrary;

namespace SwitchinglSvc
{
    public partial class SwitchControlSvc : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SwitchControlSvc));
        private static bool _logConfigured = false;
        private ServiceHost _serviceHost = null;
        private const string Log_file_name = @"E:\Dev\switcher\switcher\SwitchinglSvc\bin\Debug\log4net.cfg.xml";
        public SwitchControlSvc()
        {
            InitializeComponent();
            if (!_logConfigured && File.Exists(Log_file_name)) XmlConfigurator.Configure(new FileInfo(Log_file_name));
            _logConfigured = true;
        }

        protected override void OnStart(string[] args)
        {
            Log.Info("Starting Service");
            try
            {
                _serviceHost?.Close();
                // Create a ServiceHost for the CalculatorService type and 
                // provide the base address.
                _serviceHost = new ServiceHost(typeof (SwitchingService));
                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                _serviceHost.Open();
            }
            catch (Exception e)
            {
                Log.Error("Starting threw", e);
                throw;
            }
        }

        protected override void OnStop()
        {
            Log.Info("Stopping Service");
            if (_serviceHost == null) return;
            try
            {
                _serviceHost.Close();
            }
            catch (Exception e)
            {
                Log.Error("Stopping threw", e);
                //TODO Log exception;
            }
            _serviceHost = null;
        }
    }
}
