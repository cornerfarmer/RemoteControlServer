﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace ClientService
{
	using Definitions;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class ClientService
	{
		public virtual ClientRepository ClientRepository
		{
			get;
			set;
		}

		public virtual Client getClientForNewConnection(string ip)
		{
			throw new System.NotImplementedException();
		}

	}
}

