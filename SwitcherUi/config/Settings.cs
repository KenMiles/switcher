﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.config
{
    public class Settings
    {
        public static Dictionary<string, string> SettingsDictionary() {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
        private Dictionary<string, string> _settings;
        public Settings(Dictionary<string, string> settings)
        {
            _settings = settings == null 
                 ?  SettingsDictionary()
                 :  new Dictionary<string, string>(settings, StringComparer.OrdinalIgnoreCase);
        }

        public bool ContainsKey(string key) {
            return _settings.ContainsKey(key);
        }

        public string Value(string key, string defaultValue = "")
        {
            if (_settings.ContainsKey(key)) return _settings[key];
            return defaultValue;
        }

        public string this[string key]
        {
            get { return Value(key); }
        }

        public Dictionary<String, String> Values { get { return _settings; } }
    }
}