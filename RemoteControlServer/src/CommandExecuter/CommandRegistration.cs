using RemoteControlServer.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.CommandExecuter
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class CommandRegistration : System.Attribute
    {
        private string commandString;

        public CommandRegistration(string commandString_)
        {
            commandString = commandString_;
        }
        
        public string getCommandString()
        {
            return commandString;
        }
        
    }
}
