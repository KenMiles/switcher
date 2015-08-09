using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SwitcherCommon
{
    public class DatabaseControllerConfig
    {
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

        private static readonly ILog Log = LogManager.GetLogger(typeof(DatabaseControllerConfig));
        private readonly Dictionary<string, string[]> _databases;
        public int Timeout { get; }

        private const string TimeoutValueName = "Timeout";
        private const string SectionName = "Databases";
        public DatabaseControllerConfig(IConfiguration config)
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
            Timeout = IntToStr(cfg.ContainsKey(TimeoutValueName) ? cfg[TimeoutValueName] : "", 30);

        }

        public bool DatabaseKnown(string database)
        {
            return _databases.ContainsKey(database);
        }

        public string[] DatabaseServices(string database)
        {
            return _databases.ContainsKey(database) ? _databases[database] : new string[0];
        }

        public string[] Databases => _databases.Keys.ToArray();
    }
}
