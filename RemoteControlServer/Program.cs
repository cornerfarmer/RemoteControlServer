using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteControlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Ninject.IKernel kernel = new StandardKernel(new Configurator());

            new Thread(() => Application.Run(new GUI.Window())).Start();

            Listener.Listener listener = kernel.Get<Listener.Listener>();
            listener.run();

           
        }
    }
}
