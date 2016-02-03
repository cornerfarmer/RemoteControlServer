using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteControlServer.Composer;
using RemoteControlServer.Definitions;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class CommandComposerTest
    {
        CommandComposer commandComposer;

        [TestInitialize()]
        public void Initialize()
        {
            commandComposer = new CommandComposer();
        }

        [TestMethod]
        public void CommandParser_SimpleCommand_CorrectCommandString()
        {
            string commandString = commandComposer.compose(new Command("TEST", "SimpleCommand"));
            Assert.AreEqual(commandString, "TEST_SimpleCommand");
        }

        [TestMethod]
        public void CommandParser_CommandWithArgument_CorrectCommandString()
        {
            string commandString = commandComposer.compose(new Command("TEST", "CommandWithArg", new string[] { "Arg" }));
            Assert.AreEqual(commandString, "TEST_CommandWithArg|Arg");
        }

        [TestMethod]
        public void CommandParser_CommandWithArguments_CorrectCommandString()
        {
            string commandString = commandComposer.compose(new Command("TEST", "CommandWithArgs", new string[] { "Arg1", "Arg2", "Arg3" }));
            Assert.AreEqual(commandString, "TEST_CommandWithArgs|Arg1:Arg2:Arg3");
        }
    }
}
