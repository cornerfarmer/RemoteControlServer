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
        List<CommandTargetEntry> commandMethods;
        List<StatusTargetEntry> statusMethods;
        protected IOutputHandler outputHandler;

        public CommandExecuter(ICommandTarget[] targets_, IOutputHandler outputHandler_)
        {
            targets = targets_;
            outputHandler = outputHandler_;
            collectTargetMethods();
        }

        public void refreshClientStates(Client client)
        {
            foreach (StatusTargetEntry entry in statusMethods)
            {
                string newStatus = entry.execute().ToString();
                string oldStatus = client.getState(entry.getStatusName());
                if (newStatus != oldStatus)
                {
                    client.setState(entry.getStatusName(), newStatus);
                    outputHandler.addOutputCommand(new Command(entry.getStatusName(), new string[] { newStatus }));
                }
            }
        }

        private void collectTargetMethods()
        {
            commandMethods = new List<CommandTargetEntry>();
            statusMethods = new List<StatusTargetEntry>();
            foreach (ICommandTarget target in targets)
            {
                Type type = target.GetType();
                var commandMethodInfos = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(CommandRegistration), false).Length > 0).ToArray();

                foreach (MethodInfo method in commandMethodInfos)
                {
                    commandMethods.Add(new CommandTargetEntry(target, method, (CommandRegistration)method.GetCustomAttribute(typeof(CommandRegistration))));
                }

                var statusMethodInfos = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(StatusRegistration), false).Length > 0).ToArray();

                foreach (MethodInfo method in statusMethodInfos)
                {
                    statusMethods.Add(new StatusTargetEntry(target, method, (StatusRegistration)method.GetCustomAttribute(typeof(StatusRegistration))));
                }
            }
        }

        public bool tryToExecuteCommand(Command command)
        {
            foreach (CommandTargetEntry entry in commandMethods)
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
