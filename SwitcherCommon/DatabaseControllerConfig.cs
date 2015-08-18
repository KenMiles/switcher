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
        public int Timeout { get; private set; }
        public Dictionary<string, string[]> DatabaseAndServices { get; private set; }

        private const string TimeoutValueName = "Timeout";
        private const string SectionName = "Databases";

        private readonly IConfiguration _config;

        private void ReadConfig(Dictionary<string, string> cfg)
        {
            Log.Debug("CFG: " + string.Join(", ", cfg.Keys));
            DatabaseAndServices =
                cfg.Where(
                    c =>
                        !string.IsNullOrWhiteSpace(c.Value) &&
                        !string.Equals(c.Key, TimeoutValueName, StringComparison.InvariantCultureIgnoreCase)
                    )
                    .ToDictionary(k => k.Key, v => v.Value.Split(';').Where(w => !string.IsNullOrWhiteSpace(w)).ToArray(), StringComparer.InvariantCultureIgnoreCase);
            Timeout = IntToStr(cfg.ContainsKey(TimeoutValueName) ? cfg[TimeoutValueName] : "", 30);
        }

        public DatabaseControllerConfig(IConfiguration config)
        {
            _config = config;
            ReadConfig(config.GetSwitcherCfg(SectionName).Values);

        }

        public void SaveCfg(int timeOut, Dictionary<string, string[]> database)
        {
            var cfg = database.ToDictionary(v => v.Key, v => string.Join(";", v.Value), StringComparer.InvariantCultureIgnoreCase);
            cfg[TimeoutValueName] = Math.Min(600, Math.Max(10, timeOut)).ToString();
            _config.SetSwitcherCfg(SectionName, new Settings(cfg));
            ReadConfig(cfg);
        }

        public bool DatabaseKnown(string database)
        {
            return DatabaseAndServices.ContainsKey(database);
        }

        public string[] DatabaseServices(string database)
        {
            return DatabaseAndServices.ContainsKey(database) ? DatabaseAndServices[database] : new string[0];
        }

        public string[] Databases => DatabaseAndServices.Keys.ToArray();

    }
}
