using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Definitions
{
    public interface ICommandTarget
    {
        void refreshClientStates(Client client);
    }
}
