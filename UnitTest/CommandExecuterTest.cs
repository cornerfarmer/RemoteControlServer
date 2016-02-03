using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteControlServer.CommandExecuter;
using System.Collections.Generic;
using RemoteControlServer.Definitions;
using RemoteControlServer.Definitions.Fakes;

namespace UnitTest
{
    [TestClass]
    public class CommandExecuterTest
    {
        CommandExecuter commandExecuter;
        TestCommandTarget target;
        Command executedCommand;

        [TestInitialize()]
        public void Initialize()
        {
            target = new TestCommandTarget();
            IOutputHandler fakeOutputHandler = new StubIOutputHandler()
            {
                AddOutputCommandCommand = (command) =>
                {
                    executedCommand = command;
                }
            };
            commandExecuter = new CommandExecuter(new ICommandTarget[] { target }, fakeOutputHandler);
            
        }

        [TestMethod]
        public void CommandExecuter_OneSimpleCommand_ExecutedCommand()
        {
            Command testCommand = new Command("TestCommand");
            commandExecuter.tryToExecuteCommand(testCommand);
            Assert.AreEqual(target.executed, true);
            Assert.AreEqual(target.executedArgs, false);
        }

        [TestMethod]
        public void CommandExecuter_OneNotRegisteredCommand_NoExecutedCommand()
        {
            Command testCommand = new Command("TestCommandFake");
            commandExecuter.tryToExecuteCommand(testCommand);
            Assert.AreEqual(target.executed, false);
            Assert.AreEqual(target.executedArgs, false);
        }

        [TestMethod]
        public void CommandExecuter_OneCommandWithArguments_ExecutedCommand()
        {
            Command testCommand = new Command("TestCommandArgs", new string[] { "Arg", "0", "true" });
            commandExecuter.tryToExecuteCommand(testCommand);
            Assert.AreEqual(target.executedArgs, true);
            Assert.AreEqual(target.arg1, "Arg");
            Assert.AreEqual(target.arg2, 0);
            Assert.AreEqual(target.arg3, true);
            Assert.AreEqual(target.executed, false);
        }

        [TestMethod]
        public void CommandExecuter_OneCommandWithTooLessArguments_NoExecutedCommand()
        {
            Command testCommand = new Command("TestCommandArgs", new string[] { "Arg", "0" });
            commandExecuter.tryToExecuteCommand(testCommand);
            Assert.AreEqual(target.executedArgs, false);
            Assert.AreEqual(target.executed, false);
        }

        [TestMethod]
        public void CommandExecuter_OneCommandWithInvalidArguments_NoExecutedCommand()
        {
            Command testCommand = new Command("TestCommandArgs", new string[] { "Arg", "0", "string" });
            commandExecuter.tryToExecuteCommand(testCommand);
            Assert.AreEqual(target.executedArgs, false);
            Assert.AreEqual(target.executed, false);
            testCommand = new Command("TestCommandArgs", new string[] { "Arg", "0NoNumber", "false" });
            commandExecuter.tryToExecuteCommand(testCommand);
            Assert.AreEqual(target.executedArgs, false);
            Assert.AreEqual(target.executed, false);
        }

        [TestMethod]
        public void CommandExecuter_NewStatus_CorrectOutputCommand()
        {
            Client client = new Client();
            commandExecuter.refreshClientStates(client);
            Assert.AreNotEqual(executedCommand, null);
            Assert.AreEqual(executedCommand.getName(), "TestStatus");
            Assert.AreEqual(executedCommand.getArguments().Length, 1);
            Assert.AreEqual(executedCommand.getArguments()[0], "42");
        }

        [TestMethod]
        public void CommandExecuter_SameStatus_NoOutputCommand()
        {
            Client client = new Client();
            client.setState("TestStatus", "42");
            commandExecuter.refreshClientStates(client);
            Assert.AreEqual(executedCommand, null);
        }



    }
}
