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

	public interface IOutputHandler 
	{
		void addOutputCommand(Command command);

		string getBufferedOutput();

	}
}

