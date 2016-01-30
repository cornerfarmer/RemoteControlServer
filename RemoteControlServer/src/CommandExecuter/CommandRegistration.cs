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
        private string command;

        public CommandRegistration(string command_)
        {
            this.command = command_;
        }
        
        public string getCommand()
        {
            return command;
        }
    }
}
