using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SwitcherCommon;

namespace SwitcherLibrary
{
    internal delegate string[] ControlDbServices(ServiceControl serviceControl);
    class DatabaseController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DatabaseController));
        private readonly DatabaseControllerConfig _config;

        private DatabaseController(DatabaseControllerConfig config)
        {
            _config = config;
        }

        public DatabaseController(IConfiguration config): this(new DatabaseControllerConfig(config))
        {
        }

        private string[] ControlDb(string database, ControlDbServices controlDbServices)
        {
            var services = _config.DatabaseServices(database);
            if (services.Length == 0) return new[] { $"ERROR: Unknown datatbase '{database}'" };
            var serviceControl = new ServiceControl(services, _config.Timeout);
            return controlDbServices(serviceControl);
        }

        public string[] StartDatabase(string database)
        {
            return ControlDb(database, control => control.Start());
        }

        public string[] StopDatabase(string database)
        {
            return ControlDb(database, control => control.Stop());
        }
    }
}
