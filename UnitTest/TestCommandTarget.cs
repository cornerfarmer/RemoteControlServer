using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteControlServer.CommandExecuter;
using RemoteControlServer.Definitions;

namespace UnitTest
{
    class TestCommandTarget : ICommandTarget
    {
        public Command executedCommand;

        [CommandRegistration("TestCommand")]
        public void doSomething(Command commandToExecute)
        {
            executedCommand = commandToExecute;

        }
    }
}
