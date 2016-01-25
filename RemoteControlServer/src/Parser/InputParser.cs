﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace RemoteControlServer.Parser
{
    using Definitions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class InputParser : IInputParser
	{

        List<string> commandStrings;

        public virtual List<string> parse(string input)
		{
            commandStrings = new List<string>();

            input = validateInputTermination(input);
            parseCommandStrings(input);

            return commandStrings;
        }


        private string validateInputTermination(string input)
        {
            if (input.Last<char>() != ';')
                throw new ArgumentException("The given input '" + input + "' does not terminate correctly!");
            return input.Substring(0, input.Length - 1);
        }



        private void parseCommandStrings(string input)
        {
            while (isCommandLeft(input))
            {
                addNextCommandString(input);
                input = proceedToNextCommandString(input);
            }
        }

        private bool isCommandLeft(string input)
        {
            return input != "";
        }

        private void addNextCommandString(string input)
        {
            string commandString = getNextCommandString(input);
            if (commandString.Length > 0)
            {
                commandStrings.Add(commandString);
            }
        }

        private string getNextCommandString(string input)
        {
            if (!input.Contains(","))
                throw new ArgumentException("The given input '" + input + "' does not terminate correctly!");
            return input.Substring(0, input.IndexOf(","));
        }

        private string proceedToNextCommandString(string input)
        {
            return input.Substring(input.IndexOf(",") + 1);
        }
	}
}

