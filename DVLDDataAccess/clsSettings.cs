using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DVLDDataAccess
{
    public static class clsSettings
    {
       public static string ConnectionString = "server = .;database = DVLD; Integrated Security=True;";

       public static void LogErrorInEventLog(string message)
        {
            string SourceName = "DVLD";

            if(!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }

            EventLog.WriteEntry(SourceName, message,EventLogEntryType.Error);
        }

    }

}
