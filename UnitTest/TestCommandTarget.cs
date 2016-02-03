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
        public bool executed;
        public bool executedArgs;
        public string arg1;
        public int arg2;
        public bool arg3;

        [CommandRegistration("TestCommand")]
        public void doSomething()
        {
            executed = true;

        }

        [CommandRegistration("TestCommandArgs")]
        public void doSomething2(string arg1_, int arg2_, bool arg3_)
        {
            executedArgs = true;
            arg1 = arg1_;
            arg2 = arg2_;
            arg3 = arg3_;
        }


        [StatusRegistration("TestStatus")]
        public int getStatus()
        {
            return 42;
        }

        public void refreshClientStates(Client client)
        {
            throw new NotImplementedException();
        }

        public string getTargetPrefix()
        {
            return "TEST";
        }
    }
}
