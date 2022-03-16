using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.services.contracts
{
    public interface IConfigurationService
    {
        string GetString(string section);
        string GetConnectionString(string connectionName);
        bool GetBool(string section);
        int GetInt(string section);
        double GetDouble(string section);
    }
}
