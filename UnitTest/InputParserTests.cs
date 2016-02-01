using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteControlServer.Parser;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class InputParserTests
    {
        InputParser inputParser;
            
        [TestInitialize()]
        public void Initialize()
        {
            inputParser = new InputParser();
        }

        [TestMethod]
        public void InputParser_EmptyInput_ExceptionThrown()
        {
            try
            {
                List<string> commandString = inputParser.parse("");
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "does not terminate correctly");
                return;
            }
            Assert.Fail("No exception thrown");
        }

        [TestMethod]
        public void InputParser_OnlyEndingInput_EmptyList()
        {
            List<string> commandString = inputParser.parse(";");
            Assert.AreEqual(commandString.Count, 0);
        }

        [TestMethod]
        public void InputParser_OneInputCommand_OneCommandString()
        {
            List<string> commandString = inputParser.parse("TESTCOMMAND,;");
            Assert.AreEqual(commandString.Count, 1);
            Assert.AreEqual(commandString[0], "TESTCOMMAND");
        }

        [TestMethod]
        public void InputParser_MultipleInputCommands_MultipleCommandStrings()
        {
            List<string> commandString = inputParser.parse("FirstCommand,SecondCommand|2:1,ThirdCommand,;");
            Assert.AreEqual(commandString.Count, 3);
            Assert.AreEqual(commandString[0], "FirstCommand");
            Assert.AreEqual(commandString[1], "SecondCommand|2:1");
            Assert.AreEqual(commandString[2], "ThirdCommand");
        }

        [TestMethod]
        public void InputParser_OneUnterminatedInputCommand_ExceptionThrown()
        {
            try
            {
                List<string> commandString = inputParser.parse("UnterminatedCommand");
            }
            catch(ArgumentException e)
            {
                StringAssert.Contains(e.Message, "UnterminatedCommand");
                return;
            }
            Assert.Fail("No exception thrown");
        }
    }
}
