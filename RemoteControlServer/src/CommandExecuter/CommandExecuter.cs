using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteControlServer.Definitions;
using System.Reflection;

namespace RemoteControlServer.CommandExecuter
{
    public class CommandExecuter : ICommandExecuter
    {
        ICommandTarget[] targets;

        public CommandExecuter(ICommandTarget[] targets_)
        {
            targets = targets_;
        }

        public void refreshClientStates(Client client)
        {
            
        }

        public bool tryToExecuteCommand(Command command)
        {

            foreach (ICommandTarget target in targets)
            {
                Type type = target.GetType();
                var methods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(CommandRegistration), false).Length > 0).ToArray();

                foreach (MethodInfo method in methods)
                {
                    method.Invoke(target, new object[] { command });
                }
            }
            
            return true; 
        }
    }
}
