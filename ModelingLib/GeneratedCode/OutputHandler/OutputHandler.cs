﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace OutputHandler
{
	using Definitions;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class OutputHandler
	{
		public virtual string[] buffer
		{
			get;
			set;
		}

		public virtual OutputComposer OutputComposer
		{
			get;
			set;
		}

		public virtual CommandComposer CommandComposer
		{
			get;
			set;
		}

		public virtual string getBufferedOutput()
		{
			throw new System.NotImplementedException();
		}

		public virtual void addOutputCommand(Command command)
		{
			throw new System.NotImplementedException();
		}

	}
}

