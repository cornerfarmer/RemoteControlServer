using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Ninject.IKernel kernel = new StandardKernel(new Configurator());

            Listener.Listener listener = kernel.Get<Listener.Listener>();
            listener.run();
        }
    }
}
