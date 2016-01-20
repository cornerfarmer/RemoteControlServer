using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteControlServer.Composer;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class ComposerTests
    {
        OutputComposer outputComposer;

        [TestInitialize()]
        public void Initialize()
        {
            outputComposer = new OutputComposer();
        }

        [TestMethod]
        public void OutputComposer_EmptyList_TerminatorOutput()
        {
            List<string> commandStrings = new List<string>();
            string output = outputComposer.compose(commandStrings);
            Assert.AreEqual(output, ";");
        }

        [TestMethod]
        public void OutputComposer_OneCommandString_OneOutputCommand()
        {
            List<string> commandStrings = new List<string>() { "Command|0:2" };
            string output = outputComposer.compose(commandStrings);
            Assert.AreEqual(output, "Command|0:2;;");
        }

        [TestMethod]
        public void OutputComposer_MultipleCommandStrings_MultipleOutputCommand()
        {
            List<string> commandStrings = new List<string>() { "FirstCommand|0:2", "SecondCommand", "ThirdCommand|3" };
            string output = outputComposer.compose(commandStrings);
            Assert.AreEqual(output, "FirstCommand|0:2;SecondCommand;ThirdCommand|3;;");
        }

        [TestMethod]
        public void OutputComposer_WrongCommandString_ExceptionThrown()
        {
            try
            {
                List<string> commandStrings = new List<string>() { "WrongCommand;" };
                string output = outputComposer.compose(commandStrings);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "WrongCommand;");
                return;
            }
            Assert.Fail("No exception thrown");
        }
    }
}
