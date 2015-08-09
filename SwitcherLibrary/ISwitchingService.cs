using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SwitcherLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISwitchingService" in both code and config file together.
    [ServiceContract]
    public interface ISwitchingService
    {
        [OperationContract]
        string SetJavaHome(string javaVersion);

        [OperationContract]
        void SetJavaEnvVars();

        [OperationContract]
        string[] StartDatabase(string database);

        [OperationContract]
        string[] StopDatabase(string database);
    }
}
