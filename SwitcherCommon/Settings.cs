using System;
using System.Collections.Generic;
using System.Linq;

namespace SwitcherCommon
{
    public class Settings
    {
        public static Dictionary<string, string> SettingsDictionary() {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public Settings(Dictionary<string, string> settings)
        {
            Values = settings == null 
                 ?  SettingsDictionary()
                 :  new Dictionary<string, string>(settings, StringComparer.OrdinalIgnoreCase);
        }

        public bool ContainsKey(string key) {
            return Values.ContainsKey(key);
        }

        public string Value(string key, string defaultValue = "")
        {
            if (Values.ContainsKey(key)) return Values[key];
            return defaultValue;
        }

        private string[] GetArrayValue(string key, string defaultValues = "") { 
            if (Values.ContainsKey(key)) return (Values[key]??"").Split(';');
            return (defaultValues??"").Split(';');
        }
        public string[] ArrayValue(string key, string defaultValues = "")
        {
            return (GetArrayValue(key, defaultValues) ?? new string[0])
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }

        public void SetArrayValue(string key, string[] value)
        {
            this[key] = string.Join(";", value);
        }

        public string this[string key]
        {
            get { return Value(key); }
            set { Values[key] = value; }
        }

        public Dictionary<string, string> Values { get; }
    }
}
