using RemoteControlServer.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Definitions
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class StatusRegistration : System.Attribute
    {
        private string statusName;

        public StatusRegistration(string statusName_)
        {
            statusName = statusName_;
        }
        
        public string getStatusName()
        {
            return statusName;
        }
        
    }
}
