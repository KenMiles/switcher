﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SwitcherCommon
{
    public class CommandArguments
    {
        private readonly Dictionary<string, string> _arguments;

        private static string ArgName(string name)
        {
            return name?.ToUpper().Trim() ?? "";
        }

        private static string ArgumentName(string fromValue)
        {
            var pos = fromValue.IndexOf(":", StringComparison.Ordinal);
            var argumentName = ArgName(pos > 0 ? fromValue.Substring(0, pos) : fromValue);
            if (argumentName.StartsWith("/") || argumentName.StartsWith("-")) return argumentName.Substring(1).Trim();
            return argumentName;
        }

        private static string ArgumentValue(string fromValue)
        {
            var pos = fromValue.IndexOf(":", StringComparison.Ordinal);
            return pos < 0 ? "" : fromValue.Substring(pos + 1);
        }

        public CommandArguments(string[] args)
        {
            _arguments = args.Where(s => !string.IsNullOrEmpty(s)).ToDictionary(ArgumentName, ArgumentValue);
        }

        public bool ArgumentExists(string argumentName)
        {
            return (_arguments.ContainsKey(ArgName(argumentName)));
        }

        public string AsString(string argumentName, string defaultValue)
        {
            return !ArgumentExists(argumentName) ? defaultValue : _arguments[ArgName(argumentName)];
        }

        public string AsString(string argumentName)
        {
            return AsString(argumentName, "");
        }

        private static int ToInt(string intStr, int defaultValue)
        {
            int result;
            return int.TryParse(intStr.Trim(), out result) ? result : defaultValue;
        }

        public int AsInt(string argumentName, int defaultValue)
        {
            return !ArgumentExists(argumentName) ? defaultValue : ToInt(AsString(argumentName), defaultValue);
        }

        public int AsInt(string argumentName)
        {
            return AsInt(argumentName, 0);
        }

        public bool HelpRequested()
        {
            return ArgumentExists("?") || ArgumentExists("help");
        }

        public TimeSpan AsTimeSpan(string argumentName, TimeSpan defaultValue)
        {
            if (!ArgumentExists(argumentName)) return defaultValue;
            var parts = AsString(argumentName).Split(':');
            var hours = ToInt(parts[0], 0);
            var mins = ToInt(parts.Length > 1 ? parts[1] : "0", 0);
            return new TimeSpan(hours, mins, 0);
        }

        public TimeSpan AsTimeSpan(string argumentName)
        {
            return AsTimeSpan(argumentName, new TimeSpan(0, 0, 0));
        }
    }
}

