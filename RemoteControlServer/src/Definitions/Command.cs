﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace RemoteControlServer.Definitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Command
    {
        private string name;

        private string[] arguments;

        public Command()
        {
            name = "";
            arguments = new string[0];
        }

        public Command(string name_)
        {
            name = name_;
            arguments = new string[0];
        }

        public Command(string name_, string[] arguments_)
        {
            name = name_;
            arguments = arguments_;
        }

        public string getName()
        {
            return name;
        }

        public string[] getArguments()
        {
            return arguments;
        }

    }
}

