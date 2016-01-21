using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteControlServer.InputHandler;
using System.Collections.Generic;
using RemoteControlServer.Definitions;
using RemoteControlServer.Definitions.Fakes;

namespace UnitTest
{
    [TestClass]
    public class InputHandlerTests
    {
        InputHandler inputHandler;

        string parsedInput;
        List<string> parsedCommands;
        List<ICommand> executedCommands1;
        List<ICommand> executedCommands2;
        List<ICommand> executedCommands3;
        ICommand[] commands;
        [TestInitialize()]
        public void Initialize()
        {
            parsedInput = "";
            parsedCommands = new List<string>();
            executedCommands1 = new List<ICommand>();
            executedCommands2 = new List<ICommand>();
            executedCommands3 = new List<ICommand>();
            commands = new ICommand[3] { new StubICommand(), new StubICommand(), new StubICommand() };
            IInputParser fakeInputParser = new StubIInputParser()
            {
                ParseString = (input) => {
                    parsedInput = input;
                    List<string> commandStrings = new List<string>();
                    for (int i = 0; i < input.Split(';').Length - 1; i++)
                        commandStrings.Add("commandString" + i);
                    return commandStrings;
                }
            };
            ICommandParser fakeCommandParser = new StubICommandParser()
            {
                ParseCommandString = (command) =>
                {
                    parsedCommands.Add(command);
                    return commands[parsedCommands.Count - 1];
                }
            };
            ICommandExecuter[] fakeCommandExecuters = new ICommandExecuter[3];
            fakeCommandExecuters[0] = new StubICommandExecuter()
            {
                 TryToExecuteCommandICommand = (command) =>
                 {
                     executedCommands1.Add(command);
                     return false;
                 }
            };
            fakeCommandExecuters[1] = new StubICommandExecuter()
            {
                TryToExecuteCommandICommand = (command) =>
                {
                    executedCommands2.Add(command);
                    return true;
                }
            };
            fakeCommandExecuters[2] = new StubICommandExecuter()
            {
                TryToExecuteCommandICommand = (command) =>
                {
                    executedCommands3.Add(command);
                    return true;
                }
            };
            inputHandler = new InputHandler(fakeCommandParser, fakeInputParser, fakeCommandExecuters);
        }

        [TestMethod]
        public void InputHandler_EmptyInput_EmptyCalls()
        {
            inputHandler.handleInput("");
            Assert.AreEqual(parsedInput, "");
            Assert.AreEqual(parsedCommands.Count, 0);
            Assert.AreEqual(executedCommands1.Count, 0);
            Assert.AreEqual(executedCommands2.Count, 0);
            Assert.AreEqual(executedCommands3.Count, 0);
        }

        [TestMethod]
        public void InputHandler_OneInputCommand_ResultingCalls()
        {
            inputHandler.handleInput("CommandOne|2;");
            Assert.AreEqual(parsedInput, "CommandOne|2;");
            Assert.AreEqual(parsedCommands.Count, 1);
            Assert.AreEqual(parsedCommands[0], "commandString0");
            Assert.AreEqual(executedCommands1.Count, 1);
            Assert.AreEqual(executedCommands1[0], commands[0]);
            Assert.AreEqual(executedCommands2.Count, 1);
            Assert.AreEqual(executedCommands2[0], commands[0]);
            Assert.AreEqual(executedCommands3.Count, 0);
        }


        [TestMethod]
        public void InputHandler_TowInputCommands_ResultingCalls()
        {
            inputHandler.handleInput("CommandOne|2;CommandTwo;");
            Assert.AreEqual(parsedInput, "CommandOne|2;CommandTwo;");
            Assert.AreEqual(parsedCommands.Count, 2);
            Assert.AreEqual(parsedCommands[0], "commandString0");
            Assert.AreEqual(parsedCommands[1], "commandString1");
            Assert.AreEqual(executedCommands1.Count, 2);
            Assert.AreEqual(executedCommands1[0], commands[0]);
            Assert.AreEqual(executedCommands1[1], commands[1]);
            Assert.AreEqual(executedCommands2.Count, 2);
            Assert.AreEqual(executedCommands2[0], commands[0]);
            Assert.AreEqual(executedCommands2[1], commands[1]);
            Assert.AreEqual(executedCommands3.Count, 0);
        }

        [TestMethod]
        public void InputHandler_ThreeInputCommands_ResultingCalls()
        {
            inputHandler.handleInput("CommandOne|2;CommandTwo;CommandThree|10:2;");
            Assert.AreEqual(parsedInput, "CommandOne|2;CommandTwo;CommandThree|10:2;");
            Assert.AreEqual(parsedCommands.Count, 3);
            Assert.AreEqual(parsedCommands[0], "commandString0");
            Assert.AreEqual(parsedCommands[1], "commandString1");
            Assert.AreEqual(parsedCommands[2], "commandString2");
            Assert.AreEqual(executedCommands1.Count, 3);
            Assert.AreEqual(executedCommands1[0], commands[0]);
            Assert.AreEqual(executedCommands1[1], commands[1]);
            Assert.AreEqual(executedCommands1[2], commands[2]);
            Assert.AreEqual(executedCommands2.Count, 3);
            Assert.AreEqual(executedCommands2[0], commands[0]);
            Assert.AreEqual(executedCommands2[1], commands[1]);
            Assert.AreEqual(executedCommands2[2], commands[2]);
            Assert.AreEqual(executedCommands3.Count, 0);
        }

    }
}
