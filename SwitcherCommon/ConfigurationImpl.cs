﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IniParser;
using IniParser.Model;
using log4net;

namespace SwitcherCommon
{
    public delegate Dictionary<string, string> DefaultDictionay();
    delegate void Update(IniData ini);
    public class ConfigurationImpl : IConfiguration
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigurationImpl));
        private readonly FileIniDataParser _parser = new FileIniDataParser();
        private readonly string _iniFileName;
        private IniData _ini;
        private const string BlockingProcessSection = "blocking-processes";
        private const string WarningProcessSection = "warning-processes";

        private void DoUpdate(Update updates) {
            RefreshConfig();
            updates(_ini);
            _parser.WriteFile(_iniFileName, _ini);
            RefreshConfig();
        }

        private string CfgFile(DirectoryInfo dir) {
            return Path.Combine(dir.FullName, "switcher.cfg");
        }

        private string DefaultConfigFileName()
        {
            string[] args = Environment.GetCommandLineArgs();
            // If run from visual studio location - skip to project dir
            string[] toSkipDown = new[] { "bin", "release", "debug" };
            var currentDir = new FileInfo(args[0]).Directory;
            while (!File.Exists(CfgFile(currentDir)) && toSkipDown.Contains(currentDir.Name.ToLower())) {
                currentDir = currentDir.Parent;
            }
            // skip to solution dir if doesn't exist in project dir
            if (!File.Exists(CfgFile(currentDir))) {
                if (File.Exists(CfgFile(currentDir.Parent))) return CfgFile(currentDir.Parent);
            }
            return CfgFile(currentDir);
        }

        private void RefreshConfig() {
          _ini = File.Exists(_iniFileName) ? _parser.ReadFile(_iniFileName) : new IniData();
        }

        public ConfigurationImpl(CommandArguments args)
        {
            _iniFileName = args.AsString("cfg", DefaultConfigFileName());
            RefreshConfig();
        }

        public ConfigurationImpl() : this(new CommandArguments(Environment.GetCommandLineArgs()))
        {
        }

        private Dictionary<string, string> CaseInsensitiveDictionary(Dictionary<string, string> source) {
            if (source == null) return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            return new Dictionary<string, string>(source , StringComparer.OrdinalIgnoreCase);
        }
        protected Settings GetSection(string sectionName, DefaultDictionay defaultDictionary = null) {
            if (!_ini.Sections.ContainsSection(sectionName)) return new Settings(defaultDictionary?.Invoke());
            return new Settings(_ini.Sections[sectionName].ToDictionary(k => k.KeyName, v => v.Value));
        }

        protected void SetSection(string sectionName, Settings settings)
        {
            DoUpdate(i => {
                if (!i.Sections.ContainsSection(sectionName)) i.Sections.AddSection(sectionName);
                var sec = i.Sections[sectionName];
                sec.RemoveAllKeys();
                foreach (var val in settings.Values) {
                    sec[val.Key] = val.Value;
                }
            });
        }

        public Settings BlockingProcesses
        {
            get
            {
                return GetSection(BlockingProcessSection, () => new Dictionary<string, string> { { "eclipse", "Eclipse" } });
            }
            set {
                SetSection(BlockingProcessSection, value);
            }
        }

        public Settings WarningProcesses
        {
            get
            {
                return GetSection(WarningProcessSection, () => new Dictionary<string, string> {
                        { "devenv", "Visual Studio" },
                        { "sqltools", "SQL Tools" }, }
                );
            }
            set
            {
                SetSection(WarningProcessSection, value);
            }
        }

        public string[] ProjectNames
        {
            get
            {
                return _ini.Sections.Select(s => s.SectionName)
                     .Where(s => s.StartsWith(ProjectPrefix, StringComparison.InvariantCultureIgnoreCase))
                     .Select(s => s.Substring(ProjectPrefix.Length))
                     .ToArray();
            }
        }

        private const string CurrentSection = "Current";
        private const string CurrentProjectIdent = "Project";
        public string CurrentProject
        {
            get
            {
                var section = GetSection(CurrentSection);
                return section.ContainsKey(CurrentProjectIdent) ? section[CurrentProjectIdent] : "";
            }
            set {
                DoUpdate(i => {
                    if (!i.Sections.ContainsSection(CurrentSection)) i.Sections.AddSection(CurrentSection);
                    var sec = i.Sections[CurrentSection];
                    sec[CurrentProjectIdent] = value;
                 });
            }
        }

        private string IgnoreServicesFileName(string dir)
        {
            var fileName = Path.Combine(dir, "ignoreservices.cfg");
            return File.Exists(fileName) ? fileName : "";
        }

        private string IgnoreServicesFileName()
        {
            var paths = new []
            {
                IgnoreServicesFileName(Path.GetDirectoryName(_iniFileName)),
                    IgnoreServicesFileName(Path.GetDirectoryName(DefaultConfigFileName()))
            };
            return paths.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p));
        }

        private Dictionary<string, string> _ignoreServices;
        public Dictionary<string, string> LoadIgnoreServices()
        {
            var fileName = IgnoreServicesFileName();
            try
            {
                if (string.IsNullOrWhiteSpace(fileName)) return new Dictionary<string, string>();
                var ini = _parser.ReadFile(fileName);
                if (!ini.Sections.ContainsSection("Ignore Services")) return new Dictionary<string, string>();
                return ini.Sections["Ignore Services"].ToDictionary(k => k.KeyName, v => v.Value,
                    StringComparer.CurrentCultureIgnoreCase);
            }
            catch (Exception e)
            {
                Log.Error($"Reading Ignore Services from '{fileName}' threw Exception", e);
            }
            return new Dictionary<string, string>();
        }

        public Dictionary<string, string> IgnoreServices()
        {
            return _ignoreServices = _ignoreServices ?? LoadIgnoreServices();
        }


        private const string ProjectPrefix = "Project ";
        public Settings this[string projectName]
        {
            get
            {
                return GetSection(ProjectPrefix + projectName);
            }

            set
            {
                SetSection(ProjectPrefix + projectName, value);
            }
        }

        private const string ProjectSectionPrefix = "Project_Section ";
        public Settings this[string projectName, string section]
        {
            get
            {
                return GetSection(ProjectSectionPrefix + projectName + " - " + section);
            }

            set
            {
                SetSection(ProjectSectionPrefix + projectName + " - " + section, value);
            }
        }

        private const string SwitcherPrefix = "Config Switcher ";
        public Settings GetSwitcherCfg(string switcherName)
        {
            return GetSection(SwitcherPrefix + switcherName);
        }

        public void SetSwitcherCfg(string switcherName, Settings value)
        {
            SetSection(SwitcherPrefix + switcherName, value);
        }

        public Settings GetSwitcherCfg(string switcherName, string section)
        {
            return GetSection(SwitcherPrefix + switcherName + " - " + section);
        }

        public void SetSwitcherCfg(string switcherName, string section, Settings value)
        {
            SetSection(SwitcherPrefix + switcherName + " - " + section, value);
        }


    }
}
