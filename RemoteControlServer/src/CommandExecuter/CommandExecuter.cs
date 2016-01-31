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
        List<CommandTargetEntry> targetMethods;

        public CommandExecuter(ICommandTarget[] targets_)
        {
            targets = targets_;
            collectTargetMethods();
        }

        public void refreshClientStates(Client client)
        {
            foreach (ICommandTarget target in targets)
            {
                target.refreshClientStates(client);
            }
        }

        private void collectTargetMethods()
        {
            targetMethods = new List<CommandTargetEntry>();
            foreach (ICommandTarget target in targets)
            {
                Type type = target.GetType();
                var methods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(CommandRegistration), false).Length > 0).ToArray();

                foreach (MethodInfo method in methods)
                {
                    targetMethods.Add(new CommandTargetEntry(target, method, (CommandRegistration)method.GetCustomAttribute(typeof(CommandRegistration))));
                }
            }
        }

        public bool tryToExecuteCommand(Command command)
        {
            foreach (CommandTargetEntry entry in targetMethods)
            {
                if (entry.matchesCommand(command))
                {
                    entry.execute(command);
                }
            }

            return true; 
        }
    }
}
