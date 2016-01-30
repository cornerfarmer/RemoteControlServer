using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteControlServer.CommandExecuter;
using System.Collections.Generic;
using RemoteControlServer.Definitions;

namespace UnitTest
{
    [TestClass]
    public class CommandExecuterTest
    {
        CommandExecuter commandExecuter;
        TestCommandTarget target;

        [TestInitialize()]
        public void Initialize()
        {
            target = new TestCommandTarget();
            commandExecuter = new CommandExecuter(new ICommandTarget[] { target });
        }

        [TestMethod]
        public void CommandExecuter_OneSimpleCommand_ExecutedCommand()
        {
            Command testCommand = new Command("TestCommand");
            commandExecuter.tryToExecuteCommand(testCommand);
            Assert.AreEqual(target.executedCommand, testCommand);
        }
    }
}
