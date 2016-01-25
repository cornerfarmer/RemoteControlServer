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
            string commandString = commandComposer.compose(new Command("SimpleCommand"));
            Assert.AreEqual(commandString, "SimpleCommand");
        }

        [TestMethod]
        public void CommandParser_CommandWithArgument_CorrectCommandString()
        {
            string commandString = commandComposer.compose(new Command("CommandWithArg", new string[] { "Arg" }));
            Assert.AreEqual(commandString, "CommandWithArg|Arg");
        }

        [TestMethod]
        public void CommandParser_CommandWithArguments_CorrectCommandString()
        {
            string commandString = commandComposer.compose(new Command("CommandWithArgs", new string[] { "Arg1", "Arg2", "Arg3" }));
            Assert.AreEqual(commandString, "CommandWithArgs|Arg1:Arg2:Arg3");
        }
    }
}
