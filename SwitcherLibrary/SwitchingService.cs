using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SwitcherCommon;

namespace SwitcherLibrary
{
    public class SwitchingService : ISwitchingService
    {
        private IConfiguration Config()
        {
            return new ConfigurationImpl(new CommandArguments(Environment.GetCommandLineArgs()));
        }

        public string SetJavaHome(string javaVersion)
        {
            var homeSetter = new JavaHomeSetter(Config());
            return homeSetter.SetJavaHome(javaVersion);
        }

        public void SetJavaEnvVars()
        {
            var homeSetter = new JavaHomeSetter(Config());
            homeSetter.SetupJavaEnvironmentalVariables();
        }

        public string[] StartDatabase(string database)
        {
            var dbController = new DatabaseController(Config());
            return dbController.StartDatabase(database);
        }

        public string[] StopDatabase(string database)
        {
            var dbController = new DatabaseController(Config());
            return dbController.StopDatabase(database);
        }
    }
}
