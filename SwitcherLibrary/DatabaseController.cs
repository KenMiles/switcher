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
        private readonly Dictionary<string, string[]> _databases;
        private readonly int _timeout;

        private static int IntToStr(string intStr, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(intStr)) return defaultValue;
            try
            {
                return int.Parse(intStr.Trim());
            }
            catch (Exception e)
            {
                Log.Error($"Converting '{intStr}' to int", e);
                return defaultValue;
            }
        }
        private const string TimeoutValueName = "Timeout";
        private const string SectionName = "Databases";

        public DatabaseController(IConfiguration config)
        {
            var cfg = config.GetSwitcherCfg(SectionName).Values;
            Log.Debug("CFG: " + string.Join(", ", cfg.Keys));
            _databases =
                cfg.Where(
                    c =>
                        !string.IsNullOrWhiteSpace(c.Value) &&
                        !string.Equals(c.Key, TimeoutValueName, StringComparison.InvariantCultureIgnoreCase)
                    )
                    .ToDictionary(k => k.Key, v => v.Value.Split(';').Where(w => !string.IsNullOrWhiteSpace(w)).ToArray(), StringComparer.InvariantCultureIgnoreCase);
            _timeout = IntToStr(cfg.ContainsKey(TimeoutValueName) ? cfg[TimeoutValueName] : "", 30);
        }

        private string[] ControlDb(string database, ControlDbServices controlDbServices)
        {
            if (!_databases.ContainsKey(database)) return new[] { $"ERROR: Unknown datatbase '{database}'" };
            var serviceControl = new ServiceControl(_databases[database], _timeout);
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
