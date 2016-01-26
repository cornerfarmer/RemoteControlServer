using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteControlServer.OutputHandler;
using System.Collections.Generic;
using RemoteControlServer.Definitions;
using RemoteControlServer.Definitions.Fakes;

namespace UnitTest
{
    [TestClass]
    public class OutputHandlerTests
    {
        OutputHandler outputHandler;

        string composedOutput;
        List<Command> composedCommands;
        String[] commandStrings;
        List<string> commandStringsToCompose;
        Command[] commands;
        [TestInitialize()]
        public void Initialize()
        {
            composedOutput = "";
            commandStringsToCompose = new List<string>();
            composedCommands = new List<Command>();
            commandStrings = new String[3] { "CommandOne|2", "CommandTwo", "CommandThree|10:2" };
            commands = new Command[3] { new Command(), new Command(), new Command() };
            IOutputComposer fakeOutputComposer = new StubIOutputComposer()
            {
                ComposeListOfString = (commandStrings) =>
                {
                    commandStringsToCompose = new List<string>(commandStrings);
                    return composedOutput;
                }
            };
            ICommandComposer fakeCommandComposer = new StubICommandComposer()
            {
                ComposeCommand = (command) =>
                {
                    composedCommands.Add(command);
                    return commandStrings[composedCommands.Count - 1];
                }
            };

            outputHandler = new OutputHandler(fakeOutputComposer, fakeCommandComposer, new StubILogger());
        }

        [TestMethod]
        public void OutputHandler_NoCommand_EmptyOutput()
        {
            composedOutput = ";";
            Assert.AreEqual(outputHandler.getBufferedOutput(), composedOutput);
            CollectionAssert.AreEqual(commandStringsToCompose, new List<string>(commandStrings).GetRange(0, 0));
            composedOutput = "";
            Assert.AreEqual(outputHandler.getBufferedOutput(), composedOutput);
            CollectionAssert.AreEqual(commandStringsToCompose, new List<string>());

            Assert.AreEqual(composedCommands.Count, 0);
        }

        [TestMethod]
        public void OutputHandler_OneCommand_ResultingOutput()
        {
            outputHandler.addOutputCommand(commands[0]);

            composedOutput = "CommandOne|2;";
            Assert.AreEqual(outputHandler.getBufferedOutput(), composedOutput);
            CollectionAssert.AreEqual(commandStringsToCompose, new List<string>(commandStrings).GetRange(0, 1));
            composedOutput = "";
            Assert.AreEqual(outputHandler.getBufferedOutput(), composedOutput);
            CollectionAssert.AreEqual(commandStringsToCompose, new List<string>());

            Assert.AreEqual(composedCommands.Count, 1);
            Assert.AreEqual(composedCommands[0], commands[0]);
        }

        [TestMethod]
        public void OutputHandler_TwoCommands_ResultingOutput()
        {
            outputHandler.addOutputCommand(commands[0]);
            outputHandler.addOutputCommand(commands[1]);

            composedOutput = "CommandOne|2;CommandTwo;";
            Assert.AreEqual(outputHandler.getBufferedOutput(), composedOutput);
            CollectionAssert.AreEqual(commandStringsToCompose, new List<string>(commandStrings).GetRange(0, 2));
            composedOutput = "";
            Assert.AreEqual(outputHandler.getBufferedOutput(), composedOutput);
            CollectionAssert.AreEqual(commandStringsToCompose, new List<string>());

            Assert.AreEqual(composedCommands.Count, 2);
            Assert.AreEqual(composedCommands[0], commands[0]);
            Assert.AreEqual(composedCommands[1], commands[1]);
        }

        [TestMethod]
        public void OutputHandler_ThreeCommands_ResultingOutput()
        {
            outputHandler.addOutputCommand(commands[0]);
            outputHandler.addOutputCommand(commands[1]);
            outputHandler.addOutputCommand(commands[2]);

            composedOutput = "CommandOne|2;CommandTwo;CommandThree|10:2;";
            Assert.AreEqual(outputHandler.getBufferedOutput(), composedOutput);
            CollectionAssert.AreEqual(commandStringsToCompose, new List<string>(commandStrings).GetRange(0, 3));
            composedOutput = "";
            Assert.AreEqual(outputHandler.getBufferedOutput(), composedOutput);
            CollectionAssert.AreEqual(commandStringsToCompose, new List<string>());

            Assert.AreEqual(composedCommands.Count, 3);
            Assert.AreEqual(composedCommands[0], commands[0]);
            Assert.AreEqual(composedCommands[1], commands[1]);
            Assert.AreEqual(composedCommands[2], commands[2]);
        }

    }
}
